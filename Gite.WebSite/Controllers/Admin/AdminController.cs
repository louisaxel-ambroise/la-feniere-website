using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Domain.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AdminController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public AdminController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var alerts = _reservationRepository.QueryValids().Count(x => (!x.AdvancePaymentReceived && x.BookedOn <= DateTime.UtcNow.AddDays(-4)) || (!x.PaymentReceived && x.FirstWeek <= DateTime.UtcNow.AddDays(11)));
            alerts += _reservationRepository.Query().Count(x => !x.IsCancelled && ((x.BookedOn < DateTime.UtcNow.AddDays(-5) && !x.AdvancePaymentDeclared) || (x.BookedOn < DateTime.UtcNow.AddDays(-9) && !x.AdvancePaymentReceived)));

            var model = new HomeModel
            {
                Alerts = alerts
            };

            return View("~/Views/Admin/Home/Index.cshtml", model);
        }
    }
}