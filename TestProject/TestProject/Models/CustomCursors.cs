using System;
using System.Windows.Interop;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using TestProject.Win32API;

namespace TestProject.Models
{
    class CustomCursors
    {
        class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeIconHandle()
                : base(true)
            {
            }

            public SafeIconHandle(IntPtr hIcon)
                : base(true)
            {
                this.SetHandle(hIcon);
            }

            protected override bool ReleaseHandle()
            {
                return Win32Tools.DestroyIcon(this.handle);
            }
        }

        public static Cursor Cursor_EmptyCircle(int diameter)
        {
            System.Drawing.Size s = new System.Drawing.Size(diameter, diameter);
            System.Drawing.Image prebmp = new System.Drawing.Bitmap(Constants.SolutionRoot + "Images/Cursors/CursorCircle.png");
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(prebmp, s);
            Cursor cur = InternalCreateCursor(bmp, diameter/2, diameter/2);
            bmp.Dispose();
            prebmp.Dispose();
            return cur;
        }
        public static Cursor Cursor_EmptySquare(int size)
        {
            System.Drawing.Size s = new System.Drawing.Size(size, size);
            System.Drawing.Image prebmp = new System.Drawing.Bitmap(Constants.SolutionRoot + "Images/Cursors/CursorSquare.png");
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(prebmp, s);
            Cursor cur = InternalCreateCursor(bmp, size / 2, size / 2);
            bmp.Dispose();
            prebmp.Dispose();
            return cur;
        }
        public static Cursor Cursor_Eraser()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Constants.SolutionRoot + "Images/Cursors/CursorEraser.png");
            Cursor cur = InternalCreateCursor(bmp, 7, 7);
            bmp.Dispose();
            return cur;
        }

        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp,
            int xHotSpot, int yHotSpot)
        {
            Win32Tools.ICONINFO tmp = new Win32Tools.ICONINFO();
            Win32Tools.GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;

            IntPtr ptr = Win32Tools.CreateIconIndirect(ref tmp);
            SafeIconHandle handle = new SafeIconHandle(ptr);
            return CursorInteropHelper.Create(handle);
        }
        public static Cursor CreateCursor(System.Drawing.Bitmap bmp, int xHotSpot, int yHotSopt)
        {
            Cursor cur = InternalCreateCursor(bmp, xHotSpot, yHotSopt);
            return cur;
        }
        public static Cursor CreateCursor(UIElement element, int xHotSpot, int yHotSpot)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(0, 0, element.DesiredSize.Width,
                element.DesiredSize.Height));

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)element.DesiredSize.Width,
                (int)element.DesiredSize.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(element);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);

            ms.Close();
            ms.Dispose();

            Cursor cur = InternalCreateCursor(bmp, xHotSpot, yHotSpot);

            bmp.Dispose();

            return cur;
        }
    }
}
