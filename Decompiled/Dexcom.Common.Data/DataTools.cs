// Type: Dexcom.Common.Data.DataTools
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dexcom.Common.Data
{
  public static class DataTools
  {
    public const string FileContentsTag = "FileContents";

    [SecuritySafeCritical]
    public static object ConvertBytesToObject(byte[] bytes, int startOffset, System.Type type)
    {
      int num1 = Marshal.SizeOf(type);
      if (num1 <= 0)
        throw new DexComException("ConvertBytesToObject: Marshal.SizeOf(T) returned a size of 0.");
      if (num1 > bytes.Length - startOffset)
        throw new DexComException("ConvertBytesToObject: Requested offset + size of object exceeds length of bytes buffer to read.");
      IntPtr num2 = IntPtr.Zero;
      try
      {
        num2 = Marshal.AllocHGlobal(num1);
        Marshal.Copy(bytes, startOffset, num2, num1);
        return Marshal.PtrToStructure(num2, type);
      }
      finally
      {
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
      }
    }

    [SecuritySafeCritical]
    public static byte[] ConvertObjectToBytes(object obj)
    {
      byte[] destination = (byte[]) null;
      if (obj != null)
      {
        IntPtr num = IntPtr.Zero;
        int length = Marshal.SizeOf(obj.GetType());
        if (length <= 0)
          throw new DexComException("ConvertObjectToBytes: Marshal.SizeOf(T) returned a size of 0.");
        try
        {
          num = Marshal.AllocHGlobal(length);
          if (num != IntPtr.Zero)
          {
            Marshal.StructureToPtr(obj, num, false);
            destination = new byte[length];
            Marshal.Copy(num, destination, 0, length);
          }
        }
        finally
        {
          if (num != IntPtr.Zero)
            Marshal.FreeHGlobal(num);
        }
      }
      return destination;
    }

    [SecuritySafeCritical]
    public static byte[] ConvertArrayToBytes<T>(T[] array)
    {
      byte[] destination = (byte[]) null;
      if (array != null)
      {
        IntPtr num1 = IntPtr.Zero;
        int num2 = Marshal.SizeOf(typeof (T));
        if (num2 <= 0)
          throw new DexComException("ConvertArrayToBytes: Marshal.SizeOf(T) returned a size of 0.");
        try
        {
          num1 = Marshal.AllocHGlobal(num2);
          if (num1 != IntPtr.Zero)
          {
            int startIndex = 0;
            destination = new byte[num2 * array.Length];
            for (int index = 0; index < array.Length; ++index)
            {
              Marshal.StructureToPtr((object) array[index], num1, false);
              Marshal.Copy(num1, destination, startIndex, num2);
              startIndex += num2;
            }
          }
        }
        finally
        {
          if (num1 != IntPtr.Zero)
            Marshal.FreeHGlobal(num1);
        }
      }
      return destination;
    }

    [SecuritySafeCritical]
    public static T[] ConvertBytesToArray<T>(byte[] bytes)
    {
      T[] objArray = (T[]) null;
      if (bytes != null)
      {
        int num1 = Marshal.SizeOf(typeof (T));
        if (num1 <= 0)
          throw new DexComException("ConvertBytesToArray: Marshal.SizeOf(T) returned a size of 0.");
        if (bytes.Length == 0)
        {
          objArray = new T[0];
        }
        else
        {
          if (bytes.Length % num1 != 0)
            throw new DexComException("ConvertBytesToArray: length of bytes argument not a multiple of type T's size.");
          int length = bytes.Length / num1;
          int startIndex = 0;
          IntPtr num2 = IntPtr.Zero;
          try
          {
            num2 = Marshal.AllocHGlobal(num1);
            objArray = new T[length];
            System.Type structureType = typeof (T);
            for (int index = 0; index < length; ++index)
            {
              Marshal.Copy(bytes, startIndex, num2, num1);
              objArray[index] = (T) Marshal.PtrToStructure(num2, structureType);
              startIndex += num1;
            }
          }
          finally
          {
            if (num2 != IntPtr.Zero)
              Marshal.FreeHGlobal(num2);
          }
        }
      }
      return objArray;
    }

    public static XmlElement CreateCompressedBinaryElement(byte[] sourceData, string name, XmlDocument ownerDocument)
    {
      return DataTools.CreateCompressedElement(sourceData, name, ownerDocument, DataTools.CompressedHintType.Binary);
    }

    public static XmlElement CreateCompressedStringElement(string sourceData, string name, XmlDocument ownerDocument)
    {
      return DataTools.CreateCompressedElement(CommonTools.StringToUTF8ByteArray(sourceData), name, ownerDocument, DataTools.CompressedHintType.String);
    }

    public static XmlElement CreateCompressedXmlElement(XmlElement sourceData, string name, XmlDocument ownerDocument)
    {
      return DataTools.CreateCompressedElement(CommonTools.StringToUTF8ByteArray(sourceData.OuterXml), name, ownerDocument, DataTools.CompressedHintType.XmlElement);
    }

    public static XmlElement CreateCompressedXmlElement(XmlElement sourceData, string name, XmlDocument ownerDocument, int compressionLevel)
    {
      return DataTools.CreateCompressedElement(CommonTools.StringToUTF8ByteArray(sourceData.OuterXml), name, ownerDocument, DataTools.CompressedHintType.XmlElement, compressionLevel);
    }

    public static XmlElement CreateCompressedXmlFragmentElement(string sourceData, string name, XmlDocument ownerDocument)
    {
      return DataTools.CreateCompressedElement(CommonTools.StringToUTF8ByteArray(sourceData), name, ownerDocument, DataTools.CompressedHintType.XmlFragment);
    }

    public static XmlElement CreateCompressedXmlFragmentElement(XmlElement sourceData, string name, XmlDocument ownerDocument)
    {
      return DataTools.CreateCompressedElement(CommonTools.StringToUTF8ByteArray(sourceData.InnerXml), name, ownerDocument, DataTools.CompressedHintType.XmlFragment);
    }

    public static XmlElement CreateCompressedElement(byte[] sourceData, string name, XmlDocument ownerDocument, DataTools.CompressedHintType hintType)
    {
      return DataTools.CreateCompressedElement(sourceData, name, ownerDocument, hintType, 6);
    }

    public static XmlElement CreateCompressedElement(byte[] sourceData, string name, XmlDocument ownerDocument, DataTools.CompressedHintType hintType, int compressionLevel)
    {
      XObject xobject = new XObject(name, ownerDocument);
      if (sourceData != null && sourceData.Length > 0)
      {
        xobject.SetAttribute("Size", sourceData.Length);
        byte[] inArray = DataTools.CompressData(sourceData, compressionLevel);
        if (inArray == null)
          throw new DexComException("Failed to compress data!");
        xobject.SetAttribute("IsCompressed", true);
        xobject.SetAttribute("CompressedSize", inArray.Length);
        xobject.SetAttribute("CompressedHintType", ((object) hintType).ToString());
        xobject.Element.InnerText = Convert.ToBase64String(inArray);
        xobject.SetAttribute("Crc32", Crc.CalculateCrc32(sourceData, 0, sourceData.Length));
      }
      else
      {
        xobject.SetAttribute("Size", 0);
        xobject.SetAttribute("IsCompressed", false);
        xobject.SetAttribute("CompressedSize", 0);
        xobject.SetAttribute("CompressedHintType", ((object) hintType).ToString());
        xobject.SetAttribute("Crc32", 0);
      }
      return xobject.Element;
    }

    public static byte[] DecompressElement(XmlElement sourceElement)
    {
      DataTools.CompressedHintType hintType = DataTools.CompressedHintType.Unknown;
      return DataTools.DecompressElement(sourceElement, out hintType);
    }

    public static object DecompressElementWithHint(XmlElement sourceElement)
    {
      DataTools.CompressedHintType hintType = DataTools.CompressedHintType.Unknown;
      byte[] numArray = DataTools.DecompressElement(sourceElement, out hintType);
      if (hintType == DataTools.CompressedHintType.Binary || hintType == DataTools.CompressedHintType.Unknown)
        return (object) numArray;
      if (hintType == DataTools.CompressedHintType.String)
        return (object) CommonTools.UTF8ByteArrayToString(numArray);
      if (hintType == DataTools.CompressedHintType.XmlElement)
      {
        XmlDocument xmlDocument = new XmlDocument();
        using (MemoryStream memoryStream = new MemoryStream(numArray))
          xmlDocument.Load((Stream) memoryStream);
        return (object) xmlDocument.DocumentElement;
      }
      else
      {
        if (hintType != DataTools.CompressedHintType.XmlFragment)
          return (object) numArray;
        XmlDocumentFragment documentFragment = new XmlDocument().CreateDocumentFragment();
        documentFragment.InnerXml = CommonTools.UTF8ByteArrayToString(numArray);
        return (object) documentFragment;
      }
    }

    public static byte[] DecompressElement(XmlElement sourceElement, out DataTools.CompressedHintType hintType)
    {
      byte[] buf = new byte[0];
      XObject xobject = new XObject(sourceElement);
      int attribute1 = xobject.GetAttribute<int>("Size");
      bool attribute2 = xobject.GetAttribute<bool>("IsCompressed");
      xobject.GetAttribute<int>("CompressedSize");
      DataTools.CompressedHintType compressedHintType = DataTools.CompressedHintType.Binary;
      if (xobject.HasAttribute("CompressedHintType"))
        compressedHintType = (DataTools.CompressedHintType) xobject.GetAttributeAsEnum("CompressedHintType", typeof (DataTools.CompressedHintType));
      hintType = compressedHintType;
      if (attribute2)
      {
        buf = DataTools.DecompressData(Convert.FromBase64String(xobject.Element.InnerText), attribute1);
        if (attribute1 != buf.Length)
          throw new DexComException("Failed to extract binary data from xml element: Mismatched Size!");
        if (xobject.HasAttribute("Crc32") && (int) xobject.GetAttribute<uint>("Crc32") != (int) Crc.CalculateCrc32(buf, 0, buf.Length))
          throw new DexComException("Failed to extract binary data from xml element: Invalid CRC!");
      }
      return buf;
    }

    public static bool IsCompressedElement(XmlElement sourceElement)
    {
      XObject xobject = new XObject(sourceElement);
      return xobject.IsNotNull() && xobject.HasAttribute("Size") && xobject.HasAttribute("IsCompressed") && xobject.HasAttribute("CompressedSize");
    }

    public static XmlElement CreateBinaryXmlElement(byte[] sourceData, string name, XmlDocument ownerDocument)
    {
      XObject xobject = new XObject(name, ownerDocument);
      xobject.SetAttribute("IsBinary", true);
      if (sourceData != null && sourceData.Length > 0)
      {
        xobject.SetAttribute("Size", sourceData.Length);
        xobject.SetAttribute("Crc32", Crc.CalculateCrc32(sourceData, 0, sourceData.Length));
        xobject.Element.InnerText = Convert.ToBase64String(sourceData);
      }
      else
      {
        xobject.SetAttribute("Size", 0);
        xobject.SetAttribute("Crc32", 0);
      }
      return xobject.Element;
    }

    public static bool IsBinaryXmlElement(XmlElement sourceElement)
    {
      XObject xobject = new XObject(sourceElement);
      return xobject.IsNotNull() && xobject.HasAttribute("IsBinary") && xobject.HasAttribute("Size") && xobject.HasAttribute("Crc32");
    }

    public static byte[] ExtractBinaryXmlElement(XmlElement sourceElement)
    {
      byte[] buf = new byte[0];
      XObject xobject = new XObject(sourceElement);
      int attribute1 = xobject.GetAttribute<int>("Size");
      bool attribute2 = xobject.GetAttribute<bool>("IsBinary");
      uint attribute3 = xobject.GetAttribute<uint>("Crc32");
      if (attribute2)
      {
        buf = Convert.FromBase64String(xobject.Element.InnerText);
        if (attribute1 != buf.Length)
          throw new DexComException("Failed to extract binary data from xml element: Mismatched Size!");
        uint num = Crc.CalculateCrc32(buf, 0, buf.Length);
        if ((int) attribute3 != (int) num)
          throw new DexComException("Failed to extract binary data from xml element: Invalid CRC!");
      }
      return buf;
    }

    public static byte[] CompressData(byte[] originalData)
    {
      return DataTools.CompressData(originalData, -1);
    }

    public static byte[] CompressData(byte[] originalData, int compressionLevel)
    {
      byte[] numArray = (byte[]) null;
      if (originalData != null && originalData.Length > 0)
      {
        Deflater deflater = new Deflater(compressionLevel, false);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) memoryStream, deflater))
          {
            deflaterOutputStream.Write(originalData, 0, originalData.Length);
            deflaterOutputStream.Flush();
            deflaterOutputStream.Close();
            numArray = memoryStream.ToArray();
          }
        }
      }
      return numArray;
    }

    public static byte[] DecompressData(byte[] compressedData, int originalSize)
    {
      byte[] numArray1 = (byte[]) null;
      if (compressedData != null && originalSize > 0 && compressedData.Length > 0)
      {
        byte[] buffer = new byte[(int) ushort.MaxValue];
        Inflater inflater = new Inflater(false);
        using (MemoryStream memoryStream = new MemoryStream(compressedData))
        {
          using (InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) memoryStream, inflater, buffer.Length))
          {
            byte[] numArray2 = new byte[originalSize];
            int destinationIndex = 0;
            while (true)
            {
              int length = inflaterInputStream.Read(buffer, 0, buffer.Length);
              if (length > 0)
              {
                Array.Copy((Array) buffer, 0, (Array) numArray2, destinationIndex, length);
                destinationIndex += length;
              }
              else
                break;
            }
            inflaterInputStream.Close();
            if (destinationIndex != originalSize)
              throw new DexComException("Failed to decompress correct size for data!");
            numArray1 = numArray2;
          }
        }
      }
      return numArray1;
    }

    public static XFolderScan ScanFolder(DirectoryInfo directoryInfo, bool includeSubFolders, bool includeFiles, string filePattern)
    {
      return DataTools.ScanFolder(directoryInfo, includeSubFolders, includeFiles, new List<string>()
      {
        filePattern
      }, (BackgroundWorker) null);
    }

    public static XFolderScan ScanFolder(DirectoryInfo directoryInfo, bool includeSubFolders, bool includeFiles, List<string> filePatterns, BackgroundWorker backgroundWorker)
    {
      XFolderScan xfolderScan = (XFolderScan) null;
      if (directoryInfo.Exists)
      {
        List<string> filePatterns1 = new List<string>();
        if (includeFiles && filePatterns != null)
        {
          foreach (string str in filePatterns)
          {
            if (includeFiles && str != null && str.Trim().Length > 0)
            {
              string file_pattern = str.Trim();
              if (filePatterns1.FindIndex((Predicate<string>) (item => string.Compare(item, file_pattern, StringComparison.InvariantCultureIgnoreCase) == 0)) == -1)
                filePatterns1.Add(file_pattern);
            }
          }
          if (filePatterns1.Count == 0)
            filePatterns1.Add(string.Empty);
        }
        string str1 = string.Empty;
        if (includeFiles && filePatterns1.Count > 0)
          str1 = string.Join(":", filePatterns1.ToArray());
        xfolderScan = new XFolderScan();
        xfolderScan.FolderPath = directoryInfo.FullName;
        xfolderScan.IncludeSubFolders = includeSubFolders;
        xfolderScan.IncludeFiles = includeFiles;
        xfolderScan.FilePattern = str1;
        XFolderInfo xfolderInfo = DataTools.DoGetXFolderInfo(directoryInfo, xfolderScan.Element.OwnerDocument);
        if ((includeFiles || includeSubFolders) && (xfolderInfo != null && xfolderInfo.IsNotNull()))
          DataTools.DoScanFolderAndFileInfo(directoryInfo, xfolderInfo, includeSubFolders, includeFiles, filePatterns1, new DataTools.FolderScanProgress(), backgroundWorker);
        xfolderScan.AppendChild((XObject) xfolderInfo);
      }
      return xfolderScan;
    }

    private static void DoScanFolderAndFileInfo(DirectoryInfo directoryInfo, XFolderInfo xParentFolderInfo, bool includeSubFolders, bool includeFiles, List<string> filePatterns, DataTools.FolderScanProgress scanProgress, BackgroundWorker backgroundWorker)
    {
      if (backgroundWorker != null && backgroundWorker.CancellationPending || !directoryInfo.Exists)
        return;
      if (backgroundWorker != null)
      {
        ++scanProgress.FolderCount;
        backgroundWorker.ReportProgress(1, (object) scanProgress);
      }
      XmlDocument ownerDocument = xParentFolderInfo.Element.OwnerDocument;
      if (includeSubFolders)
      {
        try
        {
          DirectoryInfo[] directories = directoryInfo.GetDirectories();
          if (directories.Length > 0)
          {
            XObject xChildObject = new XObject("Folders", ownerDocument);
            xParentFolderInfo.AppendChild(xChildObject);
            xChildObject.SetAttribute("Count", directories.Length);
            foreach (DirectoryInfo directoryInfo1 in directories)
            {
              try
              {
                if (backgroundWorker != null)
                {
                  if (backgroundWorker.CancellationPending)
                    break;
                }
                XFolderInfo xfolderInfo = DataTools.DoGetXFolderInfo(directoryInfo1, ownerDocument);
                if (xfolderInfo != null)
                {
                  if (xfolderInfo.IsNotNull())
                  {
                    DataTools.DoScanFolderAndFileInfo(directoryInfo1, xfolderInfo, includeSubFolders, includeFiles, filePatterns, scanProgress, backgroundWorker);
                    xChildObject.AppendChild((XObject) xfolderInfo);
                  }
                }
              }
              catch (UnauthorizedAccessException ex)
              {
              }
            }
          }
        }
        catch (UnauthorizedAccessException ex)
        {
        }
      }
      if (!includeFiles)
        return;
      try
      {
        foreach (string searchPattern in filePatterns)
        {
          FileInfo[] fileInfoArray = searchPattern != string.Empty ? directoryInfo.GetFiles(searchPattern) : directoryInfo.GetFiles();
          if (fileInfoArray.Length > 0)
          {
            XObject xChildObject = new XObject("Files", ownerDocument);
            xParentFolderInfo.AppendChild(xChildObject);
            xChildObject.SetAttribute("Count", fileInfoArray.Length);
            foreach (FileInfo fileInfo in fileInfoArray)
            {
              try
              {
                if (backgroundWorker != null)
                {
                  if (backgroundWorker.CancellationPending)
                    break;
                }
                XFileInfo xfileInfo = DataTools.DoGetXFileInfo(fileInfo, ownerDocument);
                if (xfileInfo != null)
                {
                  if (xfileInfo.IsNotNull())
                  {
                    xChildObject.AppendChild((XObject) xfileInfo);
                    if (backgroundWorker != null)
                    {
                      ++scanProgress.FileCount;
                      backgroundWorker.ReportProgress(2, (object) scanProgress);
                    }
                  }
                }
              }
              catch (UnauthorizedAccessException ex)
              {
              }
            }
          }
        }
      }
      catch (UnauthorizedAccessException ex)
      {
      }
    }

    private static XFolderInfo DoGetXFolderInfo(DirectoryInfo directoryInfo, XmlDocument ownerDoc)
    {
      XFolderInfo xfolderInfo = (XFolderInfo) null;
      if (directoryInfo.Exists)
      {
        xfolderInfo = new XFolderInfo(ownerDoc);
        xfolderInfo.Name = Path.GetFileName(directoryInfo.FullName);
        xfolderInfo.FolderPath = directoryInfo.FullName;
      }
      return xfolderInfo;
    }

    public static XFolderInfo GetXFolderInfo(string strFolderPath)
    {
      return DataTools.DoGetXFolderInfo(new DirectoryInfo(strFolderPath), new XmlDocument());
    }

    public static XFolderInfo GetXFolderInfo(DirectoryInfo directoryInfo)
    {
      return DataTools.DoGetXFolderInfo(directoryInfo, new XmlDocument());
    }

    private static XFileInfo DoGetXFileInfo(FileInfo fileInfo, XmlDocument ownerDoc)
    {
      XFileInfo xfileInfo = (XFileInfo) null;
      if (fileInfo.Exists)
      {
        xfileInfo = new XFileInfo(ownerDoc);
        xfileInfo.FilePath = fileInfo.FullName;
        xfileInfo.Name = fileInfo.Name;
        xfileInfo.Basename = Path.GetFileNameWithoutExtension(fileInfo.Name);
        xfileInfo.Extension = fileInfo.Extension;
        xfileInfo.Length = fileInfo.Length;
        xfileInfo.DateCreated = fileInfo.CreationTime;
        xfileInfo.DateModified = fileInfo.LastWriteTime;
      }
      return xfileInfo;
    }

    public static XFileInfo GetXFileInfo(string strFilePath)
    {
      return DataTools.GetXFileInfo(new FileInfo(strFilePath), new XmlDocument());
    }

    public static XFileInfo GetXFileInfo(FileInfo fileInfo)
    {
      return DataTools.GetXFileInfo(fileInfo, new XmlDocument());
    }

    public static XFileInfo GetXFileInfo(FileInfo fileInfo, XmlDocument ownerDoc)
    {
      return DataTools.DoGetXFileInfo(fileInfo, ownerDoc);
    }

    public static void AttachFileContents(XFileInfo xFileInfo, bool isCompressed)
    {
      if (!File.Exists(xFileInfo.FilePath))
        return;
      byte[] contents = File.ReadAllBytes(xFileInfo.FilePath);
      DataTools.AttachFileContents(xFileInfo, contents, isCompressed);
    }

    public static void AttachFileContents(XFileInfo xFileInfo, byte[] contents, bool isCompressed)
    {
      XmlElement xmlElement1 = !isCompressed ? DataTools.CreateBinaryXmlElement(contents, "FileContents", xFileInfo.Element.OwnerDocument) : DataTools.CreateCompressedBinaryElement(contents, "FileContents", xFileInfo.Element.OwnerDocument);
      XmlElement xmlElement2 = DataTools.FileContentsElement(xFileInfo);
      if (xmlElement2 == null)
        xFileInfo.Element.AppendChild((XmlNode) xmlElement1);
      else
        xFileInfo.Element.ReplaceChild((XmlNode) xmlElement1, (XmlNode) xmlElement2);
    }

    public static XmlElement FileContentsElement(this XFileInfo xFileInfo)
    {
      return xFileInfo.Element.SelectSingleNode("FileContents") as XmlElement;
    }

    public static byte[] ExtractFileContents(this XFileInfo xFileInfo)
    {
      byte[] numArray = (byte[]) null;
      XmlElement sourceElement = DataTools.FileContentsElement(xFileInfo);
      if (sourceElement != null)
      {
        if (DataTools.IsCompressedElement(sourceElement))
          numArray = DataTools.DecompressElement(sourceElement);
        else if (DataTools.IsBinaryXmlElement(sourceElement))
          numArray = DataTools.ExtractBinaryXmlElement(sourceElement);
      }
      return numArray;
    }

    public static XmlDocument CreateApplicationXmlDocument(string documentContents, bool addSignature)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      XmlDocument ownerDocument = new XmlDocument();
      XObject xobject = new XObject(documentContents, ownerDocument);
      ownerDocument.AppendChild((XmlNode) xobject.Element);
      xobject.Id = Guid.NewGuid();
      xobject.SetAttribute("DateTimeCreated", now);
      xobject.SetAttribute("DateTimeCreatedLocal", now.LocalDateTime);
      xobject.SetAttribute("DateTimeCreatedUtc", now.UtcDateTime);
      XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(ownerDocument);
      xobject.AppendChild((XObject) xapplicationInfo);
      XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(ownerDocument);
      xobject.AppendChild((XObject) xcomputerInfo);
      XmlDocument xDocument = new XmlDocument();
      xDocument.PreserveWhitespace = true;
      xDocument.LoadXml(CommonTools.FormatXml(ownerDocument.DocumentElement.OuterXml));
      XmlDeclaration xmlDeclaration = xDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
      xDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xDocument.DocumentElement);
      if (addSignature)
        CommonTools.AddDocumentSignature(xDocument);
      return xDocument;
    }

    public static XComputerInfo GetXComputerInfo(XmlDocument ownerDocument)
    {
      XComputerInfo xcomputerInfo = new XComputerInfo(ownerDocument);
      xcomputerInfo.HostName = CommonTools.GetDnsHostName();
      xcomputerInfo.HostIp = CommonTools.GetDnsHostIp();
      xcomputerInfo.MACAddress = CommonTools.GetMacAddress();
      xcomputerInfo.DriveId = CommonTools.GetDriveSignatureForHardDrive0();
      xcomputerInfo.VolumeId = CommonTools.GetVolumeSerialNumber();
      xcomputerInfo.HardwareId = CommonTools.GetHardwareId();
      xcomputerInfo.MachineName = Environment.MachineName;
      xcomputerInfo.OSVersion = Environment.OSVersion.ToString();
      xcomputerInfo.UserDomainName = Environment.UserDomainName;
      xcomputerInfo.UserName = Environment.UserName;
      xcomputerInfo.ClrVersion = ((object) Environment.Version).ToString();
      xcomputerInfo.OSDirectory = Environment.SystemDirectory;
      try
      {
        XObject xChildObject1 = new XObject("OperatingSystems", ownerDocument);
        xcomputerInfo.AppendChild(xChildObject1);
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
        {
          XObject xChildObject2 = new XObject("OperatingSystem", ownerDocument);
          xChildObject1.AppendChild(xChildObject2);
          xChildObject2.SetAttribute("Caption", managementObject["Caption"] as string);
          xChildObject2.SetAttribute("Version", managementObject["CSDVersion"] as string);
          xChildObject2.SetAttribute("BuildNumber", managementObject["BuildNumber"] as string);
          string strValue = managementObject["OSArchitecture"] as string;
          xChildObject2.SetAttribute("OSArchitecture", strValue);
          xcomputerInfo.OSArchitecture = strValue;
        }
      }
      catch
      {
      }
      return xcomputerInfo;
    }

    public static XObject GetFirstXOperatingSystem()
    {
      XObject xobject = new XObject("OperatingSystem");
      try
      {
        using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get().GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            ManagementObject managementObject = (ManagementObject) enumerator.Current;
            xobject.SetAttribute("Caption", managementObject["Caption"] as string);
            xobject.SetAttribute("Version", managementObject["CSDVersion"] as string);
            xobject.SetAttribute("BuildNumber", managementObject["BuildNumber"] as string);
            string strValue = managementObject["OSArchitecture"] as string;
            xobject.SetAttribute("OSArchitecture", strValue);
          }
        }
      }
      catch
      {
      }
      return xobject;
    }

    public static string GetOperatingSystemKind()
    {
      string str = string.Empty;
      XObject xoperatingSystem = DataTools.GetFirstXOperatingSystem();
      if (xoperatingSystem.HasAttribute("Caption"))
      {
        string attribute = xoperatingSystem.GetAttribute<string>("Caption");
        if (!string.IsNullOrEmpty(attribute))
        {
          if (attribute.Contains("Vista"))
            str = "Vista";
          else if (attribute.Contains("XP"))
            str = "XP";
          else if (attribute.Contains(" 7"))
            str = "7";
          else if (attribute.Contains("2008 R2"))
            str = "2008 R2";
          else if (attribute.Contains("2008"))
            str = "2008";
          else if (attribute.Contains("2003"))
            str = "2003";
        }
      }
      return str;
    }

    public static void DeidentifyComputerInfo(XmlElement xml)
    {
      if (xml == null)
        return;
      foreach (XmlElement otherElement in xml.SelectNodes(string.Format("//{0}", (object) "ComputerInfo")))
        new XComputerInfo(otherElement).Deidentify();
    }

    public static XComputerInfo GetXComputerInfo()
    {
      return DataTools.GetXComputerInfo(new XmlDocument());
    }

    [DllImport("kernel32", SetLastError = true)]
    public static IntPtr LoadLibrary(string libraryName);

    [DllImport("kernel32", SetLastError = true)]
    public static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

    public static bool IsOS64Bit()
    {
      return IntPtr.Size == 8 || IntPtr.Size == 4 && DataTools.Is32BitProcessOn64BitProcessor();
    }

    [SecuritySafeCritical]
    private static DataTools.IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
    {
      IntPtr hwnd = DataTools.LoadLibrary("kernel32");
      if (hwnd != IntPtr.Zero)
      {
        IntPtr procAddress = DataTools.GetProcAddress(hwnd, "IsWow64Process");
        if (procAddress != IntPtr.Zero)
          return (DataTools.IsWow64ProcessDelegate) Marshal.GetDelegateForFunctionPointer(procAddress, typeof (DataTools.IsWow64ProcessDelegate));
      }
      return (DataTools.IsWow64ProcessDelegate) null;
    }

    private static bool Is32BitProcessOn64BitProcessor()
    {
      DataTools.IsWow64ProcessDelegate wow64ProcessDelegate = DataTools.GetIsWow64ProcessDelegate();
      bool isWow64Process;
      if (wow64ProcessDelegate == null || !wow64ProcessDelegate(Process.GetCurrentProcess().Handle, out isWow64Process))
        return false;
      else
        return isWow64Process;
    }

    public static XApplicationInfo GetXApplicationInfo(XmlDocument ownerDocument)
    {
      XApplicationInfo xapplicationInfo = new XApplicationInfo(ownerDocument);
      xapplicationInfo.ExecutablePath = Application.ExecutablePath;
      xapplicationInfo.StartupPath = Application.StartupPath;
      xapplicationInfo.ProductName = Application.ProductName;
      xapplicationInfo.ProductVersion = Application.ProductVersion;
      string str1 = "";
      string str2 = "";
      try
      {
        str1 = AssemblyUtils.Version;
        str2 = AssemblyUtils.FileVersion;
      }
      catch
      {
      }
      xapplicationInfo.AssemblyVersion = str1;
      xapplicationInfo.AssemblyFileVersion = str2;
      return xapplicationInfo;
    }

    public static XApplicationInfo GetXApplicationInfo()
    {
      return DataTools.GetXApplicationInfo(new XmlDocument());
    }

    public static XmlDocument EnsureXmlRootObject(XmlElement xRoot)
    {
      return DataTools.EnsureXmlRootObject(xRoot, true);
    }

    public static XmlDocument EnsureXmlRootObject(XmlElement xRoot, bool insertXmlDeclaration)
    {
      XmlDocument xmlDocument;
      if (xRoot.OwnerDocument.DocumentElement == null)
      {
        xRoot.OwnerDocument.AppendChild((XmlNode) xRoot);
        xmlDocument = xRoot.OwnerDocument;
        if (insertXmlDeclaration)
        {
          XmlDeclaration xmlDeclaration = xRoot.OwnerDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
          xRoot.OwnerDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xRoot.OwnerDocument.DocumentElement);
        }
      }
      else if (xRoot.OwnerDocument.DocumentElement != xRoot)
      {
        xmlDocument = new XmlDocument();
        XmlNode newChild = xmlDocument.ImportNode((XmlNode) xRoot, true);
        xmlDocument.AppendChild(newChild);
        if (insertXmlDeclaration)
        {
          XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
          xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xmlDocument.DocumentElement);
        }
      }
      else
        xmlDocument = xRoot.OwnerDocument;
      return xmlDocument;
    }

    public static DataRow AddDataToTable(DataTable table, ColumnInfoAttribute[] columnInfo, object dataObject)
    {
      DataRow row = table.NewRow();
      foreach (ColumnInfoAttribute columnInfoAttribute in columnInfo)
      {
        try
        {
          row[columnInfoAttribute.Index] = columnInfoAttribute.PropertyDescriptor.GetValue(dataObject);
        }
        catch (TargetInvocationException ex)
        {
          row[columnInfoAttribute.Index] = (object) DBNull.Value;
        }
      }
      table.Rows.Add(row);
      return row;
    }

    public static DataRow AddDataToTable(DataTable table, ColumnInfoAttribute[] columnInfo, XObject dataObject)
    {
      DataRow row = table.NewRow();
      foreach (ColumnInfoAttribute columnInfoAttribute in columnInfo)
      {
        try
        {
          row[columnInfoAttribute.Index] = dataObject.GetAttribute(columnInfoAttribute.Name, columnInfoAttribute.ColumnType);
        }
        catch (TargetInvocationException ex)
        {
          row[columnInfoAttribute.Index] = (object) DBNull.Value;
        }
      }
      table.Rows.Add(row);
      return row;
    }

    public static void AddDataToTable(DataTable table, ColumnInfoAttribute[] columnInfo, IEnumerable itemList)
    {
      foreach (object dataObject in itemList)
        DataTools.AddDataToTable(table, columnInfo, dataObject);
    }

    public static void AddDataToTable(DataTable table, System.Type itemType, IEnumerable itemList)
    {
      ColumnInfoAttribute[] columnInfo = ColumnInfoAttribute.GetColumnInfo(itemType);
      DataTools.AddDataToTable(table, columnInfo, itemList);
    }

    public static DataTable CreateDataTable(string strTableName, ColumnInfoAttribute[] columnInfo)
    {
      DataTable dataTable = new DataTable();
      dataTable.TableName = strTableName;
      foreach (ColumnInfoAttribute columnInfoAttribute in columnInfo)
        dataTable.Columns.Add(new DataColumn(columnInfoAttribute.Name, columnInfoAttribute.ColumnType)
        {
          Caption = columnInfoAttribute.DisplayName
        });
      return dataTable;
    }

    public static DataTable CreateDataTable(System.Type itemType)
    {
      ColumnInfoAttribute[] columnInfo = ColumnInfoAttribute.GetColumnInfo(itemType);
      return DataTools.CreateDataTable(itemType.Name, columnInfo);
    }

    public enum CompressedHintType
    {
      Unknown,
      Binary,
      String,
      XmlElement,
      XmlFragment,
    }

    public class FolderScanProgress
    {
      public int FileCount { get; set; }

      public int FolderCount { get; set; }
    }

    private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, out bool isWow64Process);
  }
}
