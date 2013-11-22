// Type: Dexcom.R2Receiver.R2BlockInfo
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class R2BlockInfo
  {
    private R2DatabaseBlockPrefix m_blockPrefix = new R2DatabaseBlockPrefix();
    private R2DatabaseRecordPrefix m_lastRecordPrefix = new R2DatabaseRecordPrefix();
    private TimeSpan m_lastSkewOffset = TimeSpan.Zero;
    private TimeSpan m_lastUserOffset = TimeSpan.Zero;
    private ushort m_crc;
    private int m_blockOffset;
    private int m_blockIndex;
    private object m_tag;

    public int Offset
    {
      get
      {
        return this.m_blockOffset;
      }
      set
      {
        this.m_blockOffset = value;
      }
    }

    public int Index
    {
      get
      {
        return this.m_blockIndex;
      }
      set
      {
        this.m_blockIndex = value;
      }
    }

    public ushort Crc
    {
      get
      {
        return this.m_crc;
      }
      set
      {
        this.m_crc = value;
      }
    }

    public uint Number
    {
      get
      {
        return this.m_blockPrefix.m_blockHeader.m_number;
      }
    }

    public ushort Status
    {
      get
      {
        return this.m_blockPrefix.m_blockHeader.m_status;
      }
    }

    public object Tag
    {
      get
      {
        return this.m_tag;
      }
      set
      {
        this.m_tag = value;
      }
    }

    public DateTime FirstRecordTimeStampUtc
    {
      get
      {
        if ((int) this.Status == (int) R2ReceiverValues.BlockStatusUsed || (int) this.Status == (int) R2ReceiverValues.BlockStatusReadyToErase)
          return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_blockPrefix.m_databaseKey.m_timeStamp);
        else
          return CommonValues.EmptyDateTime;
      }
    }

    public DateTime LastRecordTimeStampUtc
    {
      get
      {
        if ((int) this.Status == (int) R2ReceiverValues.BlockStatusUsed || (int) this.Status == (int) R2ReceiverValues.BlockStatusReadyToErase)
          return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_lastRecordPrefix.m_databaseKey.m_timeStamp);
        else
          return CommonValues.EmptyDateTime;
      }
    }

    public TimeSpan LastSkewOffset
    {
      get
      {
        return this.m_lastSkewOffset;
      }
      set
      {
        this.m_lastSkewOffset = value;
      }
    }

    public TimeSpan LastUserOffset
    {
      get
      {
        return this.m_lastUserOffset;
      }
      set
      {
        this.m_lastUserOffset = value;
      }
    }

    public void SetBlockPrefix(R2DatabaseBlockPrefix blockPrefix)
    {
      this.m_blockPrefix = blockPrefix;
    }

    public void SetLastRecordPrefix(R2DatabaseRecordPrefix recordPrefix)
    {
      this.m_lastRecordPrefix = recordPrefix;
    }

    public XmlElement GetXml()
    {
      return this.GetXml(new XmlDocument());
    }

    public XmlElement GetXml(XmlDocument xmlOwnerDocument)
    {
      XObject xobject = new XObject(xmlOwnerDocument.CreateElement("BlockInfo"));
      xobject.SetAttribute("Index", this.Index);
      xobject.SetAttribute("Offset", this.Offset);
      xobject.SetAttribute("Crc", this.Crc);
      xobject.SetAttribute("Number", this.Number);
      xobject.SetAttribute("Status", this.Status);
      xobject.SetAttribute("FirstRecordTimeStampUtc", this.FirstRecordTimeStampUtc);
      xobject.SetAttribute("LastRecordTimeStampUtc", this.LastRecordTimeStampUtc);
      xobject.SetAttribute("LastSkewOffset", this.LastSkewOffset);
      xobject.SetAttribute("LastUserOffset", this.LastUserOffset);
      return xobject.Element;
    }
  }
}
