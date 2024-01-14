using OfficeOpenXml;

namespace ExcelLib
{
    public class Sheet
    {
        private readonly ExcelWorksheet _Worksheet;

        public Sheet(ExcelWorksheet worksheet)
        {
            _Worksheet = worksheet;
        }

        public Cell GetCell(int row, int column) 
        {
            var cell = _Worksheet.Cells[row, column];
            return new Cell(cell);
        }

        public Cell GetCellRange(int row1, int column1, int row2, int column2)
        {
            var cell = _Worksheet.Cells[row1, column1, row2, column2];
            return new Cell(cell);
        }

        public void SetWidthColumn(int numCol,int width)
        {
            _Worksheet.Columns[numCol].Width = width;
        }

    }
}
