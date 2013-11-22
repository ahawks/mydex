// Type: Dexcom.Client.Common.DateTimeOffsetConverter
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Dexcom.Client.Common
{
  public class DateTimeOffsetConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (value != null && value is DateTimeOffset && (DateTimeOffset) value != CommonValues.EmptyDateTimeOffset)
        obj = (object) ((DateTimeOffset) value).LocalDateTime;
      return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (value != null && value is DateTime)
        obj = (object) new DateTimeOffset((DateTime) value);
      else if (value == null)
        obj = (object) CommonValues.EmptyDateTimeOffset;
      return obj;
    }
  }
}
