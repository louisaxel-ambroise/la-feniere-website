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

        public IHttpActionResult Get()
        {
            var now = DateTime.Now;

            return Ok(_reservationRepository.Query().Where(x => x.StartingOn > now).ToList());
        }
    }
}