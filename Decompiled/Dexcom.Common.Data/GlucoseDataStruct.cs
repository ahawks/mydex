// Type: Dexcom.Common.Data.GlucoseDataStruct
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using System;
using System.Runtime.InteropServices;

namespace Dexcom.Common.Data
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct GlucoseDataStruct
  {
    public Guid DeviceId;
    public uint RecordNumber;
    public DateTime DisplayTime;
    public DateTime InternalTime;
    public short Value;
    public int GapCount;
    public TimeSpan GapTimeSpan;
  }
}
