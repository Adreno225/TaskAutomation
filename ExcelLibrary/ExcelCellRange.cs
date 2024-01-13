using System.Buffers.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelCellRange
    {
        private Excel.Range _Range;
        public ExcelCellRange(Excel.Range cell) => _Range = cell;
        public void Merge()
        {
            _Range.Merge();
            _Range.Borders.Weight = Excel.XlBorderWeight.xlThin;
        }
            
    }
}
