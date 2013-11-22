﻿// Type: Dexcom.ReceiverApi.ReceiverMeterRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;
using System.Runtime.InteropServices;

namespace Dexcom.ReceiverApi
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct ReceiverMeterRecord : IGenericReceiverRecord
  {
    public uint SystemSeconds;
    public uint DisplaySeconds;
    public ushort MeterValue;
    public uint MeterTime;
    public ushort m_crc;

    public DateTime MeterTimeStamp
    {
      get
      {
        return ReceiverTools.ConvertReceiverTimeToDateTime(this.MeterTime);
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return ReceiverRecordType.MeterData;
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
