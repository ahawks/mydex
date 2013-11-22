// Type: Dexcom.Client.Controls.FormSettings
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Client.Common;
using Dexcom.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.Xml;

namespace Dexcom.Client.Controls
{
  public class FormSettings : Component
  {
    private string m_settingsSuffix = string.Empty;
    private bool m_isEnabled = true;
    private bool m_doSaveMaximized = true;
    private bool m_doSaveSize = true;
    private bool m_doSaveLocation = true;
    private Container components;
    private Form m_hostForm;
    private Rectangle m_formBounds;
    private bool m_canResize;
    private bool m_doSaveMinimized;

    [Browsable(false)]
    public Form HostForm
    {
      get
      {
        if (this.DesignMode && this.m_hostForm == null)
        {
          IDesignerHost designerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
          if (designerHost != null)
            this.m_hostForm = designerHost.RootComponent as Form;
        }
        return this.m_hostForm;
      }
      set
      {
        if (this.DesignMode)
        {
          this.m_hostForm = value;
        }
        else
        {
          if (this.m_hostForm != null && this.m_hostForm != value)
            throw new InvalidOperationException("Attempt to change HostForm at run time.");
          if (this.m_hostForm == value)
            return;
          this.m_hostForm = value;
          this.m_hostForm.Load += new EventHandler(this.HostForm_Load);
          this.m_hostForm.Closing += new CancelEventHandler(this.HostForm_Closing);
          this.m_hostForm.Resize += new EventHandler(this.HostForm_Resize);
          this.m_hostForm.Move += new EventHandler(this.HostForm_Move);
          if (this.m_hostForm.FormBorderStyle == FormBorderStyle.Sizable || this.m_hostForm.FormBorderStyle == FormBorderStyle.SizableToolWindow)
            this.m_canResize = true;
          else
            this.m_canResize = false;
        }
      }
    }

    [Category("Behavior")]
    [Description("Without a suffix the default name where settings are saved is the type name of the form.  Adding a suffix allows form settings to be saved under an altered name for the same type of form.")]
    [DefaultValue("")]
    [Browsable(true)]
    public string SettingsSuffix
    {
      get
      {
        return this.m_settingsSuffix;
      }
      set
      {
        this.m_settingsSuffix = value.Trim();
      }
    }

    [Description("Turns on/off the saving of form settings.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool Enabled
    {
      get
      {
        return this.m_isEnabled;
      }
      set
      {
        this.m_isEnabled = value;
      }
    }

    [Description("Allow form's minimized state to be saved/restored.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool SaveMinimized
    {
      get
      {
        return this.m_doSaveMinimized;
      }
      set
      {
        this.m_doSaveMinimized = value;
      }
    }

    [Description("Allow form's maximized state to be saved/restored.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool SaveMaximized
    {
      get
      {
        return this.m_doSaveMaximized;
      }
      set
      {
        this.m_doSaveMaximized = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Allow form's size (width, height) to be saved/restored.")]
    public bool SaveSize
    {
      get
      {
        return this.m_doSaveSize;
      }
      set
      {
        this.m_doSaveSize = value;
      }
    }

    [Description("Allow form's location (x, y) to be saved and restored.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool SaveLocation
    {
      get
      {
        return this.m_doSaveLocation;
      }
      set
      {
        this.m_doSaveLocation = value;
      }
    }

    public event FormSettings.SaveFormSettingsDelegate SaveFormSettings;

    public event FormSettings.LoadFormSettingsDelegate LoadFormSettings;

    public FormSettings(IContainer container)
    {
      container.Add((IComponent) this);
      this.InitializeComponent();
    }

    public FormSettings()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.m_hostForm = (Form) null;
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void HostForm_Resize(object sender, EventArgs e)
    {
      if (this.m_hostForm == null || this.m_hostForm.Disposing || (this.m_hostForm.IsDisposed || this.m_hostForm.WindowState != FormWindowState.Normal))
        return;
      this.m_formBounds = this.m_hostForm.Bounds;
    }

    private void HostForm_Move(object sender, EventArgs e)
    {
      if (this.m_hostForm == null || this.m_hostForm.Disposing || (this.m_hostForm.IsDisposed || this.m_hostForm.WindowState != FormWindowState.Normal))
        return;
      this.m_formBounds = this.m_hostForm.Bounds;
    }

    private void HostForm_Load(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_isEnabled || KeyUtils.IsShiftKeyDown())
          return;
        using (IsolatedStorageFile storeForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
        {
          XmlDocument xmlDocument = new XmlDocument();
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/FormSettings.xml", FileMode.Open, FileAccess.Read, storeForAssembly))
            xmlDocument.Load((Stream) storageFileStream);
          if (xmlDocument.DocumentElement == null)
            return;
          string xpath = string.Format("/FormSettings/Form[@Name='{0}']", (object) (this.m_hostForm.GetType().FullName + this.m_settingsSuffix));
          XmlElement xmlElement = (XmlElement) xmlDocument.DocumentElement.SelectSingleNode(xpath);
          if (xmlElement == null)
            return;
          XmlElement element = (XmlElement) xmlElement.SelectSingleNode("Bounds");
          if (element != null && this.m_canResize && (this.m_doSaveLocation || this.m_doSaveSize))
          {
            XObject xobject = new XObject(element);
            int x = xobject.GetAttributeAsInt("X");
            int y = xobject.GetAttributeAsInt("Y");
            int width = xobject.GetAttributeAsInt("Width");
            int height = xobject.GetAttributeAsInt("Height");
            if (!this.m_doSaveLocation)
            {
              x = this.m_hostForm.Location.X;
              y = this.m_hostForm.Location.Y;
            }
            if (!this.m_doSaveSize)
            {
              width = this.m_hostForm.Width;
              height = this.m_hostForm.Height;
            }
            Screen[] allScreens = Screen.AllScreens;
            Region region = new Region(new Rectangle());
            foreach (Screen screen in allScreens)
              region.Union(screen.WorkingArea);
            Rectangle rect = new Rectangle(x, y, width, height);
            rect.Width -= 40;
            rect.Height -= 40;
            rect.X += 20;
            rect.Y += 20;
            if (region.IsVisible(rect, this.m_hostForm.CreateGraphics()))
            {
              this.m_hostForm.StartPosition = FormStartPosition.Manual;
              this.m_hostForm.Bounds = new Rectangle(x, y, width, height);
            }
          }
          if (this.m_doSaveMinimized || this.m_doSaveMaximized)
          {
            object obj1 = (object) xmlElement.GetAttribute("WindowState");
            if (obj1 != null && (string) obj1 != string.Empty)
            {
              object obj2 = Enum.Parse(typeof (FormWindowState), (string) obj1);
              FormWindowState formWindowState = obj2 == null ? FormWindowState.Normal : (FormWindowState) obj2;
              if (formWindowState != FormWindowState.Normal)
              {
                if (formWindowState == FormWindowState.Maximized && this.m_doSaveMaximized)
                  this.m_hostForm.WindowState = formWindowState;
                else if (formWindowState == FormWindowState.Minimized && this.m_doSaveMinimized)
                  this.m_hostForm.WindowState = formWindowState;
              }
            }
          }
          EventUtils.FireEvent((Delegate) this.LoadFormSettings, (object) this, (object) xmlElement);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void HostForm_Closing(object sender, CancelEventArgs e)
    {
      try
      {
        if (!this.m_isEnabled || KeyUtils.IsShiftKeyDown())
          return;
        using (IsolatedStorageFile storeForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
        {
          storeForAssembly.CreateDirectory("Settings");
          XmlElement xmlElement1 = (XmlElement) null;
          XmlDocument xmlDocument = new XmlDocument();
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/FormSettings.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, storeForAssembly))
          {
            if (storageFileStream.Length != 0L)
            {
              xmlDocument.Load((Stream) storageFileStream);
              xmlElement1 = (XmlElement) xmlDocument.SelectSingleNode("FormSettings");
              if (xmlElement1 == null)
              {
                xmlDocument.LoadXml("<FormSettings/>");
                xmlElement1 = (XmlElement) xmlDocument.SelectSingleNode("FormSettings");
              }
            }
            else
            {
              xmlElement1 = xmlDocument.CreateElement("FormSettings");
              xmlElement1 = (XmlElement) xmlDocument.AppendChild((XmlNode) xmlElement1);
            }
          }
          if (xmlElement1 == null)
            return;
          string str = this.m_hostForm.GetType().FullName + this.m_settingsSuffix;
          string xpath = string.Format("Form[@Name='{0}']", (object) str);
          XmlElement xmlElement2 = (XmlElement) xmlElement1.SelectSingleNode(xpath);
          if (xmlElement2 == null)
          {
            XmlElement element = xmlDocument.CreateElement("Form");
            xmlElement2 = (XmlElement) xmlElement1.AppendChild((XmlNode) element);
          }
          if (xmlElement2 == null)
            return;
          xmlElement2.SetAttribute("Name", str);
          xmlElement2.SetAttribute("WindowState", ((object) this.m_hostForm.WindowState).ToString());
          Rectangle rectangle1 = new Rectangle();
          Rectangle rectangle2 = this.m_hostForm.WindowState != FormWindowState.Normal ? this.m_formBounds : this.m_hostForm.Bounds;
          XmlElement xmlElement3 = (XmlElement) xmlElement2.SelectSingleNode("Bounds");
          if (xmlElement3 == null)
          {
            XmlElement element = xmlDocument.CreateElement("Bounds");
            xmlElement3 = (XmlElement) xmlElement2.AppendChild((XmlNode) element);
          }
          if (xmlElement3 != null)
          {
            xmlElement3.SetAttribute("X", rectangle2.X.ToString());
            xmlElement3.SetAttribute("Y", rectangle2.Y.ToString());
            xmlElement3.SetAttribute("Width", rectangle2.Width.ToString());
            xmlElement3.SetAttribute("Height", rectangle2.Height.ToString());
          }
          using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream("Settings/FormSettings.xml", FileMode.Truncate, FileAccess.Write, storeForAssembly))
          {
            EventUtils.FireEvent((Delegate) this.SaveFormSettings, (object) this, (object) xmlElement2);
            xmlDocument.Save((Stream) storageFileStream);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void InitializeComponent()
    {
      this.components = new Container();
    }

    public delegate void SaveFormSettingsDelegate(FormSettings sender, XmlElement xFormSettings);

    public delegate void LoadFormSettingsDelegate(FormSettings sender, XmlElement xFormSettings);
  }
}
