using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskAutomation.Models.Base;

namespace TaskAutomation.Models;

public class ObjectInf:SimpleModel
{
    private const string Text = "Сооружение";

    #region Подобъекты 
    private ObservableCollection<string> _Subobjects = new();
    public ObservableCollection<string> Subobjects
    {
        get => _Subobjects;
        set => Set(ref _Subobjects, value);
    }
    #endregion

    #region Позиция по ГП 
    private string _Position = "";
    public string Position
    {
        get => _Position;
        set => Set(ref _Position, value);
    }
    #endregion

    #region Параметры оборудования 
    private string _ParametersEquipment ="";
    public string ParametersEquipment
    {
        get => _ParametersEquipment;
        set => Set(ref _ParametersEquipment, value);
    }
    #endregion

    #region Продукт 
    private Product _Product = new();
    public Product Product
    {
        get => _Product;
        set => Set(ref _Product, value);
    }
    #endregion
      
    #region Параметры 
    public IEnumerable<Parameter> Parameters => DefineTypeObjects<Parameter>(MainItems);
    #endregion

    public ObjectInf():base(Text) { }
}