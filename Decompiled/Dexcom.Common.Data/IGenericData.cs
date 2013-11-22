// Type: Dexcom.Common.Data.IGenericData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using System;

namespace Dexcom.Common.Data
{
  public interface IGenericData : IComparable
  {
    DateTime InternalTime { get; set; }

    TimeSpan DisplayOffset { get; set; }
  }
}
