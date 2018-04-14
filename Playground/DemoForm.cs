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
            // ApexTextBox.Text = ApexTextBox.Text;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitConversionProject();
        }

        private ConversionProject ConversionProject { get; set; }

        private void InitConversionProject()
        {
            // recreate the conversion project
            ConversionProject = new ConversionProject
            {
                ApexDirectoryName = Settings.Default.ApexDirectory,
                CSharpDirectoryName = Settings.Default.CSharpDirectory
            };

            ConversionProject.LoadFiles();

            // display the files in the list boxes
            ApexFilesBox.DataSource = ConversionProject.ApexFiles;
            CSharpFilesBox.DataSource = ConversionProject.CSharpFiles;

            // refresh the UI
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

        private void ApexTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (ConversionProject?.CurrentApexFile != null)
            {
                ConversionProject.CurrentApexFile.CurrentText = ApexTextBox.Text;
            }
        }

        private void ApexTextBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
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

        private void CSharpTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (ConversionProject?.CurrentCSharpFile != null)
            {
                ConversionProject.CurrentCSharpFile.CurrentText = CSharpTextBox.Text;
            }
        }

        private void CSharpTextBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
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
                InitConversionProject();
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

        private void ListFilesBox_MouseClick(object sender, MouseEventArgs mouseArgs)
        {
            var listBox = sender as ListBox;
            int index = listBox.IndexFromPoint(mouseArgs.Location);
            if (index != ListBox.NoMatches)
            {
                SelectFile(listBox, index);
            }
        }

        private void SelectFile(ListBox listBox, int index)
        {
            var targetTextBox = ApexTextBox;
            if (listBox == CSharpFilesBox)
            {
                targetTextBox = CSharpTextBox;
            }

            var item = listBox.Items[index] as ConversionFileItem;
            if (!item.IsNew && !item.IsLoaded)
            {
                item.Load();
            }

            ConversionProject.SetCurrentFileItem(item);
            targetTextBox.Text = item.CurrentText;
        }
    }
}
