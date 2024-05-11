using Microsoft.Extensions.DependencyInjection;
using TaskAutomation.ViewModels.Lists;
using TaskAutomation.ViewModels.SubClasses;
using TaskAutomation.ViewModels.TreeItems;

namespace TaskAutomation.ViewModels;

public static class Registrator
{
    /// <summary>
    /// Регистрация ViewModels
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.
            AddSingleton<MainWindowViewModel>().
            AddSingleton<IMainData, MainData>().
            AddTransient<IProjectRepositories, ProjectRepositories>().
            RegisterTreeItems().
            RegisterLists().
            RegisterSubClasses()
            ;
        return services;
    }
}