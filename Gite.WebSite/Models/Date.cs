using System;

namespace Gite.Model.Model
{
    public class Date
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Reserved { get; set; }
        public double Price { get; set; }
    }
}