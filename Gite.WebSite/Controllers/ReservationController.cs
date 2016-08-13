using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using Gite.WebSite.Models;
using Gite.Model.Services.Reservations;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IBooker _reservationBooker;
        private readonly IWeekCalendar _weekCalendar;
        private readonly IReservationPlanner _reservationPlanner;

        public ReservationController(IWeekCalendar weekCalendar, IReservationPlanner reservationPlanner, IBooker reservationBooker)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");
            if (reservationBooker == null) throw new ArgumentNullException("reservationBooker");
            if (reservationPlanner == null) throw new ArgumentNullException("reservationPlanner");

            _weekCalendar = weekCalendar;
            _reservationPlanner = reservationPlanner;
            _reservationBooker = reservationBooker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListWeekForMonth(int year, int month)
        {
            var result = _weekCalendar.ListWeeksForMonth(year, month).MapToDate();

            return PartialView(result);
        }

        public ActionResult CheckIn()
        {
            Reservation reservation;
            DateTime firstWeek, lastWeek;

            try
            {
                firstWeek = DateTime.Parse(Request.Params.Get("f"));
                lastWeek = DateTime.Parse(Request.Params.Get("l"));
                reservation = _reservationPlanner.PlanReservationForWeeks(firstWeek, lastWeek);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (reservation.Weeks.Any(x => x.IsReserved))
            {
                throw new Exception("Week is already booked.");
            }

            var ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
            var model = reservation.MapToReservationModel(ip);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckIn(string id, ReservationModel model)
        {
            var reservation = _reservationPlanner.PlanReservationForWeeks(model.StartsOn, model.EndsOn);

            model.ReservationId = _reservationBooker.Book(reservation, null);
           
            return View("ValidateBooking", model);
        }
    }
}