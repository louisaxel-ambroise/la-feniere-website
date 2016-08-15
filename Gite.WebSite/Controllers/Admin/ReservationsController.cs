using Gite.Model.Repositories;
using Gite.Model.Services.Reservations.Payment;
using Gite.WebSite.Models.Admin;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentManager _paymentManager;

        public ReservationsController(IReservationRepository reservationRepository, IPaymentManager paymentManager)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (paymentManager == null) throw new ArgumentNullException("paymentManager");

            _reservationRepository = reservationRepository;
            _paymentManager = paymentManager;
        }

        public ActionResult Index()
        {
            var valids = _reservationRepository.QueryValidReservations().Where(x => x.FirstWeek >= DateTime.Now).ToList();

            var newReservations = valids.Count(x => x.AdvancedDeclarationDate == null);
            var pendingAdvance = valids.Count(x => x.AdvancedDeclarationDate != null && x.AdvancedReceptionDate == null);
            var incomingReservations = valids.Count(x => x.FirstWeek >= DateTime.Now.AddMonths(-2));

            var model = new ReservationsOverview
            {
                New = newReservations,
                PendingAdvance = pendingAdvance,
                Incoming = incomingReservations
            };

            return View("~/Views/Admin/Reservations/Index.cshtml", model);
        }

        public ActionResult New()
        {
            var reservations = _reservationRepository.QueryValidReservations()
                .Where(x => x.AdvancedDeclarationDate == null)
                .OrderBy(x => x.BookedOn).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/New.cshtml", model);
        }

        public ActionResult PendingAdvance()
        {
            var reservations = _reservationRepository.QueryValidReservations()
                .Where(x => x.AdvancedDeclarationDate != null && x.AdvancedReceptionDate == null)
                .OrderBy(x => x.AdvancedDeclarationDate).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/PendingAdvance.cshtml", model);
        }

        public ActionResult Incoming()
        {
            var reservations = _reservationRepository.QueryValidReservations()
                .Where(x => x.FirstWeek >= DateTime.Now.AddMonths(-2))
                .OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/Incoming.cshtml", model);
        }

        public ActionResult All()
        {
            var reservations = _reservationRepository.QueryValidReservations().OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/All.cshtml", model);
        }

        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);
            var model = reservation.MapToReservationModel();

            ViewBag.Previous = Request.QueryString["from"] ?? "/admin/reservations";
            return View("~/Views/Admin/Reservations/Details.cshtml", model);
        }

        [HttpPost]
        public ActionResult AdvanceReceived(Guid id)
        {
            _paymentManager.DeclareAdvanceReceived(id, 0); // TODO: add amount

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PaymentReceived(Guid id)
        {
            _paymentManager.DeclarePaymentReceived(id, 0); // TODO: add amount

            return RedirectToAction("Index");
        }
    }
}