using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class UserLoginFailedException :Exception
    {
        public UserLoginFailedException()  : base("Wrong username or password!")
        {

        }
        public UserLoginFailedException(string message) : base(message)
        {

        }
    }
}
