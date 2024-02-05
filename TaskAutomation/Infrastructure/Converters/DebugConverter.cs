using System;
using System.Globalization;
using TaskAutomation.Infrastructure.Converters.Base;
using System.Diagnostics;

namespace TaskAutomation.Infrastructure.Converters;

internal class DebugConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Debugger.Break();
        return value;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Debugger.Break();
        return value;
    }

}