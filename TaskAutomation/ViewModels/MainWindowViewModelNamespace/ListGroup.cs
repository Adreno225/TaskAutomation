using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class ListGroup<T> : TableGroup<T> where T : class, IItem, new()
{
    public ObservableCollection<ITreeItem> OutputTreeItems { get; }

    protected override void AddNewItem(IItem newItem)
    {
        base.AddNewItem(newItem);
        OutputTreeItems.Add(CreatorTreeItem.GetTreeItem(newItem));
    }

    protected override void OnRemoveSelectedItemCommandExecuted(object p)
    {
        Items.Remove(((ITreeItem)SelectedItem).Object);
        OutputTreeItems.Remove((ITreeItem)SelectedItem);
    }

    protected override bool IsSelectedCanCommandExecute() => base.IsSelectedCanCommandExecute() && SelectedItem is not SubTreeItem;

    public ListGroup(ObservableCollection<ITreeItem> treeItems, string text, ObservableCollection<IItem> items):base(text,items)
    {
        OutputTreeItems = treeItems;
    }
}

public class ListGroup<T, Y> : ListGroup<T> where T : class, IItem,new() where Y : class, IItem, new()
{
    #region Добавить айтем (тип 2)
    public ICommand AddItemCommand2 { get; }

    private void OnAddItemCommandExecuted2(object obj)
    {
        var newItem = new Y();
        AddNewItem(newItem);
    } 
    #endregion

    public ListGroup(ObservableCollection<ITreeItem> treeItems,
        string text, ObservableCollection<IItem> items) : base(treeItems, text, items)
    {
        #region Команды
        AddItemCommand2 = new LambdaCommand(OnAddItemCommandExecuted2, CanAddItemCommandExecute);
        #endregion
    }
}