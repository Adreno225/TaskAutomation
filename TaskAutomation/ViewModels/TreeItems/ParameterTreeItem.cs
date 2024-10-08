﻿using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using TaskAutomation.ViewModels.Lists;
using TaskAutomation.ViewModels.SubClasses;


namespace TaskAutomation.ViewModels.TreeItems;
/// <summary>
/// Интерфейс параметра
/// </summary>
public interface IParameterTreeItem: ITreeItem
{
    /// <summary>
    /// Таблица сигнализаций
    /// </summary>
    ITableGroup<ISignaling> ListSignalings { get; }
    /// <summary>
    /// Таблица алгоритмов
    /// </summary>
    ITableGroup<IAlgorithm> ListAlgorithms { get; }
    /// <summary>
    /// Сигнализации
    /// </summary>
    List<ISignaling> Signalings { get; }
    /// <summary>
    /// Алгоритмы
    /// </summary>
    List<IAlgorithm> Algorithms { get; }
    /// <summary>
    /// Требуется ли включать параметр в задание
    /// </summary>
    bool IsControl {  get; set; }
    /// <summary>
    /// Ед. изм.
    /// </summary>
    string Unit { get; set; }
    /// <summary>
    /// Относится ли параметр к системе ПАЗ
    /// </summary>
    bool ESD { get; set; }
    /// <summary>
    /// Местное измерение
    /// </summary>
    IMeasure ManualMeasure { get; set; }
    /// <summary>
    /// Дистанционное измерение
    /// </summary>
    IMeasure RemoteMeasure { get; set; }
    /// <summary>
    /// Расчетное значение
    /// </summary>
    string CalculatedValue { get; set; }
    /// <summary>
    /// Диапазон управления/регулирования
    /// </summary>
    string RangeControl { get; set; }
    /// <summary>
    /// Примечание
    /// </summary>
    string Note { get; set; }
}
/// <summary>
/// Реализация параметра
/// </summary>
public partial class ParameterTreeItem : TreeItem, IParameterTreeItem
{
    private const string DefaultName = "Параметр";
    private const string DefaultUnit = "";
    private const string DefualtCalculatedValue = "";
    private const string DefaultRangeControl = "";
    private const string DefaultNote = "";
    private const bool DefaultIsControl = false;
    private const bool DefaultESD = false;

    public ITableGroup<ISignaling> ListSignalings { get; }
    public ITableGroup<IAlgorithm> ListAlgorithms { get; }
    [JsonIgnore]
    public List<ISignaling> Signalings => DefineTypeObjects<ISignaling,ISignaling>(ListSignalings.Items);
    [JsonIgnore]
    public List<IAlgorithm> Algorithms => DefineTypeObjects<IAlgorithm,IAlgorithm>(ListAlgorithms.Items);

    #region Измеримость
    [ObservableProperty]
    private bool _isControl;
    #endregion

    #region Ед.изм.
    [ObservableProperty]
    private string _unit;
    #endregion

    #region ПАЗ
    [ObservableProperty]
    private bool _ESD;
    #endregion

    #region Местное изменение 
    public IMeasure ManualMeasure { get; set; }
    #endregion

    #region Дистанционное изменение 
    public IMeasure RemoteMeasure { get; set; }
    #endregion

    #region Расчетное значение
    [ObservableProperty]
    private string _calculatedValue;
    #endregion

    #region Диапазон управления/регулирования
    [ObservableProperty]
    private string _rangeControl;
    #endregion

    #region Примечание
    [ObservableProperty]
    private string _note;
    #endregion
    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="manualMeasure">Местное измерение</param>
    /// <param name="remoteMeasure">Дистанционное измерение</param>
    /// <param name="listSignalings">Таблица сигнализаций</param>
    /// <param name="listAlgorithms">Таблица алгоритмов</param>
    public ParameterTreeItem(IMeasure manualMeasure, IMeasure remoteMeasure,
        ITableGroup<ISignaling> listSignalings, ITableGroup<IAlgorithm> listAlgorithms) 
        :this(DefaultName, manualMeasure,remoteMeasure,listSignalings,listAlgorithms,DefaultIsControl,
             DefaultUnit,DefaultESD,DefualtCalculatedValue,DefaultRangeControl,DefaultNote) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование элемента дерева</param>
    /// <param name="manualMeasure">Местное измерение</param>
    /// <param name="remoteMeasure">Дистанционное измерение</param>
    /// <param name="listSignalings">Таблица сигнализаций</param>
    /// <param name="listAlgorithms">Таблица алгоритмов</param>
    /// <param name="isControl">Требуется ли включать параметр в задание</param>
    /// <param name="unit">Ед. изм.</param>
    /// <param name="eSD">Относится ли параметр к системе ПАЗ</param>
    /// <param name="calculatedValue">Расчетное значение</param>
    /// <param name="rangeControl">Диапазон управления/регулирования</param>
    /// <param name="note">Примечание</param>
    [JsonConstructor]
    public ParameterTreeItem(string name, IMeasure manualMeasure, IMeasure remoteMeasure,
        ITableGroup<ISignaling> listSignalings, ITableGroup<IAlgorithm> listAlgorithms,
        bool isControl, string unit, bool eSD, string calculatedValue, 
        string rangeControl, string note): base(name, null)
    {
        ManualMeasure = manualMeasure;
        RemoteMeasure = remoteMeasure;
        ListSignalings= listSignalings;
        ListAlgorithms = listAlgorithms;
        IsControl = isControl;
        Unit = unit;
        ESD = eSD;
        CalculatedValue = calculatedValue;
        RangeControl = rangeControl;
        Note = note;
    }

    public override ITreeItem Copy() =>
        new ParameterTreeItem(Name, ManualMeasure.Copy(), RemoteMeasure.Copy(), ListSignalings.Copy(), 
            ListAlgorithms.Copy(), IsControl, Unit, ESD, CalculatedValue, RangeControl, Note);
}