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
            ApexFilesBox.DataBindings.Clear();
            ApexFilesBox.DataBindings.Add(new Binding(nameof(ApexFilesBox.SelectedItem), ConversionProject, nameof(ConversionProject.CurrentApexFile)));
            CSharpFilesBox.DataSource = ConversionProject.CSharpFiles;
            CSharpFilesBox.DataBindings.Clear();
            CSharpFilesBox.DataBindings.Add(new Binding(nameof(CSharpFilesBox.SelectedItem), ConversionProject, nameof(ConversionProject.CurrentCSharpFile)));

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
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            try
            {
                return ApexSharpParser.ConvertApexToCSharp(s, Settings.Default.CSharpNamespace);
            }
            catch (ParseException)
            {
                return string.Empty;
            }
        }

        private string ToApex(string s) =>
            string.IsNullOrWhiteSpace(s) ? string.Empty :
            ApexSharpParser.ToApex(s).FirstOrDefault();

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

        private void SaveApexFileButton_Click(object sender, EventArgs e)
        {
            if (ConversionProject.CurrentApexFile != null)
            {
                ConversionProject.CurrentApexFile.Save();
                return;
            }

            // file not selected, Save as
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog.FileName, ApexTextBox.Text);
            }
        }

        private void SaveCSharpFileButton_Click(object sender, EventArgs e)
        {
            if (ConversionProject.CurrentCSharpFile != null)
            {
                ConversionProject.CurrentCSharpFile.Save();
                return;
            }

            // file not selected, Save as
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

            SaveApexFileButton.Text = "Save " + ConversionProject.CurrentApexFile.FileName;
            SaveCSharpFileButton.Text = "Save " + ConversionProject.CurrentCSharpFile.FileName;
        }

        private void SaveAllApexFilesButton_Click(object sender, EventArgs e)
        {
            foreach (var file in ConversionProject.ApexFiles.Where(f => f.IsNew || f.IsModified))
            {
                file.Save();
            }
        }

        private void SaveAllCSharpFilesButton_Click(object sender, EventArgs e)
        {
            foreach (var file in ConversionProject.CSharpFiles.Where(f => f.IsNew || f.IsModified))
            {
                file.Save();
            }
        }

        private void ApexFilesBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // we don't use that
            e.DrawBackground();

            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            ListBox lb = (ListBox)sender;
            g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }
    }
}
