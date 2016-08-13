using Gite.Model.Model;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static ReservationModel MapToReservationModel(this Reservation reservation, string ip = null)
        {
            return new ReservationModel
            {
                StartsOn = reservation.StartsOn(),
                EndsOn = reservation.EndsOn(),
                Price = reservation.Price(),
                Reduction = reservation.Reduction(),
                Caution = 280,
                Ip = ip
            };
        }

        public static ReservationOverview MapToOverview(this Reservation reservation)
        {
            return new ReservationOverview
            {

            };
        }
    }
}