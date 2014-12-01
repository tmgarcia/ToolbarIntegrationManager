using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TIM.UserControls;
using TIM.Win32API;

namespace TIM.Tools
{
    /// <summary>
    /// Interaction logic for Measure_ClipboardInner.xaml
    /// </summary>
    public partial class Notes_ClipboardInner : UserControl
    {
        private int WM_CLIPBOARDUPDATE = 0x031D;
        private int maxDataObjects = 10;
        private Storyboard warningFadeOut;

        private ObservableCollection<IDataObject> dataObjects;
        public Notes_ClipboardInner()
        {
            UIPermission clipBoard = new UIPermission(PermissionState.None);
            clipBoard.Clipboard = UIPermissionClipboard.AllClipboard;
            dataObjects = new ObservableCollection<IDataObject>();
            InitializeComponent();
            maxEntriesLabel.Content = "Max Entires: " + maxDataObjects;
            clipboardListBox.ItemsSource = dataObjects;
            SetupAnimation();
            Application.Current.DispatcherUnhandledException += Application_DispatcherUnhandledException;
        }
        void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var comException = e.Exception as System.Runtime.InteropServices.COMException;
            if (comException != null && (comException.ErrorCode == -2147221040 || comException.ErrorCode == -2147467262))
            {
                e.Handled = true;
            }
        }
        private void SetupAnimation()
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            fadeOutAnimation.From = 1.0;
            fadeOutAnimation.To = 0.0;
            fadeOutAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
            fadeOutAnimation.AutoReverse = false;
            warningFadeOut = new Storyboard();
            warningFadeOut.Children.Add(fadeOutAnimation);
            Storyboard.SetTargetName(fadeOutAnimation, warningLabel.Name);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(Label.OpacityProperty));
            warningFadeOut.Completed += warningFadeOut_Completed;
        }
        void Notes_ClipboardInner_Loaded(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            w.SourceInitialized += w_SourceInitialized;
            this.Loaded -= Notes_ClipboardInner_Loaded;
        }
        void w_SourceInitialized(object sender, EventArgs e)
        {
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
                UpdateClipboardDisplay();
            }
            return IntPtr.Zero;
        }
        private void UpdateClipboardDisplay()
        {
            //System.Windows.Forms.Clipboard
            IDataObject data = Clipboard.GetDataObject();
            //IDataObject data = Clipboard.GetDataObject();
            if (data != null)
            {
                TestText.Text = "";
                string[] formats = data.GetFormats();
                foreach (string f in formats)
                {
                    TestText.Text += f + "\n";
                }
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
            display.FailedDataObjectDisplay += display_FailedDataObjectDisplay;
            display.SuccessfulDataObjectDisplay += display_SuccessfulDataObjectDisplay;
            display.SetupDisplay();
        }

        void display_SuccessfulDataObjectDisplay(object sender, RoutedEventArgs e)
        {
            ((ClipboardDataObjectDisplay)sender).FailedDataObjectDisplay -= display_FailedDataObjectDisplay;
            ((ClipboardDataObjectDisplay)sender).SuccessfulDataObjectDisplay -= display_SuccessfulDataObjectDisplay;
            if (dataObjects.Count > maxDataObjects)
            {
                dataObjects.RemoveAt(0);
            }
        }
        void display_FailedDataObjectDisplay(object sender, RoutedEventArgs e)
        {
            ClipboardDataObjectDisplay.FailedObjectEventArgs ef = e as ClipboardDataObjectDisplay.FailedObjectEventArgs;
            ((ClipboardDataObjectDisplay)sender).FailedDataObjectDisplay -= display_FailedDataObjectDisplay;
            ((ClipboardDataObjectDisplay)sender).SuccessfulDataObjectDisplay -= display_SuccessfulDataObjectDisplay;
            dataObjects.Remove(ef.FailedObject);
            warningLabel.Content = ef.FailMessage;
            warningLabel.Visibility = System.Windows.Visibility.Visible;
            warningFadeOut.Begin(this);
        }
        void warningFadeOut_Completed(object sender, EventArgs e)
        {
            warningLabel.Visibility = System.Windows.Visibility.Collapsed;
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
        private childItem FindVisualChild<childItem>(DependencyObject obj)where childItem : DependencyObject
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

        private void DataObjectDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IDataObject selectedObject = ((ClipboardDataObjectDisplay)sender).dataObject;
            dataObjects.Remove(selectedObject);
            DataObject newDo = new DataObject(selectedObject);
            Clipboard.SetDataObject(newDo, false);
            //System.Windows.Forms.Clipboard.SetDataObject(newDo, false, 4, 3);
        }

        public void ReorientHorizontal()
        {

        }

        public void ReorientVertical()
        {

        }

        public void Collapse()
        {

        }
    }
}
