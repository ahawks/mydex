// Type: Dexcom.Client.Common.ClientTools
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dexcom.Client.Common
{
  public static class ClientTools
  {
    public static T GetFrameworkElementByName<T>(FrameworkElement referenceElement, string name) where T : FrameworkElement
    {
      FrameworkElement referenceElement1 = (FrameworkElement) null;
      for (int childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount((DependencyObject) referenceElement); ++childIndex)
      {
        referenceElement1 = VisualTreeHelper.GetChild((DependencyObject) referenceElement, childIndex) as FrameworkElement;
        if (referenceElement1 == null || !(referenceElement1.Name == name) || !(referenceElement1 is T))
        {
          if (referenceElement1 != null)
          {
            referenceElement1 = (FrameworkElement) ClientTools.GetFrameworkElementByName<T>(referenceElement1, name);
            if (referenceElement1 != null)
              break;
          }
        }
        else
          break;
      }
      return referenceElement1 as T;
    }

    public static List<T> GetFrameworkElements<T>(FrameworkElement referenceElement, Predicate<T> predicate) where T : FrameworkElement
    {
      List<T> list = new List<T>();
      for (int childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount((DependencyObject) referenceElement); ++childIndex)
      {
        FrameworkElement referenceElement1 = VisualTreeHelper.GetChild((DependencyObject) referenceElement, childIndex) as FrameworkElement;
        if (referenceElement1 != null && referenceElement1 is T && predicate(referenceElement1 as T))
          list.Add(referenceElement1 as T);
        else if (referenceElement1 != null)
        {
          List<T> frameworkElements = ClientTools.GetFrameworkElements<T>(referenceElement1, predicate);
          list.AddRange((IEnumerable<T>) frameworkElements);
        }
      }
      return list;
    }

    public static T FindAncestor<T>(this DependencyObject child) where T : DependencyObject
    {
      for (DependencyObject parentObject = ClientTools.GetParentObject(child); parentObject != null; {
        T obj;
        parentObject = ClientTools.GetParentObject((DependencyObject) obj);
      }
      )
      {
        obj = parentObject as T;
        if ((object) obj != null)
          return obj;
      }
      return default (T);
    }

    public static DependencyObject GetParentObject(this DependencyObject child)
    {
      if (child == null)
        return (DependencyObject) null;
      ContentElement reference = child as ContentElement;
      if (reference != null)
      {
        DependencyObject parent = ContentOperations.GetParent(reference);
        if (parent != null)
          return parent;
        FrameworkContentElement frameworkContentElement = reference as FrameworkContentElement;
        if (frameworkContentElement == null)
          return (DependencyObject) null;
        else
          return frameworkContentElement.Parent;
      }
      else
      {
        FrameworkElement frameworkElement = child as FrameworkElement;
        if (frameworkElement != null)
        {
          DependencyObject parent = frameworkElement.Parent;
          if (parent != null)
            return parent;
        }
        return VisualTreeHelper.GetParent(child);
      }
    }

    public static bool CopyFilesToClipboard(List<string> filePaths, bool isMoveEffect)
    {
      bool flag = false;
      if (filePaths.Count > 0)
      {
        StringCollection fileDropList = new StringCollection();
        foreach (string str in filePaths)
          fileDropList.Add(str);
        byte[] numArray1 = new byte[4];
        numArray1[0] = (byte) 2;
        byte[] buffer1 = numArray1;
        byte[] numArray2 = new byte[4];
        numArray2[0] = (byte) 5;
        byte[] buffer2 = numArray2;
        MemoryStream memoryStream = new MemoryStream();
        if (isMoveEffect)
          memoryStream.Write(buffer1, 0, buffer1.Length);
        else
          memoryStream.Write(buffer2, 0, buffer2.Length);
        DataObject dataObject = new DataObject();
        dataObject.SetFileDropList(fileDropList);
        dataObject.SetData("Preferred DropEffect", (object) memoryStream);
        try
        {
          Clipboard.Clear();
          Clipboard.SetDataObject((object) dataObject, true);
          flag = true;
        }
        catch
        {
          try
          {
            Thread.Sleep(1000);
            Clipboard.Clear();
            Clipboard.SetDataObject((object) dataObject, true);
            flag = true;
          }
          catch
          {
          }
        }
      }
      return flag;
    }

    [DllImport("gdi32.dll")]
    private static bool DeleteObject(IntPtr hObject);

    [SecuritySafeCritical]
    public static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
    {
      IntPtr hbitmap = bitmap.GetHbitmap();
      try
      {
        return Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      }
      finally
      {
        if (hbitmap != IntPtr.Zero)
          ClientTools.DeleteObject(hbitmap);
      }
    }

    public static BitmapImage ConvertDrawingBitmapToMediaBitmapImage(Bitmap bitmap)
    {
      BitmapImage bitmapImage = (BitmapImage) null;
      MemoryStream memoryStream = new MemoryStream();
      try
      {
        bitmap.Save((Stream) memoryStream, ImageFormat.Bmp);
        bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = (Stream) memoryStream;
        bitmapImage.EndInit();
      }
      catch
      {
        memoryStream.Dispose();
      }
      return bitmapImage;
    }

    public static Bitmap ConvertChartToBitmap(Chart chart)
    {
      Bitmap bitmap = new Bitmap(chart.Width, chart.Height);
      try
      {
        bitmap.SetResolution(110f, 110f);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          chart.Printing.PrintPaint(graphics, new Rectangle(0, 0, chart.Width, chart.Height));
      }
      catch
      {
        if (bitmap != null)
          bitmap.Dispose();
        throw;
      }
      return bitmap;
    }

    public static BitmapSource ConvertChartToBitmapSource(Chart chart)
    {
      using (Bitmap bitmap = ClientTools.ConvertChartToBitmap(chart))
        return ClientTools.ConvertBitmapToBitmapSource(bitmap);
    }

    public static BitmapImage ConvertChartToBitmapImage(Chart chart)
    {
      using (Bitmap bitmap = ClientTools.ConvertChartToBitmap(chart))
        return ClientTools.ConvertDrawingBitmapToMediaBitmapImage(bitmap);
    }
  }
}
