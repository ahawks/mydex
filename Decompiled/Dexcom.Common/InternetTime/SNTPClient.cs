// Type: InternetTime.SNTPClient
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;

namespace InternetTime
{
  public class SNTPClient
  {
    private byte[] SNTPData = new byte[48];
    private const byte SNTPDataLength = (byte) 48;
    private const byte offReferenceID = (byte) 12;
    private const byte offReferenceTimestamp = (byte) 16;
    private const byte offOriginateTimestamp = (byte) 24;
    private const byte offReceiveTimestamp = (byte) 32;
    private const byte offTransmitTimestamp = (byte) 40;
    public DateTime DestinationTimestamp;
    private string TimeServer;

    public _LeapIndicator LeapIndicator
    {
      get
      {
        switch ((byte) ((uint) this.SNTPData[0] >> 6))
        {
          case (byte) 0:
            return _LeapIndicator.NoWarning;
          case (byte) 1:
            return _LeapIndicator.LastMinute61;
          case (byte) 2:
            return _LeapIndicator.LastMinute59;
          default:
            return _LeapIndicator.Alarm;
        }
      }
    }

    public byte VersionNumber
    {
      get
      {
        return (byte) (((int) this.SNTPData[0] & 56) >> 3);
      }
    }

    public _Mode Mode
    {
      get
      {
        switch ((byte) ((uint) this.SNTPData[0] & 7U))
        {
          case (byte) 1:
            return _Mode.SymmetricActive;
          case (byte) 2:
            return _Mode.SymmetricPassive;
          case (byte) 3:
            return _Mode.Client;
          case (byte) 4:
            return _Mode.Server;
          case (byte) 5:
            return _Mode.Broadcast;
          default:
            return _Mode.Unknown;
        }
      }
    }

    public _Stratum Stratum
    {
      get
      {
        byte num = this.SNTPData[1];
        if ((int) num == 0)
          return _Stratum.Unspecified;
        if ((int) num == 1)
          return _Stratum.PrimaryReference;
        return (int) num <= 15 ? _Stratum.SecondaryReference : _Stratum.Reserved;
      }
    }

    public uint PollInterval
    {
      get
      {
        return (uint) Math.Pow(2.0, (double) (sbyte) this.SNTPData[2]);
      }
    }

    public double Precision
    {
      get
      {
        return Math.Pow(2.0, (double) (sbyte) this.SNTPData[3]);
      }
    }

    public double RootDelay
    {
      get
      {
        return 1000.0 * ((double) (256 * (256 * (256 * (int) this.SNTPData[4] + (int) this.SNTPData[5]) + (int) this.SNTPData[6]) + (int) this.SNTPData[7]) / 65536.0);
      }
    }

    public double RootDispersion
    {
      get
      {
        return 1000.0 * ((double) (256 * (256 * (256 * (int) this.SNTPData[8] + (int) this.SNTPData[9]) + (int) this.SNTPData[10]) + (int) this.SNTPData[11]) / 65536.0);
      }
    }

    public string ReferenceID
    {
      get
      {
        string str = "";
        switch (this.Stratum)
        {
          case _Stratum.Unspecified:
          case _Stratum.PrimaryReference:
            str = str + (object) (char) this.SNTPData[12] + (object) (char) this.SNTPData[13] + (object) (char) this.SNTPData[14] + (object) (char) this.SNTPData[15];
            break;
          case _Stratum.SecondaryReference:
            switch (this.VersionNumber)
            {
              case (byte) 3:
                string hostNameOrAddress = this.SNTPData[12].ToString() + "." + this.SNTPData[13].ToString() + "." + this.SNTPData[14].ToString() + "." + this.SNTPData[15].ToString();
                try
                {
                  str = Dns.GetHostEntry(hostNameOrAddress).HostName + " (" + hostNameOrAddress + ")";
                  break;
                }
                catch (Exception ex)
                {
                  str = "N/A";
                  break;
                }
              case (byte) 4:
                str = (this.ComputeDate(this.GetMilliSeconds((byte) 12)) + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)).ToString();
                break;
              default:
                str = "N/A";
                break;
            }
        }
        return str;
      }
    }

    public DateTime ReferenceTimestamp
    {
      get
      {
        return this.ComputeDate(this.GetMilliSeconds((byte) 16)) + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
      }
    }

    public DateTime OriginateTimestamp
    {
      get
      {
        return this.ComputeDate(this.GetMilliSeconds((byte) 24));
      }
    }

    public DateTime ReceiveTimestamp
    {
      get
      {
        return this.ComputeDate(this.GetMilliSeconds((byte) 32)) + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
      }
    }

    public DateTime TransmitTimestamp
    {
      get
      {
        return this.ComputeDate(this.GetMilliSeconds((byte) 40)) + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
      }
      set
      {
        this.SetDate((byte) 40, value);
      }
    }

    public int RoundTripDelay
    {
      get
      {
        return (int) (this.DestinationTimestamp - this.OriginateTimestamp - this.ReceiveTimestamp - this.TransmitTimestamp).TotalMilliseconds;
      }
    }

    public int LocalClockOffset
    {
      get
      {
        return (int) ((this.ReceiveTimestamp - this.OriginateTimestamp + this.TransmitTimestamp - this.DestinationTimestamp).TotalMilliseconds / 2.0);
      }
    }

    public SNTPClient(string host)
    {
      this.TimeServer = host;
    }

    private DateTime ComputeDate(ulong milliseconds)
    {
      return new DateTime(1900, 1, 1) + TimeSpan.FromMilliseconds((double) milliseconds);
    }

    private ulong GetMilliSeconds(byte offset)
    {
      ulong num1 = 0UL;
      ulong num2 = 0UL;
      for (int index = 0; index <= 3; ++index)
        num1 = 256UL * num1 + (ulong) this.SNTPData[(int) offset + index];
      for (int index = 4; index <= 7; ++index)
        num2 = 256UL * num2 + (ulong) this.SNTPData[(int) offset + index];
      return num1 * 1000UL + num2 * 1000UL / 4294967296UL;
    }

    private void SetDate(byte offset, DateTime date)
    {
      DateTime dateTime = new DateTime(1900, 1, 1, 0, 0, 0);
      ulong num1 = (ulong) (date - dateTime).TotalMilliseconds;
      ulong num2 = num1 / 1000UL;
      ulong num3 = num1 % 1000UL * 4294967296UL / 1000UL;
      ulong num4 = num2;
      for (int index = 3; index >= 0; --index)
      {
        this.SNTPData[(int) offset + index] = (byte) (num4 % 256UL);
        num4 /= 256UL;
      }
      ulong num5 = num3;
      for (int index = 7; index >= 4; --index)
      {
        this.SNTPData[(int) offset + index] = (byte) (num5 % 256UL);
        num5 /= 256UL;
      }
    }

    private void Initialize()
    {
      this.SNTPData[0] = (byte) 27;
      for (int index = 1; index < 48; ++index)
        this.SNTPData[index] = (byte) 0;
      this.TransmitTimestamp = DateTime.Now;
    }

    public void Connect(bool UpdateSystemTime)
    {
      this.Connect(UpdateSystemTime, -1);
    }

    public void Connect(bool UpdateSystemTime, int ReceiveTimeout)
    {
      try
      {
        IPEndPoint remoteEP = new IPEndPoint(Dns.GetHostEntry(this.TimeServer).AddressList[0], 123);
        UdpClient udpClient = new UdpClient();
        udpClient.Client.ReceiveTimeout = ReceiveTimeout;
        udpClient.Connect(remoteEP);
        this.Initialize();
        udpClient.Send(this.SNTPData, this.SNTPData.Length);
        this.SNTPData = udpClient.Receive(ref remoteEP);
        if (!this.IsResponseValid())
          throw new Exception("Invalid response from " + this.TimeServer);
        this.DestinationTimestamp = DateTime.Now;
      }
      catch (SocketException ex)
      {
        throw new Exception(ex.Message);
      }
      if (!UpdateSystemTime)
        return;
      this.SetTime();
    }

    public bool IsResponseValid()
    {
      return this.SNTPData.Length >= 48 && this.Mode == _Mode.Server;
    }

    public override string ToString()
    {
      string str1 = "Leap Indicator: ";
      switch (this.LeapIndicator)
      {
        case _LeapIndicator.NoWarning:
          str1 = str1 + "No warning";
          break;
        case _LeapIndicator.LastMinute61:
          str1 = str1 + "Last minute has 61 seconds";
          break;
        case _LeapIndicator.LastMinute59:
          str1 = str1 + "Last minute has 59 seconds";
          break;
        case _LeapIndicator.Alarm:
          str1 = str1 + "Alarm Condition (clock not synchronized)";
          break;
      }
      string str2 = str1 + "\r\nVersion number: " + this.VersionNumber.ToString() + "\r\n" + "Mode: ";
      switch (this.Mode)
      {
        case _Mode.SymmetricActive:
          str2 = str2 + "Symmetric Active";
          break;
        case _Mode.SymmetricPassive:
          str2 = str2 + "Symmetric Pasive";
          break;
        case _Mode.Client:
          str2 = str2 + "Client";
          break;
        case _Mode.Server:
          str2 = str2 + "Server";
          break;
        case _Mode.Broadcast:
          str2 = str2 + "Broadcast";
          break;
        case _Mode.Unknown:
          str2 = str2 + "Unknown";
          break;
      }
      string str3 = str2 + "\r\nStratum: ";
      switch (this.Stratum)
      {
        case _Stratum.Unspecified:
        case _Stratum.Reserved:
          str3 = str3 + "Unspecified";
          break;
        case _Stratum.PrimaryReference:
          str3 = str3 + "Primary Reference";
          break;
        case _Stratum.SecondaryReference:
          str3 = str3 + "Secondary Reference";
          break;
      }
      return str3 + "\r\nLocal time: " + this.TransmitTimestamp.ToString() + "\r\nPrecision: " + this.Precision.ToString() + " s" + "\r\nPoll Interval: " + this.PollInterval.ToString() + " s" + "\r\nReference ID: " + ((object) this.ReferenceID).ToString() + "\r\nRoot Delay: " + this.RootDelay.ToString() + " ms" + "\r\nRoot Dispersion: " + this.RootDispersion.ToString() + " ms" + "\r\nRound Trip Delay: " + this.RoundTripDelay.ToString() + " ms" + "\r\nLocal Clock Offset: " + this.LocalClockOffset.ToString() + " ms" + "\r\n";
    }

    [DllImport("kernel32.dll")]
    private static bool SetLocalTime(ref SNTPClient.SYSTEMTIME time);

    [SecuritySafeCritical]
    private void SetTime()
    {
      DateTime dateTime = DateTime.Now.AddMilliseconds((double) this.LocalClockOffset);
      SNTPClient.SYSTEMTIME time;
      time.year = (short) dateTime.Year;
      time.month = (short) dateTime.Month;
      time.dayOfWeek = (short) dateTime.DayOfWeek;
      time.day = (short) dateTime.Day;
      time.hour = (short) dateTime.Hour;
      time.minute = (short) dateTime.Minute;
      time.second = (short) dateTime.Second;
      time.milliseconds = (short) dateTime.Millisecond;
      SNTPClient.SetLocalTime(ref time);
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
