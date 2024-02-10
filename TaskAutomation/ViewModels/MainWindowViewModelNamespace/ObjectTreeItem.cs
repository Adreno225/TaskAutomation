using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using TaskAutomation.Models;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class ObjectTreeItem : MainTreeItem
{
    private const string TextParameters = "Перечень параметров:";

    private readonly IRepository<ObjectAutomation> _RepositoryObjects = App.Host.Services.GetRequiredService<IRepository<ObjectAutomation>>();
    private readonly IRepository<ParameterClassFunction> _RepositoryParameterClassFunction = App.Host.Services.GetRequiredService<IRepository<ParameterClassFunction>>();
    private readonly MainWindowViewModel _MainWindowViewModel = App.Host.Services.GetRequiredService<MainWindowViewModel>();
    public ObjectAutomation[] TypesObjects => _RepositoryObjects.Items.ToArray();

    private ObjectAutomation _SelectedTypeObject;
    public ObjectAutomation SelectedTypeObject
    {
        get => _SelectedTypeObject;
        set
        {
            Set(ref _SelectedTypeObject, value);
            SetParameters();
        }
    }

    public ListGroup<Models.Parameter> Parameters { get; }
    public TableOneRow<ObjectInf> TableObject { get; }

    public ObjectTreeItem(ObjectInf obj) : base(obj)
    {
        Parameters = new (Items, TextParameters, obj.MainItems);
        TableObject = new (obj);
    }

    private void SetParameters()
    {
        var result = _RepositoryParameterClassFunction.Items.Where(x => x.Class == _MainWindowViewModel.Task.Class && x.Parameter.ObjectAutomation == SelectedTypeObject).Include(x => x.Parameter).Include(x => x.FunctionParameter);
        var dictionary = new Dictionary<TaskAutomationDB.Entities.Parameter, List<FunctionParameter>>();
        foreach (var item in result)
        {
            if (dictionary.ContainsKey(item.Parameter))
                dictionary[item.Parameter].Add(item.FunctionParameter);
            else
                dictionary.Add(item.Parameter, new List<FunctionParameter>() { item.FunctionParameter });

        }
        var parameters = ((ObjectInf)Object).MainItems;
        parameters.Clear();
        Items.Clear();
        Name = SelectedTypeObject.Name;
        foreach (var item in dictionary)
        {
            var newParameter = new Models.Parameter() { Name = item.Key.Name };
            parameters.Add(newParameter);
            Items.Add(new ParameterTreeItem(newParameter));
        }
    }
}