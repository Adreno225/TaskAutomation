using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels;

public class ViewModelLocator
{
    public static MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
}