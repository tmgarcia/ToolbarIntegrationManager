using System;
using System.Collections.Generic;
using System.Data;
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

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string num = (sender as Button).Content as string;
            if (DisplayBottom.Text == "0")
            {
                DisplayBottom.Text = num;
            }
            else
            {
                DisplayBottom.Text += num;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DisplayTop.Text = "";
            DisplayBottom.Text = "0";
        }
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DisplayBottom.Text))
            {
                PushBottomTextToTop();
            }
            if (!string.IsNullOrWhiteSpace(DisplayTop.Text))
            {
                string expression = DisplayTop.Text;
                string[] args = expression.Split(' ');
                string finalExpression = "";
                foreach (string arg in args)
                {
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        string modifiedArg = arg;
                        if (arg.Length > 1 && arg.Contains('-'))
                        {
                            modifiedArg = "(" + arg + ")";
                        }
                        finalExpression += modifiedArg;
                    }
                }
                double answer = Evaluate(finalExpression);
                DisplayBottom.Text = "" + answer;
                DisplayTop.Text += "=";
            }
        }

        private double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        private void FlipSign_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayBottom.Text.Contains('-'))
            {
                DisplayBottom.Text = DisplayBottom.Text.Substring(1);
            }
            else
            {
                DisplayBottom.Text = "-" + DisplayBottom.Text;
            }
        }
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            string operation = (sender as Button).Content as string;
            if (!string.IsNullOrWhiteSpace(DisplayTop.Text) && IsOperation(DisplayTop.Text[DisplayTop.Text.Length - 1]))
            {
                DisplayTop.Text = DisplayTop.Text.Remove(DisplayTop.Text.Length - 1) + operation;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(DisplayBottom.Text))
                {
                    PushBottomTextToTop();
                    DisplayTop.Text += " " + operation;
                }
            }
        }
        private void PushBottomTextToTop()
        {
            if(DisplayTop.Text.Contains('='))
            {
                DisplayTop.Text = "";
            }
            if (DisplayBottom.Text[DisplayBottom.Text.Length - 1] == '.')
            {
                DisplayBottom.Text = DisplayBottom.Text.Remove(DisplayTop.Text.Length - 1);
            }
            DisplayTop.Text += " " + DisplayBottom.Text;
            DisplayBottom.Text = "0";
        }
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if(!DisplayBottom.Text.Contains("."))
            {
                if(string.IsNullOrWhiteSpace(DisplayBottom.Text))
                {
                    DisplayBottom.Text = "0";
                }
                DisplayBottom.Text += ".";
            }
        }
        private bool IsOperation(char c)
        {
            return (c == '+' || c== '-' || c== '/' || c== '*');
        }
    }
}
