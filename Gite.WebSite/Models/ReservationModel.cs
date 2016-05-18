using System.ComponentModel.DataAnnotations;

namespace Gite.WebSite.Models
{
    public class ReservationModel
    {
        public int Price { get; set; }
        public string Ip { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public string FormatAddress()
        {
            return string.Format("{0}, {1} {2} ({3})", Street, ZipCode, City, Country);
        }
    }
}