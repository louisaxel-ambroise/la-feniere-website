using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using Gite.Model.Services.Pricing;
using Gite.Model.Services.Reservations;
using Gite.WebSite.Models;
using Gite.Cqrs.Aggregates;
using Gite.Model.Aggregates;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IBooker _reservationBooker;
        private readonly IWeekCalendar _weekCalendar;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;
        private readonly IReservationCanceller _reservationCanceller;
        private readonly IPaymentManager _paymentManager;

        public ReservationController(IWeekCalendar weekCalendar, IPriceCalculator priceCalculator, IBooker reservationBooker, IAggregateManager<ReservationAggregate> aggregateManager, IReservationCanceller reservationCanceller, IPaymentManager paymentManager)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationBooker == null) throw new ArgumentNullException("reservationBooker");
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");
            if (reservationCanceller == null) throw new ArgumentNullException("reservationCanceller");
            if (paymentManager == null) throw new ArgumentNullException("paymentManager");

            _weekCalendar = weekCalendar;
            _priceCalculator = priceCalculator;
            _reservationBooker = reservationBooker;
            _aggregateManager = aggregateManager;
            _reservationCanceller = reservationCanceller;
            _paymentManager = paymentManager;
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
                AnimalsDescription = model.AnimalsType
            };

            var reservationId = _reservationBooker.Book(model.StartsOn, model.LastWeek, model.FinalPrice, contact, people);
           
            return RedirectToAction("Details", new { id = reservationId });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _aggregateManager.Load(id);

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
            _paymentManager.DeclareAdvancePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            _paymentManager.DeclarePaymentDone(id);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public ActionResult Cancel(Guid id)
        {
            var reservation = _aggregateManager.Load(id);

            return View(reservation.MapToOverview());
        }

        [HttpPost]
        public ActionResult CancelReservation(Guid id)
        {
            _reservationCanceller.CancelReservation(id, "annulé par l'utilisateur");

            return RedirectToAction("Details", new { id });
        }

        private static void EnsureDatesAreSaturday(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday)) throw new Exception("Dates must be saturdays.");
        }
    }
}