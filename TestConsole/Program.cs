using System.Collections.ObjectModel;
using TaskAutomation.ViewModels.SubClasses;

/// <summary>
/// Тестовый проект для отладки кода
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        var ds = new ObservableCollection<string> { "1", "2", "231" };
        Sd(ds);
    }

    private static void Sd(ObservableCollection<string> strings)
    {
        strings[0] = "1213";
    }
}