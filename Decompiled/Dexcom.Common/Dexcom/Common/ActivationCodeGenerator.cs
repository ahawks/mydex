// Type: Dexcom.Common.ActivationCodeGenerator
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Dexcom.Common
{
  public class ActivationCodeGenerator : IActivationServiceProvider
  {
    public string GetRequestKey()
    {
      List<string> requestKeys = this.GetRequestKeys();
      string str = string.Empty;
      if (requestKeys.Count > 0)
        str = requestKeys[0];
      if (str == string.Empty)
        throw new DexComException("Unable to determine the unique identification for this computer");
      else
        return str;
    }

    public List<string> GetRequestKeys()
    {
      NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
      List<string> list = new List<string>();
      foreach (NetworkInterface networkInterface in networkInterfaces)
      {
        if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        {
          string str = BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes()).Replace("-", "").Substring(6, 5);
          list.Add(str.ToUpper());
        }
      }
      foreach (NetworkInterface networkInterface in networkInterfaces)
      {
        if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
        {
          string str = BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes()).Replace("-", "").Substring(6, 5);
          list.Add(str.ToUpper());
        }
      }
      return list;
    }

    public string GetActivationCode(string RequestKey)
    {
      if (string.IsNullOrEmpty(RequestKey))
        throw new DexComException(ProgramContext.TryResourceLookup("Exception_ActivationCodeGenerator_InValidRequestId", "Invalid Request Key", new object[0]));
      string str = Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(RequestKey)));
      char[] chArray = new char[5];
      int index = 0;
      foreach (char c in str.ToCharArray())
      {
        if (char.IsLetterOrDigit(c))
        {
          chArray[index] = c;
          ++index;
          if (index == 5)
            break;
        }
      }
      for (; index != 5; ++index)
        chArray[index] = 'A';
      return new string(chArray).ToUpper();
    }

    public bool IsValidActivationCode(string activationCode)
    {
      bool flag = false;
      foreach (string RequestKey in this.GetRequestKeys())
      {
        string activationCode1 = this.GetActivationCode(RequestKey);
        if (activationCode == activationCode1)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
