using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TIM.Models
{
    public class QuickNote
    {
        string fileName;
        string filePath;
        string _snippet;
        int snippetLength = 25;
        FlowDocument document;
        public bool hasSavedPreviously;

        public QuickNote()
        {
            fileName = "QN_" + DateTime.Now.ToString("MM.dd.yy_H.mm.ss") + ".xaml";
            filePath = Constants.QuickNoteSavePath;
            document = new FlowDocument();
            hasSavedPreviously = false;
        }
        public QuickNote(string fileName)
        {
            this.fileName = fileName;
            filePath = Constants.QuickNoteSavePath;
            document = new FlowDocument();
            LoadDocumentFromFile();
            hasSavedPreviously = true;
        }
        public void UpdateFromTextBoxDocument(RichTextBox textBox)
        {
            document = textBox.Document;
        }
        public void SaveNoteDocumentToFile()
        {
            TextRange range;
            FileStream fstream;
            range = new TextRange(document.ContentStart, document.ContentEnd);
            fstream = new FileStream(filePath+fileName, FileMode.Create);
            range.Save(fstream, DataFormats.XamlPackage);
            fstream.Close();

            string snippetLong = range.Text;
            if (snippetLong.Length > snippetLength)
            {
                _snippet = snippetLong.Substring(0, snippetLength);
                _snippet += " ...";
            }
            else
            {
                _snippet = snippetLong;
            }
            hasSavedPreviously = true;
        }
        public void LoadDocumentToTextBox(RichTextBox textBox)
        {
            textBox.Document = document;
        }
        public void LoadDocumentFromFile()
        {
            TextRange range;
            FileStream fstream;
            if (File.Exists(filePath+fileName))
            {
                range = new TextRange(document.ContentStart, document.ContentEnd);
                fstream = new FileStream(filePath+fileName, FileMode.OpenOrCreate);
                range.Load(fstream, DataFormats.XamlPackage);
                fstream.Close();

                string snippetLong = range.Text;
                if (snippetLong.Length > snippetLength)
                {
                    _snippet = snippetLong.Substring(0, snippetLength);
                    _snippet += " ...";
                }
                else
                {
                    _snippet = snippetLong;
                }
                hasSavedPreviously = true;
            }
            hasSavedPreviously = true;
        }

        public void DeleteNote()
        {
            if (File.Exists(filePath+fileName))
            {
                File.Delete(filePath+fileName);
            }
        }

        public string Snippet { 
            get
            {
                string ret;
                if(String.IsNullOrWhiteSpace(_snippet))
                {
                    ret = "(empty)";
                }
                else
                {
                    ret = _snippet;
                }
                return ret;
            }
            private set
            {
                _snippet = value;
            }
        }
        public string GetSnippet()
        {
            string retString;
            if (String.IsNullOrWhiteSpace(_snippet))
            {
                retString = "(empty)";
            }
            else
            {
                retString = _snippet;
            }
            return retString;
        }
    }
}
