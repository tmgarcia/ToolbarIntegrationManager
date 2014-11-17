using System;
using System.Windows.Interop;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace TestProject.Models
{
    class CustomCursors
    {
        class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DestroyIcon(
                [In] IntPtr hIcon);

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
                return DestroyIcon(this.handle);
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
        private struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);


        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp,
            int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;

            IntPtr ptr = CreateIconIndirect(ref tmp);
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
