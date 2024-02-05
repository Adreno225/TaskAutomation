using TaskAutomation.Models.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public abstract class ComplexTreeItem : MainTreeItem
{
    protected ComplexTreeItem(ComplexModel complexModel, string text) : base(complexModel)
    {
        _Items.Insert(0, new SubTreeItem(complexModel, text));
    }
}