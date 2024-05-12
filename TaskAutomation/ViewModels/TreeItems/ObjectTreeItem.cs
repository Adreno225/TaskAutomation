using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskAutomation.ViewModels.Lists;
using TaskAutomationDB.Entities;
using TaskAutomation.ViewModels.SubClasses;

namespace TaskAutomation.ViewModels.TreeItems;
/// <summary>
/// Интерфейс сооружения
/// </summary>
public interface IObjectTreeItem : ITreeItem
{
    /// <summary>
    /// Выбранный тип сооружения
    /// </summary>
    ObjectAutomation SelectedTypeObject { get; set; }
    /// <summary>
    /// Позиция по ГП
    /// </summary>
    string Position { get; set; }
    /// <summary>
    /// Параметры оборудования
    /// </summary>
    string ParametersEquipment { get; set; }
    /// <summary>
    /// Параметры сооружения
    /// </summary>
    List<IParameterTreeItem> Parameters { get; }
    /// <summary>
    /// Продукт
    /// </summary>
    IProduct Product { get; }
}
/// <summary>
/// Реализация сооружения
/// </summary>
public partial class ObjectTreeItem : TreeItem, IObjectTreeItem
{
    private const string DefaultName = "Сооружение";
    private const string DefaultPosition = "";
    private const string DefaultParametersEquipment = "";

    private ObjectAutomation _selectedTypeObject;

    public ObjectAutomation SelectedTypeObject
    {
        get => _selectedTypeObject;
        set
        {
            SetProperty(ref _selectedTypeObject, value);
            ViewModelLocator.MainWindowViewModel.QueryCreator.SetParameters(this);
        }
    }

    [JsonIgnore]
    public List<IParameterTreeItem> Parameters => DefineTypeObjects<IParameterTreeItem, ITreeItem>(ListGroup.Items);

    #region Подобъекты
    [ObservableProperty]
    private ObservableCollection<string> _subobjects;
    #endregion

    #region Позиция по ГП
    [ObservableProperty]
    private string _position;
    #endregion

    #region Параметры оборудования 
    [ObservableProperty]
    private string _parametersEquipment;
    #endregion

    #region Продукт
    public IProduct Product { get; }
    #endregion
    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    /// <param name="product">Продукт</param>
    public ObjectTreeItem(IListGroup<IParameterTreeItem> listGroup, IProduct product):
        this(DefaultName,listGroup,product,null,new(),DefaultPosition,DefaultParametersEquipment) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование элемента дерева</param>
    /// <param name="listGroup">Список площадок/объектов</param>
    /// <param name="product">Продукт</param>
    /// <param name="selectedTypeObject">Выбранный тип сооружения</param>
    /// <param name="subobjects">Подобъекты</param>
    /// <param name="position">Позиция по ГП</param>
    /// <param name="parametersEquipment">Параметры оборудования</param>
    [JsonConstructor]
    public ObjectTreeItem(string name, IListGroup listGroup, IProduct product, ObjectAutomation selectedTypeObject,
        ObservableCollection<string> subobjects, string position, string parametersEquipment):base(name,listGroup)
    {
        Product = product;
        SelectedTypeObject = selectedTypeObject;
        Subobjects = subobjects;
        Position = position;
        ParametersEquipment = parametersEquipment;
    }

    public override ITreeItem Copy() =>
        new ObjectTreeItem(Name,ListGroup.Copy(),Product.Copy(),SelectedTypeObject,Subobjects,Position,ParametersEquipment);
}