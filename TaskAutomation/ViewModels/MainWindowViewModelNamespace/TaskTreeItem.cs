using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class TaskTreeItem : ComplexTreeItem
{
    private const string NameSubItems = "Параметры КО";
    private const string TextAreasObjects = "Перечень технологических площадок, ОИ и параметров КО:";
    public ListGroup<Area, ObjectInf> Objects { get; }
    public ListGroup<Parameter> Parameters { get; }

    public TaskTreeItem(TaskClass task) : base(task, NameSubItems)
    {
        Objects = new (Items, TextAreasObjects, task.MainItems);
    }
}