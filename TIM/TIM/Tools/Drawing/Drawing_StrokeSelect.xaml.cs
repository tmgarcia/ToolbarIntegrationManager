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
using TIM.Models;
using TIM.UserControls;

namespace TIM.Tools
{
    /// <summary>
    /// Interaction logic for Drawing_StrokeSelect.xaml
    /// </summary>
    public partial class Drawing_StrokeSelect : UserControl, IDeactivatableTool
    {
        public static readonly RoutedEvent StrokeColorSelectedEvent = EventManager.RegisterRoutedEvent("StrokeColorSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Drawing_StrokeSelect));
        public event RoutedEventHandler StrokeColorSelected
        {
            add { AddHandler(StrokeColorSelectedEvent, value); }
            remove { RemoveHandler(StrokeColorSelectedEvent, value); }
        }
        ListBox colorChoiceDisplay;
        public Color currentColor;
        public Drawing_StrokeSelect()
        {
            InitializeComponent();

            colorChoiceDisplay = expandable.PopupContent as ListBox;
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolFilledSquare"));
            Width = expandable.Width;
            Height = expandable.Height;
            outerBorder.Width = expandable.Width;
            outerBorder.Height = expandable.Height;
            expandable.Toggle.toggleControl.ToolTip = "Stroke Select";

            SetupColorChoices();
        }

        private void SetupColorChoices()
        {
            List<Color> colors = new List<Color>();
            colors.Add(Colors.Transparent);
            colors.Add(Colors.Black);
            colors.Add(Colors.White);
            colors.Add(Colors.Red);
            colors.Add(Colors.Orange);
            colors.Add(Colors.Yellow);
            colors.Add(Colors.Green);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Purple);
            colorChoiceDisplay.ItemsSource = colors;

            colorChoiceDisplay.SelectedIndex = 3;
            currentColor = colors[3];
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            outerBorderBackground.Color = (Color)colorChoiceDisplay.SelectedItem;
            currentColor = (Color)colorChoiceDisplay.SelectedItem;
            this.RaiseEvent(new RoutedEventArgs(StrokeColorSelectedEvent, this));
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }


        public void ReorientHorizontal()
        {
            ((OrientableListBox)colorChoiceDisplay).Orientation = Orientation.Vertical;
            colorChoiceDisplay.Width = 50;
            colorChoiceDisplay.Height = Double.NaN;
            expandable.SetPlacementModeBottom();
            expandable.setAlignmentPointTopLeft();
        }

        public void ReorientVertical()
        {
            colorChoiceDisplay.Height = 30;
            colorChoiceDisplay.Width = Double.NaN;
            ((OrientableListBox)colorChoiceDisplay).Orientation = Orientation.Horizontal;
            expandable.SetPlacementModeRight();
            expandable.setAlignmentPointTopLeft();
        }

        public void Collapse()
        {

        }
    }
}
