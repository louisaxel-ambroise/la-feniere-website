using Gite.Model.Repositories;
using Gite.WebSite.Models;
using System;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public OverviewController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            return View(reservation.MapToOverview());
        }
    }
}