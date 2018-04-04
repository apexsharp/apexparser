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
using Sprache;

namespace Playground
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
            LeftBox.Text = LeftBox.Text;
        }

        private string ToCSharp(string s)
        {
            try
            {
                return ApexSharpParser.ConvertApexToCSharp(s);
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
                LeftBox.Text = File.ReadAllText(OpenFileDialog.FileName);
                LeftBox.SelectionStart = 0;
                LeftBox.SelectionLength = 0;
            }
        }

        private void OpenRightButton_Click(object sender, EventArgs e)
        {
            ConvertLeftToRight = false;
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                RightBox.Text = File.ReadAllText(OpenFileDialog.FileName);
                RightBox.SelectionStart = 0;
                RightBox.SelectionLength = 0;
            }
        }

        private void LeftBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!ConvertLeftToRight)
            {
                return;
            }

            var result = ToCSharp(LeftBox.Text);
            if (!string.IsNullOrWhiteSpace(result))
            {
                RightBox.Text = result;
            }
        }

        private void RightBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!ConvertRightToLeft)
            {
                return;
            }

            var result = ToApex(RightBox.Text);
            if (!string.IsNullOrWhiteSpace(result))
            {
                LeftBox.Text = result;
            }
        }

        private void SaveLeftButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog.FileName, LeftBox.Text);
            }
        }

        private void SaveRightButton_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog.FileName, RightBox.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            SetupForm setup = new SetupForm(this);
            setup.Show();
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
    }
}
