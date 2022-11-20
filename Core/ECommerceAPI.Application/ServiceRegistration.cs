using ECommerceAPI.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));  //serviceRegistrationun bulunduğu assemblydeki bütün ırequest vs sınıflarını bul ve ekle
            collection.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateUserValidator>(ServiceLifetime.Scoped);
           
        }
    }
}
