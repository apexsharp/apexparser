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
            this.LeftBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.OpenLeftButton = new System.Windows.Forms.Button();
            this.SaveLeftButton = new System.Windows.Forms.Button();
            this.RightBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.OpenRightButton = new System.Windows.Forms.Button();
            this.SaveRightButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).BeginInit();
            this.RightPanel.SuspendLayout();
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
            this.LeftBox.AutoIndentExistingLines = false;
            this.LeftBox.AutoScrollMinSize = new System.Drawing.Size(779, 770);
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
            this.LeftBox.Location = new System.Drawing.Point(0, 46);
            this.LeftBox.Name = "LeftBox";
            this.LeftBox.Paddings = new System.Windows.Forms.Padding(0);
            this.LeftBox.RightBracket = ')';
            this.LeftBox.RightBracket2 = '}';
            this.LeftBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.LeftBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("LeftBox.ServiceColors")));
            this.LeftBox.Size = new System.Drawing.Size(445, 545);
            this.LeftBox.TabIndex = 0;
            this.LeftBox.Text = resources.GetString("LeftBox.Text");
            this.LeftBox.Zoom = 100;
            this.LeftBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.LeftBox_TextChangedDelayed);
            this.LeftBox.Enter += new System.EventHandler(this.LeftBox_Enter);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.LeftBox);
            this.SplitContainer.Panel1.Controls.Add(this.LeftPanel);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.RightBox);
            this.SplitContainer.Panel2.Controls.Add(this.RightPanel);
            this.SplitContainer.Size = new System.Drawing.Size(908, 591);
            this.SplitContainer.SplitterDistance = 445;
            this.SplitContainer.TabIndex = 2;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.OpenLeftButton);
            this.LeftPanel.Controls.Add(this.SaveLeftButton);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(445, 46);
            this.LeftPanel.TabIndex = 1;
            // 
            // OpenLeftButton
            // 
            this.OpenLeftButton.Location = new System.Drawing.Point(12, 12);
            this.OpenLeftButton.Name = "OpenLeftButton";
            this.OpenLeftButton.Size = new System.Drawing.Size(100, 23);
            this.OpenLeftButton.TabIndex = 3;
            this.OpenLeftButton.Text = "Open...";
            this.OpenLeftButton.UseVisualStyleBackColor = true;
            this.OpenLeftButton.Click += new System.EventHandler(this.OpenLeftButton_Click);
            // 
            // SaveLeftButton
            // 
            this.SaveLeftButton.Location = new System.Drawing.Point(118, 12);
            this.SaveLeftButton.Name = "SaveLeftButton";
            this.SaveLeftButton.Size = new System.Drawing.Size(118, 23);
            this.SaveLeftButton.TabIndex = 3;
            this.SaveLeftButton.Text = "Save as...";
            this.SaveLeftButton.UseVisualStyleBackColor = true;
            this.SaveLeftButton.Click += new System.EventHandler(this.SaveLeftButton_Click);
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
            this.RightBox.Location = new System.Drawing.Point(0, 46);
            this.RightBox.Name = "RightBox";
            this.RightBox.Paddings = new System.Windows.Forms.Padding(0);
            this.RightBox.RightBracket = ')';
            this.RightBox.RightBracket2 = '}';
            this.RightBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.RightBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("RightBox.ServiceColors")));
            this.RightBox.Size = new System.Drawing.Size(459, 545);
            this.RightBox.TabIndex = 0;
            this.RightBox.Zoom = 100;
            this.RightBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.RightBox_TextChangedDelayed);
            this.RightBox.Enter += new System.EventHandler(this.RightBox_Enter);
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.OpenRightButton);
            this.RightPanel.Controls.Add(this.SaveRightButton);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RightPanel.Location = new System.Drawing.Point(0, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(459, 46);
            this.RightPanel.TabIndex = 1;
            // 
            // OpenRightButton
            // 
            this.OpenRightButton.Location = new System.Drawing.Point(18, 12);
            this.OpenRightButton.Name = "OpenRightButton";
            this.OpenRightButton.Size = new System.Drawing.Size(100, 23);
            this.OpenRightButton.TabIndex = 3;
            this.OpenRightButton.Text = "Open...";
            this.OpenRightButton.UseVisualStyleBackColor = true;
            this.OpenRightButton.Click += new System.EventHandler(this.OpenRightButton_Click);
            // 
            // SaveRightButton
            // 
            this.SaveRightButton.Location = new System.Drawing.Point(124, 12);
            this.SaveRightButton.Name = "SaveRightButton";
            this.SaveRightButton.Size = new System.Drawing.Size(118, 23);
            this.SaveRightButton.TabIndex = 3;
            this.SaveRightButton.Text = "Save as...";
            this.SaveRightButton.UseVisualStyleBackColor = true;
            this.SaveRightButton.Click += new System.EventHandler(this.SaveRightButton_Click);
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
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 591);
            this.Controls.Add(this.SplitContainer);
            this.Name = "DemoForm";
            this.Text = "Convertor playground demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.LeftBox)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.LeftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightBox)).EndInit();
            this.RightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox LeftBox;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private FastColoredTextBoxNS.FastColoredTextBox RightBox;
        private System.Windows.Forms.Button OpenLeftButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button SaveRightButton;
        private System.Windows.Forms.Button SaveLeftButton;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button OpenRightButton;
    }
}

