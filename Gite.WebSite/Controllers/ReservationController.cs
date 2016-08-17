using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using Gite.Model.Services.Pricing;
using Gite.Model.Services.Reservations;
using Gite.WebSite.Models;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IBooker _reservationBooker;
        private readonly IWeekCalendar _weekCalendar;
        private readonly IPriceCalculator _priceCalculator;

        public ReservationController(IWeekCalendar weekCalendar, IPriceCalculator priceCalculator, IBooker reservationBooker)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationBooker == null) throw new ArgumentNullException("reservationBooker");

            _weekCalendar = weekCalendar;
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
            var firstWeek = DateTime.ParseExact(Request.QueryString["f"], "dd/MM/yyyy", null);
            var lastWeek = DateTime.ParseExact(Request.QueryString["l"], "dd/MM/yyyy", null);

            EnsureDatesAreSaturday(firstWeek, lastWeek);

            var price = _priceCalculator.ComputeForInterval(firstWeek, lastWeek);
            var model = new ReservationModel
            {
                StartsOn = firstWeek,
                LastWeek = lastWeek,
                EndsOn = lastWeek.AddDays(7),
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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = new Contact
            {
                Address = model.FormatAddress(),
                Mail = model.Email,
                Name = model.Name,
                Phone = model.Phone
            };
            var people = new People
            {
                Adults = model.Adults,
                Children = model.Children,
                Babies = model.Babies,
                Animals = model.AnimalsNumber,
                AnimalsDescription = model.AnimalsType
            };

            var reservationId = _reservationBooker.Book(model.StartsOn, model.LastWeek, model.FinalPrice, contact, people);
           
            return RedirectToAction("Details", "Overview", new { id = reservationId });
        }

        private static void EnsureDatesAreSaturday(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday)) throw new Exception("Dates must be saturdays.");
        }
    }
}