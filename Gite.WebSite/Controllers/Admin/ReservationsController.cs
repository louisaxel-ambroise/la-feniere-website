﻿using System;
using System.Linq;
using System.Web.Mvc;
using Gite.Cqrs.Aggregates;
using Gite.Model.Aggregates;
using Gite.Model.Readers;
using Gite.Model.Services.Reservations;
using Gite.WebSite.Models.Admin;

namespace Gite.WebSite.Controllers.Admin
{
    public class ReservationsController : AuthorizeController
    {
        private readonly IReservationReader _reservationReader;
        private readonly IPaymentManager _paymentManager;
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public ReservationsController(IReservationReader reservationReader, IPaymentManager paymentManager, IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (reservationReader == null) throw new ArgumentNullException("reservationReader");
            if (paymentManager == null) throw new ArgumentNullException("paymentManager");
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _reservationReader = reservationReader;
            _paymentManager = paymentManager;
            _aggregateManager = aggregateManager;
        }

        public ActionResult Index()
        {
            var valids = _reservationReader.QueryValids().Where(x => x.FirstWeek > DateTime.UtcNow).ToList();

            var newReservations = valids.Count(x => !x.AdvancePaymentDeclared && !x.AdvancePaymentReceived);
            var pendingAdvance = valids.Count(x => x.AdvancePaymentDeclared && !x.AdvancePaymentReceived);
            var pendingPayment = valids.Count(x => x.PaymentDeclared && !x.PaymentReceived);
            var incomingReservations = valids.Count(x => x.FirstWeek <= DateTime.UtcNow.AddMonths(2));

            var model = new ReservationsOverview
            {
                New = newReservations,
                PendingAdvance = pendingAdvance,
                PendingPayment = pendingPayment,
                Incoming = incomingReservations
            };

            return View("~/Views/Admin/Reservations/Index.cshtml", model);
        }

        public ActionResult New()
        {
            var reservations = _reservationReader.QueryValids().Where(x => !x.AdvancePaymentDeclared && !x.AdvancePaymentReceived).OrderBy(x => x.BookedOn).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/New.cshtml", model);
        }

        public ActionResult PendingAdvance()
        {
            var reservations = _reservationReader.Query().Where(x => x.AdvancePaymentDeclared && !x.AdvancePaymentReceived).OrderBy(x => x.BookedOn).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/PendingAdvance.cshtml", model);
        }

        public ActionResult PendingPayment()
        {
            var reservations = _reservationReader.QueryValids().Where(x => x.PaymentDeclared && !x.PaymentReceived).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/PendingPayment.cshtml", model);
        }

        public ActionResult Incoming()
        {
            var reservations = _reservationReader.QueryValids().Where(x => x.FirstWeek <= DateTime.UtcNow.AddMonths(2)).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/Incoming.cshtml", model);
        }

        public ActionResult All()
        {
            var reservations = _reservationReader.QueryValids().Where(x => x.FirstWeek >= DateTime.UtcNow).OrderBy(x => x.FirstWeek).ToList();
            var model = reservations.Select(x => x.MapToReservationModel()).ToArray();

            return View("~/Views/Admin/Reservations/All.cshtml", model);
        }

        public ActionResult Details(Guid id)
        {
            var reservation = _aggregateManager.Load(id);
            var model = reservation.MapToReservationModel();

            ViewBag.Previous = Request.QueryString["from"] ?? "/admin/reservations";
            return View("~/Views/Admin/Reservations/Details.cshtml", model);
        }

        public ActionResult History(Guid id)
        {
            var reservation = _aggregateManager.Load(id);
            var model = reservation.MapToEventHistory();

            return View("~/Views/Admin/Reservations/History.cshtml", model);
        }

        [HttpPost]
        public ActionResult AdvanceReceived(Guid id, FormCollection form)
        {
            var value = form.Get("acompte");
            _paymentManager.DeclareAdvanceReceived(id, double.Parse(value));

            return RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        public ActionResult ExtendExpiration(Guid id)
        {
            _paymentManager.ExtendExpiration(id, 2);

            return RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        public ActionResult PaymentReceived(Guid id, FormCollection form)
        {
            _paymentManager.DeclarePaymentReceived(id, double.Parse(form.Get("paiement")));

            return RedirectToAction("Details", new { Id = id });
        }
    }
}