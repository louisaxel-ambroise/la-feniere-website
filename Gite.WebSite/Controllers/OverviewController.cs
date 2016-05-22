using Gite.Model.Repositories;
using Gite.Model.Services.PaymentProcessor;
using Gite.Model.Services.ReservationCanceller;
using Gite.WebSite.Models;
using System;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IReservationCanceller _reservationCanceller;

        public OverviewController(IReservationRepository reservationRepository, IPaymentProcessor paymentProcessor, IReservationCanceller reservationCanceller)
        {
            _reservationRepository = reservationRepository;
            _paymentProcessor = paymentProcessor;
            _reservationCanceller = reservationCanceller;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            return View(reservation.MapToOverview());
        }

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            _paymentProcessor.PaymentDeclared(id);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult CancelReservation(Guid id)
        {
            _reservationCanceller.Cancel(id);

            return RedirectToAction("Details", new { id = id });
        }
    }
}