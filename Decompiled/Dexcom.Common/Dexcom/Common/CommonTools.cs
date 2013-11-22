// Type: Dexcom.Common.CommonTools
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using CSUACSelfElevation;
using InternetTime;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace Dexcom.Common
{
  public static class CommonTools
  {
    private static readonly double LowMmolThreshold = 2000.0 / 901.0;
    private static readonly double HighMmolThreshold = 20000.0 / 901.0;

    static CommonTools()
    {
    }

    public static string ConvertToString(Guid guid)
    {
      return guid.ToString("B").ToUpper();
    }

    public static string ConvertToString(DateTime dt)
    {
      if (dt.Millisecond == 0)
        return dt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
      else
        return dt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fff");
    }

    public static string ConvertToString(DateTimeOffset dto)
    {
      if (dto.Millisecond == 0)
        return dto.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss' 'zzz");
      else
        return dto.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fff' 'zzz");
    }

    public static int NullableComparerHelper<T>(T first, T second, Comparison<T> comparer) where T : class
    {
      if ((object) first == null)
        return (object) second == null ? 0 : -1;
      else if ((object) second == null)
        return 1;
      else
        return comparer(first, second);
    }

    public static void RemoveDuplicates<T>(List<T> list) where T : IComparable
    {
      List<int> list1 = new List<int>();
      for (int index = 0; index < list.Count - 1; ++index)
      {
        if (list[index].CompareTo((object) list[index + 1]) == 0)
          list1.Insert(0, index + 1);
      }
      foreach (int index in list1)
        list.RemoveAt(index);
    }

    public static int NullableCaparerHelper<T>(T first, T second) where T : class, IComparable
    {
      if ((object) first == null)
        return (object) second == null ? 0 : -1;
      else if ((object) second == null)
        return 1;
      else
        return first.CompareTo((object) second);
    }

    public static bool ParseTimeSpan(string timeSpanString, out TimeSpan timeSpanValue)
    {
      bool flag = false;
      string s = timeSpanString.Trim();
      timeSpanValue = TimeSpan.Zero;
      if (s != string.Empty)
      {
        try
        {
          timeSpanValue = TimeSpan.Parse(s);
          flag = true;
        }
        catch
        {
          try
          {
            timeSpanValue = TimeSpan.FromTicks(long.Parse(s));
            flag = true;
          }
          catch
          {
          }
        }
      }
      else
      {
        timeSpanValue = TimeSpan.Zero;
        flag = true;
      }
      return flag;
    }

    public static string RevisionToString(uint revision)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(revision >> 24 & (uint) byte.MaxValue);
      stringBuilder.Append('.');
      stringBuilder.Append(revision >> 16 & (uint) byte.MaxValue);
      stringBuilder.Append('.');
      stringBuilder.Append(revision >> 8 & (uint) byte.MaxValue);
      stringBuilder.Append('.');
      stringBuilder.Append(revision & (uint) byte.MaxValue);
      return ((object) stringBuilder).ToString();
    }

    public static uint StringToRevision(string revision)
    {
      uint num = 0U;
      string[] strArray = revision.Split(new char[1]
      {
        '.'
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length == 4)
        num = num + ((uint) Convert.ToByte(strArray[0]) << 24) + ((uint) Convert.ToByte(strArray[1]) << 16) + ((uint) Convert.ToByte(strArray[2]) << 8) + (uint) Convert.ToByte(strArray[3]);
      return num;
    }

    public static void UnpackRevisionNumber(string revisionValue, out byte major, out byte minor, out byte revision, out byte build)
    {
      CommonTools.UnpackRevisionNumber(CommonTools.StringToRevision(revisionValue), out major, out minor, out revision, out build);
    }

    public static void UnpackRevisionNumber(uint revisionValue, out byte major, out byte minor, out byte revision, out byte build)
    {
      uint num = revisionValue;
      major = (byte) (num >> 24 & (uint) byte.MaxValue);
      minor = (byte) (num >> 16 & (uint) byte.MaxValue);
      revision = (byte) (num >> 8 & (uint) byte.MaxValue);
      build = (byte) (num & (uint) byte.MaxValue);
    }

    public static string FormatXml(XmlDocument xDocument)
    {
      StringWriter stringWriter = new StringWriter();
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) stringWriter);
      xmlTextWriter.Formatting = Formatting.Indented;
      xmlTextWriter.Indentation = 4;
      xDocument.WriteTo((XmlWriter) xmlTextWriter);
      xmlTextWriter.Flush();
      string str = stringWriter.ToString();
      stringWriter.Close();
      return str;
    }

    public static string FormatXml(string strXml)
    {
      string str = string.Empty;
      try
      {
        XmlDocument xDocument = new XmlDocument();
        xDocument.LoadXml(strXml);
        return CommonTools.FormatXml(xDocument);
      }
      catch
      {
        return strXml;
      }
    }

    public static List<KeyValuePair<string, string>> CreateFlattenedXmlAttributesList(XmlElement xmlElement)
    {
      List<KeyValuePair<string, string>> attributeList = new List<KeyValuePair<string, string>>();
      CommonTools.CreateFlattenedXmlAttributesList(xmlElement, "", attributeList);
      return attributeList;
    }

    public static void CreateFlattenedXmlAttributesList(XmlElement xmlElement, string currentPath, List<KeyValuePair<string, string>> attributeList)
    {
      if (xmlElement == null)
        return;
      currentPath = currentPath + "/" + xmlElement.Name;
      if (xmlElement.Attributes.Count > 0)
      {
        foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) xmlElement.Attributes)
        {
          string key = currentPath + "/@" + xmlAttribute.Name;
          string str = xmlAttribute.Value;
          attributeList.Add(new KeyValuePair<string, string>(key, str));
        }
      }
      foreach (XmlElement xmlElement1 in xmlElement.ChildNodes)
        CommonTools.CreateFlattenedXmlAttributesList(xmlElement1, currentPath, attributeList);
    }

    public static string GeneratePathFromGuid(Guid guid)
    {
      string str1 = CommonTools.ConvertToString(guid);
      if (str1.Length != 38)
        throw new DexComException("Unexpected guid string format encountered.");
      string str2 = str1.Substring(35, 2);
      string str3 = str1.Substring(33, 2);
      return ((object) new StringBuilder(45).Append(str2).Append(Path.DirectorySeparatorChar).Append(str3).Append(Path.DirectorySeparatorChar).Append(str1).Append(Path.DirectorySeparatorChar)).ToString();
    }

    public static string GetHardwareId()
    {
      string str = string.Empty;
      try
      {
        using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\WPA"))
        {
          if (registryKey1 != null)
          {
            foreach (string name in registryKey1.GetSubKeyNames())
            {
              if (name.IndexOf("SigningHash") == 0)
              {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name))
                {
                  str = StringUtils.ToHexString((byte[]) registryKey2.GetValue("SigningHashData"));
                  break;
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      return str;
    }

    [DllImport("kernel32.dll")]
    internal static long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, uint VolumeNameSize, ref uint VolumeSerialNumber, ref uint MaximumComponentLength, ref uint FileSystemFlags, StringBuilder FileSystemNameBuffer, uint FileSystemNameSize);

    [SecuritySafeCritical]
    public static string GetVolumeSerialNumber()
    {
      return CommonTools.GetVolumeSerialNumber(Directory.GetDirectoryRoot(Environment.SystemDirectory));
    }

    [SecuritySafeCritical]
    public static string GetVolumeSerialNumber(string strDriveRoot)
    {
      uint VolumeSerialNumber = 0U;
      uint MaximumComponentLength = 0U;
      StringBuilder VolumeNameBuffer = new StringBuilder(256);
      uint FileSystemFlags = 0U;
      StringBuilder FileSystemNameBuffer = new StringBuilder(256);
      CommonTools.GetVolumeInformation(strDriveRoot, VolumeNameBuffer, (uint) VolumeNameBuffer.Capacity, ref VolumeSerialNumber, ref MaximumComponentLength, ref FileSystemFlags, FileSystemNameBuffer, (uint) FileSystemNameBuffer.Capacity);
      return Convert.ToString(VolumeSerialNumber);
    }

    public static string GetDnsHostName()
    {
      string str = string.Empty;
      try
      {
        str = Dns.GetHostName();
      }
      catch
      {
      }
      return str;
    }

    public static string GetDnsHostIp()
    {
      string str = string.Empty;
      try
      {
        foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
          str = str + "@" + ipAddress.ToString() + "|" + ((object) ipAddress.AddressFamily).ToString();
      }
      catch
      {
      }
      return str;
    }

    public static string GetMacAddress()
    {
      string str = string.Empty;
      try
      {
        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE DNSHostName='" + Environment.MachineName + "'"))
        {
          using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
          {
            foreach (ManagementObject managementObject in objectCollection)
            {
              if (managementObject["MACAddress"] != null)
              {
                str = managementObject["MACAddress"].ToString();
                break;
              }
            }
          }
        }
      }
      catch
      {
      }
      return str;
    }

    public static XmlElement GetNetworkInfo()
    {
      XObject xobject = new XObject("NetworkInfo");
      string hostName = Dns.GetHostName();
      string machineName = Environment.MachineName;
      xobject.SetAttribute("HostName", hostName);
      xobject.SetAttribute("MachineName", machineName);
      foreach (IPAddress ipAddress in Dns.GetHostEntry(hostName).AddressList)
      {
        XObject xChildObject = new XObject(xobject.CreateElement("HostEntry"));
        xChildObject.SetAttribute("AddressFamily", ((object) ipAddress.AddressFamily).ToString());
        if (((object) ipAddress.AddressFamily).ToString() == ((object) ProtocolFamily.InterNetworkV6).ToString())
          xChildObject.SetAttribute("ScopeId", ipAddress.ScopeId.ToString());
        xChildObject.SetAttribute("IPAddress", ipAddress.ToString());
        xobject.AppendChild(xChildObject);
      }
      XObject xChildObject1 = new XObject(xobject.CreateElement("AdapterInfo"));
      xobject.AppendChild(xChildObject1);
      try
      {
        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE DNSHostName='" + machineName + "'"))
        {
          using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
          {
            foreach (ManagementObject managementObject in objectCollection)
            {
              XObject xChildObject2 = new XObject(xobject.CreateElement("Adapter"));
              if (managementObject["Index"] != null)
                xChildObject2.SetAttribute("Index", managementObject["Index"].ToString());
              if (managementObject["Description"] != null)
                xChildObject2.SetAttribute("Description", managementObject["Description"].ToString());
              if (managementObject["MacAddress"] != null)
                xChildObject2.SetAttribute("MacAddress", managementObject["MacAddress"].ToString());
              if ((bool) managementObject["IPEnabled"])
              {
                string[] strArray = (string[]) managementObject["IPAddress"];
                xChildObject2.SetAttribute("IPAddress", string.Join(",", strArray));
                if (managementObject["DNSHostName"] != null)
                  xChildObject2.SetAttribute("DNSHostName", (string) managementObject["DNSHostName"]);
                if (managementObject["DNSDomain"] != null)
                  xChildObject2.SetAttribute("DNSDomain", (string) managementObject["DNSDomain"]);
              }
              xChildObject1.AppendChild(xChildObject2);
            }
          }
        }
      }
      catch
      {
      }
      return xobject.Element;
    }

    public static string GetDriveSignatureForHardDrive0()
    {
      string str = string.Empty;
      try
      {
        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
        {
          using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
          {
            foreach (ManagementObject managementObject in objectCollection)
            {
              if (managementObject["Signature"] != null && managementObject["DeviceID"] != null && managementObject["DeviceID"].ToString() == "\\\\.\\PHYSICALDRIVE0")
              {
                str = managementObject["Signature"].ToString();
                break;
              }
            }
          }
        }
      }
      catch
      {
      }
      return str;
    }

    public static void AddDocumentSignature(XmlDocument xDocument)
    {
      SignedXml signedXml = new SignedXml(xDocument);
      signedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
      Reference reference = new Reference("");
      reference.AddTransform((Transform) new XmlDsigEnvelopedSignatureTransform());
      signedXml.AddReference(reference);
      RSA key = (RSA) new RSACryptoServiceProvider(new CspParameters()
      {
        Flags = CspProviderFlags.UseMachineKeyStore
      });
      signedXml.SigningKey = (AsymmetricAlgorithm) key;
      KeyInfo keyInfo = new KeyInfo();
      keyInfo.AddClause((KeyInfoClause) new RSAKeyValue(key));
      signedXml.KeyInfo = keyInfo;
      signedXml.ComputeSignature();
      XmlElement xml = signedXml.GetXml();
      xDocument.DocumentElement.AppendChild((XmlNode) xml);
    }

    public static void RemoveDocumentSignature(XmlDocument xDocument)
    {
      for (XmlNodeList elementsByTagName = xDocument.GetElementsByTagName("Signature"); elementsByTagName.Count > 0; elementsByTagName = xDocument.GetElementsByTagName("Signature"))
      {
        XmlNode oldChild = elementsByTagName[0];
        oldChild.ParentNode.RemoveChild(oldChild);
      }
    }

    public static void ReplaceDocumentSignature(XmlDocument xDocument)
    {
      CommonTools.RemoveDocumentSignature(xDocument);
      CommonTools.AddDocumentSignature(xDocument);
    }

    public static bool VerifyDocumentSignature(XmlDocument xDocument)
    {
      bool flag = false;
      if (!xDocument.PreserveWhitespace)
        throw new DexComException("XML Document must be loaded with PreserveWhitespace = true!");
      SignedXml signedXml = new SignedXml(xDocument);
      XmlNodeList elementsByTagName = xDocument.GetElementsByTagName("Signature");
      if (elementsByTagName.Count > 0)
      {
        XmlElement xmlElement = elementsByTagName[0] as XmlElement;
        signedXml.LoadXml(xmlElement);
        if (signedXml.CheckSignature())
          flag = true;
      }
      return flag;
    }

    public static bool VerifyDocumentSignature(string strFilePath)
    {
      bool flag = false;
      string str = strFilePath.Trim();
      if (str.Length > 0 && new FileInfo(str).Exists)
      {
        XmlDocument xDocument = new XmlDocument();
        xDocument.PreserveWhitespace = true;
        try
        {
          xDocument.Load(str);
          flag = CommonTools.VerifyDocumentSignature(xDocument);
        }
        catch (XmlException ex)
        {
        }
      }
      return flag;
    }

    public static bool IsRoleMember(string strRoleName)
    {
      bool flag = false;
      try
      {
        new PrincipalPermission((string) null, strRoleName).Demand();
        flag = true;
      }
      catch (SecurityException ex)
      {
      }
      return flag;
    }

    public static string GetBuiltInAdminstratorsGroupName()
    {
      return new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, (SecurityIdentifier) null).Translate(typeof (NTAccount)).Value;
    }

    public static bool IsUserReallyInAdminGroup()
    {
      try
      {
        return UacSelfEvaluation.IsUserInAdminGroup();
      }
      catch
      {
        return false;
      }
    }

    public static string UTF8ByteArrayToString(byte[] charActers)
    {
      return new UTF8Encoding().GetString(charActers);
    }

    public static byte[] StringToUTF8ByteArray(string pXmlString)
    {
      return new UTF8Encoding().GetBytes(pXmlString);
    }

    public static void CleanupFolder(DirectoryInfo directoryInfo, bool includeSubFolders, string filePattern, TimeSpan olderThan)
    {
      if (directoryInfo.Parent == null || directoryInfo.Parent.Parent == null)
        throw new DexComException("CleanupFolder() called on root folder or root sub-folder.");
      if (directoryInfo.FullName.StartsWith(Environment.SystemDirectory, StringComparison.InvariantCultureIgnoreCase))
        throw new DexComException("CleanupFolder() called on system folder.");
      if (!directoryInfo.Exists)
        return;
      if (includeSubFolders)
      {
        foreach (DirectoryInfo directoryInfo1 in directoryInfo.GetDirectories())
          CommonTools.CleanupFolder(directoryInfo1, includeSubFolders, filePattern, olderThan);
      }
      foreach (FileInfo fileInfo in filePattern != string.Empty ? directoryInfo.GetFiles(filePattern) : directoryInfo.GetFiles())
      {
        if (DateTime.Now - fileInfo.LastWriteTime > olderThan)
        {
          try
          {
            fileInfo.Delete();
          }
          catch
          {
          }
        }
      }
      try
      {
        if (directoryInfo.GetDirectories().Length != 0 || directoryInfo.GetFiles().Length != 0)
          return;
        ((FileSystemInfo) directoryInfo).Delete();
      }
      catch
      {
      }
    }

    public static Image ScaleImage(Image sourceImage, double scale)
    {
      Point location1 = new Point(0, 0);
      Size size1 = sourceImage.Size;
      Point location2 = new Point(0, 0);
      Size size2 = new Size((int) Math.Round((double) size1.Width * scale), (int) Math.Round((double) size1.Height * scale));
      Bitmap bitmap = new Bitmap(size2.Width, size2.Height, sourceImage.PixelFormat);
      bitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.DrawImage(sourceImage, new Rectangle(location2, size2), new Rectangle(location1, size1), GraphicsUnit.Pixel);
      }
      return (Image) bitmap;
    }

    public static Image ResizeImage(Image sourceImage, int newWidth, int newHeight)
    {
      double num1 = (double) newWidth / (double) sourceImage.Width;
      double num2 = (double) newHeight / (double) sourceImage.Height;
      double scale = num2 < num1 ? num2 : num1;
      return CommonTools.ScaleImage(sourceImage, scale);
    }

    public static ImageCodecInfo GetImageEncoder(string imageType)
    {
      imageType = imageType.ToUpperInvariant();
      foreach (ImageCodecInfo imageCodecInfo in ImageCodecInfo.GetImageEncoders())
      {
        if (imageCodecInfo.FormatDescription == imageType)
          return imageCodecInfo;
      }
      return (ImageCodecInfo) null;
    }

    public static void SaveImageAsJpeg(Image image, string filePath, long qualityLevel)
    {
      ImageCodecInfo imageEncoder = CommonTools.GetImageEncoder("JPEG");
      EncoderParameters encoderParams = new EncoderParameters(1);
      encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityLevel);
      image.Save(filePath, imageEncoder, encoderParams);
    }

    public static void SaveImageAsJpeg(Image image, Stream stream, long qualityLevel)
    {
      ImageCodecInfo imageEncoder = CommonTools.GetImageEncoder("JPEG");
      EncoderParameters encoderParams = new EncoderParameters(1);
      encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityLevel);
      image.Save(stream, imageEncoder, encoderParams);
    }

    public static double ConvertDateTimeToMatlabDays(DateTime input)
    {
      return (input - DateTime.MinValue).TotalDays + 367.0;
    }

    public static DateTime ConvertMatlabDaysToDateTime(double input)
    {
      TimeSpan timeSpan = TimeSpan.FromDays(input - 367.0);
      return DateTime.MinValue + timeSpan;
    }

    public static double ConvertDateTimeToExcelDays(DateTime input, bool useExcelBug)
    {
      double num = (input - new DateTime(1900, 1, 1)).TotalDays + 1.0;
      if (useExcelBug && num >= 60.0)
        ++num;
      return num;
    }

    public static DateTime ConvertExcelDaysToDateTime(double input, bool useExcelBug)
    {
      if (useExcelBug && input >= 60.0)
        --input;
      return new DateTime(1900, 1, 1) + TimeSpan.FromDays(input - 1.0);
    }

    public static DateTime TruncateDateTimeToMinutes(DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
    }

    public static TimeSpan LocalClockOffset(out bool connected)
    {
      TimeSpan timeSpan = TimeSpan.Zero;
      connected = false;
      string str1 = ConfigurationManager.AppSettings.Get("AllowInternetTimeLookup");
      string str2 = ConfigurationManager.AppSettings.Get("InternetTimeServer");
      if (string.IsNullOrEmpty(str1) || str1 == "1")
      {
        if (string.IsNullOrEmpty(str2))
          str2 = "0.pool.ntp.org 1.pool.ntp.org 2.pool.ntp.org";
        string str3 = str2;
        char[] separator = new char[1]
        {
          ' '
        };
        int num = 1;
        foreach (string host in str3.Split(separator, (StringSplitOptions) num))
        {
          if (!string.IsNullOrEmpty(host))
          {
            try
            {
              SNTPClient sntpClient = new SNTPClient(host);
              sntpClient.Connect(false, 2000);
              timeSpan = TimeSpan.FromMilliseconds((double) sntpClient.LocalClockOffset);
              connected = true;
              break;
            }
            catch
            {
            }
          }
        }
      }
      return timeSpan;
    }

    [DllImport("kernel32.dll")]
    private static bool SetLocalTime(ref CommonTools.SYSTEMTIME time);

    [SecuritySafeCritical]
    public static void SetLocalTime(DateTime dateTimeNow)
    {
      CommonTools.SYSTEMTIME time;
      time.year = (short) dateTimeNow.Year;
      time.month = (short) dateTimeNow.Month;
      time.dayOfWeek = (short) dateTimeNow.DayOfWeek;
      time.day = (short) dateTimeNow.Day;
      time.hour = (short) dateTimeNow.Hour;
      time.minute = (short) dateTimeNow.Minute;
      time.second = (short) dateTimeNow.Second;
      time.milliseconds = (short) dateTimeNow.Millisecond;
      CommonTools.SetLocalTime(ref time);
    }

    public static double ConvertMG_DL_To_MMOL_L(uint mg_dl)
    {
      return CommonTools.ConvertMG_DL_To_MMOL_L((double) mg_dl);
    }

    public static double ConvertMG_DL_To_MMOL_L(double mg_dl)
    {
      return 50.0 / 901.0 * mg_dl;
    }

    public static double ConvertMG_DL_To_MMOL_L(uint mg_dl, int digits)
    {
      return CommonTools.ConvertMG_DL_To_MMOL_L((double) mg_dl, digits);
    }

    public static double ConvertMG_DL_To_MMOL_L(double mg_dl, int digits)
    {
      return Math.Round(CommonTools.ConvertMG_DL_To_MMOL_L(mg_dl), digits);
    }

    public static uint ConvertMMOL_L_To_MG_DL(double mmol_l)
    {
      return Convert.ToUInt32(mmol_l / (50.0 / 901.0));
    }

    public static bool IsLowGlucose(double glucose, bool isMmol)
    {
      bool flag = false;
      if (isMmol && glucose < CommonTools.LowMmolThreshold && glucose != 0.0)
        flag = true;
      else if (!isMmol && glucose < 40.0 && glucose != 0.0)
        flag = true;
      return flag;
    }

    public static bool IsHighGlucose(double glucose, bool isMmol)
    {
      bool flag = false;
      if (isMmol && glucose > CommonTools.HighMmolThreshold)
        flag = true;
      else if (!isMmol && glucose > 400.0)
        flag = true;
      return flag;
    }

    public static string FormatGlucoseValue(uint mg_dl)
    {
      return CommonTools.FormatGlucoseValue((double) mg_dl, false, false, 0);
    }

    public static string FormatGlucoseValue(double mmol_l, int precision)
    {
      return CommonTools.FormatGlucoseValue(mmol_l, true, false, precision);
    }

    public static string FormatGlucoseValue(double glucose, bool isMmol, bool convertToMmol, int precision)
    {
      string str1 = string.Empty;
      string str2;
      if (CommonTools.IsLowGlucose(glucose, isMmol))
      {
        str2 = "Low";
        if (ProgramContext.Current != null)
        {
          try
          {
            str2 = ProgramContext.Current.ResourceLookup("Common_Low");
          }
          catch
          {
          }
        }
      }
      else if (CommonTools.IsHighGlucose(glucose, isMmol))
      {
        str2 = "High";
        if (ProgramContext.Current != null)
        {
          try
          {
            str2 = ProgramContext.Current.ResourceLookup("Common_High");
          }
          catch
          {
          }
        }
      }
      else
      {
        if (!isMmol && convertToMmol)
          glucose = CommonTools.ConvertMG_DL_To_MMOL_L(glucose);
        str2 = glucose.ToString("N" + precision.ToString());
      }
      return str2;
    }

    public static bool IsRemovableDriveAttached()
    {
      bool flag = false;
      try
      {
        foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
        {
          try
          {
            if (driveInfo.IsReady)
            {
              if (driveInfo.DriveType == DriveType.Removable)
              {
                if (driveInfo.Name != "A:\\")
                {
                  flag = true;
                  break;
                }
              }
            }
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
      return flag;
    }

    public static List<DriveInfo> GetRemovableDriveList()
    {
      List<DriveInfo> list = new List<DriveInfo>();
      try
      {
        foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
        {
          try
          {
            if (driveInfo.IsReady)
            {
              if (driveInfo.DriveType == DriveType.Removable)
              {
                if (driveInfo.Name != "A:\\")
                  list.Add(driveInfo);
              }
            }
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
      return list;
    }

    public static bool HasPKZipFileSignature(byte[] fileContents)
    {
      bool flag = false;
      if (fileContents.Length > 2 && (int) fileContents[0] == 80 && (int) fileContents[1] == 75)
        flag = true;
      return flag;
    }

    private struct SYSTEMTIME
    {
      public short year;
      public short month;
      public short dayOfWeek;
      public short day;
      public short hour;
      public short minute;
      public short second;
      public short milliseconds;
    }
  }
}
