using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.SubClasses;

/// <summary>
/// Интерфейс алгоритма
/// </summary>
public interface IAlgorithm: ICopy<IAlgorithm>
{
    /// <summary>
    /// Уставка
    /// </summary>
    string SetPoint { get; set; }
    /// <summary>
    /// Действие
    /// </summary>
    string Action { get; set; }
}

/// <summary>
/// Реализация алгоритма
/// </summary>
public partial class Algorithm: ObservableObject, IAlgorithm
{
    private const string DefaultSetPoint = "";
    private const string DefaultAction = "";
    #region Уставка
    [ObservableProperty]
    private string _setPoint;
    #endregion

    #region Действия
    [ObservableProperty]
    private string _action;

    public Algorithm():this(DefaultAction,DefaultSetPoint) { }

    [JsonConstructor]
    public Algorithm(string action, string setPoint)
    {
        Action = action;
        SetPoint = setPoint;
    }

    public IAlgorithm Copy() => new Algorithm(Action,SetPoint);
    #endregion

}