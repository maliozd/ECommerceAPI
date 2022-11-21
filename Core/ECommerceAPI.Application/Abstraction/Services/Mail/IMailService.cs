using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.Mail
{
    public interface IMailService
    {
         Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
         Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
    }
}
