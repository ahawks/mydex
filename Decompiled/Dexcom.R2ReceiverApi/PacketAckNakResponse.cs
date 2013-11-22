// Type: Dexcom.R2Receiver.PacketAckNakResponse
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  public class PacketAckNakResponse
  {
    private BaseEmptyPacket m_packet;

    public static int PacketSize
    {
      get
      {
        return BaseEmptyPacket.PacketSize;
      }
    }

    public bool IsAck
    {
      get
      {
        return this.m_packet.Command == R2Commands.ACK;
      }
    }

    public bool IsNak
    {
      get
      {
        return this.m_packet.Command == R2Commands.NAK;
      }
    }

    public PacketAckNakResponse(byte[] bytes)
    {
      this.m_packet = new BaseEmptyPacket(bytes);
      if (this.m_packet.Command != R2Commands.ACK && this.m_packet.Command != R2Commands.NAK)
        throw new ApplicationException("Bad Packet. Command field does not match!");
    }
  }
}
