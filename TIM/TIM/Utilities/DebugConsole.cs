using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TIM.Utilities;
using System.Windows;

namespace TIM
{
    class DebugConsole
    {
        public static void initialize()
        {
            Application.Current.Exit += ApplicationClosing;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (loggingDebug)
            {
                if (debugWindowInstance!=null)
                {

                }
            }
        }

        static void ApplicationClosing(object sender, ExitEventArgs e)
        {
            loggingDebug = false;
        }

        private static bool loggingDebug=false;
        public static bool isLogging
        {
            get
            {
                return loggingDebug;
            }
        }
        private static DebugWindow debugWindowInstance;

        public static void StartLoggingDebug()
        {
            loggingDebug = true;
            if (debugWindowInstance == null)
            {
                OpenDebugWindow();
            }
        }
        public static void StopLoggingDebug()
        {
            loggingDebug = false;
            if (debugWindowInstance != null)
            {
                debugWindowInstance.Close();
                debugWindowInstance = null;
            }
        }
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        public static void Print(string text)
        {
            if (loggingDebug)
            {
                if (debugWindowInstance == null)
                {
                    OpenDebugWindow();
                }

                string timeStamp = "[" + DateTime.Now.ToShortTimeString() + "]";
                StackTrace stackTrace = new StackTrace();
                string caller = stackTrace.GetFrame(1).GetMethod().GetType() + "." +  stackTrace.GetFrame(1).GetMethod().Name;
                string log = timeStamp + " " + caller + ": " + text;
                debugWindowInstance.AddLine(log);

                if (debugWindowInstance.printStackFrames)
                {
                    PrintStack(stackTrace, debugWindowInstance.stackFramesToPrint);
                }
            }
        }
        public static void Print(int i)
        {
            if (loggingDebug)
            {
                if (debugWindowInstance == null)
                {
                    OpenDebugWindow();
                }
                string text = i.ToString();
                string timeStamp = "[" + DateTime.Now.ToShortTimeString() + "]";
                StackTrace stackTrace = new StackTrace();
                string caller = stackTrace.GetFrame(1).GetMethod().GetType() + "." + stackTrace.GetFrame(1).GetMethod().Name;
                string log = timeStamp + " " + caller + ": " + text;
                debugWindowInstance.AddLine(log);

                if (debugWindowInstance.printStackFrames)
                {
                    PrintStack(stackTrace, debugWindowInstance.stackFramesToPrint);
                }
            }
        }
        public static void Print(char c)
        {
            if (loggingDebug)
            {
                if (debugWindowInstance == null)
                {
                    OpenDebugWindow();
                }
                string text = c.ToString();
                string timeStamp = "[" + DateTime.Now.ToShortTimeString() + "]";
                StackTrace stackTrace = new StackTrace();
                string caller = stackTrace.GetFrame(1).GetMethod().GetType() + "." + stackTrace.GetFrame(1).GetMethod().Name;
                string log = timeStamp + " " + caller + ": " + text;
                debugWindowInstance.AddLine(log);

                if (debugWindowInstance.printStackFrames)
                {
                    PrintStack(stackTrace, debugWindowInstance.stackFramesToPrint);
                }
            }
        }
        public static void Print(string[] strings)
        {

        }
        private static void PrintStack(StackTrace stackTrace, int numFrames)
        {
            if (numFrames > 0)
            {
                for (int i = 1; i <= numFrames && i < 10; i++)
                {
                    debugWindowInstance.AddLine(stackTrace.GetFrame(i).ToString(), true);
                }
            }
        }
        private static void OpenDebugWindow()
        {
            debugWindowInstance = new DebugWindow();
            debugWindowInstance.Closed += debugWindowInstance_Closed;
            debugWindowInstance.Show();
        }

        static void debugWindowInstance_Closed(object sender, EventArgs e)
        {
            if (loggingDebug)
            {
                string messageBoxText = "**FOR DEVELOPERS AND TESTING PURPOSES**\n\n"
                + "Close Debug Console and disable logging?\n"
                + "(To open and enable again later, click the Notes toolbar's '?' while that toolbar is closed)";
                MessageBoxResult result = MessageBox.Show(messageBoxText, "Close Debug Console?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    StopLoggingDebug();
                }
                else
                {
                    OpenDebugWindow();
                }
            }
        }

        public static void DisplayDebugDialogue()
        {
            if (loggingDebug)
            {
                ShowStopLogging();
            }
            else
            {
                ShowStartLogging();
            }
        }
        private static void ShowStartLogging()
        {
            string messageBoxText = "**FOR DEVELOPERS AND TESTING PURPOSES**\n\n"
            +"Open Debug Console and enable logging?\n"
            + "(To close and disable later, click the Notes toolbar's '?' while that toolbar is closed)";
            MessageBoxResult result = MessageBox.Show(messageBoxText, "Open Debug Console?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                StartLoggingDebug();
            }
        }
        private static void ShowStopLogging()
        {
            string messageBoxText = "**FOR DEVELOPERS AND TESTING PURPOSES**\n\n"
            + "Close Debug Console and disable logging?\n"
            + "(To open and enable again later, click the Notes toolbar's '?' while that toolbar is closed)";
            MessageBoxResult result = MessageBox.Show(messageBoxText, "Close Debug Console?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                StopLoggingDebug();
            }
        }
    }
}
