using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Media;
using System.Runtime.InteropServices;
using TIM.Win32API;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace TIM
{
    class ScreenCapturer
    {
        public struct SIZE
        { 
            public int cx;
            public int cy;
        }
        public static BitmapSource CaptureFullScreen()
        {
            return ToBitmapSource(CaptureDesktop());
        }
        public static Bitmap CaptureDesktop()
        {
            SIZE size;
            IntPtr hBitmap;
            IntPtr hDC = Win32Tools.GetDC(Win32Tools.GetDesktopWindow());
            IntPtr hMemDC = GDITools.CreateCompatibleDC(hDC);

            size.cx = Win32Tools.GetSystemMetrics
                      (Win32Tools.SM_CXSCREEN);

            size.cy = Win32Tools.GetSystemMetrics
                      (Win32Tools.SM_CYSCREEN);

            hBitmap = GDITools.CreateCompatibleBitmap(hDC, size.cx, size.cy);

            if (hBitmap != IntPtr.Zero)
            {
                IntPtr hOld = (IntPtr)GDITools.SelectObject
                                       (hMemDC, hBitmap);

                GDITools.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC,
                                               0, 0, GDITools.SRCCOPY);

                GDITools.SelectObject(hMemDC, hOld);
                GDITools.DeleteDC(hMemDC);
                Win32Tools.ReleaseDC(Win32Tools.GetDesktopWindow(), hDC);
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                GDITools.DeleteObject(hBitmap);
                GC.Collect();
                return bmp;
            }
            return null;
        }
        private static BitmapSource ToBitmapSource(Bitmap source)
        {
            var hBitmap = source.GetHbitmap();

            try
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                return null;
            }
            finally
            {
                GDITools.DeleteObject(hBitmap);
            }
        }
    }
}
