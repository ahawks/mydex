// Type: Dexcom.Common.AssemblyUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Reflection;

namespace Dexcom.Common
{
  public static class AssemblyUtils
  {
    public static string Company
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyCompanyAttribute) customAttributes[0]).Company;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string Product
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyProductAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyProductAttribute) customAttributes[0]).Product;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string Copyright
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string Title
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyTitleAttribute) customAttributes[0]).Title;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string Description
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string Version
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyVersionAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyVersionAttribute) customAttributes[0]).Version;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }

    public static string FileVersion
    {
      get
      {
        string str = string.Empty;
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          object[] customAttributes = entryAssembly.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
          if (customAttributes != null && customAttributes.Length > 0)
            str = ((AssemblyFileVersionAttribute) customAttributes[0]).Version;
          if (string.IsNullOrEmpty(str))
            str = string.Empty;
        }
        return str;
      }
    }
  }
}
