// Type: Dexcom.Common.EventUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Dexcom.Common
{
  public class EventUtils
  {
    public static void FireEvent(Delegate del, params object[] args)
    {
      Delegate @delegate = del;
      if (@delegate == null)
        return;
      foreach (Delegate del1 in @delegate.GetInvocationList())
        EventUtils.InvokeDelegate(del1, false, (StackTrace) null, args);
    }

    public static void FireEventWithExceptions(Delegate del, params object[] args)
    {
      Delegate @delegate = del;
      if (@delegate == null)
        return;
      foreach (Delegate del1 in @delegate.GetInvocationList())
        EventUtils.InvokeDelegate(del1, true, (StackTrace) null, args);
    }

    public static void FireEventAsync(Delegate del, params object[] args)
    {
      Delegate @delegate = del;
      if (@delegate == null)
        return;
      foreach (Delegate del1 in @delegate.GetInvocationList())
      {
        EventUtils.AsyncInvokeDelegate asyncInvokeDelegate = new EventUtils.AsyncInvokeDelegate(EventUtils.InvokeDelegate);
        StackTrace stack = (StackTrace) null;
        asyncInvokeDelegate.BeginInvoke(del1, false, stack, args, (AsyncCallback) null, (object) null);
      }
    }

    [Conditional("DEBUG")]
    private static void DoFetchStackTrace(ref StackTrace stack)
    {
      stack = new StackTrace(1, true);
    }

    [Conditional("DEBUG")]
    private static void DoAssert(Exception exception, StackTrace stack)
    {
      string message = ((object) exception).ToString();
      if (stack != null)
        message = message + " ORIGINAL CALLER STACK: " + ((object) stack).ToString();
      if (ProgramContext.Current == null)
        return;
      ProgramContext.Current.AddSessionLogEntrySync(message);
    }

    private static void InvokeDelegate(Delegate del, bool allowExceptions, StackTrace stack, object[] args)
    {
      ISynchronizeInvoke synchronizeInvoke = del.Target as ISynchronizeInvoke;
      if (synchronizeInvoke != null)
      {
        if (synchronizeInvoke.InvokeRequired)
        {
          try
          {
            synchronizeInvoke.Invoke(del, args);
            return;
          }
          catch (Exception ex)
          {
            if (!allowExceptions)
              return;
            throw;
          }
        }
      }
      try
      {
        del.DynamicInvoke(args);
      }
      catch (Exception ex)
      {
        if (!allowExceptions)
          return;
        throw;
      }
    }

    private delegate void AsyncInvokeDelegate(Delegate del, bool allowExceptions, StackTrace stack, object[] args);
  }
}
