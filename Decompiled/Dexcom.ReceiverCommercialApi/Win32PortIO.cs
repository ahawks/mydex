// Type: Dexcom.ReceiverApi.Win32PortIO
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Dexcom.ReceiverApi
{
  [SecuritySafeCritical]
  internal class Win32PortIO
  {
    internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
    internal static readonly IntPtr NULL = IntPtr.Zero;
    internal readonly byte I_AM_READY = Convert.ToByte('C');
    internal const int GENERIC_READ = -2147483648;
    internal const int GENERIC_WRITE = 1073741824;
    internal const uint ERROR_SUCCESS = 0U;
    internal const uint ERROR_FILE_NOT_FOUND = 2U;
    internal const uint ERROR_PATH_NOT_FOUND = 3U;
    internal const uint ERROR_ACCESS_DENIED = 5U;
    internal const uint ERROR_INVALID_HANDLE = 6U;
    internal const uint ERROR_BROKEN_PIPE = 109U;
    internal const uint ERROR_NO_DATA = 232U;
    internal const uint ERROR_HANDLE_EOF = 38U;
    internal const uint ERROR_OPERATION_ABORTED = 995U;
    internal const uint ERROR_IO_PENDING = 997U;
    internal const uint ERROR_SHARING_VIOLATION = 32U;
    internal const uint ERROR_FILE_EXISTS = 80U;
    internal const uint ERROR_INVALID_PARAMETER = 87U;
    internal const uint ERROR_FILENAME_EXCED_RANGE = 206U;
    internal const byte ONESTOPBIT = (byte) 0;
    internal const byte ONE5STOPBITS = (byte) 1;
    internal const byte TWOSTOPBITS = (byte) 2;
    internal const byte NOPARITY = (byte) 0;
    internal const byte ODDPARITY = (byte) 1;
    internal const byte EVENPARITY = (byte) 2;
    internal const byte MARKPARITY = (byte) 3;
    internal const byte SPACEPARITY = (byte) 4;
    internal const int DTR_CONTROL_DISABLE = 0;
    internal const int DTR_CONTROL_ENABLE = 1;
    internal const int DTR_CONTROL_HANDSHAKE = 2;
    internal const int RTS_CONTROL_DISABLE = 0;
    internal const int RTS_CONTROL_ENABLE = 1;
    internal const int RTS_CONTROL_HANDSHAKE = 2;
    internal const int RTS_CONTROL_TOGGLE = 3;
    internal const int MS_CTS_ON = 16;
    internal const int MS_DSR_ON = 32;
    internal const int MS_RING_ON = 64;
    internal const int MS_RLSD_ON = 128;
    internal const byte EOFCHAR = (byte) 26;
    internal const byte DEFAULTXONCHAR = (byte) 17;
    internal const byte DEFAULTXOFFCHAR = (byte) 19;
    internal const int EV_RXCHAR = 1;
    internal const int EV_RXFLAG = 2;
    internal const int EV_CTS = 8;
    internal const int EV_DSR = 16;
    internal const int EV_RLSD = 32;
    internal const int EV_BREAK = 64;
    internal const int EV_ERR = 128;
    internal const int EV_RING = 256;
    internal const int ALL_EVENTS = 507;
    internal const int CE_RXOVER = 1;
    internal const int CE_OVERRUN = 2;
    internal const int CE_PARITY = 4;
    internal const int CE_FRAME = 8;
    internal const int CE_BREAK = 16;
    internal const int CE_TXFULL = 256;
    internal const int PURGE_TXABORT = 1;
    internal const int PURGE_RXABORT = 2;
    internal const int PURGE_TXCLEAR = 4;
    internal const int PURGE_RXCLEAR = 8;
    internal const int SETXOFF = 1;
    internal const int SETXON = 2;
    internal const int SETRTS = 3;
    internal const int CLRRTS = 4;
    internal const int SETDTR = 5;
    internal const int CLRDTR = 6;
    internal const int SETBREAK = 8;
    internal const int CLRBREAK = 9;
    internal const byte SOH = (byte) 1;
    internal const byte STX = (byte) 2;
    internal const byte EOT = (byte) 4;
    internal const byte ACK = (byte) 6;
    internal const byte NAK = (byte) 21;
    internal const byte CAN = (byte) 24;
    internal const uint TIMEOUT = 4081U;
    internal const uint PACKET_NO_ERROR = 4082U;
    internal const uint CANCELED = 4083U;
    internal const uint NO_CHAR = 4084U;
    internal const uint PACKET_128 = 4085U;
    internal const uint PACKET_1024 = 4086U;
    internal const uint END_OF_TRANS = 4087U;
    internal const int OPEN_EXISTING = 3;
    internal const int OPEN_ALWAYS = 4;
    internal const int FILE_FLAG_NOBUFFERING = 5242880;
    internal const int FILE_FLAG_OVERLAPPED = 1073741824;
    internal const int FILE_ATTRIBUTE_NORMAL = 128;
    internal const int FILE_TYPE_DISK = 1;
    internal const int FILE_TYPE_CHAR = 2;
    internal const int FILE_TYPE_PIPE = 3;

    static Win32PortIO()
    {
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool CloseHandle(IntPtr handle);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool GetCommState(IntPtr hFile, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool SetCommState(IntPtr hFile, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool SetupComm(IntPtr hFile, int dwInQueue, int dwOutQueue);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool SetCommTimeouts(IntPtr hFile, ref Win32PortIO.COMMTIMEOUTS lpCommTimeouts);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool GetCommTimeouts(IntPtr hFile, ref Win32PortIO.COMMTIMEOUTS lpCommTimeouts);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool SetCommBreak(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool ClearCommBreak(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool ClearCommError(IntPtr hFile, ref int lpErrors, ref Win32PortIO.COMSTAT lpStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool ClearCommError(IntPtr hFile, ref int lpErrors, IntPtr lpStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool PurgeComm(IntPtr hFile, uint dwFlags);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool GetCommProperties(IntPtr hFile, ref Win32PortIO.COMMPROP lpCommProp);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static int GetFileType(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool EscapeCommFunction(IntPtr hFile, uint dwFunc);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool BuildCommDCB(string def, ref Win32PortIO.DCB lpDCB);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool FlushFileBuffers(IntPtr hFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool GetCommModemStatus(IntPtr hFile, out uint modemStat);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr securityAttrs, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static int ReadFile(IntPtr handle, [MarshalAs(UnmanagedType.LPArray)] byte[] bytes, int numBytesToRead, out int numBytesRead, IntPtr lpOverlapped);

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static int WriteFile(IntPtr handle, [MarshalAs(UnmanagedType.LPArray)] byte[] bytes, int numBytesToWrite, out int numBytesWritten, IntPtr lpOverlapped);

    public static void FlushPort(IntPtr hPort)
    {
      if (!(hPort != Win32PortIO.NULL) || !(hPort != Win32PortIO.INVALID_HANDLE_VALUE))
        return;
      Win32PortIO.PurgeComm(hPort, 8U);
    }

    internal struct DCB
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

    internal struct SerialTimeouts
    {
      public int ReadIntervalTimeout;
      public int ReadTotalTimeoutMultiplier;
      public int ReadTotalTimeoutConstant;
      public int WriteTotalTimeoutMultiplier;
      public int WriteTotalTimeoutConstant;
    }

    internal struct COMSTAT
    {
      public uint Flags;
      public uint cbInQue;
      public uint cbOutQue;
    }

    internal struct COMMTIMEOUTS
    {
      public uint ReadIntervalTimeout;
      public uint ReadTotalTimeoutMultiplier;
      public uint ReadTotalTimeoutConstant;
      public uint WriteTotalTimeoutMultiplier;
      public uint WriteTotalTimeoutConstant;
    }

    internal struct COMMPROP
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
    internal class SECURITY_ATTRIBUTES
    {
      internal int nLength;
      internal int lpSecurityDescriptor;
      internal int bInheritHandle;
    }
  }
}
