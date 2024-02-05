using OfficeOpenXml;

namespace ExcelLib;

internal class CellRange
{
    private readonly ExcelRange _ExcelRange;

    public CellRange(ExcelRange excelRange) 
    {
        _ExcelRange = excelRange;
    }
}