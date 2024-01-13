using System;
using System.IO;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace TaskAutomation.Services
{
    public class ExcelCreator:IDisposable   
    {
        private Excel.Application _Application;
        private Excel.Workbook _Workbook;
        private string _Path;

        public ExcelCreator()
        {
            _Application = new Excel.Application();
        }

        public bool Open(string path)
        {
            try
            {
                if(File.Exists(path))
                {
                    _Workbook = _Application.Workbooks.Open(path);
                }
                else
                {
                    _Workbook = _Application.Workbooks.Add();
                    _Path = path;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                _Workbook?.Close();
                _Application?.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
