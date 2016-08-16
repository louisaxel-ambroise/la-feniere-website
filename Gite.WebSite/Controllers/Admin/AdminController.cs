using System;
using System.Linq;
using Gite.Model.Repositories;
using System.Web.Mvc;
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
            var alerts = _reservationRepository.Query().Count(x => !x.IsCancelled && ((!x.AdvancePaymentReceived && x.BookedOn <= DateTime.Now.Date.AddDays(-4)) || (!x.PaymentReceived && x.FirstWeek <= DateTime.Now.Date.AddDays(11))));

            var model = new HomeModel
            {
                Alerts = alerts
            };

            return View("~/Views/Admin/Home/Index.cshtml", model);
        }
    }
}