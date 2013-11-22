// Type: Dexcom.ReceiverApi.ReceiverUserEventRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;
using System.Runtime.InteropServices;

namespace Dexcom.ReceiverApi
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct ReceiverUserEventRecord : IGenericReceiverRecord
  {
    public uint SystemSeconds;
    public uint DisplaySeconds;
    public UserEvent EventType;
    public byte EventSubType;
    public uint EventTime;
    public uint EventValue;
    public ushort m_crc;

    public DateTime EventTimeStamp
    {
      get
      {
        return ReceiverTools.ConvertReceiverTimeToDateTime(this.EventTime);
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return ReceiverRecordType.UserEventData;
      }
    }

    public ushort RecordedCrc
    {
      get
      {
        return this.m_crc;
      }
    }

    public int RecordSize
    {
      get
      {
        return Marshal.SizeOf((object) this);
      }
    }

    public DateTime SystemTimeStamp
    {
      get
      {
        return ReceiverTools.ConvertReceiverTimeToDateTime(this.SystemSeconds);
      }
    }

    public DateTime DisplayTimeStamp
    {
      get
      {
        return ReceiverTools.ConvertReceiverTimeToDateTime(this.DisplaySeconds);
      }
    }
  }
}
