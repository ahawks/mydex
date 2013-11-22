// Type: Dexcom.Common.LocalizationKeyAttribute
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;

namespace Dexcom.Common
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class LocalizationKeyAttribute : Attribute
  {
    public string ResourceKey { get; private set; }

    public LocalizationKeyAttribute(string key)
    {
      this.ResourceKey = key;
    }

    public static string GetResourceKey(Enum en)
    {
      string name = ((object) en).ToString();
      LocalizationKeyAttribute[] localizationKeyAttributeArray = (LocalizationKeyAttribute[]) en.GetType().GetField(name).GetCustomAttributes(typeof (LocalizationKeyAttribute), false);
      string str = (string) null;
      if (localizationKeyAttributeArray != null && localizationKeyAttributeArray.Length > 0)
        str = localizationKeyAttributeArray[0].ResourceKey;
      if (string.IsNullOrEmpty(str))
        str = (string) null;
      return str;
    }
  }
}
