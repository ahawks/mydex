// Type: Dexcom.R2Receiver.PacketReadSettings4Response
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class PacketReadSettings4Response
  {
    private BaseSettings4Packet m_packet;

    public static int PacketSize
    {
      get
      {
        return BaseSettings4Packet.PacketSize;
      }
    }

    public R2Settings4Record Settings
    {
      get
      {
        return this.m_packet.Settings;
      }
    }

    public PacketReadSettings4Response(byte[] bytes)
    {
      this.m_packet = new BaseSettings4Packet(bytes);
    }
  }
}
