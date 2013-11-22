// Type: Dexcom.ReceiverApi.XPage
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  [Serializable]
  public class XPage : XObject, ISerializable
  {
    public const string Tag = "Page";
    public const string PageDataTag = "PageData";

    public XPageHeader PageHeader
    {
      get
      {
        return new XPageHeader(this.Element.SelectSingleNode("PageHeader") as XmlElement);
      }
    }

    public byte[] PageData
    {
      get
      {
        byte[] numArray = (byte[]) null;
        XmlElement sourceElement = this.Element.SelectSingleNode("PageData") as XmlElement;
        if (DataTools.IsBinaryXmlElement(sourceElement))
          numArray = DataTools.ExtractBinaryXmlElement(sourceElement);
        else if (DataTools.IsCompressedElement(sourceElement))
          numArray = (byte[]) DataTools.DecompressElementWithHint(sourceElement);
        return numArray;
      }
      set
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("PageData") as XmlElement;
        XmlElement binaryXmlElement = DataTools.CreateBinaryXmlElement(value, "PageData", this.Element.OwnerDocument);
        if (xmlElement == null)
          this.Element.AppendChild((XmlNode) binaryXmlElement);
        else
          this.Element.ReplaceChild((XmlNode) binaryXmlElement, (XmlNode) xmlElement);
      }
    }

    public XPage()
      : this(new XmlDocument())
    {
    }

    public XPage(XmlDocument ownerDocument)
      : base("Page", ownerDocument)
    {
      this.AppendChild((XObject) new XPageHeader(ownerDocument));
    }

    public XPage(XmlElement element)
      : base(element)
    {
    }

    protected XPage(SerializationInfo info, StreamingContext context)
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
