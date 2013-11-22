// Type: Dexcom.Client.Common.WindowSettings
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

namespace Dexcom.Client.Common
{
  public class WindowSettings
  {
    private Window m_hostWindow;
    private bool m_canResize;
    private XObject m_xWindowSettings;
    private bool m_isHostLoaded;

    public Window HostWindow
    {
      get
      {
        return this.m_hostWindow;
      }
      set
      {
        if (this.m_hostWindow != null && this.m_hostWindow != value)
        {
          if (value != null)
            throw new InvalidOperationException("Attempt to change Host Window at run time.");
          this.m_hostWindow.Initialized -= new EventHandler(this.m_hostWindow_Initialized);
          this.m_hostWindow.Loaded -= new RoutedEventHandler(this.m_hostWindow_Loaded);
          this.m_hostWindow.Closing -= new CancelEventHandler(this.m_hostWindow_Closing);
          this.m_hostWindow = value;
        }
        else
        {
          if (this.m_hostWindow == value)
            return;
          this.m_hostWindow = value;
          this.m_hostWindow.Initialized += new EventHandler(this.m_hostWindow_Initialized);
          this.m_hostWindow.Loaded += new RoutedEventHandler(this.m_hostWindow_Loaded);
          this.m_hostWindow.Closing += new CancelEventHandler(this.m_hostWindow_Closing);
          if (this.m_hostWindow.ResizeMode != ResizeMode.NoResize)
            this.m_canResize = true;
          else
            this.m_canResize = false;
        }
      }
    }

    public string WindowSuffix { get; set; }

    public bool IsEnabled { get; set; }

    public bool SaveMinimized { get; set; }

    public bool SaveMaximized { get; set; }

    public bool SaveSize { get; set; }

    public bool SaveLocation { get; set; }

    public XObject XWindowSettings
    {
      get
      {
        return this.m_xWindowSettings;
      }
    }

    public event WindowSettings.SaveWindowSettingsDelegate SaveWindowSettings;

    public WindowSettings()
    {
      this.IsEnabled = true;
      this.SaveMinimized = false;
      this.SaveMaximized = true;
      this.SaveSize = true;
      this.SaveLocation = true;
      this.WindowSuffix = string.Empty;
    }

    private void m_hostWindow_Loaded(object sender, RoutedEventArgs e)
    {
      this.m_isHostLoaded = true;
    }

    private void m_hostWindow_Initialized(object sender, EventArgs e)
    {
      try
      {
        if (this.IsEnabled && !KeyUtils.IsShiftKeyDown())
        {
          using (IsolatedStorageFile storeForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
          {
            XmlDocument xmlDocument = new XmlDocument();
            using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/WindowSettings.xml", FileMode.Open, FileAccess.Read, storeForAssembly))
              xmlDocument.Load((Stream) storageFileStream);
            if (xmlDocument.DocumentElement != null)
            {
              string xpath = string.Format("/WindowSettings/Window[@Name='{0}']", (object) (this.m_hostWindow.GetType().FullName + (string.IsNullOrEmpty(this.WindowSuffix) ? string.Empty : this.WindowSuffix.Trim())));
              XmlElement xmlElement = (XmlElement) xmlDocument.DocumentElement.SelectSingleNode(xpath);
              if (xmlElement != null)
              {
                this.m_xWindowSettings = new XObject(xmlElement.CloneNode(true) as XmlElement);
                XmlElement element = (XmlElement) xmlElement.SelectSingleNode("Bounds");
                if (element != null && this.m_canResize && (this.SaveLocation || this.SaveSize))
                {
                  XObject xobject = new XObject(element);
                  double num1 = xobject.GetAttribute<double>("X");
                  double num2 = xobject.GetAttribute<double>("Y");
                  double num3 = xobject.GetAttribute<double>("Width");
                  double num4 = xobject.GetAttribute<double>("Height");
                  if (!this.SaveLocation)
                  {
                    num1 = this.m_hostWindow.Left;
                    num2 = this.m_hostWindow.Top;
                  }
                  if (!this.SaveSize)
                  {
                    num3 = this.m_hostWindow.Width;
                    num4 = this.m_hostWindow.Height;
                  }
                  Screen[] allScreens = Screen.AllScreens;
                  Region region = new Region(new Rectangle());
                  foreach (Screen screen in allScreens)
                    region.Union(screen.WorkingArea);
                  RectangleF rect = new RectangleF(Convert.ToSingle(num1), Convert.ToSingle(num2), this.SaveSize ? Convert.ToSingle(num3) : 100f, this.SaveSize ? Convert.ToSingle(num4) : 100f);
                  rect.Width -= 40f;
                  rect.Height -= 40f;
                  rect.X += 20f;
                  rect.Y += 20f;
                  if (region.IsVisible(rect))
                  {
                    if (this.SaveSize)
                      this.m_hostWindow.SizeToContent = SizeToContent.Manual;
                    this.m_hostWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    this.m_hostWindow.Left = num1;
                    this.m_hostWindow.Top = num2;
                    this.m_hostWindow.Width = num3;
                    this.m_hostWindow.Height = num4;
                  }
                }
                if (!this.SaveMinimized && !this.SaveMaximized)
                  return;
                object obj1 = (object) xmlElement.GetAttribute("WindowState");
                if (obj1 == null || !((string) obj1 != string.Empty))
                  return;
                object obj2 = Enum.Parse(typeof (WindowState), (string) obj1);
                WindowState windowState = obj2 == null ? WindowState.Normal : (WindowState) obj2;
                if (windowState == WindowState.Normal)
                  return;
                if (windowState == WindowState.Maximized && this.SaveMaximized)
                {
                  this.m_hostWindow.WindowState = windowState;
                }
                else
                {
                  if (windowState != WindowState.Minimized || !this.SaveMinimized)
                    return;
                  this.m_hostWindow.WindowState = windowState;
                }
              }
              else
                this.DoAssignDefaultSize();
            }
            else
              this.DoAssignDefaultSize();
          }
        }
        else
        {
          if (!this.IsEnabled)
            return;
          this.DoAssignDefaultSize();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void DoAssignDefaultSize()
    {
      if (double.IsNaN(this.m_hostWindow.Width) && !double.IsPositiveInfinity(this.m_hostWindow.MinWidth))
        this.m_hostWindow.Width = this.m_hostWindow.MinWidth;
      if (!double.IsNaN(this.m_hostWindow.Height) || double.IsPositiveInfinity(this.m_hostWindow.MinHeight))
        return;
      this.m_hostWindow.Height = this.m_hostWindow.MinHeight;
    }

    private void m_hostWindow_Closing(object sender, CancelEventArgs e)
    {
      try
      {
        if (!this.m_isHostLoaded || !this.IsEnabled || KeyUtils.IsShiftKeyDown())
          return;
        using (IsolatedStorageFile storeForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
        {
          storeForAssembly.CreateDirectory("Settings");
          XmlElement xmlElement1 = (XmlElement) null;
          XmlDocument xmlDocument = new XmlDocument();
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/WindowSettings.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, storeForAssembly))
          {
            if (storageFileStream.Length != 0L)
            {
              try
              {
                xmlDocument.Load((Stream) storageFileStream);
                xmlElement1 = (XmlElement) xmlDocument.SelectSingleNode("WindowSettings");
              }
              catch (Exception ex)
              {
              }
              if (xmlElement1 == null)
              {
                xmlDocument.LoadXml("<WindowSettings/>");
                xmlElement1 = (XmlElement) xmlDocument.SelectSingleNode("WindowSettings");
              }
            }
            else
            {
              xmlElement1 = xmlDocument.CreateElement("WindowSettings");
              xmlElement1 = (XmlElement) xmlDocument.AppendChild((XmlNode) xmlElement1);
            }
          }
          if (xmlElement1 == null)
            return;
          string str = this.m_hostWindow.GetType().FullName + (string.IsNullOrEmpty(this.WindowSuffix) ? string.Empty : this.WindowSuffix.Trim());
          string xpath = string.Format("Window[@Name='{0}']", (object) str);
          XmlElement element1 = (XmlElement) xmlElement1.SelectSingleNode(xpath);
          if (element1 == null)
          {
            XmlElement element2 = xmlDocument.CreateElement("Window");
            element1 = (XmlElement) xmlElement1.AppendChild((XmlNode) element2);
          }
          if (element1 == null)
            return;
          element1.SetAttribute("Name", str);
          element1.SetAttribute("WindowState", ((object) this.m_hostWindow.WindowState).ToString());
          Rect restoreBounds = this.m_hostWindow.RestoreBounds;
          XmlElement xmlElement2 = element1.SelectSingleNode("Bounds") as XmlElement;
          if (xmlElement2 == null)
          {
            XmlElement element2 = xmlDocument.CreateElement("Bounds");
            xmlElement2 = (XmlElement) element1.AppendChild((XmlNode) element2);
          }
          if (xmlElement2 != null)
          {
            xmlElement2.SetAttribute("X", restoreBounds.X.ToString());
            xmlElement2.SetAttribute("Y", restoreBounds.Y.ToString());
            xmlElement2.SetAttribute("Width", restoreBounds.Width.ToString());
            xmlElement2.SetAttribute("Height", restoreBounds.Height.ToString());
          }
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/WindowSettings.xml", FileMode.Truncate, FileAccess.Write, storeForAssembly))
          {
            EventUtils.FireEvent((Delegate) this.SaveWindowSettings, (object) this, (object) new XObject(element1));
            xmlDocument.Save((Stream) storageFileStream);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    public delegate void SaveWindowSettingsDelegate(WindowSettings sender, XObject xWindowSettings);
  }
}
