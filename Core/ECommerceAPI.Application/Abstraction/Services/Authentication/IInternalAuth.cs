using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.Authentication
{
    public interface IInternalAuth 
    {
        Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password,int tokenLifeTime);
    }
}
