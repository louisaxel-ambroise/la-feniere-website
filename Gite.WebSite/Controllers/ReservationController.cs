using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Domain.Model;
using Gite.Domain.Services.Calendar;
using Gite.Domain.Services.Pricing;
using Gite.Domain.Services.Reservations;
using Gite.WebSite.Models;
using Gite.Domain.Readers;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IWeekCalendar _weekCalendar;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IReservationManager _reservationManager;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IWeekCalendar weekCalendar, IPriceCalculator priceCalculator, IReservationRepository reservationRepository, IReservationManager reservationManager)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationManager == null) throw new ArgumentNullException("reservationManager");
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _weekCalendar = weekCalendar;
            _priceCalculator = priceCalculator;
            _reservationManager = reservationManager;
            _reservationRepository = reservationRepository;
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
            EnsureWeeksAreStillFree(firstWeek, lastWeek);

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

            if (!ModelState.IsValid) return View(model);

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
                AnimalsDescription = model.AnimalsType ?? ""
            };

            var reservationId = _reservationManager.Book(model.StartsOn, model.LastWeek, model.FinalPrice, contact, people);
           
            return RedirectToAction("Details", new { id = reservationId });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.IsLastMinute)
            {
                return View("LastMinute", reservation.MapToOverview());
            }
            else
            {
                return View(reservation.MapToOverview());
            }
        }

        [HttpGet]
        public ActionResult AdvanceDeclared(Guid id)
        {
            _reservationManager.DeclareAdvancePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            _reservationManager.DeclarePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult Cancel(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            return View(reservation.MapToOverview());
        }

        [HttpPost]
        public ActionResult CancelReservation(Guid id)
        {
            _reservationManager.CancelReservation(id, "annulé par l'utilisateur");

            return RedirectToAction("Details", new { id });
        }

        private static void EnsureDatesAreSaturday(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday)) throw new Exception("Dates must be saturdays.");
        }

        private void EnsureWeeksAreStillFree(DateTime firstWeek, DateTime lastWeek)
        {
            if(_reservationRepository.QueryValids().Any(x => (x.FirstWeek <= firstWeek && x.LastWeek >= firstWeek) || (x.FirstWeek >= firstWeek && x.FirstWeek < lastWeek)))
            {
                throw new Exception("There is already a reservation for these dates");
            }
        }
    }
}