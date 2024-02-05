using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskAutomationDB;
using TaskAutomationDB.Context;

namespace TaskAutomation.Data;

public static class DbRegistrator
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskAutomationContext>(opt =>
        {
            var type = configuration["Type"];
            switch (type)
            {
                case null: throw new InvalidOperationException("Не определен тип БД");
                default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                case "MSSQL":
                    opt.UseSqlServer(configuration.GetConnectionString(type));
                    break;
            }
        });
        services.AddTransient<DbInitializer>();
        services.AddRepositoriesInDB();
        return services;
    }
}