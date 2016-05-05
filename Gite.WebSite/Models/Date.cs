using System;

namespace Gite.WebSite.Models
{
    public class Date
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int Year { get; set; }
        public int DayOfYear { get; set; }
        public bool Reserved { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }

        public Date()
        {
        }

        public Date(DateTime beginDate) : this()
        {
            BeginDate = beginDate.ToString("dd/MM/yyyy");
            EndDate = beginDate.AddDays(6).ToString("dd/MM/yyyy");
            Year = beginDate.Year;
            DayOfYear = beginDate.DayOfYear;
            Reserved = beginDate < DateTime.Now.Date;
            Price = 400;
            Currency = "€";
        }
    }
}