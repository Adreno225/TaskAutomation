using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using TaskAutomation.Data;
using TaskAutomation.Services;
using TaskAutomation.ViewModels;

namespace TaskAutomation;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost __Host;

    /// <summary>
    /// Хост приложения
    /// </summary>
    public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    /// <summary>
    /// Сервисы приложения
    /// </summary>
    public static IServiceProvider Services => Host.Services;

    /// <summary>
    /// Метод при запуске приложения
    /// </summary>
    /// <param name="e">Аргументы события запуска</param>
    protected override async void OnStartup(StartupEventArgs e)
    {
        var host= Host;
        using (var scope = Services.CreateScope())
            scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();
        base.OnStartup(e);
        await host.StartAsync().ConfigureAwait(false);
    }
    /// <summary>
    /// Метод при выходе из приложения
    /// </summary>
    /// <param name="e">Аргументы события выхода</param>
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        var host = Host;
        await host.StopAsync().ConfigureAwait(false);
        host.Dispose();
        __Host = null;
    }
    /// <summary>
    /// Метод конфигурирования сервисов
    /// </summary>
    /// <param name="host">Хост</param>
    /// <param name="services">Коллекция сервисов</param>
    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
        .AddDatabase(host.Configuration.GetSection("Database"))
        .RegisterServices()
        .RegisterViewModels()
    ;
}