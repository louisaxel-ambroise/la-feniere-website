using System.Web.Http;
using System.Web.Http.Controllers;

namespace Gite.WebSite.Attributes
{
    public class FromParamAttribute : ParameterBindingAttribute
    {
        public string Name { get; set; }

        public FromParamAttribute(string name)
        {
            Name = name;
        }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new FromParamParameterBinding(parameter, Name);
        }
    }
}