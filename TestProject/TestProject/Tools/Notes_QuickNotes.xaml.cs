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
using TestProject.Models;
using TestProject.UserControls;

namespace TestProject.Tools
{
    /// <summary>
    /// Interaction logic for Notes_QuickNotes.xaml
    /// </summary>
    public partial class Notes_QuickNotes : UserControl, IDeactivatableTool
    {
        public QuickNoteCollection noteCollection;
        ListBox noteDisplay;

        public Notes_QuickNotes(QuickNoteCollection noteCollection)
        {
            this.noteCollection = noteCollection;
            InitializeComponent();
            expandable.setSymbol((DrawingImage)Application.Current.FindResource("SymbolNote"));
            StackPanel content = expandable.PopupContent as StackPanel;
            noteDisplay = content.Children[1] as ListBox;
            noteDisplay.ItemsSource = noteCollection.notes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewQuickNote();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteDisplay.SelectedIndex != -1)
            {
                QuickNote q = noteDisplay.SelectedItem as QuickNote;
                if (q.hasSavedPreviously)
                {
                    q.DeleteNote();
                }
                noteCollection.notes.Remove(q);
            }
        }

        private void CreateNewQuickNote()
        {
            QuickNote q = new QuickNote();
            QuickNoteWindow qDisplay = new QuickNoteWindow(q);
            qDisplay.AddButtonClicked += qDisplay_AddButtonClicked;
            qDisplay.DeleteButtonClicked += qDisplay_DeleteButtonClicked;
            qDisplay.CloseButtonClicked += qDisplay_CloseButtonClicked;
            qDisplay.Show();
        }

        void qDisplay_CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            QuickNoteWindow qDisplay = sender as QuickNoteWindow;
            if (!qDisplay.note.hasSavedPreviously)
            {
                qDisplay.note.SaveNoteDocumentToFile();
                noteCollection.notes.Add(qDisplay.note);
            }
            qDisplay.Close();
        }

        void qDisplay_DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            QuickNoteWindow qDisplay = sender as QuickNoteWindow;
            if (qDisplay.note.hasSavedPreviously)
            {
                qDisplay.note.DeleteNote();
                noteCollection.notes.Remove(qDisplay.note);
            }
            qDisplay.Close();
        }

        void qDisplay_AddButtonClicked(object sender, RoutedEventArgs e)
        {
            CreateNewQuickNote();
        }

        private void NoteDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (noteDisplay.SelectedIndex != -1)
            {
                QuickNote q = noteDisplay.SelectedItem as QuickNote;
                QuickNoteWindow qDisplay = new QuickNoteWindow(q);
                qDisplay.AddButtonClicked += qDisplay_AddButtonClicked;
                qDisplay.DeleteButtonClicked += qDisplay_DeleteButtonClicked;
                qDisplay.CloseButtonClicked += qDisplay_CloseButtonClicked;
                qDisplay.Show();
            }
        }

        void qDisplay_ClosedWithoutSaving(object sender, RoutedEventArgs e)
        {
            QuickNote q = sender as QuickNote;
            if (!q.hasSavedPreviously)
            {
                q.SaveNoteDocumentToFile();
                noteCollection.notes.Add(q);
            }
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
