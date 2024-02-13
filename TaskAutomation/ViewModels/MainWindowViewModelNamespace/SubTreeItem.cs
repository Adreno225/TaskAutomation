using TaskAutomation.Models;
using TaskAutomation.Models.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class SubTreeItem : TreeItem
{
    private const string TextParameters = "Перечень параметров:";
    public ListGroup<Parameter> Parameters { get; }

    public override string Name 
    {
        get => base.Name; 
        set => Set(ref _Name, value); 
    }
    public SubTreeItem(ComplexModel complexModel, string text) : base(complexModel)
    {
        Name = text;
        InitializeItems();
        Parameters = new (Items, TextParameters, complexModel.SubItems);
    }

    protected override void InitializeItems()
    {
        if (Object is ComplexModel complexModel)
            AddItems(complexModel.SubItems);
    }
}