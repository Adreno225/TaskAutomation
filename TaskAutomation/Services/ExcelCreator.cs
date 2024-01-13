using ExcelLibrary;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using TaskAutomation.Models;
using TaskAutomation.ViewModels;

namespace TaskAutomation.Services
{
    public class ExcelCreator
    {
        const int StartRow = 9;
        const int FirstColumnObj = 2;
        const int FirstColumnParam = 8;
        private string PathExcelTemplate = Environment.CurrentDirectory + "\\TemplateExcel.xlsx";

        public MainWindowViewModel MainModel { get; internal set; }

        private Task _Task => MainModel.Task;

        public void Create() 
        {
            using (var excelApp = new ExcelApp(PathExcelTemplate))
            {
                var workSheet = excelApp.ExcelWorkBook.GetExcelSheet(1);
                WriteMainData(workSheet);
                var areas = _Task.Areas;
                var numRow = StartRow;
                foreach (var area in areas) 
                {
                    if (area.Parameters.Where(x=>x.IsControl).Any()||area.Objects.Where(x=>x.Parameters.Where(y=>y.IsControl).Any()).Any())
                    {
                        var firstRowArea = numRow;
                        var countRowsArea = 0;
                        workSheet.GetCell(numRow, 1).SetValue(area.Name).SetCenterHAlign();
                        var areaParams = area.Parameters;
                        foreach (var parameter in areaParams)
                            WriteMainDataParameter(workSheet, ref numRow, parameter, ref countRowsArea);
                        MergeCellsObject(workSheet, firstRowArea, countRowsArea);
                        var objects = area.Objects;
                        foreach (var obj in objects)
                        {
                            if (obj.Parameters.Where(x => x.IsControl).Any())
                            {
                                WriteMainDataObject(workSheet, ref numRow, obj);
                                MergeCellsObject(workSheet, numRow, countRowsArea);
                                var parameters = obj.Parameters;
                                foreach (var par in parameters)
                                    WriteMainDataParameter(workSheet, ref numRow, par, ref countRowsArea);
                            } 
                        }
                        MergeCells(workSheet, countRowsArea, firstRowArea, 1);
                    }
                }
                MessageBox.Show("Файл создан!");
            }
        }

        private static void WriteMainDataObject(ExcelSheet workSheet,ref int numRow, Models.Object obj)
        {
            workSheet.GetCell(numRow, FirstColumnObj).SetValue(obj.Name).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnObj + 1).SetValue(obj.ParametersEquipment).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnObj + 2).SetValue(obj.Position).SetCenterHAlign();
            //Добавить сюда поле с подобъектами
            workSheet.GetCell(numRow, FirstColumnObj + 4).SetValue(obj.Product.Name).SetCenterHAlign();
            workSheet.GetCell(numRow, FirstColumnObj + 5).SetValue(obj.Product.Parameters).SetCenterHAlign();
        }

        private static void WriteMainDataParameter(ExcelSheet workSheet, ref int numRow, Models.Parameter parameter, ref int countRowsArea)
        {
            if (parameter.IsControl)
            {
                workSheet.GetCell(numRow, FirstColumnParam).SetValue(parameter.Name).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 1).SetValue(parameter.Unit).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 2).SetValue(parameter.ESD).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 3).SetValue(parameter.Mode.ToString()).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 4).SetValue(parameter.RangeMeasure).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 5).SetValue(parameter.CalculatedValue).SetCenterHAlign();
                workSheet.GetCell(numRow, FirstColumnParam + 6).SetValue(parameter.Note).SetCenterHAlign();
                numRow++;
                countRowsArea++;
            }
        }

        private void WriteMainData(ExcelSheet workSheet)
        {
            workSheet.GetCell(4, 1).SetValue(_Task.Code).SetCenterHAlign();
            workSheet.GetCell(4, 2).SetValue(_Task.Name).SetCenterHAlign();
            workSheet.GetCell(4, 3).SetValue(_Task.Object).SetCenterHAlign();
            workSheet.GetCell(4, 4).SetValue(_Task.Stage.ToString()).SetCenterHAlign();
            workSheet.GetCell(4, 5).SetValue(_Task.Class.ToString()).SetCenterHAlign();
        }

        private void MergeCellsObject(ExcelSheet workSheet, int numRow, int count)
        {
            MergeCells(workSheet, count, numRow, FirstColumnObj);
            MergeCells(workSheet, count, numRow, FirstColumnObj + 1);
            MergeCells(workSheet, count, numRow, FirstColumnObj + 2);
            MergeCells(workSheet, count, numRow, FirstColumnObj + 3);
            MergeCells(workSheet, count, numRow, FirstColumnObj + 4);
            MergeCells(workSheet, count, numRow, FirstColumnObj + 5);

        }

        private void MergeCells(ExcelSheet workSheet, int count, int numRow, int numCol)
        {
            var range = workSheet.GetCellRange(numRow, numCol, numRow + count - 1, numCol);
            range.Merge();
        }
    }
}
