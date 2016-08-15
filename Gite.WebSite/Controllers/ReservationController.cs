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
                firstWeek = DateTime.ParseExact(Request.Params.Get("f"), "dd/MM/yyyy", null);
                lastWeek = DateTime.ParseExact(Request.Params.Get("l"), "dd/MM/yyyy", null);

                EnsureDatesAreSaturday(firstWeek, lastWeek);
                if(_reservationPlanner.ContainsBookedWeek(firstWeek, lastWeek)) throw new Exception("Week already booked.");

                reservation = _reservationPlanner.PlanReservationForWeeks(firstWeek, lastWeek);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
            var model = reservation.MapToReservationModel(ip);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckIn(ReservationModel model)
        {
            EnsureDatesAreSaturday(model.StartsOn, model.LastWeek);
            var reservation = _reservationPlanner.PlanReservationForWeeks(model.StartsOn, model.LastWeek);

            if(_reservationPlanner.ContainsBookedWeek(model.StartsOn, model.LastWeek)) throw new Exception("Week already booked.");
            if (model.FinalPrice != reservation.FinalPrice || !ModelState.IsValid) return View(model);

            var details = new ReservationDetails
            {
                Contact = new Contact
                {
                    Address = model.FormatAddress(), Mail = model.Email, Name = model.Name, Phone = model.Phone
                },
                People = new People
                {
                    Adults = model.Adults, Children = model.Children, Babies = model.Babies,
                    Animals = model.AnimalsNumber, AnimalsDescription = model.AnimalsType
                }
            };

            var reservationId = _reservationBooker.Book(reservation, details);
           
            return RedirectToAction("Details", "Overview", new { id = reservationId });
        }

        private static void EnsureDatesAreSaturday(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday))
                throw new Exception("Dates must be saturdays.");
        }
    }
}