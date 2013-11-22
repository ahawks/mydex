// Type: Dexcom.Client.Common.DateTimeConverter
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Dexcom.Client.Common
{
  public class DateTimeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (value != null && value is DateTime && (DateTime) value != CommonValues.EmptyDateTime)
        obj = (object) (DateTime) value;
      return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (value != null && value is DateTime)
        obj = (object) (DateTime) value;
      else if (value == null)
        obj = (object) CommonValues.EmptyDateTime;
      return obj;
    }
  }
}
