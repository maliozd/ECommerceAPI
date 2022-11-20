using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ECommerceAPI.Infrastructure.Filters
{
    public class UsernameBindActionFilterAttribute : HttpParameterBinding
    {
        public UsernameBindActionFilterAttribute(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(System.Web.Http.Metadata.ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
           var data = actionContext.ActionArguments.Values.First();

            var tsc = new TaskCompletionSource<object>();
            tsc.SetResult(null);
            return tsc.Task;
        }
    }
}
