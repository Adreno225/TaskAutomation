using System.CodeDom;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelCell
    {
        protected Excel.Range _Range;
        public ExcelCell(Excel.Range cell) => _Range = cell;
        public string Address { get => _Range.Address; }
        public ExcelCell SetValue(object value)
        {
            _Range.Value = value;
            _Range.Borders.Weight = Excel.XlBorderWeight.xlThin;
            return this;
        } 
        public string GetValue() => _Range.Value;
        public ExcelCell SetCenterHAlign()
        {
            _Range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            return this;
        }
        public ExcelCell SetCenterVAlign()
        {
            _Range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            return this;
        }


    }
}
