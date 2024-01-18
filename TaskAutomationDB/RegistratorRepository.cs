using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomationDB
{
    public static class RegistratorRepository
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Class>, DbRepository<Class>>()
            .AddTransient<IRepository<Stage>, DbRepository<Stage>>()
            .AddTransient<IRepository<Mode>, DbRepository<Mode>>()
            .AddTransient<IRepository<Customer>, DbRepository<Customer>>()
            .AddTransient<IRepository<TypeCO>, DbRepository<TypeCO>>()
            ;
    }
}
