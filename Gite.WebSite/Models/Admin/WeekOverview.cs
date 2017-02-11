using System;

namespace Gite.WebSite.Models.Admin
{
    public class WeekOverview
    {
        public Guid? ReservationId { get; internal set; }
        public bool IsReserved { get; internal set; }
        public bool IsValidated { get; internal set; }
        public DateTime StartsOn { get; set; }
    }
}