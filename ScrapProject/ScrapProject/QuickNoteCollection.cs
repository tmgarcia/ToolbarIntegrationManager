using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapProject
{
    class QuickNoteCollection
    {
        public ObservableCollection<QuickNote> notes;
        public bool existingNotesLoaded;
        public QuickNoteCollection()
        {
            existingNotesLoaded = false;
            notes = new ObservableCollection<QuickNote>();
        }
        public void LoadAllQuickNotes()
        {
            existingNotesLoaded = true;
            DirectoryInfo dirInfo = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "QuickNotes/");
            FileInfo[] fileInfos = dirInfo.GetFiles("*.xaml", SearchOption.TopDirectoryOnly);
            if (fileInfos.Length > 0)
            {
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    QuickNote q = new QuickNote(fileInfos[i].Name);
                    notes.Add(q);
                }
            }
        }
        public void SaveAllQuickNotes()
        {
            if (notes.Count > 0)
            {
                foreach (QuickNote q in notes)
                {
                    q.SaveNoteDocumentToFile();
                }
            }
        }
    }
}
