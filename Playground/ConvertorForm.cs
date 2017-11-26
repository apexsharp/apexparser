using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private string ToCSharp(string s) => ApexParser.ApexParser.ConvertApexToCSharp(s);

        private string ToApex(string s) => ApexParser.CSharpHelper.ToApex(s).FirstOrDefault();

        private void LeftBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e) => DoConvert();

        private void DoConvert()
        {
            RightBox.Text = Convert(LeftBox.Text);
        }
    }
}
