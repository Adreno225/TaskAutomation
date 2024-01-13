using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelSheet
    {
        private Excel.Worksheet _Worksheet;
        public int CountRows { get => _Worksheet.Rows.Count; }
        public int CountColumns { get => _Worksheet.Columns.Count; }
        public ExcelSheet(Excel.Worksheet worksheet) => _Worksheet = worksheet;
        public ExcelCell GetCell(int numRow, int numCol) => new ExcelCell(_Worksheet.Cells[numRow, numCol]);
        public ExcelCellRange GetCellRange(int row1, int col1, int row2, int col2) 
        {
            var adr1 = new ExcelCell(_Worksheet.Cells[row1, col1]).Address;
            var adr2 = new ExcelCell(_Worksheet.Cells[row2, col2]).Address;
            Excel.Range range = _Worksheet.Range[adr1, adr2];
            return new ExcelCellRange(range);
        }

      
    }
}
