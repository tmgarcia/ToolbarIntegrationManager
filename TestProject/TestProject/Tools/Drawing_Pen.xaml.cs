﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_Pen.xaml
    /// </summary>
    public partial class Drawing_Pen : UserControl
    {
        public Drawing_Pen()
        {
            InitializeComponent();
            toggle.SetSymbol((DrawingImage)Application.Current.FindResource("SymbolPencil"));
            toggle.Toggle.Checked += Toggle_Checked;
            toggle.Toggle.Unchecked += Toggle_Unchecked;
        }
        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Pen;
        }
        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Arrow;
        }

        
    }
}