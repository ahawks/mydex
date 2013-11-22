// Type: Dexcom.Common.OnlineGuidConverter
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace Dexcom.Common
{
  public class OnlineGuidConverter : GuidConverter
  {
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException("destinationType");
      if (destinationType == typeof (string))
        return (object) CommonTools.ConvertToString((Guid) value);
      else
        return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
