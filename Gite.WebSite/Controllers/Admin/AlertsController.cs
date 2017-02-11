using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Domain.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AlertsController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public AlertsController(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationReader");

            _reservationRepository = reservationRepository;
        }

        public ActionResult Index()
        {
            var advances = _reservationRepository.QueryValids().Count(x => !x.AdvancePaymentReceived && x.BookedOn <= DateTime.UtcNow.Date.AddDays(-4));
            var payments = _reservationRepository.QueryValids().Count(x => !x.PaymentReceived && x.FirstWeek <= DateTime.UtcNow.Date.AddDays(11));
            var expired = _reservationRepository.Query().Count(x => !x.IsCancelled && ((x.BookedOn < DateTime.UtcNow.AddDays(-5) && !x.AdvancePaymentDeclared) || (x.BookedOn < DateTime.UtcNow.AddDays(-9) && !x.AdvancePaymentReceived)));
            
            var model = new AlertsModel
            {
                Advances = advances,
                Payments = payments,
                Expired = expired
            };

            return View("~/Views/Admin/Alerts/Index.cshtml", model);
        }

        public ActionResult Advances()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => !x.AdvancePaymentReceived && x.BookedOn <= DateTime.UtcNow.Date.AddDays(-4)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Advances.cshtml", model);
        }

        public ActionResult Payments()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => !x.PaymentReceived && x.FirstWeek <= DateTime.UtcNow.Date.AddDays(11)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Payments.cshtml", model);
        }

        public ActionResult Expired()
        {
            var reservations = _reservationRepository.Query().Where(x => !x.IsCancelled && ((x.BookedOn < DateTime.UtcNow.AddDays(-5) && !x.AdvancePaymentDeclared) || (x.BookedOn < DateTime.UtcNow.AddDays(-9) && !x.AdvancePaymentReceived))).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Expired.cshtml", model);
        }
    }
}