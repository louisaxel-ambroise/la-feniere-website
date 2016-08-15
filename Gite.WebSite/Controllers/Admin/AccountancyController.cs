using Gite.Model.Repositories;
using Gite.WebSite.Models.Admin;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountancyController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public AccountancyController(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        public ActionResult Index()
        {
            var reservations = _reservationRepository.QueryValidReservations().Where(x => x.AdvancedReceptionDate != null).ToList();
            var model = new AccountancyOverview
            {
                Month = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01)).Sum(x => x.FinalPrice),
                Year = reservations.Where(x => x.FirstWeek >= new DateTime(DateTime.Now.Year, 01, 01)).Sum(x => x.FinalPrice),
                AllTime = reservations.Sum(x => x.FinalPrice)
            };

            return View("~/Views/Admin/Accountancy/Index.cshtml", model);
        }
    }
}