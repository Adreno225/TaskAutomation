using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelLib
{
    internal class CellRange
    {
        private readonly ExcelRange _ExcelRange;

        public CellRange(ExcelRange excelRange) 
        {
            _ExcelRange = excelRange;
        }
    }
}
