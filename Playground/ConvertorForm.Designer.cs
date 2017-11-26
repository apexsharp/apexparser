namespace Playground
{
    partial class ConvertorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertorForm));
            this.LeftBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.DirectionBox = new System.Windows.Forms.ComboBox();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.RightBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftBox
            // 
            this.LeftBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.LeftBox.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.LeftBox.AutoScrollMinSize = new System.Drawing.Size(387, 112);
            this.LeftBox.BackBrush = null;
            this.LeftBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.LeftBox.CharHeight = 14;
            this.LeftBox.CharWidth = 8;
            this.LeftBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LeftBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.LeftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.LeftBox.IsReplaceMode = false;
            this.LeftBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.LeftBox.LeftBracket = '(';
            this.LeftBox.LeftBracket2 = '{';
            this.LeftBox.Location = new System.Drawing.Point(0, 0);
            this.LeftBox.Name = "LeftBox";
            this.LeftBox.Paddings = new System.Windows.Forms.Padding(0);
            this.LeftBox.RightBracket = ')';
            this.LeftBox.RightBracket2 = '}';
            this.LeftBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.LeftBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("LeftBox.ServiceColors")));
            this.LeftBox.Size = new System.Drawing.Size(368, 487);
            this.LeftBox.TabIndex = 0;
            this.LeftBox.Text = "public with sharing class ClassOne\r\n{\r\n     public void CallClassTwo()\r\n     {\r\n " +
    "         ClassTwo classTwo = new ClassTwo();\r\n          System.debug(\'Test\');\r\n " +
    "    }\r\n}";
            this.LeftBox.Zoom = 100;
            this.LeftBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.LeftBox_TextChangedDelayed);
            // 
            // DirectionBox
            // 
            this.DirectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectionBox.FormattingEnabled = true;
            this.DirectionBox.Items.AddRange(new object[] {
            "Apex → C#",
            "C# → Apex"});
            this.DirectionBox.Location = new System.Drawing.Point(12, 12);
            this.DirectionBox.Name = "DirectionBox";
            this.DirectionBox.Size = new System.Drawing.Size(250, 21);
            this.DirectionBox.TabIndex = 1;
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 41);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.LeftBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.RightBox);
            this.SplitContainer.Size = new System.Drawing.Size(750, 487);
            this.SplitContainer.SplitterDistance = 368;
            this.SplitContainer.TabIndex = 2;
            // 
            // RightBox
            // 
            this.RightBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.RightBox.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.RightBox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.RightBox.BackBrush = null;
            this.RightBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.RightBox.CharHeight = 14;
            this.RightBox.CharWidth = 8;
            this.RightBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RightBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.RightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightBox.IsReplaceMode = false;
            this.RightBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.RightBox.LeftBracket = '(';
            this.RightBox.LeftBracket2 = '{';
            this.RightBox.Location = new System.Drawing.Point(0, 0);
            this.RightBox.Name = "RightBox";
            this.RightBox.Paddings = new System.Windows.Forms.Padding(0);
            this.RightBox.RightBracket = ')';
            this.RightBox.RightBracket2 = '}';
            this.RightBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.RightBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("RightBox.ServiceColors")));
            this.RightBox.Size = new System.Drawing.Size(378, 487);
            this.RightBox.TabIndex = 0;
            this.RightBox.Zoom = 100;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.DirectionBox);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(750, 41);
            this.TopPanel.TabIndex = 3;
            this.TopPanel.Visible = false;
            // 
            // ConvertorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 528);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.TopPanel);
            this.Name = "ConvertorForm";
            this.Text = "Convertor playground";
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox LeftBox;
        private System.Windows.Forms.ComboBox DirectionBox;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private FastColoredTextBoxNS.FastColoredTextBox RightBox;
        private System.Windows.Forms.Panel TopPanel;
    }
}

