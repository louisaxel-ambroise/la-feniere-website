using System;
using System.ComponentModel.DataAnnotations;

namespace Gite.WebSite.Models
{
    public class ReservationModel
    {
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }

        public double Price { get; set; }
        public double Reduction { get; set; }
        public double Caution { get; set; }
        public Guid ReservationId { get; set; }
        public string Ip { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez entrer votre adresse mail")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez entrer votre nom complet")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez entrer votre numéro de téléphone")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez renseigner votre adresse")]
        public string Street { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez renseigner votre code postal")]
        public string ZipCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez renseigner votre ville")]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez renseigner votre pays")]
        public string Country { get; set; }

        [Range(1, 6, ErrorMessage = "Au moins un adulte doit être présent")]
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }


        public int AnimalsNumber { get; set; }
        public string AnimalsType{ get; set; }

        public string FormatAddress()
        {
            return string.Format("{0}, {1} {2} ({3})", Street, ZipCode, City, Country);
        }
    }
}