// Type: Dexcom.Client.Common.RelativeFontSizeConverter
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Dexcom.Client.Common
{
  public class RelativeFontSizeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (value is double && !string.IsNullOrEmpty(parameter as string))
      {
        string str = (parameter as string).Trim();
        if (Enumerable.Contains<char>((IEnumerable<char>) str, '%'))
        {
          string s = str.Replace('%', ' ');
          obj = (object) ((double) value + (double) value * (double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture) / 100.0));
        }
        else
          obj = (object) ((double) value + double.Parse(parameter as string, (IFormatProvider) CultureInfo.InvariantCulture));
      }
      return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return Binding.DoNothing;
    }
  }
}
