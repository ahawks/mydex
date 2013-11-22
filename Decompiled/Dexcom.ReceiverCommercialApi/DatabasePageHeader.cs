// Type: Dexcom.ReceiverApi.DatabasePageHeader
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System.Runtime.InteropServices;

namespace Dexcom.ReceiverApi
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DatabasePageHeader
  {
    public uint FirstRecordIndex;
    public uint NumberOfRecords;
    public ReceiverRecordType RecordType;
    public byte Revision;
    public uint PageNumber;
    public uint Reserved2;
    public uint Reserved3;
    public uint Reserved4;
    public ushort Crc;
  }
}
