using ExcelLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using TaskAutomation.Infrastructure.DialogWindows;
using TaskAutomation.ViewModels;
using TaskAutomation.ViewModels.SubClasses;
using TaskAutomation.ViewModels.TreeItems;

namespace TaskAutomation.Services;
/// <summary>
/// Интерфейс сервиса по созданию задания
/// </summary>
public interface ICreatorTask
{
    /// <summary>
    /// Создание задания
    /// </summary>
    void Create();
}
/// <summary>
/// Реализация сервиса создания заданий
/// </summary>
public class ExcelCreator : ICreatorTask
{
    const int StartRow = 9;
    const int FirstColumnObj = 2;
    const int FirstColumnParam = 8;
    const int StartColumn = 16;
    const int HeaderRow = 6;
    const int Width1ColumnSign = 16;
    const int Width2ColumnSign = 11;
    const int Width1ColumnAlg = 11;
    const int Width2ColumnAlg = 30;
    const string MainText1Sign = "Предупредительная сигнализация";
    const string MainText2Sign = "Аварийная сигнализация";
    const string MainText3Sign = "Другие сигнализации";
    const string ModeSign = "Режим сигнализации";
    const string LLSign = "Мин. (LL)";
    const string LSign = "Мин. (L)";
    const string HSign = "Макс. (H)";
    const string HHSign = "Макс. (HH)";
    const string SetPoint = "Уставка";
    const string MainTextAlg = "Алгоритмы";
    const string Actions = "Действия";
    const string Automation = "Автоматизация";
    const string FiltersDialogWindow = "Excel Worksheets|*.xlsx";

    private readonly string PathExcelTemplate = Environment.CurrentDirectory + "\\Templates\\TemplateExcel.xlsx";
    private static readonly System.Drawing.Color _Color = System.Drawing.Color.LightGreen;
    private static readonly Dictionary<object, int> _ColumnsSignalingsAlgs = new();
    private static int _EndColumn;
    private readonly IMainData _mainData;
    private IComplexObjectTreeItem TaskTreeItem => _mainData.ComplexObject;

    public ExcelCreator(IMainData mainData)
    {
        _mainData = mainData;
    }

    public void Create()
    {
        var dialog = new SaveDialog();
        dialog.Save(MainMethod, filter: FiltersDialogWindow);
    }

    private void MainMethod(string pathSave)
    {
        using var excel = new Package(PathExcelTemplate, pathSave);
        _ColumnsSignalingsAlgs.Clear();
        var workSheet = excel.SelectSheet(1);
        CreateHeaderRow(TaskTreeItem, workSheet);
        WriteMainData(workSheet);
        var numRow = StartRow;
        var Parameters = TaskTreeItem.Parameters.Where(x => x.IsControl);
        if (Parameters.Any())
        {
            workSheet.GetCell(numRow, 1).SetValue("Общие параметры").SetLeftHorAling().SetTopVAlign();
            var countRows1 = 0;
            var countRows2 = 0;
            foreach (var parameter in Parameters)
                WriteMainDataParameter(workSheet, ref numRow, parameter, ref countRows1, ref countRows2);
            if (countRows1 > 0)
            {
                MergeCellsObject(workSheet, StartRow, countRows1);
                WriteEmptyData(workSheet, StartRow);
            }
            MergeCells(workSheet, countRows1, StartRow, 1);
        }
        var mainItems = TaskTreeItem.ListGroup.Items
            .Where(x => (x is ObjectTreeItem objectInf && IsObjectContainsControledParameters(objectInf)) ||
            (x is AreaTreeItem area && IsAreaContainsControledParameters(area)));
        foreach (var mainItem in mainItems)
        {
            if (mainItem is AreaTreeItem area)
                WriteDataArea(workSheet, ref numRow, area);
            if (mainItem is ObjectTreeItem objectInf)
            {
                var startRow = numRow;
                var countRows1 = 0;
                var countRows2 = 0;
                workSheet.GetCell(startRow, 1).SetValue("-").SetLeftHorAling().SetTopVAlign();
                WriteDataObject(workSheet, ref numRow, ref countRows1, ref countRows2, objectInf);
                if (countRows1 > 0)
                {
                    MergeCells(workSheet, countRows1, startRow, 1);
                }
            }
        }
        if (excel.Save())
            if (MessageBox.Show("Файл задания успешно создан! Откыть файл с заданием?", "Задание создано", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Process.Start(new ProcessStartInfo { FileName = pathSave, UseShellExecute = true });
    }

    private static bool IsAreaContainsControledParameters(AreaTreeItem area) =>
        IsContainsControledParameters(area.Parameters) || area.Objects.Where(IsObjectContainsControledParameters).Any();

    private static bool IsObjectContainsControledParameters(IObjectTreeItem objectInf) => IsContainsControledParameters(objectInf.Parameters);

    private static bool IsContainsControledParameters(IEnumerable<IParameterTreeItem> parameters) => parameters.Where(y => y.IsControl).Any();

    private static void WriteDataArea(Sheet workSheet, ref int numRow, AreaTreeItem area)
    {
        var firstRowArea = numRow;
        var countRowsArea = 0;
        var countRowsObj = 0;
        workSheet.GetCell(numRow, 1).SetValue(area.Name).SetLeftHorAling();
        var areaParams = area.Parameters;
        foreach (var parameter in areaParams)
            WriteMainDataParameter(workSheet, ref numRow, parameter, ref countRowsObj, ref countRowsArea);
        if (countRowsObj > 0)
        {
            MergeCellsObject(workSheet, firstRowArea, countRowsArea);
            WriteEmptyData(workSheet, firstRowArea);
        }
        var objects = area.Objects;
        foreach (var obj in objects)
            WriteDataObject(workSheet, ref numRow, ref countRowsArea, ref countRowsObj, obj);
        MergeCells(workSheet, countRowsArea, firstRowArea, 1);
    }

    private static void WriteDataObject(Sheet workSheet, ref int numRow, ref int countRowsArea, ref int countRowsObj, IObjectTreeItem obj)
    {
        var firstRowObj = numRow;
        countRowsObj = 0;
        WriteMainDataObject(workSheet, numRow, obj);
        var parameters = obj.Parameters;
        foreach (var par in parameters)
            WriteMainDataParameter(workSheet, ref numRow, par, ref countRowsObj, ref countRowsArea);
        if (countRowsObj > 0)
            MergeCellsObject(workSheet, firstRowObj, countRowsObj);
    }

    private static void CreateHeaderRow(IComplexObjectTreeItem task, Sheet sheet)
    {
        var par1 = task.AreasObjects.SelectMany(x => (x as AreaTreeItem)?.Parameters)?.Where(x => x.IsControl);
        var par2 = task.AreasObjects.SelectMany(x => (x as ObjectTreeItem)?.Parameters).Where(x => x.IsControl);
        var parameters = par1.Concat(par2);
        var signalings = parameters.SelectMany(x => x.Signalings);
        var columnHeader = StartColumn;
        if (signalings.Where(x => x.Type == TypeSignaling.L).Any() || signalings.Where(x => x.Type == TypeSignaling.H).Any())
            SetWarnSignaling(sheet, signalings, ref columnHeader);
        if (signalings.Where(x => x.Type == TypeSignaling.LL).Any() || signalings.Where(x => x.Type == TypeSignaling.HH).Any())
            SetAlarms(sheet, signalings, ref columnHeader);
        var temp = parameters.Select(x => x.Signalings).Select(x => x.Where(y => y.Type == TypeSignaling.Other).Count());
        var countOtherSign = (temp == null || !temp.Any()) ? 0 : temp.Max();
        if (countOtherSign != 0)
            SetOtherSignalings(sheet, ref columnHeader, countOtherSign);
        temp = parameters.Select(x => x.Algorithms).Select(x => x.Count());
        var countAlg = (temp == null || !temp.Any()) ? 0 : temp.Max();
        if (countAlg != 0)
            SetAlgorithms(sheet, ref columnHeader, countAlg);
        sheet.GetCell(HeaderRow, FirstColumnParam).SetValue(Automation);
        _EndColumn = columnHeader;
        var range = sheet.GetCellRange(HeaderRow, FirstColumnParam, HeaderRow, columnHeader - 1);
        range.Merge = true;
        range.SetBoldFont().SetCenterHAlign().SetWideBoard().SetColor(_Color);
    }

    private static void SetAlgorithms(Sheet sheet, ref int columnHeader, int count)
    {
        var firstCol = columnHeader;
        SetKeyDictionaryColumns("Alg", columnHeader);
        sheet.GetCell(HeaderRow + 1, columnHeader).SetValue(MainTextAlg).SetBoldFont();
        for (int i = 0; i < count; i++)
            SetHeaderAlg(sheet, ref columnHeader);
        var range = sheet.GetCellRange(HeaderRow + 1, firstCol, HeaderRow + 1, columnHeader - 1);
        range.Merge = true;
        range.SetBorder();
    }

    private static void SetOtherSignalings(Sheet sheet, ref int columnHeader, int countOtherSign)
    {
        var firstCol = columnHeader;
        sheet.GetCell(HeaderRow + 1, columnHeader).SetValue(MainText3Sign).SetBoldFont();
        SetKeyDictionaryColumns(TypeSignaling.Other, columnHeader);
        for (int i = 0; i < countOtherSign; i++)
            SetHeaderSinglaing(sheet, ref columnHeader, SetPoint);
        var range = sheet.GetCellRange(HeaderRow + 1, firstCol, HeaderRow + 1, columnHeader - 1);
        range.Merge = true;
        range.SetBorder();
    }

    private static void SetWarnSignaling(Sheet sheet, IEnumerable<ISignaling> signalings, ref int columnHeader)
    {
        var firstCol = columnHeader;
        sheet.GetCell(HeaderRow + 1, columnHeader).SetValue(MainText1Sign).SetBoldFont();
        if (signalings.Where(x => x.Type == TypeSignaling.L).Any())
        {
            SetKeyDictionaryColumns(TypeSignaling.L, columnHeader);
            SetHeaderSinglaing(sheet, ref columnHeader, LSign);
        }

        if (signalings.Where(x => x.Type == TypeSignaling.H).Any())
        {
            SetKeyDictionaryColumns(TypeSignaling.H, columnHeader);
            SetHeaderSinglaing(sheet, ref columnHeader, HSign);
        }

        var range = sheet.GetCellRange(HeaderRow + 1, firstCol, HeaderRow + 1, columnHeader - 1);
        range.Merge = true;
        range.SetBorder();
    }

    private static void SetKeyDictionaryColumns(object key, int value)
    {
        if (_ColumnsSignalingsAlgs.ContainsKey(key))
            _ColumnsSignalingsAlgs[key] = value;
        else
            _ColumnsSignalingsAlgs.Add(key, value);
    }

    private static void SetAlarms(Sheet sheet, IEnumerable<ISignaling> signalings, ref int columnHeader)
    {
        var firstCol = columnHeader;
        sheet.GetCell(HeaderRow + 1, columnHeader).SetValue(MainText2Sign).SetBoldFont();
        if (signalings.Where(x => x.Type == TypeSignaling.LL).Any())
        {
            SetKeyDictionaryColumns(TypeSignaling.LL, columnHeader);
            SetHeaderSinglaing(sheet, ref columnHeader, LLSign);
        }
        if (signalings.Where(x => x.Type == TypeSignaling.HH).Any())
        {
            SetKeyDictionaryColumns(TypeSignaling.HH, columnHeader);
            SetHeaderSinglaing(sheet, ref columnHeader, HHSign);
        }
        var range = sheet.GetCellRange(HeaderRow + 1, firstCol, HeaderRow + 1, columnHeader - 1);
        range.Merge = true;
        range.SetBorder();
    }

    private static void SetHeaderAlg(Sheet sheet, ref int columnHeader)
    {
        sheet.SetWidthColumn(columnHeader, Width1ColumnAlg);
        sheet.SetWidthColumn(columnHeader + 1, Width2ColumnAlg);
        sheet.GetCell(HeaderRow + 2, columnHeader).SetValue(SetPoint).SetBoldFont();
        sheet.GetCell(HeaderRow + 2, columnHeader + 1).SetValue(Actions).SetBoldFont();
        columnHeader += 2;
    }

    private static void SetHeaderSinglaing(Sheet sheet, ref int columnHeader, string setPoint)
    {
        sheet.SetWidthColumn(columnHeader, Width1ColumnSign);
        sheet.SetWidthColumn(columnHeader + 1, Width2ColumnSign);
        sheet.GetCell(HeaderRow + 2, columnHeader).SetValue(ModeSign).SetBoldFont();
        sheet.GetCell(HeaderRow + 2, columnHeader + 1).SetValue(setPoint).SetBoldFont();
        columnHeader += 2;
    }

    private static void WriteEmptyData(Sheet workSheet, int numRow)
    {
        var countCell = 6;
        for (int i = 0; i < countCell; i++)
            workSheet.GetCell(numRow, FirstColumnObj + i).SetValue("").SetLeftHorAling().SetTopVAlign();
    }

    private static void WriteMainDataObject(Sheet workSheet, int numRow, IObjectTreeItem obj)
    {
        workSheet.GetCell(numRow, FirstColumnObj).SetValue(obj.Name).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 1).SetValue(obj.ParametersEquipment).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 2).SetValue(obj.Position).SetLeftHorAling().SetTopVAlign();
        //Добавить сюда поле с подобъектами
        workSheet.GetCell(numRow, FirstColumnObj + 4).SetValue(obj.Product.Name).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 5).SetValue(obj.Product.Parameters).SetLeftHorAling().SetTopVAlign();
    }

    private static void WriteMainDataParameter(Sheet workSheet, ref int numRow, IParameterTreeItem parameter, ref int countRowsObj, ref int countRowsArea)
    {
        if (parameter.IsControl)
        {
            workSheet.GetCell(numRow, FirstColumnParam).SetValue(parameter.Name).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 1).SetValue(parameter.Unit).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 2).SetValue(GetTextBoll(parameter.ESD)).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 3).SetValue(GetTextBoll(parameter.ManualMeasure.IsMeasurable)).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 4).SetValue(GetTextBoll(parameter.RemoteMeasure.IsMeasurable)).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 5).SetValue(parameter.ManualMeasure.Range).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 6).SetValue(parameter.CalculatedValue).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 7).SetValue(parameter.Note).SetLeftHorAling().SetTopVAlign();
            WriteSignalingsData(workSheet, numRow, parameter);
            WriteAlgorithmData(workSheet, numRow, parameter);
            for (int i = StartColumn; i < _EndColumn; i++)
                workSheet.GetCell(numRow, i).SetBorder();
            workSheet.SetAutosizeHeightRow(numRow);
            numRow++;
            countRowsArea++;
            countRowsObj++;
        }
    }

    private static string GetTextBoll(bool input) => input ? "+" : "-";

    private static void WriteAlgorithmData(Sheet workSheet, int numRow, IParameterTreeItem parameter)
    {
        var algorithms = parameter.Algorithms;
        if (algorithms.Any())
        {
            var column = _ColumnsSignalingsAlgs["Alg"];
            foreach (var alg in algorithms)
            {
                workSheet.GetCell(numRow, column).SetValue(alg.SetPoint).SetLeftHorAling().SetTopVAlign();
                workSheet.GetCell(numRow, column + 1).SetValue(alg.Action).SetLeftHorAling().SetTopVAlign();
                column += 2;
            }
        }
    }

    private static void WriteSignalingsData(Sheet workSheet, int numRow, IParameterTreeItem parameter)
    {
        var signalings = parameter.Signalings;
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.L);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.H);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.LL);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.HH);
        var signs = signalings.Where(x => x.Type == TypeSignaling.Other);
        if (signs != null && signs.Any())
        {
            var column = _ColumnsSignalingsAlgs[TypeSignaling.Other];
            foreach (var signaling in signs)
            {
                workSheet.GetCell(numRow, column).SetValue(signaling.Mode.ToString()).SetLeftHorAling().SetTopVAlign();
                workSheet.GetCell(numRow, column + 1).SetValue(signaling.SetPoint).SetLeftHorAling().SetTopVAlign();
                column += 2;
            }
        }
    }

    private static void SetSignaling(Sheet workSheet, int numRow, IEnumerable<ISignaling> signalings, TypeSignaling type)
    {
        var sign = signalings.FirstOrDefault(x => x.Type == type);
        if (sign != null)
        {
            workSheet.GetCell(numRow, _ColumnsSignalingsAlgs[type]).SetValue(sign.Mode.ToString()).SetLeftHorAling().SetTopVAlign();
            workSheet.GetCell(numRow, _ColumnsSignalingsAlgs[type] + 1).SetValue(sign.SetPoint).SetLeftHorAling().SetTopVAlign();
        }
    }

    private void WriteMainData(Sheet workSheet)
    {
        workSheet.GetCell(4, 1).SetValue(_mainData.Code).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(4, 2).SetValue(_mainData.Name).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(4, 3).SetValue(_mainData.Object).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(4, 4).SetValue(_mainData.Stage.ToString()).SetLeftHorAling().SetTopVAlign();
        workSheet.GetCell(4, 5).SetValue(_mainData.Class.ToString()).SetLeftHorAling().SetTopVAlign();
    }

    private static void MergeCellsObject(Sheet workSheet, int numRow, int count)
    {
        var countMerge = 6;
        for (int i = 0; i < countMerge; i++)
            MergeCells(workSheet, count, numRow, FirstColumnObj + i);
    }

    private static void MergeCells(Sheet workSheet, int count, int numRow, int numCol)
    {
        var range = workSheet.GetCellRange(numRow, numCol, numRow + count - 1, numCol);
        range.Merge = true;
        range.SetBorder();
    }

}