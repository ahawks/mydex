from xml.etree import ElementTree

def create_application_xml(doc_contents):
	doc = ElementTree()


	"""
	    public static XmlDocument CreateApplicationXmlDocument(string documentContents, bool addSignature)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      XmlDocument ownerDocument = new XmlDocument();
      XObject xobject = new XObject(documentContents, ownerDocument);
      ownerDocument.AppendChild((XmlNode) xobject.Element);
      xobject.Id = Guid.NewGuid();
      xobject.SetAttribute("DateTimeCreated", now);
      xobject.SetAttribute("DateTimeCreatedLocal", now.LocalDateTime);
      xobject.SetAttribute("DateTimeCreatedUtc", now.UtcDateTime);
      XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(ownerDocument);
      xobject.AppendChild((XObject) xapplicationInfo);
      XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(ownerDocument);
      xobject.AppendChild((XObject) xcomputerInfo);
      XmlDocument xDocument = new XmlDocument();
      xDocument.PreserveWhitespace = true;
      xDocument.LoadXml(CommonTools.FormatXml(ownerDocument.DocumentElement.OuterXml));
      XmlDeclaration xmlDeclaration = xDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
      xDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xDocument.DocumentElement);
      if (addSignature)
        CommonTools.AddDocumentSignature(xDocument);
      return xDocument;
    }"""