// Type: Dexcom.R2Receiver.R2BlockInfoParser
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;

namespace Dexcom.R2Receiver
{
  public class R2BlockInfoParser : R2DatabaseParserBase
  {
    private int m_currentBlock = -1;
    private R2BlockInfo[] m_blockInfoArray;

    public R2BlockInfo[] BlockInfoArray
    {
      get
      {
        return this.m_blockInfoArray;
      }
    }

    public override void Parse(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (data.Length % R2ReceiverValues.BlockSize != 0)
        throw new ArgumentOutOfRangeException("data", "Block data length must be a multiple of the block size 64K (0x10000)");
      this.m_blockInfoArray = new R2BlockInfo[data.Length / R2ReceiverValues.BlockSize];
      base.Parse(data);
    }

    protected override void BlockPrefixHandler(byte[] data, int offset)
    {
      ++this.m_currentBlock;
      this.m_blockInfoArray[this.m_currentBlock] = new R2BlockInfo();
      this.m_blockInfoArray[this.m_currentBlock].Offset = offset;
      this.m_blockInfoArray[this.m_currentBlock].Index = this.m_currentBlock;
      R2DatabaseBlockPrefix blockPrefix = (R2DatabaseBlockPrefix) DataTools.ConvertBytesToObject(data, offset, typeof (R2DatabaseBlockPrefix));
      this.m_blockInfoArray[this.m_currentBlock].SetBlockPrefix(blockPrefix);
      if ((int) blockPrefix.m_blockHeader.m_status != (int) R2ReceiverValues.BlockStatusUsed && (int) blockPrefix.m_blockHeader.m_status != (int) R2ReceiverValues.BlockStatusReadyToErase || offset + R2ReceiverValues.BlockSize > data.Length)
        return;
      this.m_blockInfoArray[this.m_currentBlock].Crc = Crc.CalculateCrc16(data, offset, offset + R2ReceiverValues.BlockSize);
    }

    protected override void EndOfBlockEmptyHandler(byte[] data, int offset)
    {
      if (this.PriorRecordOffset > 0 && Enum.IsDefined(typeof (R2RecordType), (object) data[this.PriorRecordOffset]))
        this.m_blockInfoArray[this.m_currentBlock].SetLastRecordPrefix((R2DatabaseRecordPrefix) DataTools.ConvertBytesToObject(data, this.PriorRecordOffset, typeof (R2DatabaseRecordPrefix)));
      this.m_blockInfoArray[this.m_currentBlock].LastSkewOffset = this.LatestSkewOffset;
      this.m_blockInfoArray[this.m_currentBlock].LastUserOffset = this.LatestUserOffset;
    }

    protected override void EndOfBlockRecordHandler(byte[] data, int offset)
    {
      if (this.PriorRecordOffset > 0 && Enum.IsDefined(typeof (R2RecordType), (object) data[this.PriorRecordOffset]))
        this.m_blockInfoArray[this.m_currentBlock].SetLastRecordPrefix((R2DatabaseRecordPrefix) DataTools.ConvertBytesToObject(data, this.PriorRecordOffset, typeof (R2DatabaseRecordPrefix)));
      this.m_blockInfoArray[this.m_currentBlock].LastSkewOffset = this.LatestSkewOffset;
      this.m_blockInfoArray[this.m_currentBlock].LastUserOffset = this.LatestUserOffset;
    }
  }
}
