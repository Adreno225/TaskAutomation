using System;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class CreatorTreeItem
{
    public static ITreeItem GetTreeItem(IItem item)
    {
        return item switch
        {
            TaskClass task => new TaskTreeItem(task),
            Area area => new AreaTreeItem(area),
            ObjectInf obj => new ObjectTreeItem(obj),
            Parameter parameter => new ParameterTreeItem(parameter),
            _ => throw new ArgumentException($"Не поддерживаемый тип айтема дерева:{item.GetType()}!"),
        };
    }
}