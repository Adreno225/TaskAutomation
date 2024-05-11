using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels;
/// <summary>
/// Класс Локатора. Отсюда можно получить доступ к любой ViewModel из любого участка кода.
/// Анти-паттерн. Лучше не использовать!
/// </summary>
public class ViewModelLocator
{
    private static MainWindowViewModel _mainWindowViewModel;
    /// <summary>
    /// Главная ViewModel
    /// </summary>
    public static MainWindowViewModel MainWindowViewModel 
        => _mainWindowViewModel ??= App.Host.Services.GetRequiredService<MainWindowViewModel>();
}