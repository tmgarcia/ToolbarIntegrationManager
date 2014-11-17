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
using TestProject.Models;

namespace TestProject.UserControls
{
    /// <summary>
    /// Interaction logic for QuickNoteWindow.xaml
    /// </summary>
    public partial class QuickNoteWindow : Window
    {
        public static readonly RoutedEvent AddButtonClickedEvent = EventManager.RegisterRoutedEvent("AddButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuickNoteWindow));
        public event RoutedEventHandler AddButtonClicked
        {
            add { AddHandler(AddButtonClickedEvent, value); }
            remove { RemoveHandler(AddButtonClickedEvent, value); }
        }
        public static readonly RoutedEvent DeleteButtonClickedEvent = EventManager.RegisterRoutedEvent("DeleteButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuickNoteWindow));
        public event RoutedEventHandler DeleteButtonClicked
        {
            add { AddHandler(DeleteButtonClickedEvent, value); }
            remove { RemoveHandler(DeleteButtonClickedEvent, value); }
        }
        public static readonly RoutedEvent CloseButtonClickedEvent = EventManager.RegisterRoutedEvent("CloseButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuickNoteWindow));
        public event RoutedEventHandler CloseButtonClicked
        {
            add { AddHandler(CloseButtonClickedEvent, value); }
            remove { RemoveHandler(CloseButtonClickedEvent, value); }
        }

        public QuickNote note;
        bool closeButtonWasClicked = false;
        public QuickNoteWindow(QuickNote note)
        {
            this.note = note;
            InitializeComponent();
            note.LoadDocumentToTextBox(noteTextBox);
            this.Deactivated += QuickNoteWindow_Deactivated;
            this.Activated += QuickNoteWindow_Activated;

            this.ShowInTaskbar = false;
        }

        void QuickNoteWindow_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        void QuickNoteWindow_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(AddButtonClickedEvent, this));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(DeleteButtonClickedEvent, this));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            closeButtonWasClicked = true;
            note.UpdateFromTextBoxDocument(noteTextBox);
            noteTextBox.Document = new FlowDocument();
            this.RaiseEvent(new RoutedEventArgs(CloseButtonClickedEvent, this));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
