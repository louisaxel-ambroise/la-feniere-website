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
        public IHttpActionResult Incoming()
        {
            var now = DateTime.Now;

            return Ok(_reservationRepository.Query().Where(x => x.StartingOn > now && x.StartingOn < now.AddMonths(2)).ToList());
        }

        [HttpGet]
        public IHttpActionResult Past()
        {
            var now = DateTime.Now;

            return Ok(_reservationRepository.Query().Where(x => x.StartingOn > now.AddMonths(-2) && x.StartingOn < now).ToList());
        }
    }
}