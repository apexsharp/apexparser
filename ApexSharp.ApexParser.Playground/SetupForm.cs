using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Playground.Properties;

namespace Playground
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApexDirectoryTextBox.Text = Settings.Default.ApexDirectory;
            CSharpDirectoryTextBox.Text = Settings.Default.CSharpDirectory;
            CSharpNamespaceTextBox.Text = Settings.Default.CSharpNamespace;
            TextEditorFontTextBox.Text = Settings.Default.TextEditorFont;
        }

        private void OpenApexDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.SelectedPath = ApexDirectoryTextBox.Text;
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ApexDirectoryTextBox.Text = FolderBrowserDialog.SelectedPath;
            }
        }

        private void OpenCSharpDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.SelectedPath = CSharpDirectoryTextBox.Text;
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CSharpDirectoryTextBox.Text = FolderBrowserDialog.SelectedPath;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Settings.Default.ApexDirectory = ApexDirectoryTextBox.Text;
            Settings.Default.CSharpDirectory = CSharpDirectoryTextBox.Text;
            Settings.Default.CSharpNamespace = CSharpNamespaceTextBox.Text;
            Settings.Default.TextEditorFont = TextEditorFontTextBox.Text;
            Settings.Default.Save();

            // close the form
            DialogResult = DialogResult.OK;
        }

        private void SelectFontButton_Click(object sender, EventArgs e)
        {
            var fontSpec = TextEditorFontTextBox.Text;
            var font = default(Font);
            if (!string.IsNullOrWhiteSpace(fontSpec))
            {
                try
                {
                    font = new FontConverter().ConvertFromString(fontSpec) as Font;
                }
                catch
                {
                    // invalid font specification
                }
            }

            if (font != null)
            {
                FontDialog.Font = font;
            }

            if (FontDialog.ShowDialog() == DialogResult.OK)
            {
                TextEditorFontTextBox.Text = new FontConverter().ConvertToString(FontDialog.Font);
            }
        }
    }
}
