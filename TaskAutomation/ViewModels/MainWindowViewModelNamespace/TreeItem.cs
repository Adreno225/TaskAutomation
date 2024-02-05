using System.Collections.ObjectModel;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public abstract class TreeItem : ViewModel<object>, ITreeItem
{
    private string _Name;
    public virtual string Name
    {
        get => _Name;
        set 
        {
            Set(ref _Name, value);
            Object.Name = value;
        } 
    }
    #region Объект 
    public BaseModel Object { get; }
    #endregion

    #region Вложенные объекты 
    protected ObservableCollection<ITreeItem> _Items = new();
    public ObservableCollection<ITreeItem> Items => _Items;
    #endregion

    protected TreeItem(BaseModel obj)
    {
        Object = obj;
    }

    protected abstract void InitializeItems();

    protected void AddItems(ObservableCollection<IItem> items)
    {
        foreach (var item in items)
            _Items.Add(CreatorTreeItem.GetTreeItem(item));
    }
}