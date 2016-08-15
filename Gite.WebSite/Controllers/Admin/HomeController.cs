﻿using System;
using System.Linq;
using Gite.Model.Repositories;
using System.Web.Mvc;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class HomeController : AuthorizeController
    {
        private readonly IReservationRepository _reservationRepository;

        public HomeController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var alerts = _reservationRepository.Query().Count(x =>
                (x.AdvancedReceptionDate == null && x.BookedOn <= DateTime.Now.Date.AddDays(-4)) || (x.PaymentReceptionDate == null && x.FirstWeek <= DateTime.Now.Date.AddDays(11)));

            var model = new HomeModel
            {
                Alerts = alerts
            };

            return View("~/Views/Admin/Home/Index.cshtml", model);
        }
    }
}