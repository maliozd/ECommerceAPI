using ECommerceAPI.Application.Abstraction.Services.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
       public string UserId { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); } }
    }
}
