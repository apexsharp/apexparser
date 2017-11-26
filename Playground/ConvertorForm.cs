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
            DirectionBox.SelectedIndex = 0;
            DoConvert();
        }

        private Func<string, string> Convert =>
            DirectionBox.SelectedIndex == 0 ?
                new Func<string, string>(ToCSharp) : ToApex;

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
    }
}
