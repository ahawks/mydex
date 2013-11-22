// Type: Dexcom.ReceiverApi.XPCParameterRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  [Serializable]
  public class XPCParameterRecord : XObject, ISerializable
  {
    public const string Tag = "PCParameterRecord";
    public const int MaxNicknameLength = 20;

    public Guid ReceiverId
    {
      get
      {
        return this.GetAttribute<Guid>("ReceiverId");
      }
      set
      {
        this.SetAttribute("ReceiverId", value);
      }
    }

    public DateTimeOffset DateTimeCreated
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeCreated");
      }
      set
      {
        this.SetAttribute("DateTimeCreated", value);
      }
    }

    public Guid FirmwareImageId
    {
      get
      {
        return this.GetAttribute<Guid>("FirmwareImageId");
      }
      set
      {
        this.SetAttribute("FirmwareImageId", value);
      }
    }

    public string Nickname
    {
      get
      {
        return this.GetAttribute<string>("Nickname");
      }
      set
      {
        string strValue = value.Trim();
        if (strValue.Length > 20)
          strValue = strValue.Substring(0, 20);
        this.SetAttribute("Nickname", strValue);
      }
    }

    public XPCParameterRecord()
      : this(new XmlDocument())
    {
    }

    public XPCParameterRecord(XmlDocument ownerDocument)
      : base("PCParameterRecord", ownerDocument)
    {
      this.ReceiverId = Guid.Empty;
      this.FirmwareImageId = Guid.Empty;
      this.DateTimeCreated = CommonValues.EmptyDateTimeOffset;
      this.Nickname = string.Empty;
    }

    public XPCParameterRecord(XmlElement element)
      : base(element)
    {
    }

    protected XPCParameterRecord(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}
