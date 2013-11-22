﻿// Type: Dexcom.R2Receiver.PacketAmIFirmwareCommand
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class PacketAmIFirmwareCommand
  {
    public static PacketAmIFirmwareCommand Instance = new PacketAmIFirmwareCommand();
    private BaseEmptyPacket m_packet = new BaseEmptyPacket(R2Commands.AmIFirmware);

    public byte[] Bytes
    {
      get
      {
        return this.m_packet.Bytes;
      }
    }

    static PacketAmIFirmwareCommand()
    {
    }
  }
}
