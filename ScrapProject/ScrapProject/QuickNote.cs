using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ScrapProject
{
    class QuickNote
    {
        string fileName;
        string filePath;
        string snippet;
        int snippetLength = 25;
        FlowDocument noteDocument;

        public QuickNote()
        {
            fileName = "QN_" + DateTime.Now.ToString("0:MM.dd.yy_H.mm.ss") + ".xaml";
            filePath = System.AppDomain.CurrentDomain.BaseDirectory + "QuickNotes/";
            noteDocument = new FlowDocument();
        }
        public QuickNote(string fileName)
        {
            this.fileName = fileName;
            filePath = System.AppDomain.CurrentDomain.BaseDirectory + "QuickNotes/";
            LoadDocumentFromFile();
        }

        public void UpdateFromTextBoxDocument(RichTextBox textBox)
        {
            noteDocument = textBox.Document;
        }
        public void SaveNoteDocumentToFile()
        {
            TextRange range;
            FileStream fstream;
            range = new TextRange(noteDocument.ContentStart, noteDocument.ContentEnd);
            fstream = new FileStream(filePath + fileName, FileMode.Create);
            range.Save(fstream, DataFormats.XamlPackage);
            fstream.Close();

            string snippetLong = range.Text;
            if (snippetLong.Length > snippetLength)
            {
                snippet = snippetLong.Substring(0, snippetLength);
                snippet += " ...";
            }
            else
            {
                snippet = snippetLong;
            }
        }
        public void SaveNoteFromTextBoxToFile(RichTextBox textBox)
        {
            noteDocument = textBox.Document;
            TextRange range;
            FileStream fstream;
            range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            fstream = new FileStream(filePath + fileName, FileMode.Create);
            range.Save(fstream, DataFormats.XamlPackage);
            fstream.Close();

            string snippetLong = range.Text;
            if (snippetLong.Length > snippetLength)
            {
                snippet = snippetLong.Substring(0, snippetLength);
                snippet += " ...";
            }
            else
            {
                snippet = snippetLong;
            }
        }
        public void LoadDocumentToTextBox(RichTextBox textBox)
        {
            textBox.Document = noteDocument;
        }
        public void LoadDocumentFromFile()
        {
            TextRange range;
            FileStream fstream;
            if (File.Exists(filePath + fileName))
            {
                range = new TextRange(noteDocument.ContentStart, noteDocument.ContentEnd);
                fstream = new FileStream(filePath + fileName, FileMode.OpenOrCreate);
                range.Load(fstream, DataFormats.XamlPackage);
                fstream.Close();
            }
        }
        public void LoadNoteToTextBoxFromFile(RichTextBox textBox)
        {
            TextRange range;
            FileStream fstream;
            if (File.Exists(filePath + fileName))
            {
                range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
                fstream = new FileStream(filePath + fileName, FileMode.OpenOrCreate);
                range.Load(fstream, DataFormats.XamlPackage);
                fstream.Close();
            }
        }

        public void DeleteNote()
        {
            if (File.Exists(filePath + fileName))
            {
                File.Delete(filePath + fileName);
            }
        }

        public string GetSnippet()
        {
            string retString;
            if (String.IsNullOrWhiteSpace(snippet))
            {
                retString = "(empty)";
            }
            else
            {
                retString = snippet;
            }
            return retString;
        }
    }
}
