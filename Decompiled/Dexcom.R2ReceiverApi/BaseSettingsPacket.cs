// Type: Dexcom.R2Receiver.BaseSettingsPacket
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
  public struct BaseSettingsPacket
  {
    public static readonly int NumBytes = 6 + Marshal.SizeOf(typeof (R2SettingsRecord));
    private byte m_SOF;
    private R2Commands m_command;
    private short m_size;
    private R2SettingsRecord m_settings;
    private ushort m_crc;

    public static int PacketSize
    {
      get
      {
        return BaseSettingsPacket.NumBytes;
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

    public R2SettingsRecord Settings
    {
      get
      {
        return this.m_settings;
      }
    }

    static BaseSettingsPacket()
    {
    }

    public BaseSettingsPacket(R2Commands command, R2SettingsRecord settings)
    {
      this.m_SOF = (byte) 1;
      this.m_command = command;
      this.m_size = Convert.ToInt16(BaseSettingsPacket.NumBytes - 6);
      this.m_settings = settings;
      this.m_crc = (ushort) 0;
      byte[] bytes = this.Bytes;
      this.m_crc = Crc.CalculateCrc16(bytes, 0, BaseSettingsPacket.NumBytes - 2);
      if ((int) this.m_settings.m_header.m_type != (int) this.m_settings.m_footer.m_type)
        throw new ApplicationException("Failed to match record type in header/footer of SETTINGS record!");
      int start = 4;
      int end = start + Marshal.OffsetOf(this.m_settings.GetType(), "m_footer").ToInt32();
      if ((int) Crc.CalculateCrc16(bytes, start, end) != (int) this.m_settings.m_footer.m_crc)
        throw new ApplicationException("Bad CRC in SETTINGS record!");
    }

    public BaseSettingsPacket(byte[] bytes)
    {
      R2Commands r2Commands = R2Commands.ACK;
      if (bytes.Length < BaseSettingsPacket.PacketSize)
        throw new ApplicationException("Not enough bytes in argument used to create packet.");
      this.m_SOF = (byte) 1;
      this.m_command = R2Commands.Unknown;
      this.m_size = (short) 0;
      this.m_settings = new R2SettingsRecord();
      this.m_crc = (ushort) 0;
      this = (BaseSettingsPacket) DataTools.ConvertBytesToObject(bytes, 0, this.GetType());
      if ((int) this.m_SOF != 1)
        throw new ApplicationException("Bad Packet. SOF field not valid!");
      if (this.m_command != r2Commands)
        throw new ApplicationException("Bad Packet. Command field not valid!");
      if ((int) this.m_size != BaseSettingsPacket.NumBytes - 6)
        throw new ApplicationException("Bad Packet. Size field not valid!");
      if ((int) this.m_crc != (int) Crc.CalculateCrc16(bytes, 0, BaseSettingsPacket.NumBytes - 2))
        throw new ApplicationException("Bad Packet. CRC did not match!");
      if ((int) this.m_settings.m_header.m_type != (int) this.m_settings.m_footer.m_type)
        throw new ApplicationException("Failed to match record type in header/footer of SETTINGS record!");
      int start = 4;
      int end = start + Marshal.OffsetOf(this.m_settings.GetType(), "m_footer").ToInt32();
      if ((int) Crc.CalculateCrc16(bytes, start, end) != (int) this.m_settings.m_footer.m_crc)
        throw new ApplicationException("Bad CRC in SETTINGS record!");
    }
  }
}
