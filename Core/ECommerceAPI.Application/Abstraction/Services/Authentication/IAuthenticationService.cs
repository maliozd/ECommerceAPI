using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.Authentication
{
    public interface IAuthenticationService : IExternalAuth, IInternalAuth
    {
    }
}
