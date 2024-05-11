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

    [ObservableProperty]
    private ObjectAutomation _selectedTypeObject;

    [JsonIgnore]
    public List<IParameterTreeItem> Parameters => DefineTypeObjects<IParameterTreeItem, ITreeItem>(ListGroup.Items);

    #region Подобъекты
    [ObservableProperty]
    private ObservableCollection<string> _subobjects = new();
    #endregion

    #region Позиция по ГП
    [ObservableProperty]
    private string _position = "";
    #endregion

    #region Параметры оборудования 
    [ObservableProperty]
    private string _parametersEquipment = "";
    #endregion

    #region Продукт
    public IProduct Product { get; }
    #endregion
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    /// <param name="product">Продукт</param>
    public ObjectTreeItem(IListGroup<IParameterTreeItem> listGroup, IProduct product):base(DefaultName, listGroup)
    {
        Product = product;
    }

    public override ITreeItem Copy() =>
        new ObjectTreeItem((IListGroup<IParameterTreeItem>)ListGroup.Copy(), Product.Copy()) 
        { 
            Name = Name,
            ListGroup = ListGroup.Copy(),
            ParametersEquipment = ParametersEquipment,
            Position = Position,
            SelectedTypeObject = SelectedTypeObject,
        };
}