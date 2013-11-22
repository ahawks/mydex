// Type: Dexcom.R2Receiver.R2RecordFooterUpdateableWithCrc
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct R2RecordFooterUpdateableWithCrc
  {
    public ushort m_crc;
    public ushort m_updateableData;
    public ushort m_updateableCrc;
    public byte m_type;
  }
}
