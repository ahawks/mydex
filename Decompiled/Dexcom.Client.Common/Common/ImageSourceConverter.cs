// Type: Dexcom.Client.Common.ImageSourceConverter
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Dexcom.Client.Common
{
  public class ImageSourceConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = Binding.DoNothing;
      if (!string.IsNullOrEmpty(value as string))
        obj = (object) new BitmapImage(new Uri(value as string));
      return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return Binding.DoNothing;
    }
  }
}
