using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.
                    Where(x => x.Value.Errors.Any()).                   //key ilgili propertyi getirecek. error ise o propa karşılık olan bütün validasyon mesajlarını getirecek.
                    ToDictionary(E => E.Key, x => x.Value.Errors.
                    Select(e => e.ErrorMessage)).ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return; //? bir sonraki filtera geçmemesi için?
            }
            await next();
        }
    }
}
