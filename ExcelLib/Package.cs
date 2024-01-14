﻿using OfficeOpenXml;
using System;
using System.IO;
using System.Windows;

namespace ExcelLib
{
    public class Package: IDisposable
    {
        private readonly ExcelPackage _Package;
        private readonly ExcelWorkbook _Workbook;

        public Package(string pathTemlate, string newPath)
        {
            var fileInfoTemplate = new FileInfo(pathTemlate);
            var fileInfoNew = new FileInfo(newPath);
            _Package = new ExcelPackage(fileInfoTemplate);
            _Package.Compatibility.IsWorksheets1Based = true;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _Workbook = _Package.Workbook;
        }

        public Sheet SelectSheet(int numSheet) 
        {
            var sheet = _Workbook.Worksheets[numSheet];
            return new Sheet(sheet);
        }

        public void Dispose()
        {
            while (true)
            {
                try
                {
                    _Package.SaveAs("C:\\Wpf\\TaskAutomation\\1.xlsx");
                    break;
                }
                catch (InvalidOperationException)
                {
                    var result = MessageBox.
                        Show("Отказано в доступе к файлу. Попробуйте закрыть данный файл и далее нажать на кнопку \"ОК\"","Отказ в доступе", 
                        MessageBoxButton.OKCancel,icon: MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK) continue;
                    MessageBox.Show("Не удалось сохранить задание в связи с отсутсвием доступа к файлу!", "Ошибка записи файла",
                        MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    break;
                }
            }
            _Package.Dispose();
            MessageBox.Show("Файл задания успешно создан!");
        }
    }
}
