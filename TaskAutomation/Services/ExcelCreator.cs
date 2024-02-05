using ExcelLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using TaskAutomation.Infrastructure.DialogWindows;
using TaskAutomation.Models;
using TaskAutomation.ViewModels;

namespace TaskAutomation.Services;

public interface ICreatorTask
{
    void Create();
    MainWindowViewModel MainModel { get; set; }
}
public class ExcelCreator:ICreatorTask
{
    const int StartRow = 9;
    const int FirstColumnObj = 2;
    const int FirstColumnParam = 8;
    const int StartColumn = 15;
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
    private TaskClass _Task => MainModel.Task;

    public MainWindowViewModel MainModel { get; set; }

    public void Create()
    {
        var dialog = new SaveDialog();
        dialog.Save(MainMethod, filter: FiltersDialogWindow);
    }

    private void MainMethod(string pathSave)
    {
        using (var excel = new Package(PathExcelTemplate, pathSave))
        {
            _ColumnsSignalingsAlgs.Clear();
            var workSheet = excel.SelectSheet(1);
            CreateHeaderRow(_Task, workSheet);
            WriteMainData(workSheet);
            var areas = _Task.Areas;
            var numRow = StartRow;
            foreach (Area area in areas)
            {
                if (area.Parameters.Where(x => x.IsControl).Any() || area.Objects.Where(x => x.Parameters.Where(y => y.IsControl).Any()).Any())
                {
                    var firstRowArea = numRow;
                    var countRowsArea = 0;
                    var countRowsObj = 0;
                    workSheet.GetCell(numRow, 1).SetValue(area.Name).SetCenterHAlign();
                    var areaParams = area.Parameters;
                    foreach (Parameter parameter in areaParams)
                        WriteMainDataParameter(workSheet, ref numRow, parameter, ref countRowsObj, ref countRowsArea);
                    if (countRowsObj > 0)
                    {
                        MergeCellsObject(workSheet, firstRowArea, countRowsArea);
                        WriteEmptyData(workSheet, firstRowArea);
                    }
                    var objects = area.Objects;
                    foreach (ObjectInf obj in objects)
                    {
                        if (obj.Parameters.Where(x => x.IsControl).Any())
                        {
                            var firstRowObj = numRow;
                            countRowsObj = 0;
                            WriteMainDataObject(workSheet, numRow, obj);
                            var parameters = obj.Parameters;
                            foreach (Parameter par in parameters)
                                WriteMainDataParameter(workSheet, ref numRow, par, ref countRowsObj, ref countRowsArea);
                            MergeCellsObject(workSheet, firstRowObj, countRowsObj);
                        }
                    }
                    MergeCells(workSheet, countRowsArea, firstRowArea, 1);
                }
            }
            if (excel.Save())
            {
                if (MessageBox.Show("Файл задания успешно создан! Откыть файл с заданием?",
                        "Задание создано", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                    Process.Start(new ProcessStartInfo { FileName = pathSave, UseShellExecute = true });
            }
        }
    }

    private static void CreateHeaderRow (TaskClass task, Sheet sheet)
    {
        var par1= task.Areas.SelectMany(x => x.Parameters).Where(x=>x.IsControl);
        var par2 = task.Areas.SelectMany(x => x.Objects).SelectMany(x=> x.Parameters).Where(x => x.IsControl);
        var parameters = par1.Concat(par2);
        var signalings = parameters.SelectMany(x => x.Signalings);
        var columnHeader = StartColumn;
        if (signalings.Where(x => x.Type == TypeSignaling.L).Any() || signalings.Where(x => x.Type == TypeSignaling.H).Any())
            SetWarnSignaling(sheet,signalings,ref columnHeader);
        if (signalings.Where(x => x.Type == TypeSignaling.LL).Any() || signalings.Where(x => x.Type == TypeSignaling.HH).Any())
            SetAlarms(sheet, signalings, ref columnHeader);
        var temp = parameters.Select(x => x.Signalings).Select(x => x.Where(y => y.Type == TypeSignaling.Other).Count());
        var countOtherSign = (temp == null || !temp.Any()) ? 0 : temp.Max();
        if (countOtherSign!=0)
            SetOtherSignalings(sheet, ref columnHeader, countOtherSign);
        temp = parameters.Select(x => x.Algorithms).Select(x=>x.Count());
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
        var range = sheet.GetCellRange(HeaderRow+1, firstCol, HeaderRow+1, columnHeader - 1);
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
        var range = sheet.GetCellRange(HeaderRow+1, firstCol, HeaderRow+1, columnHeader - 1);
        range.Merge = true;
        range.SetBorder();
    }

    private static void SetWarnSignaling(Sheet sheet, IEnumerable<Signaling> signalings, ref int columnHeader)
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
                
        var range = sheet.GetCellRange(HeaderRow+1, firstCol, HeaderRow+1, columnHeader - 1);
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

    private static void SetAlarms(Sheet sheet, IEnumerable<Signaling> signalings, ref int columnHeader)
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
        var range = sheet.GetCellRange(HeaderRow+1, firstCol, HeaderRow+1, columnHeader - 1);
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
        workSheet.GetCell(numRow, FirstColumnObj).SetValue("").SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 1).SetValue("").SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 2).SetValue("").SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 3).SetValue("").SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 4).SetValue("").SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 5).SetValue("").SetCenterHAlign();
    }

    private static void WriteMainDataObject(Sheet workSheet, int numRow, ObjectInf obj)
    {
        workSheet.GetCell(numRow, FirstColumnObj).SetValue(obj.Name).SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 1).SetValue(obj.ParametersEquipment).SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 2).SetValue(obj.Position).SetCenterHAlign();
        //Добавить сюда поле с подобъектами
        workSheet.GetCell(numRow, FirstColumnObj + 4).SetValue(obj.Product.Name).SetCenterHAlign();
        workSheet.GetCell(numRow, FirstColumnObj + 5).SetValue(obj.Product.Parameters).SetCenterHAlign();
    }

    private static void WriteMainDataParameter(Sheet workSheet, ref int numRow, Parameter parameter, ref int countRowsObj, ref int countRowsArea)
    {
        if (parameter.IsControl)
        {
            workSheet.GetCell(numRow, FirstColumnParam).SetValue(parameter.Name).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 1).SetValue(parameter.Unit).SetCenterHAlign();
            //Сделать преобразование bool to string
            var texBool = parameter.ESD ? "+":"-";
            workSheet.GetCell(numRow, FirstColumnParam + 2).SetValue(texBool).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 3).SetValue(parameter.Mode.ToString()).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 4).SetValue(parameter.RangeMeasure).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 5).SetValue(parameter.CalculatedValue).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnParam + 6).SetValue(parameter.Note).SetCenterHAlign();
            WriteSignalingsData(workSheet, numRow, parameter);
            WriteAlgorithmData(workSheet, numRow, parameter);
            for (int i = StartColumn; i < _EndColumn; i++)
                workSheet.GetCell(numRow, i).SetBorder();
            numRow++;
            countRowsArea++;
            countRowsObj++;
        }
    }

    private static void WriteAlgorithmData(Sheet workSheet, int numRow, Parameter parameter)
    {
        var algorithms = parameter.Algorithms;
        if (algorithms.Any())
        {
            var column = _ColumnsSignalingsAlgs["Alg"];
            foreach (var alg in algorithms)
            {
                workSheet.GetCell(numRow, column).SetValue(alg.SetPoint).SetCenterHAlign();
                workSheet.GetCell(numRow, column + 1).SetValue(alg.Action).SetCenterHAlign();
                column += 2;
            }
        }
    }

    private static void WriteSignalingsData(Sheet workSheet, int numRow, Parameter parameter)
    {
        var signalings = parameter.Signalings;
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.L);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.H);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.LL);
        SetSignaling(workSheet, numRow, signalings, TypeSignaling.HH);
        var signs = signalings.Where(x => x.Type == TypeSignaling.Other);
        if (signs !=null && signs.Any())
        {
            var column = _ColumnsSignalingsAlgs[TypeSignaling.Other];
            foreach (var signaling in signs)
            {
                workSheet.GetCell(numRow, column).SetValue(signaling.Mode.ToString()).SetCenterHAlign();
                workSheet.GetCell(numRow, column+1).SetValue(signaling.SetPoint).SetCenterHAlign();
                column += 2;
            }
        }
    }

    private static void SetSignaling(Sheet workSheet, int numRow, IEnumerable<Signaling> signalings, TypeSignaling type)
    {
        var sign = signalings.FirstOrDefault(x => x.Type == type);
        if (sign != null)
        {
            workSheet.GetCell(numRow, _ColumnsSignalingsAlgs[type]).SetValue(sign.Mode.ToString()).SetCenterHAlign();
            workSheet.GetCell(numRow, _ColumnsSignalingsAlgs[type]+1).SetValue(sign.SetPoint).SetCenterHAlign();
        }
    }

    private void WriteMainData(Sheet workSheet)
    {
        workSheet.GetCell(4, 1).SetValue(_Task.Code).SetCenterHAlign();
        workSheet.GetCell(4, 2).SetValue(_Task.Name).SetCenterHAlign();
        workSheet.GetCell(4, 3).SetValue(_Task.Object).SetCenterHAlign();
        workSheet.GetCell(4, 4).SetValue(_Task.Stage.ToString()).SetCenterHAlign();
        workSheet.GetCell(4, 5).SetValue(_Task.Class.ToString()).SetCenterHAlign();
    }

    private static void MergeCellsObject(Sheet workSheet, int numRow, int count)
    {
        MergeCells(workSheet, count, numRow, FirstColumnObj);
        MergeCells(workSheet, count, numRow, FirstColumnObj + 1);
        MergeCells(workSheet, count, numRow, FirstColumnObj + 2);
        MergeCells(workSheet, count, numRow, FirstColumnObj + 3);
        MergeCells(workSheet, count, numRow, FirstColumnObj + 4);
        MergeCells(workSheet, count, numRow, FirstColumnObj + 5);
    }

    private static void MergeCells(Sheet workSheet, int count, int numRow, int numCol)
    {
        var range = workSheet.GetCellRange(numRow, numCol, numRow + count-1, numCol);
        range.Merge=true;
        range.SetBorder();
    }

}