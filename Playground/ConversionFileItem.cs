using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Playground
{
    public class ConversionFileItem
    {
        public string Directory { get; set; }

        public string FileName { get; set; }

        public string ClassName { get; set; }

        public bool IsApex { get; set; }

        public string OriginalText { get; set; }

        private string currentText;

        public string CurrentText
        {
            get => currentText;
            set
            {
                if (currentText != value)
                {
                    currentText = value;
                    CurrentTextChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler CurrentTextChanged;

        public bool IsNew { get; set; }

        public bool IsModified => CurrentText != OriginalText;

        public bool IsLoaded => OriginalText != null;

        public override string ToString()
        {
            var result = FileName;
            if (IsNew)
            {
                result += " (new)";
            }
            else if (IsModified)
            {
                result += " (*)";
            }

            return result;
        }

        public void Load()
        {
            var fileName = Path.Combine(Directory, FileName);
            CurrentText = OriginalText = File.ReadAllText(fileName);
        }
    }
}
