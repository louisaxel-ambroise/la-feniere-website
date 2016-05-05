using Gite.Database;
using Gite.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationRepository _reservationRepository;

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListWeekForMonth(int year, int month)
        {
            var dates = new List<Date>{ };
            Date currentDate;

            var beginDate = new DateTime(year, month, 1);

            for (var date = beginDate; date.Month == month; date = date.AddDays(1))
            {
                if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                   currentDate = new Date(date);
                   dates.Add(currentDate);
                }
            }

            return PartialView(dates);
        }

        public ActionResult CheckIn(int year, int dayOfYear)
        {
            return View();
        }
    }
}