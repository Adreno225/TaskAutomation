using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TaskAutomation.Models;
using TaskAutomation.Services;
using TaskAutomation.ViewModels.TreeItems;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomation.ViewModels;
/// <summary>
/// Интерфейс основных данных проекта
/// </summary>
public interface IMainData: ICopy<IMainData>
{
    /// <summary>
    /// Шифр
    /// </summary>
    string Code { get; set; }
    /// <summary>
    /// Наименование проекта
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Объект проектирования
    /// </summary>
    string Object { get; set; }
    /// <summary>
    /// Стадия проекта
    /// </summary>
    Stage Stage { get; set; }
    /// <summary>
    /// Класс автоматизации
    /// </summary>
    Class Class { get; set; }
    /// <summary>
    /// Заказчик
    /// </summary>
    string Customer { get; set; }
    /// <summary>
    /// Тип комплексного объекта
    /// </summary>
    string TypeCO { get; set; }
    /// <summary>
    /// Комплексный объект
    /// </summary>
    IComplexObjectTreeItem ComplexObject { get; set; }
    /// <summary>
    /// Установка данных по умолчанию
    /// </summary>
    /// <param name="isReset">Сброс данных нажатием кнопки</param>
    void SetDefaultData(bool isReset);
}
/// <summary>
/// Реализация класса основных данных
/// </summary>
public partial class MainData : ObservableObject, IMainData
{
    private const string DefaultNameObject = "Комплексный объект";
    /// <summary>
    /// Сервис создания запросов
    /// </summary>
    private readonly IQueryCreator _queryCreator;
    private readonly IRepository<Class> _repositoryClasses = App.Services.GetRequiredService<IRepository<Class>>();
    private readonly IRepository<Stage> _repositoryStages = App.Services.GetRequiredService<IRepository<Stage>>();


    #region Шифр
    [ObservableProperty]
    private string _code = "";
    #endregion

    #region Наименование проекта
    [ObservableProperty]
    private string _name = "";
    #endregion

    #region Объект
    [ObservableProperty]
    private string _object = DefaultNameObject;
    #endregion

    #region Стадия
    [ObservableProperty]
    private Stage _stage;
    #endregion

    #region Класс
    private Class _class;
    public Class Class
    {
        get => _class;
        set
        {
            SetProperty(ref _class, value);
            _queryCreator?.SetParametersAllObjects();
        }
    }
    #endregion

    #region Заказчик
    [ObservableProperty]
    private string _customer;
    #endregion

    #region Тип КО
    [ObservableProperty]
    private string _typeCO;
    #endregion

    public IComplexObjectTreeItem ComplexObject { get; set; }
    public void SetDefaultData(bool isReset)
    {
        Code = "";
        Name = "";
        Object = DefaultNameObject;
        Class = _repositoryClasses.Get(2);
        Stage = _repositoryStages.Get(5);
        Customer = null;
        TypeCO = null;
        if (isReset)
            ComplexObject = App.Services.GetRequiredService<IComplexObjectTreeItem>();
    }

    public IMainData Copy()
    {
        return new MainData((IComplexObjectTreeItem)ComplexObject.Copy())
        {
            Code = Code,
            Name = Name,
            Object = Object,
            Stage = Stage,
            Class = Class,
            Customer = Customer,
            TypeCO = TypeCO,
        };  
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="complexObject">Комплексный объект</param>
    public MainData(IComplexObjectTreeItem complexObject)
    {
        ComplexObject = complexObject;
        SetDefaultData(false);
    }
}