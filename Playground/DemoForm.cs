using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApexParser;
using Playground.Properties;
using Sprache;

namespace Playground
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();

            // convert the code
            ApexTextBox.Text = ApexTextBox.Text;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadFiles();
        }

        private void LoadFiles()
        {
            ApexLabel.Text = "Apex";
            if (!string.IsNullOrWhiteSpace(Settings.Default.ApexDirectory))
            {
                ApexLabel.Text += " — " + Settings.Default.ApexDirectory;
            }

            CSharpLabel.Text = "C#";
            if (!string.IsNullOrWhiteSpace(Settings.Default.CSharpDirectory))
            {
                CSharpLabel.Text += " — " + Settings.Default.CSharpDirectory;
            }

            LoadFiles(ApexFilesBox, Settings.Default.ApexDirectory, "*.cls");
            LoadFiles(CSharpFilesBox, Settings.Default.CSharpDirectory, "*.cs");
        }

        private void LoadFiles(ListBox listBox, string path, string mask)
        {
            var files =
                from file in Directory.GetFiles(path, mask)
                select Path.GetFileName(file);

            listBox.Items.Clear();
            listBox.Items.AddRange(files.ToArray());
        }

        private string ToCSharp(string s)
        {
            try
            {
                return ApexSharpParser.ConvertApexToCSharp(s, Settings.Default.CSharpNamespace);
            }
            catch (ParseException)
            {
                return string.Empty;
            }
        }

        private string ToApex(string s) => ApexSharpParser.ToApex(s).FirstOrDefault();

        private bool ConvertLeftToRight { get; set; } = true;

        private bool ConvertRightToLeft => !ConvertLeftToRight;

        private void LeftBox_Enter(object sender, EventArgs e) => ConvertLeftToRight = true;

        private void RightBox_Enter(object sender, EventArgs e) => ConvertLeftToRight = false;

        private void OpenLeftButton_Click(object sender, EventArgs e)
        {
            ConvertLeftToRight = true;
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ApexTextBox.Text = File.ReadAllText(OpenFileDialog.FileName);
                ApexTextBox.SelectionStart = 0;
                ApexTextBox.SelectionLength = 0;
            }
        }

        private void OpenRightButton_Click(object sender, EventArgs e)
        {
            ConvertLeftToRight = false;
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                CSharpTextBox.Text = File.ReadAllText(OpenFileDialog.FileName);
                CSharpTextBox.SelectionStart = 0;
                CSharpTextBox.SelectionLength = 0;
            }
        }

        private void LeftBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!ConvertLeftToRight)
            {
                return;
            }

            var result = ToCSharp(ApexTextBox.Text);
            if (!string.IsNullOrWhiteSpace(result))
            {
                CSharpTextBox.Text = result;
            }
        }

        private void RightBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!ConvertRightToLeft)
            {
                return;
            }

            var result = ToApex(CSharpTextBox.Text);
            if (!string.IsNullOrWhiteSpace(result))
            {
                ApexTextBox.Text = result;
            }
        }

        private void SaveLeftButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog.FileName, ApexTextBox.Text);
            }
        }

        private void SaveRightButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog.FileName, CSharpTextBox.Text);
            }
        }

        private void SetupButton_Click(object sender, EventArgs e)
        {
            SetupForm setup = new SetupForm(this);
            if (setup.ShowDialog() == DialogResult.OK)
            {
                LoadFiles();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Save the file that is currently displayed
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Save all the files that are currently selected
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save the file that is currently displayed
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Save all the files that are currently selected
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
