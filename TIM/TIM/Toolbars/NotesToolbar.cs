﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIM.Models;
using TIM.UserControls;
using TIM.Tools;
using System.Windows;

namespace TIM.Toolbars
{
    class NotesToolbar : Toolbar
    {
        QuickNoteCollection quickNoteCollection;
        public NotesToolbar() : base()
        {
            iconA =     "notesIconActive.png";
            iconAH =    "notesIconActiveHover.png";
            iconAP =    "notesIconActivePressed.png";
            iconI =     "notesIconInactive.png";
            iconIH =    "notesIconInactiveHover.png";
            iconIP =    "notesIconInactivePressed.png";
            iconD =     "notesIconDisabled.png";
            displayIconPath = Constants.ToolbarIconPath + "notesIconInToolbar.png";
            SetupLaunchIcon();
            SetupCommandButtons();
            SetupTools();
            commandButtons.Help.Click += Help_Click;
        }

        void Help_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DebugConsole.DisplayDebugDialogue();
        }
       

        protected override void PreDisplaySetup()
        {
            if (!quickNoteCollection.existingNotesLoaded)
            {
                quickNoteCollection.LoadAllQuickNotes();
            }
            base.PreDisplaySetup();
        }

        private void SetupTools()
        {
            quickNoteCollection = new QuickNoteCollection();
            Notes_QuickNotes qnotes = new Notes_QuickNotes(quickNoteCollection);
            Notes_ScreenClipping clipping = new Notes_ScreenClipping();
            //Notes_Clipboard clip = new Notes_Clipboard();
            tools.Add(new Tool(qnotes));
            tools.Add(new Tool(clipping));
            //tools.Add(new Tool(clip));
        }
    }
}
