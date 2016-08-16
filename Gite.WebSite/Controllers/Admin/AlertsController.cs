using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AlertsController : AuthorizeController
    {
        private readonly IReservationReader _reservationReader;

        public AlertsController(IReservationReader reservationReader)
        {
            if (reservationReader == null) throw new ArgumentNullException("reservationReader");

            _reservationReader = reservationReader;
        }

        public ActionResult Index()
        {
            var advances = _reservationReader.QueryValids().Count(x => !x.AdvancePaymentReceived && x.BookedOn <= DateTime.Now.Date.AddDays(-4));
            var payments = _reservationReader.QueryValids().Count(x => !x.PaymentReceived && x.FirstWeek <= DateTime.Now.Date.AddDays(11));
            var expired = _reservationReader.Query().Count(x => !x.IsCancelled && ((x.BookedOn < DateTime.Now.AddDays(-5) && !x.AdvancePaymentDeclared) || (x.BookedOn < DateTime.Now.AddDays(-9) && !x.AdvancePaymentReceived)));
            
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
            var reservations = _reservationReader.QueryValids().Where(x => !x.AdvancePaymentReceived && x.BookedOn <= DateTime.Now.Date.AddDays(-4)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Advances.cshtml", model);
        }

        public ActionResult Payments()
        {
            var reservations = _reservationReader.QueryValids().Where(x => !x.PaymentReceived && x.FirstWeek <= DateTime.Now.Date.AddDays(11)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Payments.cshtml", model);
        }

        public ActionResult Expired()
        {
            var reservations = _reservationReader.Query().Where(x => !x.IsCancelled && ((x.BookedOn.AddDays(5) < DateTime.Now && !x.AdvancePaymentDeclared) || (x.BookedOn.AddDays(9) < DateTime.Now && !x.AdvancePaymentReceived))).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Expired.cshtml", model);
        }
    }
}