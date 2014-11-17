using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.UserControls;
using TestProject.Tools;

namespace TestProject.Toolbars
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
            Tool qn = new Tool(qnotes);
            tools.Add(qn);
        }
    }
}
