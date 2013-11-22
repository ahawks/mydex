// Type: Dexcom.Client.Common.ResourceDictionaryConverter
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Dexcom.Client.Common
{
  public class ResourceDictionaryConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (!string.IsNullOrEmpty(parameter as string))
      {
        if (ProgramContext.Current != null)
        {
          try
          {
            obj = (object) ProgramContext.Current.ResourceLookup(parameter as string);
          }
          catch
          {
            obj = (object) (parameter as string);
          }
        }
        else
          obj = (object) (parameter as string);
      }
      return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return Binding.DoNothing;
    }
  }
}
