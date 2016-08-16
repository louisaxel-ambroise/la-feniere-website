using System;

namespace Gite.WebSite.Models
{
    public class Date
    {
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }
        public bool IsReserved { get; set; }
        public double Price { get; set; }
    }
}