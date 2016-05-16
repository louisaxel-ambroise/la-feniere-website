using Gite.Database;
using Gite.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gite.Model.Business;
using Gite.Model;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPriceCalculator _priceCalculator;
        
        public ReservationController(IReservationRepository reservationRepository, IPriceCalculator priceCalculator)
        {
            _reservationRepository = reservationRepository; 
            _priceCalculator = priceCalculator;
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
                    var isReserved = _reservationRepository.IsWeekReserved(year, currentDate.DayOfYear);

                    currentDate.Reserved = currentDate.Reserved || isReserved;
                    currentDate.Price = _priceCalculator.CalculatePrice(year, currentDate.DayOfYear);

                    dates.Add(currentDate);
                }
            }

            return PartialView(dates);
        }

        public ActionResult CheckIn(string id)
        {
            try {
                var date = Date.Parse(id);
                var calculatedPrice = _priceCalculator.CalculatePrice(date.BeginDate);

                ViewBag.ReservationId = id;
                ViewBag.StartingOn = date.BeginDate.ToString("dd/MM/yyyy");
                ViewBag.EndingOn = date.BeginDate.ToString("dd/MM/yyyy");
                ViewBag.Price = calculatedPrice;

                return View(new ReservationModel { Price = calculatedPrice.Amount });
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("/");
            }
        }

        [HttpPost]
        public ActionResult ValidateBooking(string id, ReservationModel model)
        {
            // TODO: validate the model.
            //ValidateModel(model);

            try
            {
                var date = Date.Parse(id);
                var calculatedPrice = _priceCalculator.CalculatePrice(date.BeginDate).Amount;

                if (model.Price != calculatedPrice)
                {
                    return RedirectToAction("/");
                }

                var reservation = new Reservation
                {
                    Id = id,
                    StartingOn = date.BeginDate,
                    EndingOn = date.EndDate,
                    Price = calculatedPrice,
                    Confirmed = true,
                    Validated = true,
                    CreatedOn = DateTime.Now,
                    Contact = new Contact
                    {
                        Mail = model.Email,
                        Address = model.FormatAddress(),
                        Name = model.Name,
                        Phone = model.Phone
                    }
                };

                _reservationRepository.Insert(reservation);

                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("/");
            }
        }
    }
}