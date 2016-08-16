using Gite.Model.Repositories;
using Gite.WebSite.Models;
using System;
using System.Web.Mvc;
using Gite.Model.Services.Reservations;

namespace Gite.WebSite.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationCanceller _reservationCanceller;
        private readonly IPaymentManager _paymentManager;

        public OverviewController(IReservationRepository reservationRepository, IReservationCanceller reservationCanceller, IPaymentManager paymentManager)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (reservationCanceller == null) throw new ArgumentNullException("reservationCanceller");
            if (paymentManager == null) throw new ArgumentNullException("paymentManager");

            _reservationRepository = reservationRepository;
            _reservationCanceller = reservationCanceller;
            _paymentManager = paymentManager;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            return View(reservation.MapToOverview());
        }

        [HttpGet]
        public ActionResult AdvanceDeclared(Guid id)
        {
            _paymentManager.DeclareAdvancePaymentDone(id);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            _paymentManager.DeclarePaymentDone(id);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult CancelReservation(Guid id)
        {
            _reservationCanceller.CancelReservation(id, "annulé par l'utilisateur");

            return RedirectToAction("Details", new { id = id });
        }
    }
}