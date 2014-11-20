using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.UserControls;
using TestProject.Win32API;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Measure_ClipboardInner.xaml
    /// </summary>
    public partial class Notes_ClipboardInner : UserControl
    {
        private int WM_CLIPBOARDUPDATE = 0x031D;

        private ObservableCollection<IDataObject> dataObjects;
        public Notes_ClipboardInner()
        {
            dataObjects = new ObservableCollection<IDataObject>();
            InitializeComponent();
            clipboardListBox.ItemsSource = dataObjects;
            //clipboardListBox
        }

        void Notes_ClipboardInner_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("LOADED");
            Window w = Window.GetWindow(this);
            w.SourceInitialized += w_SourceInitialized;
            this.Loaded -= Notes_ClipboardInner_Loaded;
            
        }

        void w_SourceInitialized(object sender, EventArgs e)
        {
            Console.WriteLine("SOURCE INITIALIZED");
            Window w = Window.GetWindow(this);
            w.SourceInitialized -= w_SourceInitialized;
            IntPtr hwnd = new WindowInteropHelper(w).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
            Win32Tools.AddClipboardFormatListener(hwnd);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                Console.WriteLine("RECIEVED CLIPBOARD UPDATE");
                UpdateClipboardDisplay();
            }
            return IntPtr.Zero;
        }

        private void UpdateClipboardDisplay()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null)
            {
                dataObjects.Add(data);
                ListBoxItem newListBoxItem = (ListBoxItem)(clipboardListBox.ItemContainerGenerator.ContainerFromItem(clipboardListBox.Items[clipboardListBox.Items.Count-1]));
                newListBoxItem.Loaded += ListBoxItem_Loaded;
            }
        }

        void ListBoxItem_Loaded(object sender, RoutedEventArgs e)
        {
            ListBoxItem newListBoxItem = sender as ListBoxItem;
            newListBoxItem.Loaded -= ListBoxItem_Loaded;
            ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(newListBoxItem);
            DataTemplate dataTemplate = contentPresenter.ContentTemplate;
            ClipboardDataObjectDisplay display = (ClipboardDataObjectDisplay)dataTemplate.FindName("DataObjectDisplay", contentPresenter);
            display.SetupDisplay();
        }

        public void Activate()
        {
            this.Loaded += Notes_ClipboardInner_Loaded;
        }
        public void Deactivate()
        {
            Window w = Window.GetWindow(this);
            IntPtr hwnd = new WindowInteropHelper(w).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
            Win32Tools.RemoveClipboardFormatListener(hwnd);
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)

        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
