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

namespace ScrapProject
{
    /// <summary>
    /// Interaction logic for QuickNotesTest.xaml
    /// </summary>
    public partial class QuickNotesTest : Window
    {
        bool created1 = false;
        bool created2 = false;
        bool created3 = false;
        bool created4 = false;
        bool created5 = false;
        QuickNote qn1;
        QuickNote qn2;
        QuickNote qn3;
        QuickNote qn4;
        QuickNote qn5;


        QuickNoteCollection quickNotes;
        public QuickNotesTest()
        {
            quickNotes = new QuickNoteCollection();
            if (!quickNotes.existingNotesLoaded)
            {
                quickNotes.LoadAllQuickNotes();
            }
            InitializeComponent();
        }

        private void SaveBut1_Click(object sender, RoutedEventArgs e)
        {
            if (!created1)
            {
                created1 = true;
                qn1 = new QuickNote();
            }
            qn1.SaveNoteFromTextBoxToFile(Box1);
        }

        private void ClearBut1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoadBut1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBut1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBut2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBut2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBut2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBut2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBut3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBut3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBut3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBut3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBut4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBut4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBut4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBut4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBut5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBut5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBut5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBut5_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
