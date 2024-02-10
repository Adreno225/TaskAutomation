using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
}