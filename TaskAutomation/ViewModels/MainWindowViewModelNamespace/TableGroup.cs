using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class TableGroup<T>:ViewModel<T> where T : class, IItem, new()
{
    public ObservableCollection<IItem> Items { get; }
    #region Подпись в списке
    public string Text { get; }
    #endregion

    #region Выбранный айтем
    private IItem _SelectedItem;
    public IItem SelectedItem
    {
        get => _SelectedItem;
        set => Set(ref _SelectedItem, value);
    }
    #endregion

    #region Добавление айтема
    public ICommand AddItemCommand { get; }

    protected bool CanAddItemCommandExecute(object p) => true;

    protected virtual void OnAddItemCommandExecuted(object p)
    {
        var newItem = new T();
        AddNewItem(newItem);
    }

    protected virtual void AddNewItem(IItem newItem)
    {
        Items.Add(newItem);
    }
    #endregion

    #region Удаление выбранного айтема
    public ICommand RemoveSelectedItemCommand { get; }
    protected virtual bool CanRemoveSelectedItemCommandExecute(object p) => IsSelectedCanCommandExecute();

    protected virtual void OnRemoveSelectedItemCommandExecuted(object p)
    {
        Items.Remove(SelectedItem);
    }
    #endregion

    #region Копирование выбранного айтема
    public ICommand CopySelectedItemCommand { get; }

    protected virtual bool CanCopySelectedItemCommandExecute(object p) => IsSelectedCanCommandExecute();

    protected virtual void OnCopySelectedItemCommandExecuted(object p)
    {
        //Items.Add(SelectedItem);
    }
    #endregion

    protected virtual bool IsSelectedCanCommandExecute() => SelectedItem != null;
    public TableGroup(string text, ObservableCollection<IItem> items)
    {
        Text = text;
        Items = items;
        #region Команды
        AddItemCommand = new LambdaCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
        RemoveSelectedItemCommand = new LambdaCommand(OnRemoveSelectedItemCommandExecuted, CanRemoveSelectedItemCommandExecute);
        CopySelectedItemCommand = new LambdaCommand(OnCopySelectedItemCommandExecuted, CanCopySelectedItemCommandExecute);
        #endregion
    }
}