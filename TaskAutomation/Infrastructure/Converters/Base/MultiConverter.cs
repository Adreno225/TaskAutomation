﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskAutomation.Infrastructure.Converters.Base;

public abstract class MultiConverter : IMultiValueConverter
{
    public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("Обратное преобразование не поддерживается");
    }
}