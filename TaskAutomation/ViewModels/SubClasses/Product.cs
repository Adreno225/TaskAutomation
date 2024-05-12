using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.SubClasses;

/// <summary>
/// Интерфейс продукта
/// </summary>
public interface IProduct: ICopy<IProduct>
{
    /// <summary>
    /// Наименование продукта
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Характеристики продукта
    /// </summary>
    string Parameters { get; set; }
}

/// <summary>
/// Реализация продукта
/// </summary>
public partial class Product:ObservableObject, IProduct
{
    private const string DefaultNameProduct = "Продукт";
    private const string DefaultParameters = "";

    [ObservableProperty]
    private string _name;

    #region Параметры среды 
    [ObservableProperty]
    private string _parameters;
    #endregion

    public Product():this(DefaultNameProduct,DefaultParameters) { }

    [JsonConstructor]
    public Product(string name, string parameters)
    {
        Name = name;
        Parameters = parameters;
    }
    public IProduct Copy() => new Product(Name,Parameters);
}