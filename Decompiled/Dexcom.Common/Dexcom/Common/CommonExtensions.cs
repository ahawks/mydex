// Type: Dexcom.Common.CommonExtensions
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Xml;
using System.Xml.Linq;

namespace Dexcom.Common
{
  public static class CommonExtensions
  {
    public static bool IsNotNullOrEmpty(this string instance)
    {
      return !string.IsNullOrEmpty(instance);
    }

    public static bool IsNullOrEmpty(this string instance)
    {
      return string.IsNullOrEmpty(instance);
    }

    public static XmlDocument ToXmlDocument(this XDocument xdoc)
    {
      XmlDocument xmlDocument = new XmlDocument();
      using (XmlReader reader = xdoc.CreateReader())
        xmlDocument.Load(reader);
      return xmlDocument;
    }

    public static XDocument ToXDocument(this XmlDocument xmldoc)
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
        xmldoc.WriteTo(writer);
      return xdocument;
    }

    public static XElement ToXElement(this XmlElement xelement)
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
        xelement.WriteTo(writer);
      return xdocument.Root;
    }

    public static XmlElement ToXmlElement(this XElement xelement)
    {
      using (XmlReader reader = xelement.CreateReader())
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(reader);
        return xmlDocument.DocumentElement;
      }
    }
  }
}
