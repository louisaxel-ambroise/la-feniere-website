using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gite.WebSite.Attributes
{
    public class MaxSumNumberAttribute : ValidationAttribute
    {
        public MaxSumNumberAttribute(int maxValue, params string[] propertyNames)
        {
            MaxValue = maxValue;
            PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; private set; }
        public int MaxValue { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<int>();
            
            var totalNumber = values.Sum(x => x);

            if (totalNumber > MaxValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }

    public class NotNullIfFilledAttribute : ValidationAttribute
    {
        public NotNullIfFilledAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyName);
            var values = (int)property.GetValue(validationContext.ObjectInstance, null);

            if (values > 0 && (value == null || ((string)value).Length == 0))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}