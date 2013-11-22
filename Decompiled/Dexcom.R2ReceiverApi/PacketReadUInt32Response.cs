﻿// Type: Dexcom.R2Receiver.PacketReadUInt32Response
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class PacketReadUInt32Response
  {
    private Base4BytePacket m_packet;

    public static int PacketSize
    {
      get
      {
        return Base4BytePacket.PacketSize;
      }
    }

    public uint Value
    {
      get
      {
        return this.m_packet.Payload;
      }
    }

    public PacketReadUInt32Response(byte[] bytes)
    {
      this.m_packet = new Base4BytePacket(R2Commands.ACK, bytes);
    }
  }
}
