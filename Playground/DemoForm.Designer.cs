namespace Playground
{
    partial class DemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.LeftBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.RightBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.SaveAllCSharpFiles = new System.Windows.Forms.Button();
            this.SaveCSharpFile = new System.Windows.Forms.Button();
            this.SaveApexFile = new System.Windows.Forms.Button();
            this.SaveAllApexFiles = new System.Windows.Forms.Button();
            this.APEX = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).BeginInit();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.APEX);
            this.SplitContainer.Panel1.Controls.Add(this.splitContainer1);
            this.SplitContainer.Panel1.Controls.Add(this.LeftPanel);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.label1);
            this.SplitContainer.Panel2.Controls.Add(this.splitContainer2);
            this.SplitContainer.Panel2.Controls.Add(this.RightPanel);
            this.SplitContainer.Size = new System.Drawing.Size(1901, 1001);
            this.SplitContainer.SplitterDistance = 982;
            this.SplitContainer.SplitterWidth = 6;
            this.SplitContainer.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(34, 107);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SaveAllApexFiles);
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SaveApexFile);
            this.splitContainer1.Panel2.Controls.Add(this.LeftBox);
            this.splitContainer1.Size = new System.Drawing.Size(929, 737);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.TabIndex = 8;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(9, 53);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(289, 571);
            this.checkedListBox1.TabIndex = 3;
            // 
            // LeftBox
            // 
            this.LeftBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.LeftBox.AutoIndentExistingLines = false;
            this.LeftBox.AutoScrollMinSize = new System.Drawing.Size(1163, 1210);
            this.LeftBox.BackBrush = null;
            this.LeftBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.LeftBox.CharHeight = 22;
            this.LeftBox.CharWidth = 12;
            this.LeftBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LeftBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.LeftBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.LeftBox.IsReplaceMode = false;
            this.LeftBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.LeftBox.LeftBracket = '(';
            this.LeftBox.LeftBracket2 = '{';
            this.LeftBox.Location = new System.Drawing.Point(7, 49);
            this.LeftBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LeftBox.Name = "LeftBox";
            this.LeftBox.Paddings = new System.Windows.Forms.Padding(0);
            this.LeftBox.RightBracket = ')';
            this.LeftBox.RightBracket2 = '}';
            this.LeftBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.LeftBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("LeftBox.ServiceColors")));
            this.LeftBox.Size = new System.Drawing.Size(556, 605);
            this.LeftBox.TabIndex = 1;
            this.LeftBox.Text = resources.GetString("LeftBox.Text");
            this.LeftBox.Zoom = 100;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.button1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(982, 71);
            this.LeftPanel.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "Setup";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RightPanel
            // 
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightPanel.Location = new System.Drawing.Point(0, 0);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(913, 71);
            this.RightPanel.TabIndex = 1;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "Demo.cls";
            this.OpenFileDialog.Filter = "Apex (*.cls;*.apex)|*.cls;*.apex|C# (*.cs)|*.cs|All files (*.*)|*.*";
            this.OpenFileDialog.RestoreDirectory = true;
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.RestoreDirectory = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(50, 107);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.SaveCSharpFile);
            this.splitContainer2.Panel1.Controls.Add(this.RightBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.SaveAllCSharpFiles);
            this.splitContainer2.Panel2.Controls.Add(this.checkedListBox2);
            this.splitContainer2.Size = new System.Drawing.Size(796, 845);
            this.splitContainer2.SplitterDistance = 490;
            this.splitContainer2.TabIndex = 7;
            // 
            // RightBox
            // 
            this.RightBox.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.RightBox.AutoScrollMinSize = new System.Drawing.Size(35, 22);
            this.RightBox.BackBrush = null;
            this.RightBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.RightBox.CharHeight = 22;
            this.RightBox.CharWidth = 12;
            this.RightBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RightBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.RightBox.IsReplaceMode = false;
            this.RightBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.RightBox.LeftBracket = '(';
            this.RightBox.LeftBracket2 = '{';
            this.RightBox.Location = new System.Drawing.Point(0, 28);
            this.RightBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RightBox.Name = "RightBox";
            this.RightBox.Paddings = new System.Windows.Forms.Padding(0);
            this.RightBox.RightBracket = ')';
            this.RightBox.RightBracket2 = '}';
            this.RightBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.RightBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("RightBox.ServiceColors")));
            this.RightBox.Size = new System.Drawing.Size(511, 564);
            this.RightBox.TabIndex = 1;
            this.RightBox.Zoom = 100;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(7, 44);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(289, 697);
            this.checkedListBox2.TabIndex = 4;
            // 
            // SaveAllCSharpFiles
            // 
            this.SaveAllCSharpFiles.Location = new System.Drawing.Point(3, 767);
            this.SaveAllCSharpFiles.Name = "SaveAllCSharpFiles";
            this.SaveAllCSharpFiles.Size = new System.Drawing.Size(289, 58);
            this.SaveAllCSharpFiles.TabIndex = 7;
            this.SaveAllCSharpFiles.Text = "Save All";
            this.SaveAllCSharpFiles.UseVisualStyleBackColor = true;
            // 
            // SaveCSharpFile
            // 
            this.SaveCSharpFile.Location = new System.Drawing.Point(22, 753);
            this.SaveCSharpFile.Name = "SaveCSharpFile";
            this.SaveCSharpFile.Size = new System.Drawing.Size(153, 58);
            this.SaveCSharpFile.TabIndex = 5;
            this.SaveCSharpFile.Text = "Save File";
            this.SaveCSharpFile.UseVisualStyleBackColor = true;
            // 
            // SaveApexFile
            // 
            this.SaveApexFile.Location = new System.Drawing.Point(35, 662);
            this.SaveApexFile.Name = "SaveApexFile";
            this.SaveApexFile.Size = new System.Drawing.Size(153, 58);
            this.SaveApexFile.TabIndex = 8;
            this.SaveApexFile.Text = "Save File";
            this.SaveApexFile.UseVisualStyleBackColor = true;
            // 
            // SaveAllApexFiles
            // 
            this.SaveAllApexFiles.Location = new System.Drawing.Point(9, 662);
            this.SaveAllApexFiles.Name = "SaveAllApexFiles";
            this.SaveAllApexFiles.Size = new System.Drawing.Size(289, 58);
            this.SaveAllApexFiles.TabIndex = 8;
            this.SaveAllApexFiles.Text = "Save All";
            this.SaveAllApexFiles.UseVisualStyleBackColor = true;
            // 
            // APEX
            // 
            this.APEX.AutoSize = true;
            this.APEX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APEX.Location = new System.Drawing.Point(37, 72);
            this.APEX.Name = "APEX";
            this.APEX.Size = new System.Drawing.Size(84, 32);
            this.APEX.TabIndex = 9;
            this.APEX.Text = "Apex";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "C#";
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1901, 1001);
            this.Controls.Add(this.SplitContainer);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DemoForm";
            this.Text = "ApexSharp UI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private FastColoredTextBoxNS.FastColoredTextBox LeftBox;
        private System.Windows.Forms.Label APEX;
        private System.Windows.Forms.Button SaveAllApexFiles;
        private System.Windows.Forms.Button SaveApexFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button SaveCSharpFile;
        private FastColoredTextBoxNS.FastColoredTextBox RightBox;
        private System.Windows.Forms.Button SaveAllCSharpFiles;
        public System.Windows.Forms.CheckedListBox checkedListBox2;
    }
}

