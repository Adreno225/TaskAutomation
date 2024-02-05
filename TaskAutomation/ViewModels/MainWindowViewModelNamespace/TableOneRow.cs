using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class TableOneRow<T> where T : class, IItem
{
    public T[] Item { get; }
    public TableOneRow(T item) => Item = new T[] { item };
}