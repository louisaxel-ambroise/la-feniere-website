using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Domain.Readers;
using Gite.Domain.Services.Reservations;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class ReservationsController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationManager _reservationManager;

        public ReservationsController(IReservationRepository reservationRepository, IReservationManager reservationManager)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationReader");
            if (reservationManager == null) throw new ArgumentNullException("paymentManager");

            _reservationRepository = reservationRepository;
            _reservationManager = reservationManager;
        }

        public ActionResult Index()
        {
            var valids = _reservationRepository.QueryValids().Where(x => x.FirstWeek > DateTime.UtcNow).ToList();

            var newReservations = valids.Count(x => !x.AdvancePaymentDeclared && !x.AdvancePaymentReceived);
            var pendingAdvance = valids.Count(x => x.AdvancePaymentDeclared && !x.AdvancePaymentReceived);
            var pendingPayment = valids.Count(x => x.PaymentDeclared && !x.PaymentReceived);
            var incomingReservations = valids.Count(x => x.FirstWeek <= DateTime.UtcNow.AddMonths(2));

            var model = new ReservationsOverview
            {
                New = newReservations,
                PendingAdvance = pendingAdvance,
                PendingPayment = pendingPayment,
                Incoming = incomingReservations
            };

            return View("~/Views/Admin/Reservations/Index.cshtml", model);
        }

        public ActionResult New()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => !x.AdvancePaymentDeclared && !x.AdvancePaymentReceived).OrderBy(x => x.BookedOn).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/New.cshtml", model);
        }

        public ActionResult PendingAdvance()
        {
            var reservations = _reservationRepository.Query().Where(x => x.AdvancePaymentDeclared && !x.AdvancePaymentReceived).OrderBy(x => x.BookedOn).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/PendingAdvance.cshtml", model);
        }

        public ActionResult PendingPayment()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => x.PaymentDeclared && !x.PaymentReceived).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/PendingPayment.cshtml", model);
        }

        public ActionResult Incoming()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => x.FirstWeek <= DateTime.UtcNow.AddMonths(2)).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/Incoming.cshtml", model);
        }

        public ActionResult All()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => x.FirstWeek >= DateTime.UtcNow).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/All.cshtml", model);
        }

        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);
            var model = reservation.MapToDetailedReservationModel();

            ViewBag.Previous = Request.QueryString["from"] ?? "/reservations";
            return View("~/Views/Admin/Reservations/Details.cshtml", model);
        }

        public ActionResult History(Guid id)
        {
            var reservation = _reservationRepository.Load(id);
            var model = reservation.MapToEventHistory();

            return View("~/Views/Admin/Reservations/History.cshtml", model);
        }

        [HttpPost]
        public ActionResult AdvanceReceived(Guid id, FormCollection form)
        {
            var value = form.Get("acompte");
            _reservationManager.DeclareAdvanceReceived(id, double.Parse(value));

            return RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        public ActionResult ExtendExpiration(Guid id)
        {
            _reservationManager.ExtendExpiration(id, 2);

            return RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        public ActionResult PaymentReceived(Guid id, FormCollection form)
        {
            _reservationManager.DeclarePaymentReceived(id, double.Parse(form.Get("paiement")));

            return RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        public ActionResult Cancel(Guid id)
        {
            _reservationManager.CancelReservation(id, "annulé par le propriétaire");

            return RedirectToAction("All");
        }
    }
}