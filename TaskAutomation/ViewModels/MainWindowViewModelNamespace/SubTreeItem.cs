using TaskAutomation.Models;
using TaskAutomation.Models.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class SubTreeItem : TreeItem
{
    private const string TextParameters = "Перечень параметров:";
    public ListGroup<Parameter> Parameters { get; }
    public SubTreeItem(ComplexModel complexModel, string name) : base(complexModel)
    {
        Name = name;
        InitializeItems();
        Parameters = new (Items, TextParameters, complexModel.SubItems);
    }

    protected override void InitializeItems()
    {
        if (Object is ComplexModel complexModel)
            AddItems(complexModel.SubItems);
    }
}