using System.ComponentModel.DataAnnotations;

namespace Gite.WebSite.Attributes
{
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