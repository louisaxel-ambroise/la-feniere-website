using Gite.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Gite.WebSite.Models.Admin
{
    public static class WeekOverviewMapping
    {
        public static IEnumerable<WeekOverview> MapToCalendarOverview(this IEnumerable<Week> weeks)
        {
            return weeks.Select(MapToCalendarOverview);
        }

        private static WeekOverview MapToCalendarOverview(Week week)
        {
            return new WeekOverview
            {
                ReservationId = week.ReservationId,
                StartsOn = week.Start,
                IsReserved = week.IsReserved,
                IsValidated = week.IsValidated
            };
        }
    }
}