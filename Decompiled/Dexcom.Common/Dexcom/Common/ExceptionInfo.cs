// Type: Dexcom.Common.ExceptionInfo
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

namespace Dexcom.Common
{
  [Serializable]
  public class ExceptionInfo : ISerializable
  {
    private Guid m_id = Guid.NewGuid();
    private DateTime m_dateThrown = DateTime.Now;
    private ExceptionType m_type = ExceptionType.Unknown;
    private string m_strHostName = string.Empty;
    private string m_strHostIP = string.Empty;
    private static int m_infoArrayLength = System.Enum.GetValues(typeof (ExceptionType)).Length;
    private static ExceptionInfo[] m_infoArray = new ExceptionInfo[ExceptionInfo.m_infoArrayLength];
    private ExceptionSeverity m_severity;
    private ExceptionCategory m_category;

    public Guid Id
    {
      get
      {
        return this.m_id;
      }
      set
      {
        this.m_id = value;
      }
    }

    public ExceptionType Type
    {
      get
      {
        return this.m_type;
      }
    }

    public ExceptionSeverity Severity
    {
      get
      {
        return this.m_severity;
      }
      set
      {
        this.m_severity = value;
      }
    }

    public ExceptionCategory Category
    {
      get
      {
        return this.m_category;
      }
      set
      {
        this.m_category = value;
      }
    }

    public DateTime DateThrown
    {
      get
      {
        return this.m_dateThrown;
      }
      set
      {
        this.m_dateThrown = value;
      }
    }

    public string HostName
    {
      get
      {
        return this.m_strHostName;
      }
      set
      {
        this.m_strHostName = value;
      }
    }

    public string HostIP
    {
      get
      {
        return this.m_strHostIP;
      }
      set
      {
        this.m_strHostIP = value;
      }
    }

    static ExceptionInfo()
    {
      ExceptionInfo.m_infoArray[0] = new ExceptionInfo(ExceptionType.None, ExceptionSeverity.None, ExceptionCategory.None);
      ExceptionInfo.m_infoArray[1] = new ExceptionInfo(ExceptionType.Unknown, ExceptionSeverity.None, ExceptionCategory.None);
      ExceptionInfo.m_infoArray[2] = new ExceptionInfo(ExceptionType.Database, ExceptionSeverity.Severe, ExceptionCategory.Database);
      ExceptionInfo.m_infoArray[3] = new ExceptionInfo(ExceptionType.System, ExceptionSeverity.Critical, ExceptionCategory.System);
      ExceptionInfo.m_infoArray[4] = new ExceptionInfo(ExceptionType.Security, ExceptionSeverity.Severe, ExceptionCategory.Security);
      ExceptionInfo.m_infoArray[5] = new ExceptionInfo(ExceptionType.ObjectNotFound, ExceptionSeverity.Severe, ExceptionCategory.Database);
      ExceptionInfo.m_infoArray[6] = new ExceptionInfo(ExceptionType.SessionNotValid, ExceptionSeverity.Minor, ExceptionCategory.Logical);
      ExceptionInfo.m_infoArray[7] = new ExceptionInfo(ExceptionType.NotPrivileged, ExceptionSeverity.Severe, ExceptionCategory.Security);
      ExceptionInfo.m_infoArray[8] = new ExceptionInfo(ExceptionType.NameAlreadyExists, ExceptionSeverity.Minor, ExceptionCategory.Validation);
      ExceptionInfo.m_infoArray[9] = new ExceptionInfo(ExceptionType.TimedOut, ExceptionSeverity.Minor, ExceptionCategory.Validation);
      ExceptionInfo.m_infoArray[10] = new ExceptionInfo(ExceptionType.InvalidArgument, ExceptionSeverity.Severe, ExceptionCategory.Validation);
      ExceptionInfo.m_infoArray[11] = new ExceptionInfo(ExceptionType.InvalidDigitalSignature, ExceptionSeverity.Severe, ExceptionCategory.Validation);
      ExceptionInfo.m_infoArray[12] = new ExceptionInfo(ExceptionType.ObjectAlreadyExists, ExceptionSeverity.Minor, ExceptionCategory.Validation);
      ExceptionInfo.m_infoArray[13] = new ExceptionInfo(ExceptionType.InvalidPassword, ExceptionSeverity.Minor, ExceptionCategory.Validation);
    }

    private ExceptionInfo(ExceptionType eType, ExceptionSeverity eSeverity, ExceptionCategory eCategory)
    {
      this.m_type = eType;
      this.m_severity = eSeverity;
      this.m_category = eCategory;
    }

    public ExceptionInfo(ExceptionType eType)
    {
      this.m_type = eType;
      if (ExceptionInfo.m_infoArray[(int) eType] != null)
      {
        this.m_severity = ExceptionInfo.m_infoArray[(int) eType].Severity;
        this.m_category = ExceptionInfo.m_infoArray[(int) eType].Category;
      }
      this.m_strHostName = Dns.GetHostName();
      IPHostEntry hostEntry = Dns.GetHostEntry(this.m_strHostName);
      if (hostEntry.AddressList.Length <= 0)
        return;
      this.m_strHostIP = hostEntry.AddressList[0].ToString();
    }

    protected ExceptionInfo(SerializationInfo info, StreamingContext context)
    {
      this.m_id = (Guid) info.GetValue("ExceptionInfo.m_id", typeof (Guid));
      this.m_dateThrown = (DateTime) info.GetValue("ExceptionInfo.m_dateThrown", typeof (DateTime));
      this.m_type = (ExceptionType) info.GetValue("ExceptionInfo.m_type", typeof (ExceptionType));
      this.m_severity = (ExceptionSeverity) info.GetValue("ExceptionInfo.m_severity", typeof (ExceptionSeverity));
      this.m_category = (ExceptionCategory) info.GetValue("ExceptionInfo.m_category", typeof (ExceptionCategory));
      this.m_strHostName = (string) info.GetValue("ExceptionInfo.m_strHostName", typeof (string));
      this.m_strHostIP = (string) info.GetValue("ExceptionInfo.m_strHostIP", typeof (string));
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("ExceptionInfo.m_id", (object) this.m_id);
      info.AddValue("ExceptionInfo.m_dateThrown", this.m_dateThrown);
      info.AddValue("ExceptionInfo.m_type", (object) this.m_type);
      info.AddValue("ExceptionInfo.m_severity", (object) this.m_severity);
      info.AddValue("ExceptionInfo.m_category", (object) this.m_category);
      info.AddValue("ExceptionInfo.m_strHostName", (object) this.m_strHostName);
      info.AddValue("ExceptionInfo.m_strHostIP", (object) this.m_strHostIP);
    }
  }
}
