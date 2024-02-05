using OfficeOpenXml;

namespace ExcelLib;

public class BaseRange
{
    protected readonly ExcelRange _ExcelRange;
    public bool Merge { get =>_ExcelRange.Merge; set => _ExcelRange.Merge = value; }

    public BaseRange(ExcelRange excelRange)
    {
        _ExcelRange = excelRange;
    }
    public BaseRange SetValue(string value)
    {
        _ExcelRange.Value = value;
        SetBorder();
        return this;
    }

    public virtual string? GetValue()
    {
        return _ExcelRange?.Value?.ToString() ?? "";
    }

    public BaseRange SetCenterHAlign() 
    {
        _ExcelRange.Style.HorizontalAlignment= OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        return this;
    }

    public BaseRange SetLeftHorAling()
    {
        _ExcelRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        return this;
    }

    public BaseRange SetBorder()
    {
        _ExcelRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        return this;
    }

    public BaseRange SetWideBoard()
    {
        _ExcelRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
        return this;
    }

    public BaseRange SetBoldFont()
    {
        _ExcelRange.Style.Font.Bold= true;
        return this;
    }

    public BaseRange SetColor(System.Drawing.Color color)
    {
        _ExcelRange.Style.Fill.SetBackground(color);
        return this;
    }

}