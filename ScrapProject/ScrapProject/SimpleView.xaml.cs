using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScrapProject
{
    /// <summary>
    /// Interaction logic for SimpleView.xaml
    /// </summary>
    public partial class SimpleView : Window
    {
        private enum ClipboardMessages
        {
            WM_CLIPBOARDUPDATE = 0x031D,
            WM_DRAWCLIPBOARD = 0x0308
        }

        public SimpleView()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            //Console.WriteLine("ONSOURCEINITIALIZED");
            base.OnSourceInitialized(e);
            IntPtr hwnd = new WindowInteropHelper(this).Handle;   
            HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
            AddClipboardFormatListener(hwnd);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == (int)ClipboardMessages.WM_CLIPBOARDUPDATE)
            {
                //Console.WriteLine("WM_CLIPBOARDUPDATE");
                UpdateDisplayClipboard();
            }
            return IntPtr.Zero;
        }
        private void UpdateDisplayClipboard()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null)
            {
                try
                {
                    string[] formats = data.GetFormats();
                    string sourceString = "";
                    if (formats.Contains("HTML Format"))
                    {
                        string html = data.GetData("HTML Format") as string;
                        string metaSourceString = "<meta name=generator content=";
                        string imageSourceString = "src=";
                        string siteSourceString = "sourceurl:";
                        if (html.ToLower().Contains(metaSourceString))
                        {
                            int start = html.ToLower().IndexOf(metaSourceString) + metaSourceString.Length + 1;//1 for the frist "
                            int end = html.ToLower().IndexOf("\"", start);
                            int length = end - start;
                            sourceString = html.Substring(start, length);
                        }
                        else if (html.ToLower().Contains(imageSourceString))
                        {
                            int start = html.ToLower().IndexOf(imageSourceString) + imageSourceString.Length + 1;
                            int end = html.ToLower().IndexOf("\"", start);
                            int length = end - start;
                            sourceString = html.Substring(start, length);
                        }
                        else if (html.ToLower().Contains(siteSourceString))
                        {
                            int start = html.ToLower().IndexOf(siteSourceString) + siteSourceString.Length;
                            int end = html.ToLower().IndexOf('<', start) - start;
                            sourceString = html.Substring(start, end);
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(sourceString))
                    {
                        itemStackPanel.Children.Add(new Label() { Content = sourceString });
                    }
                    int snipLength = 25;
                    if (formats.Contains("DeviceIndependentBitmap"))
                    {
                        ImageSource i = DBIConverter.ImageFromDBIMemStream(data.GetData("DeviceIndependentBitmap") as MemoryStream);
                        if (i != null)
                        {
                            Image dbi = new Image();
                            dbi.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            dbi.Source = i;
                            itemStackPanel.Children.Add(dbi);
                        }
                    }
                    else if (formats.Contains("UnicodeText"))
                    {
                        string uni = data.GetData("UnicodeText") as string;
                        //if (uni.Length > snipLength)
                        //{
                        //    uni = uni.Substring(0, snipLength);
                        //}
                        itemStackPanel.Children.Add(new Label() { Content = uni });
                    }
                    else if (formats.Contains("Text"))
                    {
                        string txt = data.GetData("Text") as string;
                        //if (txt.Length > snipLength)
                        //{
                        //    txt = txt.Substring(0, snipLength);
                        //}
                        itemStackPanel.Children.Add(new Label() { Content = txt });
                    }
                    //itemStackPanel.Children.Add();
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    System.Threading.Thread.Sleep(0);
                    UpdateDisplayClipboard();
                }
            }
        }
        //private void UpdateDisplayClipboard()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    try
        //    {
        //        if (data != null)
        //        {
        //            string[] formats = data.GetFormats();

        //            formatView.Text = "";
        //            textView.Text = "";
        //            foreach (string format in formats)
        //            {
        //                Console.WriteLine(format);
        //                formatView.Text += format + ":\n";
        //                if (format == "DeviceIndependentBitmap")
        //                {
        //                    ImageSource i = DBIConverter.ImageFromDBIMemStream(data.GetData("DeviceIndependentBitmap") as MemoryStream);
        //                    if (i != null)
        //                    {
        //                        imageView.Source = i;
        //                    }
        //                }
        //                //imageView
        //                if (!format.Contains("moz"))
        //                {
        //                    string s = data.GetData(format) as string;
        //                    if (!String.IsNullOrWhiteSpace(s))
        //                    {
        //                        if (format.ToLower().Contains("html"))
        //                        {
        //                            string sFixed = FixHtml(s);
        //                            htmlView.NavigateToString(sFixed);
        //                            //textView.Text += "HTML FIXED" + ": \n";
        //                            //textView.Text += "\t" + sFixed + "\n";
        //                        }
        //                        textView.Text += format + ": \n";
        //                        textView.Text += "\t" + s + "\n";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch(System.OutOfMemoryException)
        //    {
        //        data = null;
        //    }
        //    catch (System.Runtime.InteropServices.COMException)
        //    {
        //        System.Threading.Thread.Sleep(0);
        //        UpdateDisplayClipboard();
        //    }
        //}

        public string FixHtml(string HTML)
        {
            string ret = "";
            StringBuilder sb = new StringBuilder();
            char[] s = HTML.ToCharArray();
            foreach (char c in s)
            {
                if (Convert.ToInt32(c) > 127)
                    sb.Append("&#" + Convert.ToInt32(c) + ";");
                else
                    sb.Append(c);
            }
            ret =  sb.ToString();
            if(ret.Contains("<META content=\"IE"))
            {
                ret = ret.Remove(ret.IndexOf("<!DOCTYPE"));
            }
            return ret;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardViewer(IntPtr hwnd);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
    }
}
