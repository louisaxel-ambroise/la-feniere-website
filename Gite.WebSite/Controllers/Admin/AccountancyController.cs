using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Domain.Readers;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountancyController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public AccountancyController(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationReader");

            _reservationRepository = reservationRepository;
        }

        public ActionResult Index()
        {
            var reservations = _reservationRepository.QueryValids().Where(x => x.AdvancePaymentReceived).ToList();

            var month = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 01)).ToList();
            var year = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.UtcNow.Year, 01, 01)).ToList();

            var model = new AccountancyOverview
            {
                MonthEarned = month.Where(x => x.FirstWeek <= DateTime.UtcNow).Sum(x => x.FinalPrice),
                MonthPlanned = month.Where(x => x.FirstWeek > DateTime.UtcNow).Sum(x => x.FinalPrice),
                YearEarned = year.Where(x => x.FirstWeek <= DateTime.UtcNow).Sum(x => x.FinalPrice),
                YearPlanned = year.Where(x => x.FirstWeek > DateTime.UtcNow).Sum(x => x.FinalPrice),
                AllTimeEarned = reservations.Where(x => x.FirstWeek <= DateTime.UtcNow).Sum(x => x.FinalPrice),
                AllTimePlanned = reservations.Where(x => x.FirstWeek > DateTime.UtcNow).Sum(x => x.FinalPrice)
            };

            return View("~/Views/Admin/Accountancy/Index.cshtml", model);
        }
    }
}