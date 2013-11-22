// Type: Dexcom.Common.Data.XRepositoryWatchEntry
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XRepositoryWatchEntry : XObject, ISerializable
  {
    public const string Tag = "Entry";

    [XmlAttribute]
    public string SerialNumber
    {
      get
      {
        return this.GetAttribute<string>("SerialNumber");
      }
      set
      {
        this.SetAttribute("SerialNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public int Count
    {
      get
      {
        return this.GetAttribute<int>("Count");
      }
      set
      {
        this.SetAttribute("Count", value);
      }
    }

    [XmlAttribute]
    public DateTimeOffset LastStored
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("LastStored");
      }
      set
      {
        this.SetAttribute("LastStored", value);
      }
    }

    [XmlAttribute]
    public DateTimeOffset DateExpired
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateExpired");
      }
      set
      {
        this.SetAttribute("DateExpired", value);
      }
    }

    public bool IsExpired
    {
      get
      {
        return this.DateExpired <= DateTimeOffset.Now;
      }
    }

    public XRepositoryWatchEntry()
      : this(new XmlDocument())
    {
    }

    public XRepositoryWatchEntry(XmlDocument ownerDocument)
      : base("Entry", ownerDocument)
    {
      this.SerialNumber = string.Empty;
      this.Count = 0;
      this.LastStored = CommonValues.EmptyDateTimeOffset;
      this.DateExpired = CommonValues.EmptyDateTimeOffset;
    }

    public XRepositoryWatchEntry(XmlElement element)
      : base(element)
    {
    }

    protected XRepositoryWatchEntry(SerializationInfo info, StreamingContext context)
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
