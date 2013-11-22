// Type: Dexcom.Common.StringUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Dexcom.Common
{
  public class StringUtils
  {
    public static bool IsUniqueName(string strName, IEnumerable otherNames)
    {
      if (strName == null)
        throw new ArgumentNullException("strName");
      if (otherNames == null)
        throw new ArgumentNullException("otherNames");
      bool flag = true;
      foreach (string strB in otherNames)
      {
        if (string.Compare(strName, strB, true, CultureInfo.InvariantCulture) == 0)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public static bool IsNameValidFileName(string strName)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(strName) && strName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
        flag = true;
      return flag;
    }

    public static char[] GetInvalidFileNameChars()
    {
      return Path.GetInvalidFileNameChars();
    }

    public static bool IsNameValidFileName(string strName, char[] otherInvalidChars)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(strName) && strName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 && strName.IndexOfAny(otherInvalidChars) == -1)
        flag = true;
      return flag;
    }

    public static string EnsureValidFileName(string fileName)
    {
      return StringUtils.EnsureValidFileName(fileName, '_');
    }

    public static string EnsureValidFileName(string fileName, char replacementChar)
    {
      string strName = fileName;
      if (!StringUtils.IsNameValidFileName(strName))
      {
        foreach (char oldChar in StringUtils.GetInvalidFileNameChars())
          strName = strName.Replace(oldChar, replacementChar);
      }
      if (!StringUtils.IsNameValidFileName(strName))
        throw new DexComException("Failed to ensure valid file name. Check for vaild replacement character.");
      else
        return strName;
    }

    public static string UniqueNewName(string strBaseName, string[] strOtherNames)
    {
      if (strBaseName == null)
        throw new ArgumentNullException("strBaseName");
      if (strOtherNames == null)
        throw new ArgumentNullException("strOtherNames");
      string str = string.Empty;
      string strName = string.Empty;
      bool flag = false;
      string format = string.Format("{0}{1}", (object) strBaseName, (object) "{0:00}");
      for (int index = 1; index <= strOtherNames.Length + 1; ++index)
      {
        strName = string.Format(format, (object) index);
        flag = StringUtils.IsUniqueName(strName, (IEnumerable) strOtherNames);
        if (flag)
          break;
      }
      if (!flag || strName == string.Empty)
        throw new DexComException("UniqueNewName logic failed to create new name.");
      else
        return strName;
    }

    public static string UniqueCopyOfName(string strBaseName, string[] strOtherNames)
    {
      if (strBaseName == null)
        throw new ArgumentNullException("strBaseName");
      if (strOtherNames == null)
        throw new ArgumentNullException("strOtherNames");
      string str1 = strBaseName;
      string strB1 = "Copy of ";
      string format1 = "Copy of {0}";
      string format2 = "Copy [{0:00}] of {1}";
      string strB2 = "Copy [";
      string str2 = "] of ";
      string strName = string.Empty;
      bool flag = false;
      if (ProgramContext.Current != null)
      {
        try
        {
          strB1 = ProgramContext.Current.ResourceLookup("Common_CopyOf") + " ";
          format1 = ProgramContext.Current.ResourceLookup("Common_CopyOf") + " {0}";
          format2 = ProgramContext.Current.ResourceLookup("Common_CopyNumOf") + " {1}";
          strB2 = ProgramContext.Current.ResourceLookup("Common_CopyNumOfPrefix");
          str2 = ProgramContext.Current.ResourceLookup("Common_CopyNumOfPostfix") + " ";
        }
        catch
        {
          strB1 = "Copy of ";
          format1 = "Copy of {0}";
          format2 = "Copy [{0:00}] of {1}";
          strB2 = "Copy [";
          str2 = "] of ";
        }
      }
      if (str1.Length >= strB1.Length && string.Compare(str1.Substring(0, strB1.Length), strB1, true, CultureInfo.InvariantCulture) == 0)
        str1 = str1.Substring(strB1.Length);
      else if (str1.Length >= strB2.Length && string.Compare(str1.Substring(0, strB2.Length), strB2, true, CultureInfo.InvariantCulture) == 0)
      {
        int num = str1.IndexOf(str2);
        int length = strB2.Length;
        if (num >= length)
        {
          string s = str1.Substring(strB2.Length, num - length);
          try
          {
            int.Parse(s);
            str1 = str1.Substring(num + str2.Length);
          }
          catch
          {
          }
        }
      }
      for (int index = 0; index <= strOtherNames.Length + 1; ++index)
      {
        strName = index != 0 ? string.Format(format2, (object) (index + 1), (object) str1) : string.Format(format1, (object) str1);
        flag = StringUtils.IsUniqueName(strName, (IEnumerable) strOtherNames);
        if (flag)
          break;
      }
      if (!flag || strName == string.Empty)
        throw new DexComException("UniqueCopyOfName logic failed to create new name.");
      else
        return strName;
    }

    public static string ToHexString(byte[] bytes)
    {
      string str = string.Empty;
      if (bytes != null && bytes.Length > 0)
      {
        StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
        foreach (byte num in bytes)
          stringBuilder.Append(num.ToString("X"));
        str = ((object) stringBuilder).ToString();
      }
      return str;
    }
  }
}
