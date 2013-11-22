// Type: Dexcom.FileTransfer.TransferWorker
// Assembly: Dexcom.FileTransfer, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 38934138-A845-4F5C-AA0D-8047C5BBDF07
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.FileTransfer.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using ICSharpCode.SharpZipLib.Checksums;
using System;
using System.IO;

namespace Dexcom.FileTransfer
{
  public class TransferWorker
  {
    private Guid m_workerId = Guid.NewGuid();
    private Guid m_transferId = Guid.Empty;
    private DirectoryInfo m_clientTempFolder = new DirectoryInfo(Path.GetTempPath());
    private const int m_minTransferSize = 4096;
    private const int m_maxTransferSize = 10485760;
    private const int m_defTransferSize = 65536;
    private XOnlineContext m_clientContext;
    private TransferState m_state;
    private XFileInfo m_xFileInfo;
    private TransferDirection m_direction;
    private MemoryStream m_fileMemoryStream;

    public Guid TransferId
    {
      get
      {
        return this.m_transferId;
      }
    }

    public XFileInfo FileInfo
    {
      get
      {
        return this.m_xFileInfo;
      }
    }

    public TransferState State
    {
      get
      {
        lock (this)
          return this.m_state;
      }
      private set
      {
        lock (this)
          this.m_state = value;
      }
    }

    public DirectoryInfo ClientTempFolder
    {
      get
      {
        return this.m_clientTempFolder;
      }
      set
      {
        this.m_clientTempFolder = value;
      }
    }

    public MemoryStream FileMemoryStream
    {
      get
      {
        return this.m_fileMemoryStream;
      }
      set
      {
        this.m_fileMemoryStream = value;
      }
    }

    public event TransferProgressEventHandler TransferProgress;

    public TransferWorker(XOnlineContext clientContext, Guid transferId, XFileInfo xFileInfo, TransferDirection direction)
    {
      this.m_clientContext = clientContext;
      this.m_transferId = transferId;
      this.m_xFileInfo = xFileInfo;
      this.m_direction = direction;
      this.m_state = TransferState.Ready;
    }

    public TransferWorker(Context clientContext, Guid transferId, XFileInfo xFileInfo, TransferDirection direction)
    {
      this.m_clientContext = new XOnlineContext()
      {
        SystemId = clientContext.SystemId,
        SessionId = clientContext.SessionId,
        UserId = clientContext.UserId
      };
      this.m_transferId = transferId;
      this.m_xFileInfo = xFileInfo;
      this.m_direction = direction;
      this.m_state = TransferState.Ready;
    }

    private void DoFireProgressEvent(TransferState state, int count, int total)
    {
      Delegate del = (Delegate) this.TransferProgress;
      if (del == null || del.GetInvocationList().Length <= 0)
        return;
      EventUtils.FireEventAsync(del, (object) this, (object) new TransferProgressEventArgs(state, count, total));
    }

    public void Send(IFileTransferService fileTransferService)
    {
      if (this.m_direction == TransferDirection.ToServer)
      {
        this.DoSendFileToServer(fileTransferService);
      }
      else
      {
        if (this.m_direction != TransferDirection.ToClient)
          return;
        this.DoSendFileToClient(fileTransferService);
      }
    }

    private string DoGenerateTransferToClientTempFolderPath(Guid sessionId, Guid transferId, Guid fileId)
    {
      string str = "C:\\DexCom\\Temp\\";
      if (this.m_clientTempFolder != null && !string.IsNullOrEmpty(this.m_clientTempFolder.FullName))
        str = this.m_clientTempFolder.FullName;
      DirectoryInfo directoryInfo1 = new DirectoryInfo(str);
      if (!directoryInfo1.Exists)
        directoryInfo1.Create();
      string path = Path.Combine(Path.Combine(Path.Combine(str, CommonTools.ConvertToString(sessionId)), CommonTools.ConvertToString(transferId)), CommonTools.ConvertToString(fileId));
      DirectoryInfo directoryInfo2 = new DirectoryInfo(path);
      if (!directoryInfo2.Exists)
        directoryInfo2.Create();
      return path;
    }

    private void DoSendFileToClient(IFileTransferService fileTransferService)
    {
      PerformanceTimer performanceTimer1 = new PerformanceTimer(1);
      PerformanceTimer performanceTimer2 = new PerformanceTimer(2);
      PerformanceTimer performanceTimer3 = new PerformanceTimer(3);
      int num1 = Convert.ToInt32(this.m_xFileInfo.Length);
      try
      {
        this.State = TransferState.Sending;
        XFileTransfer xFileTransfer1 = new XFileTransfer();
        xFileTransfer1.Id = this.m_workerId;
        xFileTransfer1.TransferId = this.m_transferId;
        xFileTransfer1.TransferTo = "Client";
        xFileTransfer1.FileId = this.m_xFileInfo.Id;
        xFileTransfer1.FileName = this.m_xFileInfo.Name;
        xFileTransfer1.FileSize = num1;
        xFileTransfer1.Retry = 0;
        xFileTransfer1.IsCompressed = true;
        xFileTransfer1.IsEncrypted = false;
        xFileTransfer1.IsCheckSession = false;
        xFileTransfer1.IsFinished = false;
        xFileTransfer1.IsAborted = false;
        xFileTransfer1.ChunkOffset = 0;
        xFileTransfer1.ChunkMaxSize = 10485760;
        xFileTransfer1.ChunkSize = 65536;
        xFileTransfer1.ChunkCrc = 0L;
        int num2 = 65536;
        int count = 0;
        int num3 = 0;
        string path1 = string.Empty;
        if (this.m_fileMemoryStream == null)
          path1 = this.DoGenerateTransferToClientTempFolderPath(this.m_clientContext.SessionId, xFileTransfer1.TransferId, xFileTransfer1.FileId);
        performanceTimer1.Start();
        performanceTimer3.Start();
        int num4 = xFileTransfer1.Xml.Length * 2;
        performanceTimer2.Reset();
        performanceTimer2.Start();
        XFileTransfer xFileTransfer2 = fileTransferService.BeginFileTransferToClient(this.m_clientContext, xFileTransfer1);
        performanceTimer2.Stop();
        int num5 = xFileTransfer2.Xml.Length * 2;
        num1 = xFileTransfer2.FileSize;
        this.m_xFileInfo.Length = Convert.ToInt64(num1);
        xFileTransfer2.GetFileInfo();
        xFileTransfer2.SetFileInfo((XFileInfo) null);
        int val2 = Convert.ToInt32(0.7 * (double) Math.Min(10485760, xFileTransfer2.ChunkMaxSize));
        double num6 = performanceTimer2.GetElapsedTime() / 1000.0;
        if (num6 > 0.0)
          num2 = Math.Max(Math.Min(Convert.ToInt32((double) (num4 + num5) / num6), val2), 4096);
        num3 = 65536;
        bool flag1 = false;
        while (!flag1)
        {
          this.DoFireProgressEvent(TransferState.Sending, count, num1);
          int num7 = num1 > 2097152 ? 2 : 1;
          int num8 = Math.Max(Math.Min((num2 <= 204800 ? (num2 <= 102400 ? (num2 <= 51200 ? num2 * 5 * num7 : num2 * 3 * num7) : num2 * 2 * num7) : num2 * num7) / 2, val2), 4096);
          xFileTransfer2.ChunkOffset = count;
          xFileTransfer2.ChunkSize = num8;
          xFileTransfer2.ChunkCrc = 0L;
          xFileTransfer2.Retry = 0;
          if (performanceTimer3.GetLapTime() > 300000.0)
          {
            xFileTransfer2.IsCheckSession = true;
            performanceTimer3.Reset();
            performanceTimer3.Start();
          }
          else
            xFileTransfer2.IsCheckSession = false;
          int num9 = xFileTransfer2.Xml.Length * 2;
          bool flag2 = false;
          int num10 = 0;
          Exception exception = (Exception) null;
          while (!flag2)
          {
            xFileTransfer2.SetFileTransferChunk((XFileTransferChunk) null);
            ++num10;
            try
            {
              performanceTimer2.Reset();
              performanceTimer2.Start();
              xFileTransfer2 = fileTransferService.SendFileToClient(this.m_clientContext, xFileTransfer2);
              performanceTimer2.Stop();
              flag2 = true;
              num5 = xFileTransfer2.Xml.Length * 2;
              num6 = performanceTimer2.GetElapsedTime() / 1000.0;
            }
            catch
            {
              throw;
            }
          }
          byte[] numArray1 = (byte[]) null;
          XFileTransferChunk fileTransferChunk = xFileTransfer2.GetFileTransferChunk();
          if (flag2 && fileTransferChunk != null)
          {
            double num11 = 1.0;
            if (fileTransferChunk.Size > 0)
            {
              if (fileTransferChunk.IsCompressed)
              {
                int compressedSize = fileTransferChunk.CompressedSize;
                byte[] numArray2 = Convert.FromBase64String(fileTransferChunk.Element.InnerText);
                if (numArray2.Length != compressedSize)
                  throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: File chunk compressed size does not match Base64 encoded data.");
                Adler32 adler32 = new Adler32();
                adler32.Update(numArray2);
                if (xFileTransfer2.ChunkCrc != adler32.Value)
                  throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: File chunk CRC does not match calculated CRC!");
                numArray1 = DataTools.DecompressData(numArray2, fileTransferChunk.Size);
                num11 = (double) numArray1.Length / (double) numArray2.Length;
                if (num11 < 1.1)
                  xFileTransfer2.IsCompressed = false;
              }
              else
              {
                numArray1 = Convert.FromBase64String(fileTransferChunk.Element.InnerText);
                if (numArray1.Length != fileTransferChunk.Size)
                  throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: File chunk size does not match Base64 encoded data.");
                Adler32 adler32 = new Adler32();
                adler32.Update(numArray1);
                if (xFileTransfer2.ChunkCrc != adler32.Value)
                  throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: File chunk CRC does not match calculated CRC!");
              }
            }
            if (fileTransferChunk.Size == 0)
            {
              if (xFileTransfer2.ChunkOffset != 0)
                throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: Empty file chunk encountered in middle of file.");
              if (numArray1 != null && fileTransferChunk.Size != xFileTransfer2.ChunkSize)
                throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: Resulting file chunk size did not match indicated size.");
              if (this.m_fileMemoryStream != null)
                FileUtils.WriteFileContentsChunk(this.m_fileMemoryStream, numArray1, 0, 0);
              else
                FileUtils.WriteFileContentsChunk(Path.Combine(path1, xFileTransfer2.FileName), numArray1, 0, 0);
            }
            else
            {
              if (numArray1 == null || numArray1.Length != xFileTransfer2.ChunkSize || numArray1.Length != fileTransferChunk.Size)
                throw new OnlineException(ExceptionType.InvalidArgument, "Transfer failed: Resulting file chunk size did not match indicated size.");
              if (this.m_fileMemoryStream != null)
                FileUtils.WriteFileContentsChunk(this.m_fileMemoryStream, numArray1, xFileTransfer2.ChunkOffset, numArray1.Length);
              else
                FileUtils.WriteFileContentsChunk(Path.Combine(path1, xFileTransfer2.FileName), numArray1, xFileTransfer2.ChunkOffset, numArray1.Length);
            }
            count += fileTransferChunk.Size;
            if (fileTransferChunk.HasMoreData)
            {
              num2 = 65536;
              if (num6 > 0.0)
                num2 = Math.Max(Math.Min(Convert.ToInt32((double) Convert.ToInt32((double) (num9 + num5) / num6) * num11), val2), 4096);
            }
            else
            {
              flag1 = true;
              fileTransferService.EndFileTransferToClient(this.m_clientContext, xFileTransfer2);
              this.State = TransferState.Done;
              this.DoFireProgressEvent(TransferState.Done, num1, num1);
            }
          }
          else
          {
            flag1 = true;
            fileTransferService.AbortFileTransferToClient(this.m_clientContext, xFileTransfer2);
            this.State = TransferState.Failed;
            this.DoFireProgressEvent(TransferState.Aborted, num1, num1);
            if (exception != null)
              throw exception;
          }
        }
      }
      catch (Exception ex)
      {
        this.State = TransferState.Failed;
        this.DoFireProgressEvent(TransferState.Aborted, num1, num1);
        throw;
      }
      finally
      {
        performanceTimer1.Stop();
      }
    }

    private void DoSendFileToServer(IFileTransferService fileTransferService)
    {
      PerformanceTimer performanceTimer1 = new PerformanceTimer(1);
      PerformanceTimer performanceTimer2 = new PerformanceTimer(2);
      PerformanceTimer performanceTimer3 = new PerformanceTimer(3);
      int num1 = Convert.ToInt32(this.m_xFileInfo.Length);
      try
      {
        this.State = TransferState.Sending;
        XFileTransfer xFileTransfer1 = new XFileTransfer();
        xFileTransfer1.Id = this.m_workerId;
        xFileTransfer1.TransferId = this.m_transferId;
        xFileTransfer1.TransferTo = "Server";
        xFileTransfer1.FileId = this.m_xFileInfo.Id;
        xFileTransfer1.FileName = this.m_xFileInfo.Name;
        xFileTransfer1.FileSize = num1;
        xFileTransfer1.Retry = 0;
        xFileTransfer1.IsCompressed = true;
        xFileTransfer1.IsEncrypted = false;
        xFileTransfer1.IsCheckSession = false;
        xFileTransfer1.IsFinished = false;
        xFileTransfer1.IsAborted = false;
        xFileTransfer1.ChunkOffset = 0;
        xFileTransfer1.ChunkMaxSize = 10485760;
        xFileTransfer1.ChunkSize = 65536;
        xFileTransfer1.ChunkCrc = 0L;
        xFileTransfer1.SetFileTransferChunk(new XFileTransferChunk());
        string filePath = this.m_xFileInfo.FilePath;
        int num2 = 65536;
        int num3 = 0;
        int num4 = 0;
        performanceTimer1.Start();
        performanceTimer3.Start();
        int num5 = xFileTransfer1.Xml.Length * 2;
        performanceTimer2.Reset();
        performanceTimer2.Start();
        XFileTransfer xFileTransfer2 = fileTransferService.BeginFileTransferToServer(this.m_clientContext, xFileTransfer1);
        performanceTimer2.Stop();
        int num6 = xFileTransfer2.Xml.Length * 2;
        int val2 = Convert.ToInt32(0.7 * (double) Math.Min(10485760, xFileTransfer2.ChunkMaxSize));
        double num7 = performanceTimer2.GetElapsedTime() / 1000.0;
        if (num7 > 0.0)
          num2 = Math.Max(Math.Min(Convert.ToInt32((double) (num5 + num6) / num7), val2), 4096);
        num4 = 65536;
        bool flag1 = false;
        while (!flag1)
        {
          this.DoFireProgressEvent(TransferState.Sending, num3, num1);
          int num8 = num1 > 2097152 ? 2 : 1;
          int chunkSize = Math.Max(Math.Min((num2 <= 204800 ? (num2 <= 102400 ? (num2 <= 51200 ? num2 * 5 * num8 : num2 * 3 * num8) : num2 * 2 * num8) : num2 * num8) / 2, val2), 4096);
          xFileTransfer2.ChunkOffset = num3;
          xFileTransfer2.ChunkSize = chunkSize;
          xFileTransfer2.ChunkCrc = 0L;
          xFileTransfer2.Retry = 0;
          if (performanceTimer3.GetLapTime() > 300000.0)
          {
            xFileTransfer2.IsCheckSession = true;
            performanceTimer3.Reset();
            performanceTimer3.Start();
          }
          else
            xFileTransfer2.IsCheckSession = false;
          byte[] fileData = (byte[]) null;
          bool flag2 = FileUtils.ReadFileContentsChunk(filePath, num3, chunkSize, out fileData);
          if (fileData.Length != chunkSize)
          {
            if (flag2)
              throw new OnlineException(ExceptionType.Unknown, "Failed to read requested file chunk size.");
            xFileTransfer2.ChunkSize = fileData.Length;
          }
          XFileTransferChunk xFileTransferChunk = new XFileTransferChunk();
          xFileTransferChunk.Size = fileData.Length;
          xFileTransferChunk.HasMoreData = flag2;
          xFileTransferChunk.IsEncrypted = false;
          xFileTransferChunk.IsCompressed = xFileTransfer2.IsCompressed;
          xFileTransferChunk.CompressedSize = 0;
          double num9 = 1.0;
          if (fileData.Length > 0)
          {
            if (xFileTransferChunk.IsCompressed)
            {
              byte[] numArray = DataTools.CompressData(fileData);
              if (numArray == null)
                throw new OnlineException(ExceptionType.Unknown, "Failed to compress file chunk data!");
              Adler32 adler32 = new Adler32();
              adler32.Update(numArray);
              xFileTransfer2.ChunkCrc = adler32.Value;
              xFileTransferChunk.CompressedSize = numArray.Length;
              xFileTransferChunk.Element.InnerText = Convert.ToBase64String(numArray);
              num9 = (double) fileData.Length / (double) numArray.Length;
              if (num9 < 1.1)
                xFileTransfer2.IsCompressed = false;
            }
            else
            {
              Adler32 adler32 = new Adler32();
              adler32.Update(fileData);
              xFileTransfer2.ChunkCrc = adler32.Value;
              xFileTransferChunk.Element.InnerText = Convert.ToBase64String(fileData);
            }
          }
          xFileTransfer2.SetFileTransferChunk(xFileTransferChunk);
          int num10 = xFileTransfer2.Xml.Length * 2;
          bool flag3 = false;
          int num11 = 0;
          Exception exception = (Exception) null;
          while (!flag3)
          {
            ++num11;
            try
            {
              performanceTimer2.Reset();
              performanceTimer2.Start();
              xFileTransfer2 = fileTransferService.SendFileToServer(this.m_clientContext, xFileTransfer2);
              performanceTimer2.Stop();
              flag3 = true;
            }
            catch
            {
              throw;
            }
          }
          if (flag3)
          {
            num3 += fileData.Length;
            if (flag2)
            {
              int num12 = xFileTransfer2.Xml.Length * 2;
              double num13 = performanceTimer2.GetElapsedTime() / 1000.0;
              num2 = 65536;
              if (num13 > 0.0)
                num2 = Math.Max(Math.Min(Convert.ToInt32((double) Convert.ToInt32((double) (num10 + num12) / num13) * num9), val2), 4096);
            }
            else
            {
              flag1 = true;
              fileTransferService.EndFileTransferToServer(this.m_clientContext, xFileTransfer2);
              this.State = TransferState.Done;
              this.DoFireProgressEvent(TransferState.Done, num1, num1);
            }
          }
          else
          {
            flag1 = true;
            fileTransferService.AbortFileTransferToServer(this.m_clientContext, xFileTransfer2);
            this.State = TransferState.Failed;
            this.DoFireProgressEvent(TransferState.Aborted, num1, num1);
            if (exception != null)
              throw exception;
          }
        }
      }
      catch (Exception ex)
      {
        this.State = TransferState.Failed;
        this.DoFireProgressEvent(TransferState.Aborted, num1, num1);
        throw;
      }
      finally
      {
        performanceTimer1.Stop();
      }
    }
  }
}
