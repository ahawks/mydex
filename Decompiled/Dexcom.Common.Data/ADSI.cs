// Type: Dexcom.Common.Data.ADSI
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;

namespace Dexcom.Common.Data
{
  public static class ADSI
  {
    private static string DoGetGroupDetails(ADSI.ADGroup groupName)
    {
      string str = string.Empty;
      switch (groupName)
      {
        case ADSI.ADGroup.Engineering:
          return "Engineering,OU=Engineering";
        case ADSI.ADGroup.RandD:
          return "R&D,OU=Research and Development";
        case ADSI.ADGroup.RandD_Global:
          return "RandD_Global,OU=Engineering";
        case ADSI.ADGroup.RandDAnalysts:
          return "R&DAnalysts,OU=Research and Development";
        case ADSI.ADGroup.Engineering_Global:
          return "Engineering_Global,OU=Engineering";
        default:
          throw new OnlineException(ExceptionType.InvalidArgument, "Incorrect Active Directory Group Name");
      }
    }

    private static XUser DoGetUser(SearchResult searchResult)
    {
      return new XUser()
      {
        Name = ADSI.DoGetProperty(searchResult, "samaccountname"),
        DisplayName = ADSI.DoGetProperty(searchResult, "cn"),
        FirstName = ADSI.DoGetProperty(searchResult, "givenName"),
        LastName = ADSI.DoGetProperty(searchResult, "sn"),
        EMail = ADSI.DoGetProperty(searchResult, "mail"),
        IsDomainAccount = true
      };
    }

    private static string DoGetProperty(SearchResult searchResult, string propertyName)
    {
      if (searchResult.Properties.Contains(propertyName))
        return searchResult.Properties[propertyName][0].ToString();
      else
        return string.Empty;
    }

    private static string DoGetFilterString(string objectCategory, string filterKey, string filterValue, bool is_FQDN)
    {
      string str = string.Empty;
      return "(|" + (is_FQDN ? string.Format("(&(objectCategory={0})({1}={2},DC=DexCom,DC=dexcominc,DC=com))", (object) objectCategory, (object) filterKey, (object) filterValue) : string.Format("(&(objectCategory={0})({1}={2}))", (object) objectCategory, (object) filterKey, (object) filterValue)) + ")";
    }

    public static XUser GetDomainUserInfo(string loginName)
    {
      XUser xuser = (XUser) null;
      using (DirectorySearcher directorySearcher = new DirectorySearcher())
      {
        using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://DEXDC1.DEXCOM.DEXCOMINC.COM"))
        {
          directorySearcher.SearchRoot = directoryEntry;
          directorySearcher.Filter = ADSI.DoGetFilterString("user", "samaccountname", loginName, false);
          directorySearcher.ReferralChasing = ReferralChasingOption.None;
          IEnumerator enumerator = directorySearcher.FindAll().GetEnumerator();
          try
          {
            if (enumerator.MoveNext())
              xuser = ADSI.DoGetUser((SearchResult) enumerator.Current);
          }
          finally
          {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
        }
      }
      return xuser;
    }

    public static bool IsActiveDomainUser(string loginName, out string failureReason)
    {
      bool flag = false;
      failureReason = string.Empty;
      using (DirectorySearcher directorySearcher = new DirectorySearcher())
      {
        using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://DEXDC1.DEXCOM.DEXCOMINC.COM"))
        {
          directorySearcher.SearchRoot = directoryEntry;
          directorySearcher.Filter = ADSI.DoGetFilterString("user", "samaccountname", loginName, false);
          directorySearcher.ReferralChasing = ReferralChasingOption.None;
          SearchResultCollection all = directorySearcher.FindAll();
          if (all.Count == 1)
          {
            string property1 = ADSI.DoGetProperty(all[0], "lockouttime");
            string property2 = ADSI.DoGetProperty(all[0], "useraccountcontrol");
            if (string.IsNullOrEmpty(property1) || property1 == "0")
            {
              if (!string.IsNullOrEmpty(property2))
              {
                uint num = Convert.ToUInt32(property2);
                if (((int) num & 2) == 0 && ((int) num & 16) == 0)
                {
                  string property3 = ADSI.DoGetProperty(all[0], "accountexpires");
                  if (!string.IsNullOrEmpty(property3) && property3 != "0")
                  {
                    TimeSpan timeSpan = new TimeSpan(Convert.ToInt64(property3));
                    if (timeSpan.Ticks != long.MaxValue)
                    {
                      try
                      {
                        if (new DateTime(1601, 1, 1) + timeSpan > DateTime.Now)
                        {
                          flag = true;
                        }
                        else
                        {
                          failureReason = "Account Expired";
                          flag = false;
                        }
                      }
                      catch (ArgumentOutOfRangeException ex)
                      {
                        flag = true;
                      }
                    }
                    else
                      flag = true;
                  }
                  else
                    flag = true;
                }
                else
                {
                  failureReason = "Account Disabled";
                  flag = false;
                }
              }
              else
              {
                failureReason = "Account Flags Missing";
                flag = false;
              }
            }
            else
            {
              failureReason = "Account Locked Out";
              flag = false;
              TimeSpan timeSpan = new TimeSpan(Convert.ToInt64(property1));
              try
              {
                DateTime dateTime = new DateTime(1601, 1, 1) + timeSpan;
                if (dateTime > DateTime.Now)
                {
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  string& local = @failureReason;
                  // ISSUE: explicit reference operation
                  string str = ^local + ": until " + dateTime.ToString();
                  // ISSUE: explicit reference operation
                  ^local = str;
                }
                else
                  failureReason = "Account Previously Locked Out: and not reset yet.";
              }
              catch
              {
              }
            }
          }
          else
          {
            failureReason = "Account Not Found";
            flag = false;
          }
        }
      }
      return flag;
    }

    public static List<XUser> GetAllDomainUsers()
    {
      List<XUser> list = new List<XUser>();
      using (DirectorySearcher directorySearcher = new DirectorySearcher())
      {
        using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://DEXDC1.DEXCOM.DEXCOMINC.COM"))
        {
          directorySearcher.SearchRoot = directoryEntry;
          directorySearcher.Filter = ADSI.DoGetFilterString("user", "name", "*", false);
          directorySearcher.ReferralChasing = ReferralChasingOption.None;
          foreach (SearchResult searchResult in directorySearcher.FindAll())
          {
            XUser user = ADSI.DoGetUser(searchResult);
            list.Add(user);
          }
        }
      }
      return list;
    }

    public static List<XUser> GetAllDomainUsers(ADSI.ADGroup groupName)
    {
      List<XUser> list = new List<XUser>();
      using (DirectorySearcher directorySearcher = new DirectorySearcher())
      {
        using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://DEXDC1.DEXCOM.DEXCOMINC.COM"))
        {
          string groupDetails = ADSI.DoGetGroupDetails(groupName);
          directorySearcher.SearchRoot = directoryEntry;
          directorySearcher.Filter = ADSI.DoGetFilterString("user", "memberof=CN", groupDetails, true);
          directorySearcher.ReferralChasing = ReferralChasingOption.None;
          foreach (SearchResult searchResult in directorySearcher.FindAll())
          {
            XUser user = ADSI.DoGetUser(searchResult);
            list.Add(user);
          }
        }
      }
      return list;
    }

    public enum ADGroup
    {
      Engineering,
      RandD,
      RandD_Global,
      RandDAnalysts,
      Engineering_Global,
    }
  }
}
