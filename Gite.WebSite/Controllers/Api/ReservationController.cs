using System;
using System.Linq;
using System.Web.Http;
using Gite.Model.Repositories;

namespace Gite.WebSite.Controllers.Api
{
    public class ReservationController : ApiController
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public IHttpActionResult PaymentDeclared()
        {
            var reservations = _reservationRepository.Query().ToList();

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Accountancy()
        {
            var reservations = _reservationRepository.Query().ToList();

            return Ok(new
            {
               
            });
        }

        [HttpGet]
        public IHttpActionResult PendingConfirmation()
        {
            var reservations = _reservationRepository.Query().ToList();

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Incoming()
        {
            var now = DateTime.Now;
            var reservations = _reservationRepository.Query().ToList();

            return Ok(reservations);
        }

        [HttpGet]
        public IHttpActionResult Past()
        {
            var now = DateTime.Now;
            var reservations = _reservationRepository.Query().ToList();

            return Ok(reservations);
        }

        [HttpPut]
        public IHttpActionResult PaymentReceived(Guid id)
        {
            //TODO: _paymentProcessor.PaymentReceived(id);

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult CautionRefunded(Guid id)
        {
            //TODO: _refundProcessor.Process(id);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Cancel(Guid id)
        {
            //TODO: _reservationCanceller.Cancel(id);

            return Ok();
        }
    }

    public class Account
    {
        public DateTime Date { get; set; }
        public float Price { get; set; }
    }
}