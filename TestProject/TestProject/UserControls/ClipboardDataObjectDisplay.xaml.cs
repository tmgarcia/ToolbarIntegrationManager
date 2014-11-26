using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.Win32API;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for ClipboardDataObjectDisplay.xaml
    /// </summary>
    public partial class ClipboardDataObjectDisplay : UserControl
    {
        public class FailedObjectEventArgs : RoutedEventArgs
        {
            //RoutedEvent routedEvent, object source
            public FailedObjectEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
            public IDataObject FailedObject { get; set; }
            public string FailMessage { get; set; }
        }
        public static readonly RoutedEvent FailedDataObjectDisplayEvent = EventManager.RegisterRoutedEvent("FailedDataObjectDisplay", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClipboardDataObjectDisplay));
        public event RoutedEventHandler FailedDataObjectDisplay
        {
            add { AddHandler(FailedDataObjectDisplayEvent, value); }
            remove { RemoveHandler(FailedDataObjectDisplayEvent, value); }
        }
        public static readonly RoutedEvent SuccessfulDataObjectDisplayEvent = EventManager.RegisterRoutedEvent("SuccessfulDataObjectDisplay", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClipboardDataObjectDisplay));
        public event RoutedEventHandler SuccessfulDataObjectDisplay
        {
            add { AddHandler(SuccessfulDataObjectDisplayEvent, value); }
            remove { RemoveHandler(SuccessfulDataObjectDisplayEvent, value); }
        }
        public IDataObject dataObject;
        int previewTextLength = 30;
        int previewSourceLength = 25;
        public bool selectable = true;
        
        public ClipboardDataObjectDisplay()
        {
            InitializeComponent();
            //SetupDisplay();
        }

        //parent check if null before put in list
        public void SetupDisplay()
        {
            dataObject = this.DataContext as IDataObject;
            try
            {
                string[] formats = dataObject.GetFormats();
                if (formats.Contains("FileDrop"))
                {
                    SendFailedDataObjectMessage("File data objects not currently supported.");
                }
                else
                {
                    string sourceString = FindSourceString(formats);
                    if (!String.IsNullOrWhiteSpace(sourceString))
                    {
                        if (sourceString.Length > previewSourceLength)
                        {
                            sourceString = sourceString.Substring(0, previewSourceLength) + " ...";
                        }
                        sourceString = "Source: " + sourceString;
                        TextBlock src = new TextBlock() { 
                            Text = sourceString, 
                            Foreground = new SolidColorBrush(Colors.DarkBlue), 
                            TextWrapping=System.Windows.TextWrapping.Wrap
                        };
                        displayStackPanel.Children.Add(src);
                    }
                    if (formats.Contains("DeviceIndependentBitmap"))
                    {
                        DisplayPreviewImage();
                    }
                    else if (formats.Contains("UnicodeText"))
                    {
                        string uni = dataObject.GetData("UnicodeText") as string;
                        if (uni.Length > previewTextLength)
                        {
                            uni = uni.Substring(0, previewTextLength) + " ...";
                        }
                        TextBlock uniText = new TextBlock()
                        {
                            Text = uni,
                            TextWrapping = System.Windows.TextWrapping.Wrap,
                            FontStyle = System.Windows.FontStyles.Italic
                        };
                        displayStackPanel.Children.Add(uniText);
                    }
                    else if (formats.Contains("Text"))
                    {
                        string txt = dataObject.GetData("Text") as string;
                        if (txt.Length > previewTextLength)
                        {
                            txt = txt.Substring(0, previewTextLength) + " ...";
                        }
                        TextBlock text = new TextBlock()
                        {
                            Text = txt,
                            TextWrapping = System.Windows.TextWrapping.Wrap,
                            FontStyle = System.Windows.FontStyles.Italic
                        };
                        displayStackPanel.Children.Add(text);
                    }
                    RaiseEvent(new RoutedEventArgs(SuccessfulDataObjectDisplayEvent, this));
                }
            }
            catch (System.OutOfMemoryException)
            {
                SendFailedDataObjectMessage("Data Object too large.");
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                SendFailedDataObjectMessage("Clipboard temporarily inaccesable.");
            }
            //catch (System.Exception)
            //{
            //    RaiseEvent(new RoutedEventArgs(FailedDataObjectDisplayEvent, dataObject));
            //}
        }

        private void SendFailedDataObjectMessage(string message)
        {
            RaiseEvent(new FailedObjectEventArgs(FailedDataObjectDisplayEvent, this) { FailedObject = dataObject, FailMessage = message });
        }

        private void DisplayPreviewImage()
        {
            ImageSource i = DBIConverter.ImageFromDBIMemStream(dataObject.GetData("DeviceIndependentBitmap") as MemoryStream);
            if (i != null)
            {
                Image dbi = new Image();
                dbi.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                dbi.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                dbi.Source = i;
                dbi.Stretch = Stretch.None;

                double gridHeight = this.ActualHeight - 20.0;
                double gridWidth = this.ActualWidth - 5.0;
                LinearGradientBrush mask = new LinearGradientBrush() { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };
                mask.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
                mask.GradientStops.Add(new GradientStop(Colors.Black, 0.5));
                mask.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

                Grid g = new Grid() { Width = gridWidth, Height = gridHeight };
                g.Clip = new RectangleGeometry(new Rect(0, 0, gridWidth, gridHeight));
                g.OpacityMask = mask;
                g.Children.Add(dbi);
                displayStackPanel.Children.Add(g);
            }
        }
        private string FindSourceString(string[] formats)
        {
            string sourceString = "";
            if (formats.Contains("HTML Format"))
            {
                string html = dataObject.GetData("HTML Format") as string;
                string metaSourceString = "<meta name=generator content=";//Was from a program that included its name
                string imageSourceString = "src=";//Image from the web
                string siteSourceString = "sourceurl:";//Text from the web
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
            return sourceString;
        }
    }
}
