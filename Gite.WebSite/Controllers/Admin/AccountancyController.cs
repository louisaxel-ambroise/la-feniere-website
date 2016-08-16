using Gite.Model.Repositories;
using Gite.WebSite.Models.Admin;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountancyController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public AccountancyController(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        public ActionResult Index()
        {
            var reservations = _reservationRepository.Query().Where(x => !x.IsCancelled && x.AdvancePaymentReceived).ToList();

            var month = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01)).ToList();
            var year = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, 01, 01)).ToList();

            var model = new AccountancyOverview
            {
                MonthEarned = month.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                MonthPlanned = month.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice),
                YearEarned = year.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                YearPlanned = year.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice),
                AllTimeEarned = reservations.Where(x => x.FirstWeek <= DateTime.Now).Sum(x => x.FinalPrice),
                AllTimePlanned = reservations.Where(x => x.FirstWeek > DateTime.Now).Sum(x => x.FinalPrice)
            };

            return View("~/Views/Admin/Accountancy/Index.cshtml", model);
        }
    }
}