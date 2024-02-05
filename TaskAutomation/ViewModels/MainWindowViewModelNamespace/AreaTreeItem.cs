using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class AreaTreeItem : ComplexTreeItem
{
    private const string NameSubItems = "Параметры площадки";
    private const string TextObjects = "Перечень ОИ:";
    public ListGroup<ObjectInf> Objects { get; }
    public ListGroup<Parameter> Parameters { get; }
    public AreaTreeItem(Area area) : base(area, NameSubItems)
    {
        Objects = new (Items, TextObjects, area.MainItems);
    }
}