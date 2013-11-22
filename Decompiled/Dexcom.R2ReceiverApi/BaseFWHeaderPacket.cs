// Type: Dexcom.R2Receiver.BaseFWHeaderPacket
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct BaseFWHeaderPacket
  {
    public const int NumBytes = 20;
    private byte m_SOF;
    private R2Commands m_command;
    private short m_size;
    private R2FWHeader m_header;
    private ushort m_crc;

    public static int PacketSize
    {
      get
      {
        return 20;
      }
    }

    public byte[] Bytes
    {
      get
      {
        return DataTools.ConvertObjectToBytes((object) this);
      }
    }

    public R2Commands Command
    {
      get
      {
        return this.m_command;
      }
    }

    public R2FWHeader Header
    {
      get
      {
        return this.m_header;
      }
    }

    public BaseFWHeaderPacket(R2Commands command, R2FWHeader header)
    {
      this.m_SOF = (byte) 1;
      this.m_command = command;
      this.m_size = Convert.ToInt16(14);
      this.m_header = header;
      this.m_crc = (ushort) 0;
      this.m_crc = Crc.CalculateCrc16(this.Bytes, 0, 18);
    }

    public BaseFWHeaderPacket(byte[] bytes)
    {
      R2Commands r2Commands = R2Commands.ACK;
      if (bytes.Length < BaseFWHeaderPacket.PacketSize)
        throw new ApplicationException("Not enough bytes in argument used to create packet.");
      this.m_SOF = (byte) 1;
      this.m_command = R2Commands.Unknown;
      this.m_size = (short) 0;
      this.m_header = new R2FWHeader();
      this.m_crc = (ushort) 0;
      this = (BaseFWHeaderPacket) DataTools.ConvertBytesToObject(bytes, 0, this.GetType());
      if ((int) this.m_SOF != 1)
        throw new ApplicationException("Bad Packet. SOF field not valid!");
      if (this.m_command != r2Commands)
        throw new ApplicationException("Bad Packet. Command field not valid!");
      if ((int) this.m_size != 14)
        throw new ApplicationException("Bad Packet. Size field not valid!");
      if ((int) this.m_crc != (int) Crc.CalculateCrc16(bytes, 0, 18))
        throw new ApplicationException("Bad Packet. CRC did not match!");
    }
  }
}
