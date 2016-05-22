using System;
using System.Web.Mvc;
using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using Gite.Model.Services.ReservationPersister;
using Gite.WebSite.Models;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationPersister _reservationPersister;
        private readonly ReservationCalendar _calendar;

        public ReservationController(IReservationPersister reservationPersister, ReservationCalendar calendar)
        {
            if (reservationPersister == null) throw new ArgumentNullException("reservationPersister");
            if (calendar == null) throw new ArgumentNullException("calendar");

            _reservationPersister = reservationPersister;
            _calendar = calendar;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListWeekForMonth(int year, int month)
        {
            var dates = _calendar.ListSaturdaysForMonth(year, month);

            return PartialView(dates);
        }

        public ActionResult CheckIn(string id)
        {
            var date = _calendar.GetSpecificDate(id);
                
            PrepareViewbag(id, date);

            return View(new ReservationModel { Price = date.Price.Amount, Caution =date.Price.Amount });
        }

        [HttpPost]
        public ActionResult CheckIn(string id, ReservationModel model)
        {
            var date = _calendar.GetSpecificDate(id);
                
            if (model.Price != date.Price.Amount || model.Caution != date.Price.Amount)
            {
                PrepareViewbag(id, date);
                return View(model); // TODO: redirect to error?
            }
            if (!ModelState.IsValid)
            {
                PrepareViewbag(id, date);
                return View(model);
            }

            var ipAddress = HttpContext.Request.UserHostAddress;
            var reservation = model.MapToReservation(id, ipAddress, date);

            _reservationPersister.Persist(reservation);
           
            return View("ValidateBooking", model);
        }

        private void PrepareViewbag(string id, Date date)
        {
            ViewBag.ReservationId = id;
            ViewBag.Date = date.BeginDate;
            ViewBag.StartingOn = date.BeginDate.ToString("dd/MM/yyyy");
            ViewBag.EndingOn = date.EndDate.ToString("dd/MM/yyyy");
            ViewBag.Price = date.Price;
        }
    }
}