using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using Gite.WebSite.Models;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IWeekCalendar _weekCalendar;

        public ReservationController(IWeekCalendar weekCalendar)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");

            _weekCalendar = weekCalendar;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListWeekForMonth(int year, int month)
        {
            var result = new List<Date>();
            var date = new DateTime(year, month, 1);
            while (date.DayOfWeek != DayOfWeek.Saturday) date = date.AddDays(1);

            while (date.Month == month)
            {
                result.Add(new Date
                {
                    BeginDate = date.Date,
                    EndDate = date.AddDays(7).Date,
                    Reserved = date < DateTime.Now,
                    Price = 590
                });
                date = date.AddDays(7);
            }

            return PartialView(result);
        }

        public ActionResult CheckIn()
        {
            var firstWeek = DateTime.Parse(Request.Params.Get("f"));
            var lastWeek = DateTime.Parse(Request.Params.Get("l"));

            var reservationWeeks = _weekCalendar.GetWeeksBetween(firstWeek, lastWeek);

            if (reservationWeeks.Any(x => x.IsReserved))
            {
                throw new Exception("Week is already booked.");
            }

            return View(new ReservationModel { Price = 0, Caution = 280 });
        }

        [HttpPost]
        public ActionResult CheckIn(string id, ReservationModel model)
        {
            var date = default(object); // TODO: _calendar.GetSpecificDate(id);

            //if (date.Reserved) throw new Exception("This date is already reserved...");
            //if (model.Price != date.Price.Amount || model.Caution != 280 || !ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //var ipAddress = HttpContext.Request.UserHostAddress;
            //var reservation = model.MapToReservation(id, ipAddress, date);

            //_reservationPersister.Persist(reservation);
            //model.ReservationId = reservation.Id;
           
            return View("ValidateBooking", model);
        }
    }
}