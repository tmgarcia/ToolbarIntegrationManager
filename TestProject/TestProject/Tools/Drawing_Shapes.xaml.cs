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
    /// Interaction logic for Drawing_Shapes.xaml
    /// </summary>
    public partial class Drawing_Shapes : UserControl
    {
        public Drawing_Shapes()
        {
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolShapes"));
        }

        private void Square_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void Circle_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void Triangle_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
