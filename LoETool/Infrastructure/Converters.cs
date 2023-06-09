using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LoETool.Infrastructure;

[ValueConversion(typeof(bool), typeof(bool))]
public sealed class BoolToInverseConverter : IValueConverter
{
    public object Convert(object value, Type t, object parm, CultureInfo lang) => !(bool)value;
    public object ConvertBack(object value, Type t, object parm, CultureInfo lang) => DependencyProperty.UnsetValue;
}

// convert from object to uri where if null, return null else return checkmark

[ValueConversion(typeof(object), typeof(Uri))]
public sealed class ObjectToCheckmarkConverter : IValueConverter
{
    public object? Convert(object value, Type t, object parm, CultureInfo lang) => 
        value is null ? null : new Uri(Constants.Checkmark, UriKind.Relative);
    public object ConvertBack(object value, Type t, object parm, CultureInfo lang) => DependencyProperty.UnsetValue;
}

// convert from bool to checkmark uri if true

[ValueConversion(typeof(bool), typeof(Uri))]
public sealed class BoolToCheckmarkConverter : IValueConverter
{
    public object? Convert(object value, Type t, object parm, CultureInfo lang) =>
        value is bool v && v ? new Uri(Constants.Checkmark, UriKind.Relative) : null;
    public object ConvertBack(object value, Type t, object parm, CultureInfo lang) => DependencyProperty.UnsetValue;
}

// convert from hit count (0 -> 3) to background color

[ValueConversion(typeof(int), typeof(SolidColorBrush))]
public sealed class HitCountToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type t, object parm, CultureInfo lang)
    {
        if (value is int v)
        {
            return v switch
            {
                0 => Brushes.Gray,
                1 => Brushes.Blue,
                2 => Brushes.Teal,
                _ => Brushes.Green,
            };
        }
        return Brushes.Gray;
    }
    public object ConvertBack(object value, Type t, object parm, CultureInfo lang) => DependencyProperty.UnsetValue;
}