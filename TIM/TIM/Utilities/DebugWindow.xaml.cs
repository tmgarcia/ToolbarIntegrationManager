using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TIM.Utilities
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
            
        }

        public void AddLine(string text, bool tabbed=false)
        {
            if (tabbed)
            {
                outputBox.AppendText("\t");
            }
            outputBox.AppendText(text);
            outputBox.AppendText("\u2028");
            if (autoScrollCheck.IsChecked.Value)
            {
                outputBox.ScrollToEnd();
            }
        }

        public bool printStackFrames
        {
            get
            {
                return printStacksCheck.IsChecked.Value;
            }
        }
        public bool printExceptions
        {
            get
            {
                return printExceptionsCheck.IsChecked.Value;
            }
        }
        public int stackFramesToPrint
        {
            get
            {
                int frames;
                if (!int.TryParse(stackFramesInput.Text, out frames))
                {
                    frames = 1;
                }
                return frames;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
