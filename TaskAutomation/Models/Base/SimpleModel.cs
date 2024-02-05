using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskAutomation.Models.Base;

public abstract class SimpleModel: BaseModel
{
    #region Основные элементы коллекции 
    private ObservableCollection<IItem> _MainItems = new();

    public ObservableCollection<IItem> MainItems
    {
        get => _MainItems;
        set => Set(ref _MainItems, value);
    }
    #endregion
    protected SimpleModel(string name) : base(name) { }

    protected static IEnumerable<T> DefineTypeObjects<T>(IEnumerable collection) where T : BaseModel
    {
        var result = new List<T>();
        foreach (var item in collection)
            if (item is T t)
                result.Add(t);
        return result;
    }
}

public abstract class SimpleModel2 : SimpleModel
{
    #region Основные элементы коллекции 
    private ObservableCollection<IItem> _MainItems2 = new();

    public ObservableCollection<IItem> MainItems2
    {
        get => _MainItems2;
        set => Set(ref _MainItems2, value);
    }
    #endregion
    protected SimpleModel2(string name) : base(name) { }

}

public abstract class ComplexModel:SimpleModel 
{
    #region Необязательные вложенные элементы 
    private ObservableCollection<IItem> _SubItems = new();

    public ObservableCollection<IItem> SubItems
    {
        get => _SubItems;
        set => Set(ref _SubItems, value);
    }

    #endregion
    protected ComplexModel(string name) : base(name) { }
}