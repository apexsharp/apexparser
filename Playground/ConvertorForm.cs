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
    public partial class ConvertorForm : Form
    {
        public ConvertorForm()
        {
            InitializeComponent();
            Convert = ToCSharp;
            DoConvert();
        }

        private Func<string, string> Convert { get; set; }

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

        private void LeftBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e) => DoConvert();

        private void DoConvert()
        {
            var result = Convert(LeftBox.Text);
            if (!string.IsNullOrWhiteSpace(result))
            {
                RightBox.Text = result;
            }
        }

        private void ConversionButton_Click(object sender, EventArgs e)
        {
            if (Convert == ToCSharp)
            {
                Convert = ToApex;
                ConversionButton.Text = "Conversion: C# → Apex (click to swap)";
            }
            else
            {
                Convert = ToCSharp;
                ConversionButton.Text = "Conversion: Apex → C# (click to swap)";
            }

            LeftBox.Text = RightBox.Text;
            DoConvert();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                LeftBox.Text = File.ReadAllText(OpenFileDialog.FileName);
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
    }
}
