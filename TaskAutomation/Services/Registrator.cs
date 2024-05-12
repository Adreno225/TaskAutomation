using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.Services;

public static class Registrator
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services) 
    {
        services.AddSingleton<ICreatorTask,ExcelCreator>();
        services.AddSingleton<IQueryCreator, QueryCreator>();
        services.AddSingleton<ISerializer, Serializer>();
        services.AddTransient<IDialogService, DialogWindows>();
        return services;
    }
}