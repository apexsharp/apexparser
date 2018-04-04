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
        private DemoForm _mainForm;

        public SetupForm(DemoForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApexDirectoryTextBox.Text = Settings.Default.ApexDirectory;
            CSharpDirectoryTextBox.Text = Settings.Default.CSharpDirectory;
            CSharpNamespaceTextBox.Text = Settings.Default.CSharpNamespace;
        }

        private void OpenApexDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.SelectedPath = ApexDirectoryTextBox.Text;
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ApexDirectoryTextBox.Text = FolderBrowserDialog.SelectedPath;
                Settings.Default.ApexDirectory = FolderBrowserDialog.SelectedPath;
            }
        }

        private void OpenCSharpDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.SelectedPath = CSharpDirectoryTextBox.Text;
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CSharpDirectoryTextBox.Text = FolderBrowserDialog.SelectedPath;
                Settings.Default.CSharpDirectory = FolderBrowserDialog.SelectedPath;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            Close();
        }
    }
}
