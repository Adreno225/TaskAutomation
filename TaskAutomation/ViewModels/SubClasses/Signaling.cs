using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.SubClasses;

/// <summary>
/// Интерфейс сигнализации
/// </summary>
public interface ISignaling:ICopy<ISignaling>
{
    /// <summary>
    /// режим сигнализации
    /// </summary>
    string Mode { get; set; }
    /// <summary>
    /// Уставка
    /// </summary>
    string SetPoint { get; set; }
    /// <summary>
    /// Тип сигнализации
    /// </summary>
    TypeSignaling Type { get; set; }
}
public enum TypeSignaling
{
    LL,
    L,
    H,
    HH,
    Other
}

/// <summary>
/// Реализация сигнализации
/// </summary>
public partial class Signaling : ObservableObject, ISignaling
{

    #region Режим
    [ObservableProperty]
    private string _mode = "";
    #endregion

    #region Уставка
    [ObservableProperty]
    private string _setPoint = "";
    #endregion

    #region Тип 
    [ObservableProperty]
    private TypeSignaling _type = TypeSignaling.H;
    #endregion

    public Signaling() { }

    [JsonConstructor]
    public Signaling(string mode, string setPoint, TypeSignaling type)
    {
        Mode = mode;
        SetPoint = setPoint;
        Type = type;
    }
    public ISignaling Copy() => new Signaling(Mode,SetPoint,Type);
}