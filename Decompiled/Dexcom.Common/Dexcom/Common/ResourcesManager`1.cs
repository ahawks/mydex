// Type: Dexcom.Common.ResourcesManager`1
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Xml;

namespace Dexcom.Common
{
  public class ResourcesManager<T>
  {
    private Dictionary<T, ResourcesManager<T>.ResourceEntry> m_defaultDictionary = new Dictionary<T, ResourcesManager<T>.ResourceEntry>();
    private Dictionary<int, Dictionary<T, ResourcesManager<T>.ResourceEntry>> m_dictionaryCache = new Dictionary<int, Dictionary<T, ResourcesManager<T>.ResourceEntry>>();

    public XmlElement ResourcesDictionary { get; private set; }

    public List<CultureInfo> SupportedCultures { get; private set; }

    public CultureInfo CurrentCulture { get; private set; }

    public CultureInfo CurrentUICulture { get; private set; }

    public ResourcesManager(XmlElement xResources)
    {
      if (xResources == null)
        throw new ArgumentNullException("No resource dictionary.");
      this.ResourcesDictionary = xResources;
      this.SupportedCultures = new List<CultureInfo>();
      this.DoUpdateSupportedCultures();
    }

    private void DoUpdateSupportedCultures()
    {
      this.SupportedCultures.Clear();
      XmlNodeList xmlNodeList = this.ResourcesDictionary.SelectNodes("CultureInfoList/CultureInfo");
      if (xmlNodeList.Count <= 0)
        throw new DexComException("Resources Dictionary did not contain any culture entries!");
      foreach (XmlElement element in xmlNodeList)
      {
        XObject xobject = new XObject(element);
        CultureInfo cultureInfo = new CultureInfo(xobject.Name);
        this.SupportedCultures.Add(cultureInfo);
        xobject.SetAttribute("LCID", cultureInfo.LCID);
        xobject.SetAttribute("EnglishName", cultureInfo.EnglishName);
        xobject.SetAttribute("NativeName", cultureInfo.NativeName);
        xobject.SetAttribute("DisplayName", cultureInfo.DisplayName);
      }
    }

    public void ChangeCulture(CultureInfo culture)
    {
      Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(culture);
      if (dictionary != null)
      {
        this.m_defaultDictionary = dictionary;
        this.CurrentCulture = culture;
      }
      else
      {
        dictionary = this.DoCreateDictionary(this.ResourcesDictionary, culture);
        if (dictionary != null)
        {
          this.m_dictionaryCache.Add(culture.LCID, dictionary);
          this.m_defaultDictionary = dictionary;
          this.CurrentCulture = culture;
        }
        else if (!culture.IsNeutralCulture && culture.Parent != null && !string.IsNullOrEmpty(culture.Parent.Name))
        {
          dictionary = this.DoCreateDictionary(this.ResourcesDictionary, culture.Parent);
          if (dictionary != null)
          {
            this.m_dictionaryCache.Add(culture.Parent.LCID, dictionary);
            this.m_defaultDictionary = dictionary;
            this.CurrentCulture = culture;
          }
        }
      }
      if (dictionary == null)
        throw new DexComException(string.Format("Failed to change resource dictionary for culture '{0}'.", (object) culture.Name));
      if (culture.IsNeutralCulture)
      {
        culture = CultureInfo.CreateSpecificCulture(culture.Name);
        culture = new CultureInfo(culture.LCID, true);
      }
      this.CurrentUICulture = culture;
      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
    }

    public string Lookup(T resourceKey)
    {
      return this.m_defaultDictionary[resourceKey].Value;
    }

    public string Lookup(T resourceKey, params object[] args)
    {
      return string.Format(this.m_defaultDictionary[resourceKey].Value, args);
    }

    public string Lookup(T resourceKey, CultureInfo culture)
    {
      Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(culture);
      if (dictionary != null)
        return dictionary[resourceKey].Value;
      else
        throw new DexComException(string.Format("Failed to find resource dictionary for culture '{0}'.", (object) culture.Name));
    }

    public string Lookup(T resourceKey, CultureInfo culture, params object[] args)
    {
      Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(culture);
      if (dictionary != null)
        return string.Format(dictionary[resourceKey].Value, args);
      else
        throw new DexComException(string.Format("Failed to find resource dictionary for culture '{0}'.", (object) culture.Name));
    }

    private Dictionary<T, ResourcesManager<T>.ResourceEntry> DoGetDictionary(CultureInfo culture)
    {
      Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = (Dictionary<T, ResourcesManager<T>.ResourceEntry>) null;
      if (this.m_dictionaryCache.ContainsKey(culture.LCID))
        return this.m_dictionaryCache[culture.LCID];
      if (!culture.IsNeutralCulture && culture.Parent != null && (!string.IsNullOrEmpty(culture.Parent.Name) && this.m_dictionaryCache.ContainsKey(culture.Parent.LCID)))
        return this.m_dictionaryCache[culture.Parent.LCID];
      else
        return dictionary;
    }

    private Dictionary<T, ResourcesManager<T>.ResourceEntry> DoCreateDictionary(XmlElement xResources, CultureInfo culture)
    {
      Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = (Dictionary<T, ResourcesManager<T>.ResourceEntry>) null;
      XmlNodeList xmlNodeList = xResources.SelectNodes(string.Format("StringDictionary//Entry/Value[@Culture='{0}']", (object) culture.Name));
      if (xmlNodeList.Count > 0)
      {
        dictionary = new Dictionary<T, ResourcesManager<T>.ResourceEntry>(xmlNodeList.Count);
        foreach (XmlElement element in xmlNodeList)
        {
          XObject xobject1 = new XObject(element.ParentNode as XmlElement);
          XObject xobject2 = new XObject(element);
          T obj = default (T);
          T key;
          try
          {
            key = (T) xobject1.GetAttributeAsEnum("Key", typeof (T));
          }
          catch (ArgumentException ex)
          {
            throw new DexComException(string.Format("Unknown resource key '{0}' found while loading resource dictionary for culture '{1}'.", (object) xobject1.GetAttribute<string>("Key"), (object) culture.Name));
          }
          dictionary.Add(key, new ResourcesManager<T>.ResourceEntry(xobject2.GetAttribute<string>("Value")));
        }
      }
      return dictionary;
    }

    [Conditional("DEBUG")]
    private void DoVerifyDictionary(CultureInfo culture, Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary)
    {
      foreach (T key in Enum.GetValues(typeof (T)))
      {
        if (key.ToString() != default (T).ToString() && !dictionary.ContainsKey(key))
          throw new DexComException(string.Format("Verification of resource dictionary for culture '{0}' failed.  Could not find key '{1}'.", (object) culture.Name, (object) key.ToString()));
      }
    }

    [Conditional("DEBUG")]
    public void RemoveCultureFromResourceDictionary(XmlDocument xResourcesDictionaryDocument, CultureInfo culture)
    {
      foreach (XmlElement xmlElement in xResourcesDictionaryDocument.DocumentElement.SelectNodes(string.Format("CultureInfoList/CultureInfo[@Name='{0}']", (object) culture.Name)))
        xmlElement.ParentNode.RemoveChild((XmlNode) xmlElement);
      foreach (XmlElement xmlElement in xResourcesDictionaryDocument.DocumentElement.SelectNodes(string.Format("StringDictionary//Entry/Value[@Culture='{0}']", (object) culture.Name)))
        xmlElement.ParentNode.RemoveChild((XmlNode) xmlElement);
      this.DoUpdateSupportedCultures();
    }

    [Conditional("DEBUG")]
    public void AddCultureToResourceDictionary(XmlDocument xResourcesDictionaryDocument, CultureInfo culture)
    {
      XmlElement xmlElement1 = xResourcesDictionaryDocument.DocumentElement.SelectSingleNode("CultureInfoList") as XmlElement;
      XObject xobject1 = new XObject("CultureInfo", xResourcesDictionaryDocument);
      xobject1.SetAttribute("Name", culture.Name);
      xobject1.SetAttribute("LCID", culture.LCID);
      xobject1.SetAttribute("EnglishName", culture.EnglishName);
      xobject1.SetAttribute("NativeName", culture.NativeName);
      xobject1.SetAttribute("DisplayName", culture.DisplayName);
      xmlElement1.AppendChild((XmlNode) xobject1.Element);
      XmlNodeList xmlNodeList = xResourcesDictionaryDocument.DocumentElement.SelectNodes("StringDictionary//Entry/Value[@Culture='en-US']");
      string name = culture.Name;
      foreach (XmlElement xmlElement2 in xmlNodeList)
      {
        XObject xobject2 = new XObject(xmlElement2.Clone() as XmlElement);
        xobject2.SetAttribute("Culture", name);
        xobject2.SetAttribute("Value", name + ":" + xobject2.GetAttribute("Value"));
        xmlElement2.ParentNode.AppendChild((XmlNode) xobject2.Element);
      }
      this.SupportedCultures.Add(culture);
    }

    [Conditional("DEBUG")]
    public void MergeCultureWithResourceDictionary(XmlDocument xResourcesDictionaryDocument, CultureInfo culture, XmlDocument xMergeDictionary)
    {
      if (!(xResourcesDictionaryDocument.DocumentElement.SelectSingleNode(string.Format("CultureInfoList/CultureInfo[@Name='{0}']", (object) culture.Name)) is XmlElement))
      {
        XmlElement xmlElement = xResourcesDictionaryDocument.DocumentElement.SelectSingleNode("CultureInfoList") as XmlElement;
        XObject xobject = new XObject("CultureInfo", xResourcesDictionaryDocument);
        xobject.SetAttribute("Name", culture.Name);
        xobject.SetAttribute("LCID", culture.LCID);
        xobject.SetAttribute("EnglishName", culture.EnglishName);
        xobject.SetAttribute("NativeName", culture.NativeName);
        xobject.SetAttribute("DisplayName", culture.DisplayName);
        xmlElement.AppendChild((XmlNode) xobject.Element);
        this.SupportedCultures.Add(culture);
      }
      XmlNodeList xmlNodeList = xResourcesDictionaryDocument.DocumentElement.SelectNodes("StringDictionary//Entry/Value[@Culture='en-US']");
      string name = culture.Name;
      foreach (XmlElement xmlElement1 in xmlNodeList)
      {
        string xpath = string.Format("//Entry[@Key='{0}']/Value[@Culture='{1}']", (object) (xmlElement1.ParentNode as XmlElement).GetAttribute("Key"), (object) name);
        XmlElement element = xMergeDictionary.DocumentElement.SelectSingleNode(xpath) as XmlElement;
        XmlElement xmlElement2 = xResourcesDictionaryDocument.DocumentElement.SelectSingleNode(xpath) as XmlElement;
        XObject xobject = new XObject(element);
        if (!xobject.HasAttribute("Value"))
          xobject.SetAttribute("Value", string.Empty);
        XmlElement xmlElement3 = xResourcesDictionaryDocument.ImportNode((XmlNode) element, true) as XmlElement;
        if (xmlElement2 == null)
          xmlElement1.ParentNode.AppendChild((XmlNode) xmlElement3);
        else
          xmlElement1.ParentNode.ReplaceChild((XmlNode) xmlElement3, (XmlNode) xmlElement2);
      }
    }

    [Conditional("DEBUG")]
    public void ExtractCultureFromResourceDictionary(XmlDocument xResourcesDictionaryDocument, CultureInfo culture, XmlDocument xMergeDictionary)
    {
      XmlElement xmlElement1 = xResourcesDictionaryDocument.DocumentElement.SelectSingleNode(string.Format("CultureInfoList/CultureInfo[@Name='{0}']", (object) culture.Name)) as XmlElement;
      XmlElement xmlElement2 = xResourcesDictionaryDocument.DocumentElement.SelectSingleNode(string.Format("CultureInfoList/CultureInfo[@Name='{0}']", (object) "en-US")) as XmlElement;
      if (xmlElement1 == null)
        throw new DexComException("Requested culture to be extracted missing from ResourceDictionary!");
      if (xmlElement2 == null)
        throw new DexComException("English comparison culture required for extracting from ResourceDictionary!");
      xMergeDictionary.LoadXml("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>" + Environment.NewLine + "<MergeDictionary />");
      XmlNodeList xmlNodeList = xResourcesDictionaryDocument.DocumentElement.SelectNodes("StringDictionary//Entry/Value[@Culture='en-US']");
      string name = culture.Name;
      foreach (XmlElement xmlElement3 in xmlNodeList)
      {
        XmlElement xmlElement4 = xmlElement3.ParentNode as XmlElement;
        string xpath = string.Format("Value[@Culture='{1}']", (object) xmlElement4.GetAttribute("Key"), (object) name);
        XmlElement xmlElement5 = xmlElement4.SelectSingleNode(xpath) as XmlElement;
        XmlElement xmlElement6 = xMergeDictionary.ImportNode((XmlNode) xmlElement4, false) as XmlElement;
        XmlElement xmlElement7 = xMergeDictionary.ImportNode((XmlNode) xmlElement5, true) as XmlElement;
        xmlElement6.SetAttribute("English", xmlElement3.GetAttribute("Value"));
        xmlElement6.AppendChild((XmlNode) xmlElement7);
        xMergeDictionary.DocumentElement.AppendChild((XmlNode) xmlElement6);
      }
    }

    [Conditional("DEBUG")]
    private void DoIncrementUsage(ResourcesManager<T>.ResourceEntry entry)
    {
      ++entry.Usage;
    }

    [Conditional("DEBUG")]
    public void DumpResourceUsage(string filePath, bool unusedOnly)
    {
      XObject xobject = new XObject("ResourceUsage");
      xobject.Element.OwnerDocument.AppendChild((XmlNode) xobject.Element);
      xobject.SetAttribute("DateTimeCreated", DateTimeOffset.Now);
      xobject.SetAttribute("TotalKeyCount", Enum.GetValues(typeof (T)).GetLength(0) - 1);
      foreach (CultureInfo culture in this.SupportedCultures)
      {
        XObject xChildObject1 = new XObject("Culture", xobject.Element.OwnerDocument);
        xobject.AppendChild(xChildObject1);
        xChildObject1.SetAttribute("Name", culture.Name);
        Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(culture);
        if (dictionary != null)
        {
          int intValue = 0;
          foreach (T index in Enum.GetValues(typeof (T)))
          {
            if (index.ToString() != default (T).ToString())
            {
              ResourcesManager<T>.ResourceEntry resourceEntry = dictionary[index];
              if (resourceEntry.Usage == 0)
              {
                XObject xChildObject2 = new XObject("Entry", xobject.Element.OwnerDocument);
                xChildObject2.SetAttribute("Key", index.ToString());
                xChildObject2.SetAttribute("Usage", resourceEntry.Usage);
                xChildObject1.AppendChild(xChildObject2);
                ++intValue;
                xChildObject1.SetAttribute("UnusedCount", intValue);
              }
            }
          }
          if (!unusedOnly)
          {
            foreach (T index in Enum.GetValues(typeof (T)))
            {
              if (index.ToString() != default (T).ToString())
              {
                ResourcesManager<T>.ResourceEntry resourceEntry = dictionary[index];
                if (resourceEntry.Usage > 0)
                {
                  XObject xChildObject2 = new XObject("Entry", xobject.Element.OwnerDocument);
                  xChildObject2.SetAttribute("Key", index.ToString());
                  xChildObject2.SetAttribute("Usage", resourceEntry.Usage);
                  xChildObject1.AppendChild(xChildObject2);
                }
              }
            }
          }
        }
      }
      xobject.Element.OwnerDocument.Save(filePath);
    }

    [Conditional("DEBUG")]
    public void LoadResourceUsage(string filePath)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(filePath);
      CultureInfo currentCulture = this.CurrentCulture;
      XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Culture");
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        if (xmlElement.HasAttribute("UnusedCount"))
          this.ChangeCulture(new CultureInfo(xmlElement.GetAttribute("Name")));
      }
      this.ChangeCulture(currentCulture);
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(new CultureInfo(xmlElement.GetAttribute("Name")));
        if (dictionary != null)
        {
          foreach (XmlElement element in xmlElement.SelectNodes("Entry"))
          {
            XObject xobject = new XObject(element);
            string attribute1 = xobject.GetAttribute<string>("Key");
            int attribute2 = xobject.GetAttribute<int>("Usage");
            if (Enum.IsDefined(typeof (T), (object) attribute1))
            {
              T index = (T) Enum.Parse(typeof (T), attribute1);
              dictionary[index].Usage = attribute2;
            }
          }
        }
      }
    }

    [Conditional("DEBUG")]
    public void ClearResourceUsage()
    {
      foreach (CultureInfo culture in this.SupportedCultures)
      {
        Dictionary<T, ResourcesManager<T>.ResourceEntry> dictionary = this.DoGetDictionary(culture);
        if (dictionary != null)
        {
          foreach (T index in Enum.GetValues(typeof (T)))
          {
            if (index.ToString() != default (T).ToString())
              dictionary[index].Usage = 0;
          }
        }
      }
    }

    private class ResourceEntry
    {
      public string Value { get; private set; }

      public int Usage { get; set; }

      public ResourceEntry()
      {
      }

      public ResourceEntry(string value)
      {
        this.Value = value;
      }

      public override string ToString()
      {
        return this.Value;
      }
    }
  }
}
