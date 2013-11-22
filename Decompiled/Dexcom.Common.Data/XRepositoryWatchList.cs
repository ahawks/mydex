// Type: Dexcom.Common.Data.XRepositoryWatchList
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
  public class XRepositoryWatchList : XObject, ISerializable
  {
    public const string Tag = "WatchList";
    public const string WatchEntriesTag = "WatchEntries";

    [XmlAttribute]
    public int RecentEntriesFound
    {
      get
      {
        return this.GetAttribute<int>("RecentEntriesFound");
      }
      set
      {
        this.SetAttribute("RecentEntriesFound", value);
      }
    }

    [XmlAttribute]
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

    [XmlAttribute]
    public DateTimeOffset DateTimeModified
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeModified");
      }
      set
      {
        this.SetAttribute("DateTimeModified", value);
      }
    }

    public XCollection<XRepositoryWatchEntry> WatchEntries
    {
      get
      {
        return new XCollection<XRepositoryWatchEntry>(this.Element.SelectSingleNode("WatchEntries") as XmlElement);
      }
    }

    public XRepositoryWatchList()
      : this(new XmlDocument())
    {
    }

    public XRepositoryWatchList(XmlDocument ownerDocument)
      : base("WatchList", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.Empty;
      this.RecentEntriesFound = 0;
      this.DateTimeCreated = now;
      this.DateTimeModified = now;
      this.AppendChild((XObject) new XCollection<XRepositoryWatchEntry>("WatchEntries", ownerDocument));
    }

    public XRepositoryWatchList(XmlElement element)
      : base(element)
    {
    }

    protected XRepositoryWatchList(SerializationInfo info, StreamingContext context)
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
