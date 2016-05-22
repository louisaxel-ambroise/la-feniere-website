using System;
using System.Linq;
using System.Web.Http;
using Gite.Model.Repositories;
using Gite.WebSite.Models;
using Gite.Model.Services.PaymentProcessor;
using Gite.Model.Services.DepositRefundProcessor;
using Gite.Model.Services.ReservationCanceller;

namespace Gite.WebSite.Controllers.Api
{
    public class ReservationController : ApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IRefundProcessor _refundProcessor;
        private readonly IReservationCanceller _reservationCanceller;

        public ReservationController(
            IReservationRepository reservationRepository, 
            IPaymentProcessor paymentProcessor, 
            IRefundProcessor refundProcessor,
            IReservationCanceller reservationCanceller)
        {
            _reservationRepository = reservationRepository;
            _paymentProcessor = paymentProcessor;
            _refundProcessor = refundProcessor;
            _reservationCanceller = reservationCanceller;
        }

        [HttpGet]
        public IHttpActionResult PaymentDeclared()
        {
            var reservations = _reservationRepository
                .Query()
                .Where(x => x.PaymentDeclared && !x.PaymentReceived)
                .ToList()
                .Select(x => x.MapToApiReservation());

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Accountancy()
        {
            var reservations = _reservationRepository.Query()
                .Where(x => x.StartingOn <= DateTime.Now && x.CancelToken == null)
                .Select(x => new Account { Date = x.StartingOn, Price = x.Price }).ToList();

            return Ok(new
            {
                Month = reservations.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month).Sum(x => x.Price),
                Year = reservations.Where(x => x.Date.Year == DateTime.Now.Year).Sum(x => x.Price),
                All = reservations.Sum(x => x.Price),
            });
        }

        [HttpGet]
        public IHttpActionResult PendingConfirmation()
        {
            var reservations = _reservationRepository
                .Query()
                .Where(x => x.CancelToken == null && x.PaymentDeclared && !x.PaymentReceived)
                .OrderBy(x => x.StartingOn)
                .ToList()
                .Select(x => x.MapToApiReservation());

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Incoming()
        {
            var now = DateTime.Now;
            var reservations = _reservationRepository
                .Query()
                .Where(x => x.CancelToken == null && x.StartingOn > now && x.StartingOn < now.AddMonths(2))
                .OrderBy(x => x.StartingOn)
                .ToList()
                .Select(x => x.MapToApiReservation());

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Past()
        {
            var now = DateTime.Now;
            var reservations = _reservationRepository
                .Query()
                .Where(x => x.CancelToken == null && x.StartingOn > now.AddMonths(-2) && x.StartingOn < now)
                .OrderByDescending(x => x.EndingOn)
                .ToList()
                .Select(x => x.MapToApiReservation());

            return Ok(reservations);
        }

        [HttpPut]
        public IHttpActionResult PaymentReceived(Guid id)
        {
            _paymentProcessor.PaymentReceived(id);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult CautionRefunded(Guid id)
        {
            _refundProcessor.Process(id);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Cancel(Guid id)
        {
            _reservationCanceller.Cancel(id);

            return Ok();
        }
    }

    public class Account
    {
        public DateTime Date { get; set; }
        public float Price { get; set; }
    }
}