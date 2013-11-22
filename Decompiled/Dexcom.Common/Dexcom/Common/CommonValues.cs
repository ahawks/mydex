// Type: Dexcom.Common.CommonValues
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;

namespace Dexcom.Common
{
  public abstract class CommonValues
  {
    public static readonly Guid NoneId = new Guid("{00000000-0000-0000-0000-000000000000}");
    public static readonly Guid AllId = new Guid("{11111111-1111-1111-1111-111111111111}");
    public static readonly Guid AnyId = new Guid("{10101010-1010-1010-1010-101010101010}");
    public static readonly Guid AdministratorsId = new Guid("{00000001-0001-0001-0001-000000000001}");
    public static readonly Guid R2EmptyId = new Guid("{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}");
    public static DateTime EmptyDateTime = DateTime.FromOADate(0.0);
    public static DateTimeOffset EmptyDateTimeOffset = new DateTimeOffset(DateTime.FromOADate(0.0), TimeSpan.Zero);
    public static TimeSpan EmptyTimeSpan = new TimeSpan(0, 0, 0);
    public static readonly Guid SystemIdForMyDexcom = new Guid("{D3A5117D-A53D-4CB1-8E8A-64B3B6CBCC04}");
    public static readonly Guid SystemIdForGlobal = new Guid("{19E0DDB4-5FA5-41EE-B624-AEA762865A6C}");
    public static readonly Guid ProductIdForDexNETAdminTool = new Guid("{0AF39A3E-6549-485B-872F-B73413203998}");
    public static readonly Guid ProductIdForGlobalTransferAgent = new Guid("{0528ACB8-C0E9-40F9-BE7C-5F4E989C7C6E}");
    public static readonly Guid ProductIdForMyDexcomWebSite = new Guid("{76BB16D3-CA19-4A0B-84D4-970CA9DA0EA5}");
    public static readonly Guid ProductIdForGlobalDexComStudio = new Guid("{68265A98-18F9-42A3-8447-818F096CD30E}");
    public static readonly Guid ProductIdForGlobalSupportRepository = new Guid("{0C7EFC5D-7CAC-4AEA-9518-98BA891CA78D}");
    public static readonly Guid ProductIdForFirmwareUpdateTool = new Guid("{0395EAED-3999-4779-B96C-667730EED7ED}");
    public const double MmolConversionFactor = 18.02;

    static CommonValues()
    {
    }
  }
}
