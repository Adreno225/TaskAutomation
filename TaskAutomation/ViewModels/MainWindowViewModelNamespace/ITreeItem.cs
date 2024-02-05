using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public interface ITreeItem:IItem
{
    BaseModel Object { get; }
    ObservableCollection<ITreeItem> Items { get; }
}