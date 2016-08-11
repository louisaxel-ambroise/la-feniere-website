using System;

namespace Gite.Model.Dtos
{
    public class WeekDetail
    {
        public DateTime StartsOn { get; set; }
        public double Price { get; set; }
        public bool IsReserved { get; set; } 
    }
}