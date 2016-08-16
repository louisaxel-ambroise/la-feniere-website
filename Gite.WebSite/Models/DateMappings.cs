using System.Collections.Generic;
using System.Linq;
using Gite.Model.Model;

namespace Gite.WebSite.Models
{
    public static class DateMappings
    {
        public static IEnumerable<Date> MapToDate(this IEnumerable<Week> weeks)
        {
            return weeks.Select(MapToDate).ToList();
        }

        private static Date MapToDate(Week week)
        {
            return new Date
            {
                StartsOn = week.Start,
                EndsOn = week.Start.AddDays(7),
                IsReserved = week.IsReserved,
                Price = week.Price
            };
        }
    }
}