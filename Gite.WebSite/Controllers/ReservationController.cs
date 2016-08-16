using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.Calendar;
using Gite.WebSite.Models;
using Gite.Model.Services.Reservations;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IBooker _reservationBooker;
        private readonly IWeekCalendar _weekCalendar;
        private readonly IBookedWeekReader _bookedWeekReader;
        private readonly IPriceCalculator _priceCalculator;

        public ReservationController(IWeekCalendar weekCalendar, IBookedWeekReader bookedWeekReader, IPriceCalculator priceCalculator, IBooker reservationBooker)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationBooker == null) throw new ArgumentNullException("reservationBooker");

            _weekCalendar = weekCalendar;
            _bookedWeekReader = bookedWeekReader;
            _priceCalculator = priceCalculator;
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
            DateTime firstWeek, lastWeek;

            try
            {
                firstWeek = DateTime.ParseExact(Request.Params.Get("f"), "dd/MM/yyyy", null);
                lastWeek = DateTime.ParseExact(Request.Params.Get("l"), "dd/MM/yyyy", null);

                EnsureDatesAreSaturday(firstWeek, lastWeek);
                if(_bookedWeekReader.Query().Any(x => x.IsValid() && x.Week >= firstWeek && x.Week <= lastWeek)) 
                    throw new Exception("Week already booked.");
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var price = _priceCalculator.ComputeForInterval(firstWeek, lastWeek);
            var model = new ReservationModel
            {
                StartsOn = firstWeek,
                LastWeek = lastWeek,
                FinalPrice = price.Final,
                OriginalPrice = price.Original,
                Reduction = price.Reduction,
                Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckIn(ReservationModel model)
        {
            EnsureDatesAreSaturday(model.StartsOn, model.LastWeek);
            
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

            var reservationId = _reservationBooker.Book(model.StartsOn, model.LastWeek, details);
           
            return RedirectToAction("Details", "Overview", new { id = reservationId });
        }

        private static void EnsureDatesAreSaturday(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday)) throw new Exception("Dates must be saturdays.");
        }
    }
}