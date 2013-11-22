// Type: Dexcom.R2Receiver.PacketReadADCResponse
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class PacketReadADCResponse
  {
    private Base2BytePacket m_packet;

    public static int PacketSize
    {
      get
      {
        return Base2BytePacket.PacketSize;
      }
    }

    public ushort Counts
    {
      get
      {
        return this.m_packet.Payload;
      }
    }

    public PacketReadADCResponse(byte[] bytes)
    {
      this.m_packet = new Base2BytePacket(R2Commands.ACK, bytes);
    }
  }
}
