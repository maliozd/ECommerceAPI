using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.Mail
{
    public interface IMailService
    {
        public Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        public Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
    }
}
