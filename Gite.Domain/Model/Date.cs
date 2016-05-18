using System;
using Gite.Model.Services.PriceCalculation.Strategies;

namespace Gite.Model.Model
{
    public class Date
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Year { get; set; }
        public int DayOfYear { get; set; }
        public bool Reserved { get; set; }
        public PriceResponse Price { get; set; }
        public string Currency { get; set; }

        public Date(DateTime beginDate)
        {
            if(beginDate.DayOfWeek != DayOfWeek.Saturday)
            {
                throw new InvalidOperationException("A reservation must start on saturday.");
            }

            BeginDate = beginDate;
            EndDate = beginDate.AddDays(6);
            Year = beginDate.Year;
            DayOfYear = beginDate.DayOfYear;
            Reserved = beginDate < DateTime.Now.Date;
        }
    }
}