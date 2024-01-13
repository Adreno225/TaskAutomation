using System;
using System.IO;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelApp : IDisposable
    {
        private Excel.Application _Application;
        private Excel.Workbook _Workbook;
        private ExcelWorkBook _ExcelWorkBook;
        public ExcelWorkBook ExcelWorkBook { get => _ExcelWorkBook; }

        public ExcelApp(string path)
        {
            _Application = new Excel.Application();
            _Workbook = _Application.Workbooks.Open(path);
            _ExcelWorkBook = new ExcelWorkBook(_Workbook);
        }

        public void Dispose()
        {
            try
            {
                _Workbook.Save();
                _Workbook.Close();
                _Application.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
