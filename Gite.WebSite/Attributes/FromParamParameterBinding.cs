using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Gite.WebSite.Extensions;

namespace Gite.WebSite.Attributes
{
    public class FromParamParameterBinding : HttpParameterBinding
    {
        private readonly string _name;

        public FromParamParameterBinding(HttpParameterDescriptor parameter, string name) : base(parameter)
        {
            _name = name;
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var date = actionContext.Request.RequestUri.Query.Get(_name);
            actionContext.ActionArguments[Descriptor.ParameterName] = DateTime.ParseExact(date, "dd/MM/yyyy", null);

            return Task.FromResult(0);
        }
    }
}