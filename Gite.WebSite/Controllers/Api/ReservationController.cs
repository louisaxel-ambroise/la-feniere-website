using System;
using System.Linq;
using System.Web.Http;
using Gite.Model.Repositories;
using Gite.WebSite.Models;
using Gite.Model.Services.PaymentProcessor;
using Gite.Model.Services.DepositRefundProcessor;

namespace Gite.WebSite.Controllers.Api
{
    public class ReservationController : ApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IRefundProcessor _refundProcessor;

        public ReservationController(IReservationRepository reservationRepository, IPaymentProcessor paymentProcessor, IRefundProcessor refundProcessor)
        {
            _reservationRepository = reservationRepository;
            _paymentProcessor = paymentProcessor;
            _refundProcessor = refundProcessor;
        }

        [HttpGet]
        public IHttpActionResult Incoming()
        {
            var now = DateTime.Now;
            var reservations = _reservationRepository
                .Query()
                .Where(x => x.StartingOn > now && x.StartingOn < now.AddMonths(2))
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
                .Where(x => x.StartingOn > now.AddMonths(-2) && x.StartingOn < now)
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
    }
}