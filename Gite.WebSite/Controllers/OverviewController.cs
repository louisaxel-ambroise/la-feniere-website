using System;
using System.Web.Mvc;
using Gite.Cqrs.Aggregates;
using Gite.Model.Aggregates;
using Gite.Model.Services.Reservations;
using Gite.WebSite.Models;

namespace Gite.WebSite.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;
        private readonly IReservationCanceller _reservationCanceller;
        private readonly IPaymentManager _paymentManager;

        public OverviewController(IAggregateManager<ReservationAggregate> aggregateManager, IReservationCanceller reservationCanceller, IPaymentManager paymentManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");
            if (reservationCanceller == null) throw new ArgumentNullException("reservationCanceller");
            if (paymentManager == null) throw new ArgumentNullException("paymentManager");

            _aggregateManager = aggregateManager;
            _reservationCanceller = reservationCanceller;
            _paymentManager = paymentManager;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _aggregateManager.Load(id);

            return View(reservation.MapToOverview());
        }

        [HttpGet]
        public ActionResult AdvanceDeclared(Guid id)
        {
            _paymentManager.DeclareAdvancePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            _paymentManager.DeclarePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult CancelReservation(Guid id)
        {
            _reservationCanceller.CancelReservation(id, "annulé par l'utilisateur");

            return RedirectToAction("Details", new { id });
        }
    }
}