using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCoreTwo.Models;

namespace MyCoreTwo
{
    public static class BindingExtensions
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuratio)
        {

            services.AddSingleton<IContactRepository, ContactRepository>();



        }
    }
}
