// Type: Dexcom.ReceiverApi.PacketTools
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;

namespace Dexcom.ReceiverApi
{
  public class PacketTools
  {
    public byte[] Packet = new byte[1590];
    public const ushort MaxPayloadLength = (ushort) 1584;
    public const ushort MinPacketLength = (ushort) 6;
    public const ushort MaxPacketLength = (ushort) 1590;
    public const byte SOF = (byte) 1;
    public const int OffsetToPacketSOF = 0;
    public const int OffsetToPacketLength = 1;
    public const int OffsetToPacketCommand = 3;
    public const int OffsetToPacketPayload = 4;

    public ushort PacketLength
    {
      get
      {
        return BitConverter.ToUInt16(this.Packet, 1);
      }
    }

    public void ClearPacket()
    {
      for (int index = 0; index < this.Packet.Length; ++index)
        this.Packet[index] = (byte) 0;
    }

    public byte[] NewCopyOfPacket()
    {
      byte[] numArray = (byte[]) null;
      int length = (int) this.PacketLength;
      if (length >= 6 && length <= this.Packet.Length)
      {
        numArray = new byte[length];
        Array.Copy((Array) this.Packet, (Array) numArray, length);
      }
      return numArray;
    }

    public void OverwritePacketSOF(byte newValue)
    {
      this.Packet[0] = newValue;
    }

    public void OverwritePacketLength(ushort newLength)
    {
      PacketTools.StoreBytes(newLength, this.Packet, 1);
    }

    public void OverwritePacketCommand(byte newCommand)
    {
      this.Packet[3] = newCommand;
    }

    public void OverwritePacketCrc(ushort newCrc)
    {
      int num = (int) this.PacketLength;
      if (num < 6 || num > this.Packet.Length)
        throw new DexComException("Failed to overwrite packet CRC because packet length not valid.");
      PacketTools.StoreBytes(newCrc, this.Packet, num - 2);
    }

    public void UpdatePacketCrc()
    {
      int num = (int) this.PacketLength;
      if (num < 6 || num > this.Packet.Length)
        throw new DexComException("Failed to overwrite packet CRC because packet length not valid.");
      PacketTools.StoreBytes(Crc.CalculateCrc16(this.Packet, 0, num - 2), this.Packet, num - 2);
    }

    public static byte GetStartOfFrameFromPacket(byte[] packet)
    {
      return packet[0];
    }

    public static byte GetCommandByteFromPacket(byte[] packet)
    {
      return packet[3];
    }

    public static ReceiverCommands GetReceiverCommandFromPacket(byte[] packet)
    {
      return ReceiverTools.GetReceiverCommandFromByte(packet[3]);
    }

    public static ushort GetPayloadSizeFromPacket(byte[] packet)
    {
      return BitConverter.ToUInt16(packet, 1);
    }

    public static ushort GetCrcFromEndOfPacket(byte[] packet)
    {
      return BitConverter.ToUInt16(packet, packet.Length - 2);
    }

    public static int StoreBytes(byte value, byte[] targetData, int targetOffset)
    {
      targetData[targetOffset] = value;
      return 1;
    }

    public static int StoreBytes(short value, byte[] targetData, int targetOffset)
    {
      return PacketTools.StoreBytes((ushort) value, targetData, targetOffset);
    }

    public static int StoreBytes(ushort value, byte[] targetData, int targetOffset)
    {
      targetData[targetOffset] = (byte) ((uint) value & (uint) byte.MaxValue);
      targetData[targetOffset + 1] = (byte) ((int) value >> 8 & (int) byte.MaxValue);
      return 2;
    }

    public static int StoreBytes(int value, byte[] targetData, int targetOffset)
    {
      return PacketTools.StoreBytes((uint) value, targetData, targetOffset);
    }

    public static int StoreBytes(uint value, byte[] targetData, int targetOffset)
    {
      targetData[targetOffset] = (byte) (value & (uint) byte.MaxValue);
      targetData[targetOffset + 1] = (byte) (value >> 8 & (uint) byte.MaxValue);
      targetData[targetOffset + 2] = (byte) (value >> 16 & (uint) byte.MaxValue);
      targetData[targetOffset + 3] = (byte) (value >> 24 & (uint) byte.MaxValue);
      return 4;
    }

    public static int StoreBytes(long value, byte[] targetData, int targetOffset)
    {
      return PacketTools.StoreBytes((ulong) value, targetData, targetOffset);
    }

    public static int StoreBytes(ulong value, byte[] targetData, int targetOffset)
    {
      targetData[targetOffset] = (byte) (value & (ulong) byte.MaxValue);
      targetData[targetOffset + 1] = (byte) (value >> 8 & (ulong) byte.MaxValue);
      targetData[targetOffset + 2] = (byte) (value >> 16 & (ulong) byte.MaxValue);
      targetData[targetOffset + 3] = (byte) (value >> 24 & (ulong) byte.MaxValue);
      targetData[targetOffset + 4] = (byte) (value >> 32 & (ulong) byte.MaxValue);
      targetData[targetOffset + 5] = (byte) (value >> 40 & (ulong) byte.MaxValue);
      targetData[targetOffset + 6] = (byte) (value >> 48 & (ulong) byte.MaxValue);
      targetData[targetOffset + 7] = (byte) (value >> 56 & (ulong) byte.MaxValue);
      return 8;
    }

    public int ComposePacket(ReceiverCommands command)
    {
      return PacketTools.DoComposePacket(this.Packet, command);
    }

    public int ComposePacket(ReceiverCommands command, byte payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, short payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, ushort payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, int payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, uint payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, long payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, ulong payload)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload);
    }

    public int ComposePacket(ReceiverCommands command, byte payload1, uint payload2)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload1, payload2);
    }

    public int ComposePacket(ReceiverCommands command, byte payload1, uint payload2, byte payload3)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload1, payload2, payload3);
    }

    public int ComposePacket(ReceiverCommands command, byte[] payload, int payloadLength)
    {
      return PacketTools.DoComposePacket(this.Packet, command, payload, payloadLength);
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command)
    {
      ushort num = (ushort) 6;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, byte payload)
    {
      ushort num = (ushort) 7;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload, packet, 4);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, short payload)
    {
      return PacketTools.DoComposePacket(packet, command, (ushort) payload);
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, ushort payload)
    {
      ushort num = (ushort) 8;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload, packet, 4);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, int payload)
    {
      return PacketTools.DoComposePacket(packet, command, (uint) payload);
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, uint payload)
    {
      ushort num = (ushort) 10;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload, packet, 4);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, long payload)
    {
      return PacketTools.DoComposePacket(packet, command, (ulong) payload);
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, ulong payload)
    {
      ushort num = (ushort) 14;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload, packet, 4);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, byte payload1, uint payload2)
    {
      ushort num = (ushort) 11;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload1, packet, 4);
      PacketTools.StoreBytes(payload2, packet, 5);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, byte payload1, uint payload2, byte payload3)
    {
      ushort num = (ushort) 12;
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      PacketTools.StoreBytes(payload1, packet, 4);
      PacketTools.StoreBytes(payload2, packet, 5);
      PacketTools.StoreBytes(payload3, packet, 9);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }

    private static int DoComposePacket(byte[] packet, ReceiverCommands command, byte[] payload, int payloadLength)
    {
      ushort num = (ushort) (6 + payloadLength);
      packet[0] = (byte) 1;
      PacketTools.StoreBytes(num, packet, 1);
      packet[3] = (byte) command;
      Array.Copy((Array) payload, 0, (Array) packet, 4, payloadLength);
      PacketTools.StoreBytes(Crc.CalculateCrc16(packet, 0, (int) num - 2), packet, (int) num - 2);
      return (int) num;
    }
  }
}
