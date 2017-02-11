using Gite.Domain.Services.Calendar;
using Gite.WebSite.Models.Admin;
using System;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class CalendarController : AuthorizeController
    {
        private static IWeekCalendar _calendar;

        public CalendarController(IWeekCalendar calendar)
        {
            if (calendar == null) throw new ArgumentException("calendar");

            _calendar = calendar;
        }

        public ActionResult Index()
        {
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(3);
            var weeks = _calendar.ListWeeksBetween(DateTime.Now, endDate);
            var model = weeks.MapToCalendarOverview();

            return View("~/Views/Admin/Calendar/Index.cshtml", model);
        }
    }
}