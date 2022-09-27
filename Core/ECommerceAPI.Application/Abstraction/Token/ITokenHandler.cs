using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateJwtToken(int expirationMinute);
    }
}
