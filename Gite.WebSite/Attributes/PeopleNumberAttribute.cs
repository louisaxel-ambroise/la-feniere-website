using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gite.WebSite.Attributes
{
    public class PeopleNumberAttribute : ValidationAttribute
    {
        public PeopleNumberAttribute(params string[] propertyNames)
        {
            PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<int>();
            
            var totalNumber = values.Sum(x => x);

            if (totalNumber != Convert.ToInt32(value))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}