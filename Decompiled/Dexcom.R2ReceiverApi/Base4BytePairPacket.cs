// Type: Dexcom.R2Receiver.Base4BytePairPacket
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
  public struct Base4BytePairPacket
  {
    public const int NumBytes = 14;
    private byte m_SOF;
    private R2Commands m_command;
    private short m_size;
    private uint m_value1;
    private uint m_value2;
    private ushort m_crc;

    public static int PacketSize
    {
      get
      {
        return 14;
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

    public uint Value1
    {
      get
      {
        return this.m_value1;
      }
    }

    public uint Value2
    {
      get
      {
        return this.m_value2;
      }
    }

    public Base4BytePairPacket(R2Commands command, uint value1, uint value2)
    {
      this.m_SOF = (byte) 1;
      this.m_command = command;
      this.m_size = (short) 8;
      this.m_value1 = value1;
      this.m_value2 = value2;
      this.m_crc = (ushort) 0;
      this.m_crc = Crc.CalculateCrc16(this.Bytes, 0, 12);
    }

    public Base4BytePairPacket(R2Commands command, byte[] bytes)
    {
      if (bytes.Length < Base4BytePairPacket.PacketSize)
        throw new ApplicationException("Not enough bytes in argument used to create packet.");
      this.m_SOF = (byte) 1;
      this.m_command = R2Commands.Unknown;
      this.m_size = (short) 0;
      this.m_value1 = 0U;
      this.m_value2 = 0U;
      this.m_crc = (ushort) 0;
      this = (Base4BytePairPacket) DataTools.ConvertBytesToObject(bytes, 0, this.GetType());
      if ((int) this.m_SOF != 1)
        throw new ApplicationException("Bad Packet. SOF field not valid!");
      if (this.m_command != command)
        throw new ApplicationException("Bad Packet. Command field not valid!");
      if ((int) this.m_size != 8)
        throw new ApplicationException("Bad Packet. Size field not valid!");
      if ((int) this.m_crc != (int) Crc.CalculateCrc16(bytes, 0, 12))
        throw new ApplicationException("Bad Packet. CRC did not match!");
    }
  }
}
