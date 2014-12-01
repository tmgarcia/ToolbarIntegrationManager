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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TIM.Models;

namespace TIM.Tools
{
    /// <summary>
    /// Interaction logic for Conversion_Ascii.xaml
    /// </summary>
    public partial class Conversion_Ascii : UserControl, IDeactivatableTool
    {
        public Conversion_Ascii()
        {
            InitializeComponent();
            Height = Constants.ToolButtonHeight;
            Width = 200;
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }


        public void ReorientHorizontal()
        {
            if (DisplayGrid.RowDefinitions.Count > 2)
            {
                DisplayGrid.RowDefinitions.RemoveRange(2, DisplayGrid.RowDefinitions.Count - 2);
            }
            if (DisplayGrid.ColumnDefinitions.Count < 5)
            {
                for (int i = DisplayGrid.ColumnDefinitions.Count; i < 5; i++)
                {
                    ColumnDefinition cd = new ColumnDefinition();
                    if (i == 4)
                    {
                        cd.Width = new GridLength(1.5, GridUnitType.Star);
                    }
                    DisplayGrid.ColumnDefinitions.Add(cd);
                }
            }
            Grid.SetRow(ASCIILabel, 0);
            Grid.SetColumn(ASCIILabel, 0);
            ASCIILabel.FontSize = 9;
            Grid.SetRow(UnicodeLabel, 1);
            Grid.SetColumn(UnicodeLabel, 0);
            UnicodeLabel.FontSize = 9;
            Grid.SetRow(CharLabel, 0);
            Grid.SetColumn(CharLabel, 1);
            Grid.SetRow(BinLabel, 0);
            Grid.SetColumn(BinLabel, 3);
            Grid.SetRow(DecLabel, 1);
            Grid.SetColumn(DecLabel, 1);
            Grid.SetRow(HexLabel, 1);
            Grid.SetColumn(HexLabel, 3);

            Grid.SetRow(charBox, 0);
            Grid.SetColumn(charBox, 2);
            Grid.SetRow(binBox, 0);
            Grid.SetColumn(binBox, 4);
            Grid.SetRow(decBox, 1);
            Grid.SetColumn(decBox, 2);
            Grid.SetRow(hexBox, 1);
            Grid.SetColumn(hexBox, 4);

            Height = Constants.ToolButtonHeight;
            Width = 200;
        }

        public void ReorientVertical()
        {
            if (DisplayGrid.ColumnDefinitions.Count > 1)
            {
                DisplayGrid.ColumnDefinitions.RemoveRange(1, DisplayGrid.ColumnDefinitions.Count - 1);
            }
            if (DisplayGrid.RowDefinitions.Count < 10)
            {
                for (int i = DisplayGrid.RowDefinitions.Count; i < 10; i++)
                {
                    RowDefinition rd = new RowDefinition();
                    //if (i == 4)
                    //{
                    //    cd.Width = new GridLength(1.5, GridUnitType.Star);
                    //}
                    DisplayGrid.RowDefinitions.Add(rd);
                }
            }
            Grid.SetRow(ASCIILabel, 0);
            Grid.SetColumn(ASCIILabel, 0);
            ASCIILabel.FontSize = 8;
            Grid.SetRow(UnicodeLabel, 1);
            Grid.SetColumn(UnicodeLabel, 0);
            UnicodeLabel.FontSize = 8;

            Grid.SetRow(CharLabel, 2);
            Grid.SetColumn(CharLabel, 0);
            Grid.SetRow(BinLabel, 4);
            Grid.SetColumn(BinLabel, 0);
            Grid.SetRow(DecLabel, 6);
            Grid.SetColumn(DecLabel, 0);
            Grid.SetRow(HexLabel, 8);
            Grid.SetColumn(HexLabel, 0);

            Grid.SetRow(charBox, 3);
            Grid.SetColumn(charBox, 0);
            Grid.SetRow(binBox, 5);
            Grid.SetColumn(binBox, 0);
            Grid.SetRow(decBox, 7);
            Grid.SetColumn(decBox, 0);
            Grid.SetRow(hexBox, 9);
            Grid.SetColumn(hexBox, 0);

            Width= Constants.ToolButtonHeight;
            Height = Double.NaN;
        }

        public void Collapse()
        {

        }
    }
}
