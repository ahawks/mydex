// Type: Dexcom.R2Receiver.Win32PortIO
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Dexcom.R2Receiver
{
  [SecuritySafeCritical]
   class Win32PortIO
  {
     public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
     public static readonly IntPtr NULL = IntPtr.Zero;
     public readonly byte I_AM_READY = Convert.ToByte('C');
     public const int GENERIC_READ = -2147483648;
     public const int GENERIC_WRITE = 1073741824;
     public const uint ERROR_SUCCESS = 0U;
     public const uint ERROR_FILE_NOT_FOUND = 2U;
     public const uint ERROR_PATH_NOT_FOUND = 3U;
     public const uint ERROR_ACCESS_DENIED = 5U;
     public const uint ERROR_INVALID_HANDLE = 6U;
     public const uint ERROR_BROKEN_PIPE = 109U;
     public const uint ERROR_NO_DATA = 232U;
     public const uint ERROR_HANDLE_EOF = 38U;
     public const uint ERROR_OPERATION_ABORTED = 995U;
     public const uint ERROR_IO_PENDING = 997U;
     public const uint ERROR_SHARING_VIOLATION = 32U;
     public const uint ERROR_FILE_EXISTS = 80U;
     public const uint ERROR_INVALID_PARAMETER = 87U;
     public const uint ERROR_FILENAME_EXCED_RANGE = 206U;
     public const byte ONESTOPBIT = (byte) 0;
     public const byte ONE5STOPBITS = (byte) 1;
     public const byte TWOSTOPBITS = (byte)2;
     public const byte NOPARITY = (byte)0;
     public const byte ODDPARITY = (byte)1;
     public const byte EVENPARITY = (byte)2;
     public const byte MARKPARITY = (byte)3;
     public const byte SPACEPARITY = (byte)4;
     public const int DTR_CONTROL_DISABLE = 0;
     public const int DTR_CONTROL_ENABLE = 1;
     public const int DTR_CONTROL_HANDSHAKE = 2;
     public const int RTS_CONTROL_DISABLE = 0;
     public const int RTS_CONTROL_ENABLE = 1;
     public const int RTS_CONTROL_HANDSHAKE = 2;
     public const int RTS_CONTROL_TOGGLE = 3;
     public const int MS_CTS_ON = 16;
     public const int MS_DSR_ON = 32;
     public const int MS_RING_ON = 64;
     public const int MS_RLSD_ON = 128;
     public const byte EOFCHAR = (byte)26;
     public const byte DEFAULTXONCHAR = (byte)17;
     public const byte DEFAULTXOFFCHAR = (byte)19;
     public const int EV_RXCHAR = 1;
     public const int EV_RXFLAG = 2;
     public const int EV_CTS = 8;
     public const int EV_DSR = 16;
     public const int EV_RLSD = 32;
     public const int EV_BREAK = 64;
     public const int EV_ERR = 128;
     public const int EV_RING = 256;
     public const int ALL_EVENTS = 507;
     public const int CE_RXOVER = 1;
     public const int CE_OVERRUN = 2;
     public const int CE_PARITY = 4;
     public const int CE_FRAME = 8;
     public const int CE_BREAK = 16;
     public const int CE_TXFULL = 256;
     public const int PURGE_TXABORT = 1;
     public const int PURGE_RXABORT = 2;
     public const int PURGE_TXCLEAR = 4;
     public const int PURGE_RXCLEAR = 8;
     public const int SETXOFF = 1;
     public const int SETXON = 2;
     public const int SETRTS = 3;
     public const int CLRRTS = 4;
     public const int SETDTR = 5;
     public const int CLRDTR = 6;
     public const int SETBREAK = 8;
     public const int CLRBREAK = 9;
     public const byte SOH = (byte)1;
     public const byte STX = (byte)2;
     public const byte EOT = (byte)4;
     public const byte ACK = (byte)6;
     public const byte NAK = (byte)21;
     public const byte CAN = (byte)24;
     public const uint TIMEOUT = 4081U;
     public const uint PACKET_NO_ERROR = 4082U;
     public const uint CANCELED = 4083U;
     public const uint NO_CHAR = 4084U;
     public const uint PACKET_128 = 4085U;
     public const uint PACKET_1024 = 4086U;
     public const uint END_OF_TRANS = 4087U;
     public const int OPEN_EXISTING = 3;
     public const int OPEN_ALWAYS = 4;
     public const int FILE_FLAG_NOBUFFERING = 5242880;
     public const int FILE_FLAG_OVERLAPPED = 1073741824;
     public const int FILE_ATTRIBUTE_NORMAL = 128;
     public const int FILE_TYPE_DISK = 1;
     public const int FILE_TYPE_CHAR = 2;
     public const int FILE_TYPE_PIPE = 3;

    static Win32PortIO()
    {
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool CloseHandle(IntPtr handle);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool GetCommState(IntPtr hFile, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool SetCommState(IntPtr hFile, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool SetupComm(IntPtr hFile, int dwInQueue, int dwOutQueue);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool SetCommTimeouts(IntPtr hFile, ref Win32PortIO.COMMTIMEOUTS lpCommTimeouts);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool GetCommTimeouts(IntPtr hFile, ref Win32PortIO.COMMTIMEOUTS lpCommTimeouts);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool SetCommBreak(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool ClearCommBreak(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool ClearCommError(IntPtr hFile, ref int lpErrors, ref Win32PortIO.COMSTAT lpStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool ClearCommError(IntPtr hFile, ref int lpErrors, IntPtr lpStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool PurgeComm(IntPtr hFile, uint dwFlags);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool GetCommProperties(IntPtr hFile, ref Win32PortIO.COMMPROP lpCommProp);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static int GetFileType(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool EscapeCommFunction(IntPtr hFile, uint dwFunc);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool BuildCommDCB(string def, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool FlushFileBuffers(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static bool GetCommModemStatus(IntPtr hFile, out uint modemStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr securityAttrs, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static int ReadFile(IntPtr handle, [MarshalAs(UnmanagedType.LPArray)] byte[] bytes, int numBytesToRead, out int numBytesRead, IntPtr lpOverlapped);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public extern static int WriteFile(IntPtr handle, [MarshalAs(UnmanagedType.LPArray)] byte[] bytes, int numBytesToWrite, out int numBytesWritten, IntPtr lpOverlapped);

    public static void ResetAndFlushPort(IntPtr hPort)
    {
      if (!(hPort != Win32PortIO.NULL) || !(hPort != Win32PortIO.INVALID_HANDLE_VALUE))
        return;
      byte[] bytes = new byte[3];
      int numBytesRead = 0;
      int numBytesWritten = 0;
      bytes[0] = (byte) 24;
      bytes[1] = (byte) 24;
      bytes[2] = (byte) 4;
      Win32PortIO.WriteFile(hPort, bytes, 3, out numBytesWritten, Win32PortIO.NULL);
      Win32PortIO.FlushFileBuffers(hPort);
      Win32PortIO.PurgeComm(hPort, 8U);
      Thread.Sleep(100);
      for (; Win32PortIO.ReadFile(hPort, bytes, 1, out numBytesRead, Win32PortIO.NULL) != 0 && numBytesRead == 1; numBytesRead = 0)
        Win32PortIO.PurgeComm(hPort, 8U);
    }

    public static void FlushPort(IntPtr hPort)
    {
      if (!(hPort != Win32PortIO.NULL) || !(hPort != Win32PortIO.INVALID_HANDLE_VALUE))
        return;
      byte[] bytes = new byte[3];
      int numBytesRead = 0;
      Win32PortIO.FlushFileBuffers(hPort);
      Win32PortIO.PurgeComm(hPort, 8U);
      Thread.Sleep(100);
      for (; Win32PortIO.ReadFile(hPort, bytes, 1, out numBytesRead, Win32PortIO.NULL) != 0 && numBytesRead == 1; numBytesRead = 0)
        Win32PortIO.PurgeComm(hPort, 8U);
    }

     public struct DCB
    {
      public uint DCBlength;
      public uint BaudRate;
      public uint Flags;
      public ushort wReserved;
      public ushort XonLim;
      public ushort XoffLim;
      public byte ByteSize;
      public byte Parity;
      public byte StopBits;
      public sbyte XonChar;
      public sbyte XoffChar;
      public sbyte ErrorChar;
      public sbyte EofChar;
      public sbyte EvtChar;
      public ushort wReserved1;

      public uint fBinary
      {
        get
        {
          return this.Flags & 1U;
        }
        set
        {
          this.Flags = this.Flags & 4294967294U | value;
        }
      }

      public uint fParity
      {
        get
        {
          return this.Flags >> 1 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -3 | (int) value << 1);
        }
      }

      public uint fOutxCtsFlow
      {
        get
        {
          return this.Flags >> 2 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -5 | (int) value << 2);
        }
      }

      public uint fOutxDsrFlow
      {
        get
        {
          return this.Flags >> 3 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -9 | (int) value << 3);
        }
      }

      public uint fDtrControl
      {
        get
        {
          return this.Flags >> 4 & 3U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -49 | (int) value << 4);
        }
      }

      public uint fDsrSensitivity
      {
        get
        {
          return this.Flags >> 6 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -65 | (int) value << 6);
        }
      }

      public uint fTXContinueOnXoff
      {
        get
        {
          return this.Flags >> 7 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -129 | (int) value << 7);
        }
      }

      public uint fOutX
      {
        get
        {
          return this.Flags >> 8 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -257 | (int) value << 8);
        }
      }

      public uint fInX
      {
        get
        {
          return this.Flags >> 9 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -513 | (int) value << 9);
        }
      }

      public uint fErrorChar
      {
        get
        {
          return this.Flags >> 10 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -1025 | (int) value << 10);
        }
      }

      public uint fNull
      {
        get
        {
          return this.Flags >> 11 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -2049 | (int) value << 11);
        }
      }

      public uint fRtsControl
      {
        get
        {
          return this.Flags >> 12 & 3U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -12289 | (int) value << 12);
        }
      }

      public uint fAbortOnError
      {
        get
        {
          return this.Flags >> 14 & 1U;
        }
        set
        {
          this.Flags = (uint) ((int) this.Flags & -16385 | (int) value << 14);
        }
      }
    }

     public struct SerialTimeouts
    {
      public int ReadIntervalTimeout;
      public int ReadTotalTimeoutMultiplier;
      public int ReadTotalTimeoutConstant;
      public int WriteTotalTimeoutMultiplier;
      public int WriteTotalTimeoutConstant;
    }

     public struct COMSTAT
    {
      public uint Flags;
      public uint cbInQue;
      public uint cbOutQue;
    }

     public struct COMMTIMEOUTS
    {
      public uint ReadIntervalTimeout;
      public uint ReadTotalTimeoutMultiplier;
      public uint ReadTotalTimeoutConstant;
      public uint WriteTotalTimeoutMultiplier;
      public uint WriteTotalTimeoutConstant;
    }

     public struct COMMPROP
    {
      public ushort wPacketLength;
      public ushort wPacketVersion;
      public int dwServiceMask;
      public int dwReserved1;
      public int dwMaxTxQueue;
      public int dwMaxRxQueue;
      public int dwMaxBaud;
      public int dwProvSubType;
      public int dwProvCapabilities;
      public int dwSettableParams;
      public int dwSettableBaud;
      public ushort wSettableData;
      public ushort wSettableStopParity;
      public int dwCurrentTxQueue;
      public int dwCurrentRxQueue;
      public int dwProvSpec1;
      public int dwProvSpec2;
      public char wcProvChar;
    }

    [StructLayout(LayoutKind.Sequential)]
     class SECURITY_ATTRIBUTES
    {
       int nLength;
       int lpSecurityDescriptor;
       int bInheritHandle;
    }
  }
}
