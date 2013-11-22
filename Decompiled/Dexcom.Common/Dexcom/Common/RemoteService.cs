// Type: Dexcom.Common.RemoteService
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using System.Xml;

namespace Dexcom.Common
{
  public class RemoteService
  {
    private static IDictionary m_defaultClientTypes = (IDictionary) new Hashtable();
    private static IDictionary<string, IDictionary> m_listOfExtendedClients = (IDictionary<string, IDictionary>) new Dictionary<string, IDictionary>();

    static RemoteService()
    {
      foreach (WellKnownClientTypeEntry knownClientTypeEntry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
      {
        if (knownClientTypeEntry.ObjectType == (Type) null)
          throw new RemotingException("Error reading remote type from configuration file.");
        RemoteService.m_defaultClientTypes.Add((object) knownClientTypeEntry.ObjectType, (object) knownClientTypeEntry);
      }
      RemoteService.m_listOfExtendedClients.Add("default", RemoteService.m_defaultClientTypes);
      RemoteService.DoParseExtendedRemotingSetupSection();
    }

    private static void DoParseExtendedRemotingSetupSection()
    {
      XmlElement xmlElement = (XmlElement) ConfigurationManager.GetSection("ExtendedRemotingSetup");
      if (xmlElement == null)
        return;
      string str = string.Empty;
      foreach (XmlElement element1 in xmlElement.SelectNodes("ExtendedClient"))
      {
        XObject xobject1 = new XObject(element1);
        string name = xobject1.Name;
        XmlNodeList xmlNodeList = xobject1.Element.SelectNodes("client/wellknown");
        IDictionary dictionary = (IDictionary) new Hashtable();
        foreach (XmlElement element2 in xmlNodeList)
        {
          XObject xobject2 = new XObject(element2);
          WellKnownClientTypeEntry knownClientTypeEntry = new WellKnownClientTypeEntry(Type.GetType(xobject2.GetAttribute("type")), xobject2.GetAttribute("url"));
          dictionary.Add((object) Type.GetType(xobject2.GetAttribute("type")), (object) knownClientTypeEntry);
        }
        RemoteService.m_listOfExtendedClients.Add(name, dictionary);
      }
    }

    public static object GetObject(Type type)
    {
      WellKnownClientTypeEntry knownClientTypeEntry = (WellKnownClientTypeEntry) RemoteService.m_defaultClientTypes[(object) type];
      if (knownClientTypeEntry == null)
        throw new RemotingException(string.Format("Remote type {0} not registered in configuration file!  Remember to call RemotingConfiguration.Configure()", (object) type.ToString()));
      else
        return Activator.GetObject(knownClientTypeEntry.ObjectType, knownClientTypeEntry.ObjectUrl);
    }

    public static object GetObject(Type type, string objectUrl)
    {
      return Activator.GetObject(type, objectUrl);
    }

    public static object GetExtendedObject(string extendedClientName, Type type)
    {
      WellKnownClientTypeEntry knownClientTypeEntry = RemoteService.m_listOfExtendedClients[extendedClientName][(object) type] as WellKnownClientTypeEntry;
      if (knownClientTypeEntry == null)
        throw new RemotingException(string.Format("Extended Remote type {0} not registered in ExtendedRemotingSetup configuration file!  Remember to call RemotingConfiguration.Configure()", (object) type.ToString()));
      else
        return Activator.GetObject(knownClientTypeEntry.ObjectType, knownClientTypeEntry.ObjectUrl);
    }

    public static ICollection<string> GetExtendedClientNames()
    {
      return RemoteService.m_listOfExtendedClients.Keys;
    }
  }
}
