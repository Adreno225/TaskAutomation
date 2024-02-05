﻿using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.Services;

public static class Registrator
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) 
    {
        services.AddSingleton<ICreatorTask,ExcelCreator>();
        return services;
    }
}