using System;
using System.ComponentModel.DataAnnotations;
using Gite.WebSite.Attributes;

namespace Gite.WebSite.Models
{
    public class ReservationModel
    {
        public DateTime StartsOn { get; set; }
        public DateTime LastWeek { get; set; }
        public DateTime EndsOn { get; set; }

        public double FinalPrice { get; set; }
        public double OriginalPrice { get; set; }
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
        [MaxSumNumber(6, "Adults", ErrorMessage = "Le gîte ne peux accueillir plus de 6 personnes")]
        [Range(0, int.MaxValue, ErrorMessage = "Ce nombre ne peut être négatif")]
        public int Children { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Ce nombre ne peut être négatif")]
        public int Babies { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Ce nombre ne peut être négatif")]
        public int AnimalsNumber { get; set; }
        [NotNullIfFilled("AnimalsNumber", ErrorMessage = "Veuillez entrer le type d'animaux qui seront présents.")]
        public string AnimalsType { get; set; }

        public string FormatAddress()
        {
            return string.Format("{0}, {1} {2} ({3})", Street, ZipCode, City, Country);
        }
    }
}