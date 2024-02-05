using TaskAutomation.Models;
using TaskAutomation.Models.Base;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public abstract class MainTreeItem : TreeItem
{
    protected MainTreeItem(BaseModel obj) : base(obj)
    {
        Name = obj.Name;
        InitializeItems();
    }

    protected override void InitializeItems()
    {
        if (Object is SimpleModel simpleModel)
            AddItems(simpleModel.MainItems);
    }
}