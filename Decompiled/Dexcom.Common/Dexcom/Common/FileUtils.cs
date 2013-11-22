// Type: Dexcom.Common.FileUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Dexcom.Common
{
  public class FileUtils
  {
    public static bool ReadFileContentsChunk(string strFilePath, int startPos, int chunkSize, out byte[] fileData)
    {
      using (FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        return FileUtils.ReadFileContentsChunk(fileStream, startPos, chunkSize, out fileData);
    }

    public static bool ReadFileContentsChunk(FileStream fileStream, int startPos, int chunkSize, out byte[] fileData)
    {
      fileData = (byte[]) null;
      if (startPos < 0)
        throw new ArgumentOutOfRangeException("startPos", (object) startPos, "Starting position must be greater than or equal to zero.");
      if (chunkSize <= 0)
        throw new ArgumentOutOfRangeException("chunkSize", (object) chunkSize, "Size of chunk to read must be greater than zero.");
      if (fileStream == null)
        throw new ArgumentNullException("fileStream");
      if (!fileStream.CanSeek || !fileStream.CanRead)
        throw new ArgumentException("File stream argument is invalid, closed, or can not read/seek.", "fileStream");
      string name = fileStream.Name;
      long length = fileStream.Length;
      bool flag = (long) (startPos + chunkSize) < length;
      if ((long) startPos >= length && length != 0L)
        throw new DexComException(string.Format("Attempt to start reading past end of file.\r\nFilename='{0}'", (object) name));
      if (length > 0L)
      {
        if (fileStream.Seek((long) startPos, SeekOrigin.Begin) != (long) startPos)
          throw new DexComException(string.Format("Failed to seek to required starting position calling FileStream.Seek({0}).\r\nFilename='{1}'", (object) startPos, (object) name));
        int count = chunkSize;
        if ((long) (startPos + chunkSize) > length)
          count = (int) length - startPos;
        byte[] buffer = new byte[count];
        int num1 = count;
        int offset = 0;
        do
        {
          int num2 = fileStream.Read(buffer, offset, count);
          if (num2 != 0)
          {
            offset += num2;
            count -= num2;
          }
          else
            break;
        }
        while (count > 0);
        if (offset != num1)
          throw new DexComException(string.Format("Failed to read required number of bytes calling FileStream.Read()\r\nFilename='{0}'.", (object) name));
        fileData = buffer;
      }
      else
        fileData = new byte[0];
      return flag;
    }

    public static void WriteFileContentsChunk(MemoryStream fileStream, byte[] fileData, int startPos, int chunkSize)
    {
      if (startPos < 0)
        throw new ArgumentOutOfRangeException("startPos", (object) startPos, "Starting position must be greater than or equal to zero.");
      if (startPos != 0 && chunkSize <= 0)
        throw new ArgumentOutOfRangeException("chunkSize", (object) chunkSize, "Size of chunk to write must be greater than or equal to zero.");
      if (chunkSize > 0)
      {
        if (fileData == null)
          throw new ArgumentNullException("fileData");
        if (fileData.Length < chunkSize)
          throw new ArgumentException("Requested chunk size to write is larger than file data array.");
      }
      if (fileStream == null)
        throw new ArgumentNullException("fileStream");
      if (!fileStream.CanSeek || !fileStream.CanWrite)
        throw new ArgumentException("File stream argument is invalid, closed, or can not write/seek.", "fileStream");
      long length = fileStream.Length;
      if (length != (long) startPos)
        throw new DexComException("Attempt to write file chunk out of sequence.");
      if (chunkSize <= 0)
        return;
      try
      {
        if (startPos == 0)
          fileStream.SetLength((long) chunkSize);
        else
          fileStream.SetLength(fileStream.Length + (long) chunkSize);
        if (fileStream.Seek(length, SeekOrigin.Begin) != (long) startPos)
          throw new DexComException(string.Format("Failed to seek to required starting position calling FileStream.Seek({0}).", (object) startPos));
        fileStream.Write(fileData, 0, chunkSize);
      }
      catch (Exception ex)
      {
        fileStream.SetLength(length);
        throw;
      }
    }

    public static void WriteFileContentsChunk(string strFilePath, byte[] fileData, int startPos, int chunkSize)
    {
      if (startPos < 0)
        throw new ArgumentOutOfRangeException("startPos", (object) startPos, "Starting position must be greater than or equal to zero.");
      if (startPos != 0 && chunkSize <= 0)
        throw new ArgumentOutOfRangeException("chunkSize", (object) chunkSize, "Size of chunk to write must be greater than or equal to zero.");
      if (chunkSize > 0)
      {
        if (fileData == null)
          throw new ArgumentNullException("fileData");
        if (fileData.Length < chunkSize)
          throw new ArgumentException("Requested chunk size to write is larger than file data array.");
      }
      FileMode mode = startPos != 0 ? FileMode.Open : FileMode.Create;
      using (FileStream fileStream = new FileStream(strFilePath, mode, FileAccess.Write, FileShare.None))
        FileUtils.WriteFileContentsChunk(fileStream, fileData, startPos, chunkSize);
    }

    public static void WriteFileContentsChunk(FileStream fileStream, byte[] fileData, int startPos, int chunkSize)
    {
      if (startPos < 0)
        throw new ArgumentOutOfRangeException("startPos", (object) startPos, "Starting position must be greater than or equal to zero.");
      if (startPos != 0 && chunkSize <= 0)
        throw new ArgumentOutOfRangeException("chunkSize", (object) chunkSize, "Size of chunk to write must be greater than or equal to zero.");
      if (chunkSize > 0)
      {
        if (fileData == null)
          throw new ArgumentNullException("fileData");
        if (fileData.Length < chunkSize)
          throw new ArgumentException("Requested chunk size to write is larger than file data array.");
      }
      if (fileStream == null)
        throw new ArgumentNullException("fileStream");
      if (!fileStream.CanSeek || !fileStream.CanWrite)
        throw new ArgumentException("File stream argument is invalid, closed, or can not write/seek.", "fileStream");
      string name = fileStream.Name;
      if (fileStream.Length != (long) startPos)
        throw new DexComException(string.Format("Attempt to write file chunk out of sequence.\r\nFilename='{0}'", (object) name));
      if (chunkSize <= 0)
        return;
      if (fileStream.Seek(0L, SeekOrigin.End) != (long) startPos)
        throw new DexComException(string.Format("Failed to seek to required starting position calling FileStream.Seek({0}).\r\nFilename='{1}'", (object) startPos, (object) name));
      fileStream.Write(fileData, 0, chunkSize);
    }

    public static void WriteTextFile(string strFilePath, string strContents)
    {
      bool append = false;
      UTF8Encoding utF8Encoding = new UTF8Encoding(true, true);
      using (StreamWriter streamWriter = new StreamWriter(strFilePath, append, (Encoding) utF8Encoding))
        streamWriter.Write(strContents);
    }

    public static void WriteTextFile(string strFilePath, string strContents, Encoding textEncoding)
    {
      bool append = false;
      using (StreamWriter streamWriter = new StreamWriter(strFilePath, append, textEncoding))
        streamWriter.Write(strContents);
    }

    public static string ReadTextFile(string strFilePath)
    {
      bool detectEncodingFromByteOrderMarks = true;
      UTF8Encoding utF8Encoding = new UTF8Encoding(true, true);
      string str = (string) null;
      try
      {
        using (StreamReader streamReader = new StreamReader(strFilePath, (Encoding) utF8Encoding, detectEncodingFromByteOrderMarks))
          str = streamReader.ReadToEnd();
      }
      catch (ArgumentException ex)
      {
      }
      if (str == null)
      {
        using (StreamReader streamReader = new StreamReader(strFilePath, Encoding.Default, false))
        {
          str = streamReader.ReadToEnd();
          if (str.Length > 4)
          {
            if ((int) str[1] == 0)
            {
              if ((int) str[3] == 0)
                str = (string) null;
            }
          }
        }
        if (str == null)
        {
          using (StreamReader streamReader = new StreamReader(strFilePath, Encoding.Unicode, false))
            str = streamReader.ReadToEnd();
        }
      }
      return str;
    }

    public static string ReadTextFile(string strFilePath, Encoding textEncoding)
    {
      bool detectEncodingFromByteOrderMarks = true;
      using (StreamReader streamReader = new StreamReader(strFilePath, textEncoding, detectEncodingFromByteOrderMarks))
        return streamReader.ReadToEnd();
    }

    public static bool CreateLockFile(string lockFilePath, XObject xLockInfo, out Exception exception)
    {
      bool flag = false;
      exception = (Exception) null;
      if (!File.Exists(lockFilePath))
      {
        try
        {
          using (FileStream fileStream = File.Open(lockFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read | FileShare.Delete))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream))
              streamWriter.Write(xLockInfo.Xml);
          }
          flag = true;
        }
        catch (IOException ex)
        {
          exception = (Exception) ex;
        }
        catch (Exception ex)
        {
          exception = ex;
        }
      }
      return flag;
    }

    public static bool DeleteLockFile(string lockFilePath, out Exception exception)
    {
      bool flag = false;
      exception = (Exception) null;
      if (File.Exists(lockFilePath))
      {
        try
        {
          File.Delete(lockFilePath);
          flag = true;
        }
        catch (IOException ex)
        {
          exception = (Exception) ex;
        }
        catch (Exception ex)
        {
          exception = ex;
        }
      }
      return flag;
    }

    public static XObject ReadLockFile(string lockFilePath, out Exception exception)
    {
      XObject xobject = (XObject) null;
      exception = (Exception) null;
      if (File.Exists(lockFilePath))
      {
        try
        {
          XmlDocument xmlDocument = new XmlDocument();
          using (FileStream fileStream = File.Open(lockFilePath, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete))
            xmlDocument.Load((Stream) fileStream);
          xobject = new XObject(xmlDocument.DocumentElement);
        }
        catch (IOException ex)
        {
          exception = (Exception) ex;
        }
        catch (Exception ex)
        {
          exception = ex;
        }
      }
      return xobject;
    }
  }
}
