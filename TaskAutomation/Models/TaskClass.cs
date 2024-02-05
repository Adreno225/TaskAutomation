using System.Collections.Generic;
using TaskAutomation.Models.Base;

namespace TaskAutomation.Models;

public enum Stage
{
    ОПР,
    ПД,
    РД,
    ПиР
}
public enum Class
{
    Минимальный,
    Базовый,
    Перспективный
}
public class TaskClass: ComplexModel
{
    private const string Text = "Комплексный объект";

    #region Шифр 
    private string _Code = "";
    public string Code
    {
        get => _Code;
        set => Set(ref _Code, value);
    }
    #endregion

    #region Объект 
    private string _Object = "";
    public string Object
    {
        get => _Object;
        set => Set(ref _Object, value);
    }
    #endregion

    #region Стадия 
    private string _Stage = "";
    public string Stage
    {
        get => _Stage;
        set => Set(ref _Stage, value);
    }
    #endregion

    #region Класс 
    private string _Class = "";
    public string Class
    {
        get => _Class;
        set => Set(ref _Class, value);
    }
    #endregion

    #region Заказчик 
    private string _Customer;
    public string Customer
    {
        get => _Customer;
        set => Set(ref _Customer, value);
    }
    #endregion

    #region Тип КО 
    private string _TypeCO;
    public string TypeCO
    {
        get => _TypeCO;
        set => Set(ref _TypeCO, value);
    }
    #endregion

    #region Площадки 
    public IEnumerable<Area> Areas => DefineTypeObjects<Area>(MainItems);
    #endregion

    #region Объекты 
    public IEnumerable<ObjectInf> Objects => DefineTypeObjects<ObjectInf>(MainItems);
    #endregion

    #region Параметры 
    public IEnumerable<Parameter> Parameters => DefineTypeObjects<Parameter>(SubItems);
    #endregion

    public TaskClass():base(Text) { }
}