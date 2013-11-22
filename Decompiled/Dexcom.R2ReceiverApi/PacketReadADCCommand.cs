// Type: Dexcom.R2Receiver.PacketReadADCCommand
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class PacketReadADCCommand
  {
    private Base1BytePacket m_packet;

    public byte[] Bytes
    {
      get
      {
        return this.m_packet.Bytes;
      }
    }

    public PacketReadADCCommand(byte channelNumber)
    {
      this.m_packet = new Base1BytePacket(R2Commands.ReadADC, channelNumber);
    }
  }
}
