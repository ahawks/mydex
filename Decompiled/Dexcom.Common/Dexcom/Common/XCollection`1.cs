// Type: Dexcom.Common.XCollection`1
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common
{
  [Serializable]
  public class XCollection<T> : XObject, ISerializable, IEnumerable<T>, IEnumerable where T : XObject, new()
  {
    private static T m_template = Activator.CreateInstance<T>();

    public new string Name
    {
      get
      {
        return this.Element.Name;
      }
    }

    public T this[int index]
    {
      get
      {
        Trace.Assert(this.IsNotNull(), "XCollection's element is null.");
        XmlElement xmlElement = (XmlElement) this.Element.SelectNodes(XCollection<T>.m_template.TagName)[index];
        return (T) XCollection<T>.m_template.GetType().Module.Assembly.CreateInstance(XCollection<T>.m_template.GetType().FullName, 0 != 0, BindingFlags.CreateInstance, (Binder) null, new object[1]
        {
          (object) xmlElement
        }, (CultureInfo) null, new object[0]);
      }
    }

    public int Count
    {
      get
      {
        int num = 0;
        if (this.IsNotNull())
          num = this.Element.SelectNodes(XCollection<T>.m_template.TagName).Count;
        return num;
      }
    }

    private IEnumerator<T> Items
    {
      get
      {
        Trace.Assert(this.IsNotNull(), "XCollection's element is null.");
        XmlNodeList list = this.Element.SelectNodes(XCollection<T>.m_template.TagName);
        foreach (XmlElement xmlElement in list)
        {
          this.\u003Ct_item\u003E5__3 = (T) XCollection<T>.m_template.GetType().Module.Assembly.CreateInstance(XCollection<T>.m_template.GetType().FullName, 0 != 0, BindingFlags.CreateInstance, (Binder) null, new object[1]
          {
            (object) xmlElement
          }, (CultureInfo) null, new object[0]);
          T t_item;
          yield return t_item;
        }
      }
    }

    static XCollection()
    {
    }

    public XCollection(string strCollectionName)
      : base(strCollectionName)
    {
    }

    public XCollection(string strCollectionName, XmlDocument ownerDocument)
      : base(strCollectionName, ownerDocument)
    {
    }

    public XCollection(XmlElement element)
      : base(element)
    {
    }

    protected XCollection(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public void Add(T newT)
    {
      this.AppendChild((XObject) newT);
    }

    public void RemoveFirstIf(Predicate<T> predicate)
    {
      Trace.Assert(this.IsNotNull(), "XCollection's element is null.");
      foreach (XmlElement xmlElement in this.Element.SelectNodes(XCollection<T>.m_template.TagName))
      {
        T obj = (T) XCollection<T>.m_template.GetType().Module.Assembly.CreateInstance(XCollection<T>.m_template.GetType().FullName, 0 != 0, BindingFlags.CreateInstance, (Binder) null, new object[1]
        {
          (object) xmlElement
        }, (CultureInfo) null, new object[0]);
        if (predicate(obj))
        {
          this.Element.RemoveChild((XmlNode) xmlElement);
          break;
        }
      }
    }

    public T FindFirstIf(Predicate<T> predicate)
    {
      Trace.Assert(this.IsNotNull(), "XCollection's element is null.");
      XmlNodeList xmlNodeList = this.Element.SelectNodes(XCollection<T>.m_template.TagName);
      T obj1 = default (T);
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        T obj2 = (T) XCollection<T>.m_template.GetType().Module.Assembly.CreateInstance(XCollection<T>.m_template.GetType().FullName, 0 != 0, BindingFlags.CreateInstance, (Binder) null, new object[1]
        {
          (object) xmlElement
        }, (CultureInfo) null, new object[0]);
        if (predicate(obj2))
        {
          obj1 = obj2;
          break;
        }
      }
      return obj1;
    }

    public T FindLastIf(Predicate<T> predicate)
    {
      Trace.Assert(this.IsNotNull(), "XCollection's element is null.");
      XmlNodeList xmlNodeList = this.Element.SelectNodes(XCollection<T>.m_template.TagName);
      T obj1 = default (T);
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        T obj2 = (T) XCollection<T>.m_template.GetType().Module.Assembly.CreateInstance(XCollection<T>.m_template.GetType().FullName, 0 != 0, BindingFlags.CreateInstance, (Binder) null, new object[1]
        {
          (object) xmlElement
        }, (CultureInfo) null, new object[0]);
        if (predicate(obj2))
          obj1 = obj2;
      }
      return obj1;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this.Items;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return this.Items;
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}
