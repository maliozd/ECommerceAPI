using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.API.ColumnModel
{
    public class CustomUserNameColumn : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "UserName");
            if (value != null)
            {
                var getValue = propertyFactory.CreateProperty(username, value);
                logEvent.AddPropertyIfAbsent(getValue);
            }
        }
    }

    //    / <summary>
    //    / 
    //    / </summary>
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;
        readonly IHttpContextAccessor httpContextAccessor;

        public LogUserNameMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            this.next = next;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("UserName", context.User.Identity.Name);
            var userName = httpContextAccessor.HttpContext.User.Identity.Name;
            var ab = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var a = 1;
            return next(context);
        }
    }
}

