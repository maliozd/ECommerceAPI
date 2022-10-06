using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.Authentication
{
    public interface IExternalAuth 
    {
        Task<Dtos.Token> GoogleLoginAsync(string idToken,string provider,int tokenLifeTime);
    }
}
