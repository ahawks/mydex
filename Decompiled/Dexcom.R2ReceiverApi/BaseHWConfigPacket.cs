// Type: Dexcom.R2Receiver.BaseHWConfigPacket
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
  public struct BaseHWConfigPacket
  {
    public const int NumBytes = 134;
    private byte m_SOF;
    private R2Commands m_command;
    private short m_size;
    private R2HWConfig m_config;
    private ushort m_crc;

    public static int PacketSize
    {
      get
      {
        return 134;
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

    public R2HWConfig Config
    {
      get
      {
        return this.m_config;
      }
    }

    public BaseHWConfigPacket(R2Commands command, R2HWConfig config)
    {
      this.m_SOF = (byte) 1;
      this.m_command = command;
      this.m_size = (short) 128;
      this.m_config = config;
      this.m_crc = (ushort) 0;
      byte[] bytes = this.Bytes;
      this.m_crc = Crc.CalculateCrc16(bytes, 0, 132);
      int start = 4;
      int end = start + Marshal.OffsetOf(this.m_config.GetType(), "m_crc").ToInt32();
      if ((int) Crc.CalculateCrc32(bytes, start, end) != (int) this.m_config.m_crc)
        throw new ApplicationException("Bad CRC in HW CONFIG record!");
    }

    public BaseHWConfigPacket(byte[] bytes)
    {
      R2Commands r2Commands = R2Commands.ACK;
      if (bytes.Length < BaseHWConfigPacket.PacketSize)
        throw new ApplicationException("Not enough bytes in argument used to create packet.");
      this.m_SOF = (byte) 1;
      this.m_command = R2Commands.Unknown;
      this.m_size = (short) 0;
      this.m_config = new R2HWConfig();
      this.m_crc = (ushort) 0;
      this = (BaseHWConfigPacket) DataTools.ConvertBytesToObject(bytes, 0, this.GetType());
      if ((int) this.m_SOF != 1)
        throw new ApplicationException("Bad Packet. SOF field not valid!");
      if (this.m_command != r2Commands)
        throw new ApplicationException("Bad Packet. Command field not valid!");
      if ((int) this.m_size != 128)
        throw new ApplicationException("Bad Packet. Size field not valid!");
      if ((int) this.m_crc != (int) Crc.CalculateCrc16(bytes, 0, 132))
        throw new ApplicationException("Bad Packet. CRC did not match!");
      int start = 4;
      int end = start + Marshal.OffsetOf(this.m_config.GetType(), "m_crc").ToInt32();
      if ((int) Crc.CalculateCrc32(bytes, start, end) != (int) this.m_config.m_crc)
        throw new ApplicationException("Bad CRC in HW CONFIG record!");
    }
  }
}
