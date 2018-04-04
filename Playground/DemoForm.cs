using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            ApexLabel.LinkArea = new LinkArea(0, 0);
            if (!string.IsNullOrWhiteSpace(Settings.Default.ApexDirectory))
            {
                var position = ApexLabel.Text.Length + 3;
                ApexLabel.Text += " — " + Settings.Default.ApexDirectory;
                ApexLabel.LinkArea = new LinkArea(position, Settings.Default.ApexDirectory.Length);
            }

            CSharpLabel.Text = "C#";
            CSharpLabel.LinkArea = new LinkArea(0, 0);
            if (!string.IsNullOrWhiteSpace(Settings.Default.CSharpDirectory))
            {
                var position = CSharpLabel.Text.Length + 3;
                CSharpLabel.Text += " — " + Settings.Default.CSharpDirectory;
                CSharpLabel.LinkArea = new LinkArea(position, Settings.Default.CSharpDirectory.Length);
            }

            LoadFiles(ApexFilesBox, Settings.Default.ApexDirectory, "*.cls");
            LoadFiles(CSharpFilesBox, Settings.Default.CSharpDirectory, "*.cs");
        }

        private void LoadFiles(ListBox listBox, string path, string mask)
        {
            listBox.Items.Clear();

            try
            {
                var files =
                    from file in Directory.GetFiles(path, mask)
                    select Path.GetFileName(file);

                listBox.Items.AddRange(files.ToArray());
            }
            catch (ArgumentException)
            {
                // empty path?
            }
            catch (DirectoryNotFoundException)
            {
                // bad path?
            }
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
            SetupForm setup = new SetupForm();
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

        private void ApexLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = Settings.Default.ApexDirectory
            });
        }

        private void CSharpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = Settings.Default.CSharpDirectory
            });
        }

        private void ListFilesBox_MouseDoubleClick(object sender, MouseEventArgs mouseArgs)
        {
            var listBox = sender as ListBox;
            int index = listBox.IndexFromPoint(mouseArgs.Location);
            if (index != ListBox.NoMatches)
            {
                LoadFile(listBox, index);
            }
        }

        private void LoadFile(ListBox listBox, int index)
        {
            var rootPath = Settings.Default.ApexDirectory;
            var targetTextBox = ApexTextBox;
            if (listBox == CSharpFilesBox)
            {
                rootPath = Settings.Default.CSharpDirectory;
                targetTextBox = CSharpTextBox;
            }

            var item = listBox.Items[index] as string;
            var fileName = Path.Combine(rootPath, item);
            targetTextBox.Text = File.ReadAllText(fileName);
        }
    }
}
