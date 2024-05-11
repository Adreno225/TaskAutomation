using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.SubClasses;

/// <summary>
/// Интерфейс измерения
/// </summary>
public interface IMeasure: ICopy<IMeasure>
{
    /// <summary>
    /// Измеримость
    /// </summary>
    public bool IsMeasurable { get; set; }
    /// <summary>
    /// Диапазон измерения
    /// </summary>
    public string Range { get; set; }
}

/// <summary>
/// Реализация измерения
/// </summary>
public partial class Measure : ObservableObject, IMeasure
{
    [ObservableProperty]
    private bool _isMeasurable = false;
    [ObservableProperty]
    private string _range = "";
    public Measure() { }

    [JsonConstructor]
    public Measure(bool isMeasurable, string range)
    {
        IsMeasurable = isMeasurable;
        Range = range;
    }

    public IMeasure Copy() => new Measure(IsMeasurable, Range);
}