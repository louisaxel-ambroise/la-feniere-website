using Gite.Database;
using Gite.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gite.Model.Business;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPriceCalculator _priceCalculator;
        
        // TODO: inject properties
        public ReservationController()
        {
            _reservationRepository = new StubReservationRepository(); 
            _priceCalculator = new PriceCalculator();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListWeekForMonth(int year, int month)
        {
            var dates = new List<Date>();

            var beginDate = new DateTime(year, month, 1);

            for (var date = beginDate; date.Month == month; date = date.AddDays(1))
            {
                if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                    var currentDate = new Date(date);
                    currentDate.Price = _priceCalculator.CalculatePrice(year, currentDate.DayOfYear);
                    dates.Add(currentDate);
                }
            }

            return PartialView(dates);
        }

        public ActionResult CheckIn(int year, int dayOfYear)
        {
            var date = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
            if(date.DayOfWeek != DayOfWeek.Saturday) return Redirect("/reservation");
            
            var price = _priceCalculator.CalculatePrice(year, dayOfYear);
            var reservation = _reservationRepository.CreateReservation(year, dayOfYear, price.Amount);

            return View(reservation);
        }
    }
}