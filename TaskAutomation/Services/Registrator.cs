using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAutomation.Services
{
    public static class Registrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) 
        {
            services.AddSingleton<ExcelCreator>();
            return services;
        }
    }
}
