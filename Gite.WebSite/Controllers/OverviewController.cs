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

        [HttpGet]
        public ActionResult PaymentDeclared(Guid id)
        {
            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult CancelReservation(Guid id)
        {
            return RedirectToAction("Details", new { id = id });
        }
    }
}