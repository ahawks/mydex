// Type: Dexcom.ReceiverApi.DatabasePage
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System.Runtime.InteropServices;

namespace Dexcom.ReceiverApi
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DatabasePage
  {
    public DatabasePageHeader PageHeader;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 500)]
    public byte[] PageData;
  }
}
