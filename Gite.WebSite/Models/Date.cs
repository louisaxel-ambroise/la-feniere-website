using System;
using Gite.Model.Business;

namespace Gite.WebSite.Models
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

        public static Date Parse(string id)
        {
            var year = int.Parse(id.Substring(0, 4));
            var dayOfYear = int.Parse(id.Substring(4, 3));

            var beginDate = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
            return new Date(beginDate);
        }
    }
}