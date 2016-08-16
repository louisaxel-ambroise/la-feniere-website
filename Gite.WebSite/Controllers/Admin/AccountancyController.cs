using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountancyController : AuthorizeController
    {
        private readonly IReservationReader _reservationReader;

        public AccountancyController(IReservationReader reservationReader)
        {
            if (reservationReader == null) throw new ArgumentNullException("reservationReader");

            _reservationReader = reservationReader;
        }

        public ActionResult Index()
        {
            var reservations = _reservationReader.QueryValids().Where(x => x.AdvancePaymentReceived).ToList();

            var month = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01)).ToList();
            var year = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, 01, 01)).ToList();

            var model = new AccountancyOverview
            {
                MonthEarned = month.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                MonthPlanned = month.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice),
                YearEarned = year.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                YearPlanned = year.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice),
                AllTimeEarned = reservations.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                AllTimePlanned = reservations.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice)
            };

            return View("~/Views/Admin/Accountancy/Index.cshtml", model);
        }
    }
}