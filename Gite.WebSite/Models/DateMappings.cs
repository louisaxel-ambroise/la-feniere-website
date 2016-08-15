using Gite.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace Gite.WebSite.Models
{
    public static class DateMappings
    {
        public static IEnumerable<Date> MapToDate(this IEnumerable<ReservationWeek> weeks)
        {
            return weeks.Select(MapToDate).ToList();
        }

        private static Date MapToDate(ReservationWeek week)
        {
            return new Date
            {
                StartsOn = week.StartsOn,
                EndsOn = week.EndsOn,
                IsReserved = week.IsReserved,
                Price = week.Price
            };
        }
    }
}