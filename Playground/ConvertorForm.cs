using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                return ApexParser.ApexParser.ConvertApexToCSharp(s);
            }
            catch (ParseException ex)
            {
                return string.Empty;
            }
        }

        private string ToApex(string s) => ApexParser.CSharpHelper.ToApex(s).FirstOrDefault();

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
    }
}
