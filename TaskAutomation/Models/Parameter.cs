using System.Collections.Generic;
using TaskAutomation.Models.Base;
using TaskAutomation.ViewModels.MainWindowViewModelNamespace;

namespace TaskAutomation.Models;

public class Parameter:SimpleModel2
{
    private const string Text = "Параметр";

    #region Измеримость 
    private bool _IsControl = true;
    public bool IsControl
    {
        get => _IsControl;
        set => Set(ref _IsControl, value);
    }
    #endregion

    #region Ед.изм. 
    private string _Unit ="";
    public string Unit
    {
        get => _Unit;
        set => Set(ref _Unit, value);
    }
    #endregion

    #region ПАЗ 
    private bool _ESD = false;
    public bool ESD
    {
        get => _ESD;
        set => Set(ref _ESD, value);
    }
    #endregion

    #region Местное изменение 
    private bool _IsManualMeasure;
    public bool IsManualMeasure
    {
        get => _IsManualMeasure;
        set => Set(ref _IsManualMeasure, value);
    }
    #endregion

    #region Местное изменение 
    private bool _IsRemoteMeasure;
    public bool IsRemoteMeasure
    {
        get => _IsRemoteMeasure;
        set => Set(ref _IsRemoteMeasure, value);
    }
    #endregion


    #region Диапазон измерения 
    private string _RangeMeasure = "";
    public string RangeMeasure
    {
        get => _RangeMeasure;
        set => Set(ref _RangeMeasure, value);
    }
    #endregion

    #region Расчетное значение 
    private string _CalculatedValue = "";
    public string CalculatedValue
    {
        get => _CalculatedValue;
        set => Set(ref _CalculatedValue, value);
    }
    #endregion

    #region Диапазон управления/регулирования 
    private string _RangeControl = "";
    public string RangeControl
    {
        get => _RangeControl;
        set => Set(ref _RangeControl, value);
    }
    #endregion

    #region Сигнализации 
    public IEnumerable<Signaling> Signalings => DefineTypeObjects<Signaling>(MainItems);
    #endregion

    #region Алгоритмы 
    public IEnumerable<Algorithm> Algorithms => DefineTypeObjects<Algorithm>(MainItems2);
    #endregion

    #region Примечание 
    private string _Note ="";
    public string Note
    {
        get => _Note;
        set => Set(ref _Note, value);
    }
    #endregion

    public Parameter(): base(Text) { }

}