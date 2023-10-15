using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerceAPI.Application.ObjectResults
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class InvalidModelStateValidationObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status405MethodNotAllowed;
        //public KeyValuePair<string, IEnumerable<string>>[] Errors { get; set; }
        public InvalidModelStateValidationObjectResult([ActionResultObjectValue] object? error)
          : base(error)
        {
            StatusCode = DefaultStatusCode;
        }
        public InvalidModelStateValidationObjectResult([ActionResultObjectValue] ModelStateDictionary modelState)
           : base(new SerializableError(modelState))
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            StatusCode = DefaultStatusCode;
        }
    }
}
