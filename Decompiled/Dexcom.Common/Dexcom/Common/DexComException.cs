// Type: Dexcom.Common.DexComException
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Runtime.Serialization;
using System.Security;

namespace Dexcom.Common
{
  [Serializable]
  public class DexComException : ApplicationException, ISerializable
  {
    public DexComException()
    {
    }

    public DexComException(string message)
      : base(message)
    {
    }

    public DexComException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected DexComException(SerializationInfo info, StreamingContext context)
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
