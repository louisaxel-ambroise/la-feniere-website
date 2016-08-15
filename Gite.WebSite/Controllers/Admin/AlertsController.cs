using Gite.Model.Repositories;
using Gite.WebSite.Models.Admin;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class AlertsController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public AlertsController(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        public ActionResult Index()
        {
            var advances = _reservationRepository.Query().Count(x => x.AdvancedReceptionDate == null && x.BookedOn <= DateTime.Now.Date.AddDays(-4));
            var payments = _reservationRepository.Query().Count(x => x.PaymentReceptionDate == null && x.FirstWeek <= DateTime.Now.Date.AddDays(11));

            var model = new AlertsModel
            {
                Advances = advances,
                Payments = payments
            };

            return View("~/Views/Admin/Alerts/Index.cshtml", model);
        }

        public ActionResult Advances()
        {
            var reservations = _reservationRepository.Query().Where(x => x.AdvancedReceptionDate == null && x.BookedOn <= DateTime.Now.Date.AddDays(-4)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Advances.cshtml", model);
        }

        public ActionResult Payments()
        {
            var reservations = _reservationRepository.Query().Where(x => x.PaymentReceptionDate == null && x.FirstWeek <= DateTime.Now.Date.AddDays(11)).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Alerts/Payments.cshtml", model);
        }
    }
}