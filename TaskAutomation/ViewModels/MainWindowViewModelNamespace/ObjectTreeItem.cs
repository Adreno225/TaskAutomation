using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class ObjectTreeItem : MainTreeItem
{
    private const string TextParameters = "Перечень параметров:";
    public ListGroup<Parameter> Parameters { get; }
    public TableOneRow<ObjectInf> TableObject { get; }

    public ObjectTreeItem(ObjectInf obj) : base(obj)
    {
        Parameters = new (Items, TextParameters, obj.MainItems);
        TableObject = new (obj);
    }
}