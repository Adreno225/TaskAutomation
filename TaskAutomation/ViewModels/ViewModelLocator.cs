using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels;

internal class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
}