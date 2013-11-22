// Type: Dexcom.ReceiverApi.ReceiverEGVRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;
using System.Runtime.InteropServices;

namespace Dexcom.ReceiverApi
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct ReceiverEGVRecord : IGenericReceiverRecord
  {
    public uint SystemSeconds;
    public uint DisplaySeconds;
    public ushort GlucoseValueWithFlags;
    public byte TrendArrowAndNoise;
    public ushort m_crc;

    public bool IsImmediateMatch
    {
      get
      {
        return ((int) this.TrendArrowAndNoise & (int) ReceiverValues.ImmediateMatchMask) != 0;
      }
    }

    public TrendArrow TrendArrow
    {
      get
      {
        return (TrendArrow) ((uint) this.TrendArrowAndNoise & (uint) ReceiverValues.TrendArrowMask);
      }
    }

    public NoiseMode NoiseMode
    {
      get
      {
        return (NoiseMode) (((int) this.TrendArrowAndNoise & (int) ReceiverValues.NoiseMask) >> 4);
      }
    }

    public bool IsDisplayOnly
    {
      get
      {
        return ((int) this.GlucoseValueWithFlags & (int) ReceiverValues.IsDisplayOnlyEgvMask) != 0;
      }
    }

    public ushort GlucoseValue
    {
      get
      {
        return (ushort) ((uint) this.GlucoseValueWithFlags & (uint) ReceiverValues.EgvValueMask);
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return ReceiverRecordType.EGVData;
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
