using System.Linq;
using TaskAutomation.Models;
using TaskAutomationDB.Entities;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class ObjectTreeItem : MainTreeItem
{
    private const string TextParameters = "Перечень параметров:";

    private readonly MainWindowViewModel _MainModel = ViewModelLocator.MainWindowViewModel;
    public ObjectAutomation[] TypesObjects => _MainModel.RepositoryObjectsAutomation.Items.ToArray();

    private ObjectAutomation _SelectedTypeObject;
    public ObjectAutomation SelectedTypeObject
    {
        get => _SelectedTypeObject;
        set
        {
            Set(ref _SelectedTypeObject, value);
            _MainModel.QueryCreator.SetParameters(this);
        }
    }

    public ListGroup<Models.Parameter> Parameters { get; }
    public TableOneRow<ObjectInf> TableObject { get; }

    public ObjectTreeItem(ObjectInf obj) : base(obj)
    {
        Parameters = new (Items, TextParameters, obj.MainItems);
        TableObject = new (obj);
    }
}