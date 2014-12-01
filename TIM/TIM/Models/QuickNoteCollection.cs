using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIM.Models
{
    public class QuickNoteCollection
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
            if (Directory.Exists(Constants.QuickNoteSavePath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Constants.QuickNoteSavePath);
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
            else
            {
                Directory.CreateDirectory(Constants.QuickNoteSavePath);
            }
            existingNotesLoaded = true;
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
