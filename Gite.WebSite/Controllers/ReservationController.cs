using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gite.Model.Business;
using Gite.Model.Business.Strategies;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services;
using Gite.WebSite.Models;

namespace Gite.WebSite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IReservationPersister _reservationPersister;

        public ReservationController(IReservationRepository reservationRepository, IPriceCalculator priceCalculator, IReservationPersister reservationPersister)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationPersister == null) throw new ArgumentNullException("reservationPersister");

            _reservationRepository = reservationRepository; 
            _priceCalculator = priceCalculator;
            _reservationPersister = reservationPersister;
        }

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
                if (date.DayOfWeek != DayOfWeek.Saturday) continue;

                var currentDate = new Date(date);
                var isReserved = _reservationRepository.IsWeekReserved(year, currentDate.DayOfYear);

                currentDate.Reserved = currentDate.Reserved || isReserved;
                currentDate.Price = _priceCalculator.CalculatePrice(year, currentDate.DayOfYear);

                dates.Add(currentDate);
            }

            return PartialView(dates);
        }

        public ActionResult CheckIn(string id)
        {
            try
            {
                var date = Date.Parse(id);
                var calculatedPrice = _priceCalculator.CalculatePrice(date.BeginDate);
                
                PrepareViewbag(id, date, calculatedPrice);

                return View(new ReservationModel { Price = calculatedPrice.Amount });
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("/");
            }
        }

        private void PrepareViewbag(string id, Date date, PriceResponse price)
        {
            ViewBag.ReservationId = id;
            ViewBag.StartingOn = date.BeginDate.ToString("dd/MM/yyyy");
            ViewBag.EndingOn = date.BeginDate.ToString("dd/MM/yyyy");
            ViewBag.Price = price;
        }

        [HttpPost]
        public ActionResult CheckIn(string id, ReservationModel model) // TODO: validate model.
        {
             try
            {
                var  date = Date.Parse(id);
                var calculatedPrice = _priceCalculator.CalculatePrice(date.BeginDate);

                if (!ModelState.IsValid)
                {
                    PrepareViewbag(id, date, calculatedPrice);
                    return View(model);
                }

                if (model.Price != calculatedPrice.Amount) return RedirectToAction("/"); // TODO: redirect to error

                _reservationPersister.Persist(new Reservation
                {
                    Id = id,
                    StartingOn = date.BeginDate,
                    EndingOn = date.EndDate,
                    Price = calculatedPrice.Amount,
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
                });
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("/");
            }
            return View("ValidateBooking");
        }
    }
}