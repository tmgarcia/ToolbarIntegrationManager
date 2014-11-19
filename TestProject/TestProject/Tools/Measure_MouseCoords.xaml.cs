using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using TestProject.Models;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Measure_MouseCoords.xaml
    /// </summary>
    public partial class Measure_MouseCoords : UserControl, IDeactivatableTool
    {
        public class MouseTrackEventArgs : EventArgs
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        public static event EventHandler<MouseTrackEventArgs> MouseAction = delegate { };

        public Measure_MouseCoords()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(SimpleView_Loaded);
            Unloaded += new RoutedEventHandler(SimpleView_Unloaded);

            Measure_MouseCoords.MouseAction += Measure_MouseCoords_MouseAction;
        }

        void Measure_MouseCoords_MouseAction(object sender, Measure_MouseCoords.MouseTrackEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            x = (x < 0) ? 0 : x;
            y = (y < 0) ? 0 : y;
            xCoordDisplay.Text = "" + x;
            yCoordDisplay.Text = "" + y;
        }

        void SimpleView_Loaded(object sender, RoutedEventArgs e)
        {
            _hookID = SetHook(_proc);
        }

        void SimpleView_Unloaded(object sender, RoutedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                  GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                //Debug.WriteLine(hookStruct.pt.x + ", " + hookStruct.pt.y);
                MouseAction(null, new MouseTrackEventArgs(){X=hookStruct.pt.x, Y=hookStruct.pt.y});
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
          LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
          IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }

}
