using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelLibrary
{
    public class ExcelWorkBook
    {
        private Excel.Workbook _Workbook;
        public ExcelWorkBook(Excel.Workbook workbook) => _Workbook = workbook;
        public ExcelSheet GetActiveSheet() => new ExcelSheet(_Workbook.ActiveSheet());
        public ExcelSheet GetExcelSheet(int numSheet) =>new ExcelSheet(_Workbook.Worksheets[numSheet]);
    }
}
