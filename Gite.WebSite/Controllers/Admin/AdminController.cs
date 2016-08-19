using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AdminController : AuthorizeController
    {
        private readonly IReservationReader _reservationReader;

        public AdminController(IReservationReader reservationReader)
        {
            _reservationReader = reservationReader;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var alerts = _reservationReader.QueryValids().Count(x => (!x.AdvancePaymentReceived && x.BookedOn <= DateTime.UtcNow.AddDays(-4)) || (!x.PaymentReceived && x.FirstWeek <= DateTime.UtcNow.AddDays(11)));
            alerts += _reservationReader.Query().Count(x => !x.IsCancelled && ((x.BookedOn < DateTime.UtcNow.AddDays(-5) && !x.AdvancePaymentDeclared) || (x.BookedOn < DateTime.UtcNow.AddDays(-9) && !x.AdvancePaymentReceived)));

            var model = new HomeModel
            {
                Alerts = alerts
            };

            return View("~/Views/Admin/Home/Index.cshtml", model);
        }
    }
}