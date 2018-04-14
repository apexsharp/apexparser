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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.ApexSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ApexFilesBox = new System.Windows.Forms.ListBox();
            this.ApexTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ApexLabel = new System.Windows.Forms.LinkLabel();
            this.LeftButtonPanel = new System.Windows.Forms.Panel();
            this.SaveApexFileButton = new System.Windows.Forms.Button();
            this.SaveAllApexFilesButton = new System.Windows.Forms.Button();
            this.CSharpSplitContainer = new System.Windows.Forms.SplitContainer();
            this.CSharpFilesBox = new System.Windows.Forms.ListBox();
            this.CSharpTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveCSharpFileButton = new System.Windows.Forms.Button();
            this.SaveAllCSharpFilesButton = new System.Windows.Forms.Button();
            this.CSharpLabel = new System.Windows.Forms.LinkLabel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveCurrentApexFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllApexFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveCurrentCSharpFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllCSharpFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApexSplitContainer)).BeginInit();
            this.ApexSplitContainer.Panel1.SuspendLayout();
            this.ApexSplitContainer.Panel2.SuspendLayout();
            this.ApexSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApexTextBox)).BeginInit();
            this.LeftButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CSharpSplitContainer)).BeginInit();
            this.CSharpSplitContainer.Panel1.SuspendLayout();
            this.CSharpSplitContainer.Panel2.SuspendLayout();
            this.CSharpSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CSharpTextBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // SplitContainer
            //
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(8, 8);
            this.SplitContainer.Name = "SplitContainer";
            //
            // SplitContainer.Panel1
            //
            this.SplitContainer.Panel1.Controls.Add(this.ApexSplitContainer);
            this.SplitContainer.Panel1.Controls.Add(this.ApexLabel);
            this.SplitContainer.Panel1.Controls.Add(this.LeftButtonPanel);
            this.SplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(4);
            //
            // SplitContainer.Panel2
            //
            this.SplitContainer.Panel2.Controls.Add(this.CSharpSplitContainer);
            this.SplitContainer.Panel2.Controls.Add(this.panel1);
            this.SplitContainer.Panel2.Controls.Add(this.CSharpLabel);
            this.SplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.SplitContainer.Size = new System.Drawing.Size(953, 609);
            this.SplitContainer.SplitterDistance = 491;
            this.SplitContainer.TabIndex = 2;
            //
            // ApexSplitContainer
            //
            this.ApexSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApexSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ApexSplitContainer.Location = new System.Drawing.Point(4, 25);
            this.ApexSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.ApexSplitContainer.Name = "ApexSplitContainer";
            //
            // ApexSplitContainer.Panel1
            //
            this.ApexSplitContainer.Panel1.Controls.Add(this.ApexFilesBox);
            //
            // ApexSplitContainer.Panel2
            //
            this.ApexSplitContainer.Panel2.Controls.Add(this.ApexTextBox);
            this.ApexSplitContainer.Size = new System.Drawing.Size(483, 542);
            this.ApexSplitContainer.SplitterDistance = 170;
            this.ApexSplitContainer.SplitterWidth = 3;
            this.ApexSplitContainer.TabIndex = 8;
            //
            // ApexFilesBox
            //
            this.ApexFilesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApexFilesBox.FormattingEnabled = true;
            this.ApexFilesBox.Location = new System.Drawing.Point(0, 0);
            this.ApexFilesBox.Name = "ApexFilesBox";
            this.ApexFilesBox.Size = new System.Drawing.Size(170, 542);
            this.ApexFilesBox.TabIndex = 1;
            this.ApexFilesBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ApexFilesBox_DrawItem);
            this.ApexFilesBox.SelectedValueChanged += new System.EventHandler(this.ListBox_SelectedValueChanged);
            this.ApexFilesBox.Enter += new System.EventHandler(this.LeftBox_Enter);
            //
            // ApexTextBox
            //
            this.ApexTextBox.AutoCompleteBracketsList = new char[] {
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
            this.ApexTextBox.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.ApexTextBox.AutoIndentExistingLines = false;
            this.ApexTextBox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.ApexTextBox.BackBrush = null;
            this.ApexTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ApexTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.ApexTextBox.CharHeight = 14;
            this.ApexTextBox.CharWidth = 8;
            this.ApexTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ApexTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ApexTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApexTextBox.IsReplaceMode = false;
            this.ApexTextBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.ApexTextBox.LeftBracket = '(';
            this.ApexTextBox.LeftBracket2 = '{';
            this.ApexTextBox.Location = new System.Drawing.Point(0, 0);
            this.ApexTextBox.Name = "ApexTextBox";
            this.ApexTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.ApexTextBox.RightBracket = ')';
            this.ApexTextBox.RightBracket2 = '}';
            this.ApexTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ApexTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ApexTextBox.ServiceColors")));
            this.ApexTextBox.Size = new System.Drawing.Size(310, 542);
            this.ApexTextBox.TabIndex = 2;
            this.ApexTextBox.Zoom = 100;
            this.ApexTextBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ApexTextBox_TextChanged);
            this.ApexTextBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ApexTextBox_TextChangedDelayed);
            this.ApexTextBox.Enter += new System.EventHandler(this.LeftBox_Enter);
            //
            // ApexLabel
            //
            this.ApexLabel.AutoSize = true;
            this.ApexLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ApexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ApexLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.ApexLabel.Location = new System.Drawing.Point(4, 4);
            this.ApexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ApexLabel.Name = "ApexLabel";
            this.ApexLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.ApexLabel.Size = new System.Drawing.Size(39, 21);
            this.ApexLabel.TabIndex = 9;
            this.ApexLabel.Text = "Apex";
            this.ApexLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ApexLabel_LinkClicked);
            //
            // LeftButtonPanel
            //
            this.LeftButtonPanel.Controls.Add(this.SaveApexFileButton);
            this.LeftButtonPanel.Controls.Add(this.SaveAllApexFilesButton);
            this.LeftButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LeftButtonPanel.Location = new System.Drawing.Point(4, 567);
            this.LeftButtonPanel.Name = "LeftButtonPanel";
            this.LeftButtonPanel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LeftButtonPanel.Size = new System.Drawing.Size(483, 38);
            this.LeftButtonPanel.TabIndex = 10;
            //
            // SaveApexFileButton
            //
            this.SaveApexFileButton.AutoSize = true;
            this.SaveApexFileButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SaveApexFileButton.Location = new System.Drawing.Point(363, 4);
            this.SaveApexFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveApexFileButton.MaximumSize = new System.Drawing.Size(420, 30);
            this.SaveApexFileButton.Name = "SaveApexFileButton";
            this.SaveApexFileButton.Size = new System.Drawing.Size(120, 30);
            this.SaveApexFileButton.TabIndex = 8;
            this.SaveApexFileButton.Text = "Save Apex File";
            this.SaveApexFileButton.UseVisualStyleBackColor = true;
            this.SaveApexFileButton.Click += new System.EventHandler(this.SaveApexFileButton_Click);
            //
            // SaveAllApexFilesButton
            //
            this.SaveAllApexFilesButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SaveAllApexFilesButton.Location = new System.Drawing.Point(0, 4);
            this.SaveAllApexFilesButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAllApexFilesButton.MaximumSize = new System.Drawing.Size(120, 30);
            this.SaveAllApexFilesButton.Name = "SaveAllApexFilesButton";
            this.SaveAllApexFilesButton.Size = new System.Drawing.Size(120, 30);
            this.SaveAllApexFilesButton.TabIndex = 8;
            this.SaveAllApexFilesButton.Text = "Save All Apex Files";
            this.SaveAllApexFilesButton.UseVisualStyleBackColor = true;
            this.SaveAllApexFilesButton.Click += new System.EventHandler(this.SaveAllApexFilesButton_Click);
            //
            // CSharpSplitContainer
            //
            this.CSharpSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CSharpSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.CSharpSplitContainer.Location = new System.Drawing.Point(4, 25);
            this.CSharpSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.CSharpSplitContainer.Name = "CSharpSplitContainer";
            //
            // CSharpSplitContainer.Panel1
            //
            this.CSharpSplitContainer.Panel1.Controls.Add(this.CSharpFilesBox);
            //
            // CSharpSplitContainer.Panel2
            //
            this.CSharpSplitContainer.Panel2.Controls.Add(this.CSharpTextBox);
            this.CSharpSplitContainer.Size = new System.Drawing.Size(450, 542);
            this.CSharpSplitContainer.SplitterDistance = 157;
            this.CSharpSplitContainer.SplitterWidth = 3;
            this.CSharpSplitContainer.TabIndex = 12;
            //
            // CSharpFilesBox
            //
            this.CSharpFilesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CSharpFilesBox.FormattingEnabled = true;
            this.CSharpFilesBox.Location = new System.Drawing.Point(0, 0);
            this.CSharpFilesBox.Name = "CSharpFilesBox";
            this.CSharpFilesBox.Size = new System.Drawing.Size(157, 542);
            this.CSharpFilesBox.TabIndex = 0;
            this.CSharpFilesBox.SelectedValueChanged += new System.EventHandler(this.ListBox_SelectedValueChanged);
            this.CSharpFilesBox.Enter += new System.EventHandler(this.RightBox_Enter);
            //
            // CSharpTextBox
            //
            this.CSharpTextBox.AutoCompleteBracketsList = new char[] {
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
            this.CSharpTextBox.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.CSharpTextBox.AutoIndentExistingLines = false;
            this.CSharpTextBox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.CSharpTextBox.BackBrush = null;
            this.CSharpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CSharpTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.CSharpTextBox.CharHeight = 14;
            this.CSharpTextBox.CharWidth = 8;
            this.CSharpTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CSharpTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CSharpTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CSharpTextBox.IsReplaceMode = false;
            this.CSharpTextBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.CSharpTextBox.LeftBracket = '(';
            this.CSharpTextBox.LeftBracket2 = '{';
            this.CSharpTextBox.Location = new System.Drawing.Point(0, 0);
            this.CSharpTextBox.Name = "CSharpTextBox";
            this.CSharpTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.CSharpTextBox.RightBracket = ')';
            this.CSharpTextBox.RightBracket2 = '}';
            this.CSharpTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.CSharpTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("CSharpTextBox.ServiceColors")));
            this.CSharpTextBox.Size = new System.Drawing.Size(290, 542);
            this.CSharpTextBox.TabIndex = 1;
            this.CSharpTextBox.Zoom = 100;
            this.CSharpTextBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CSharpTextBox_TextChanged);
            this.CSharpTextBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CSharpTextBox_TextChangedDelayed);
            this.CSharpTextBox.Enter += new System.EventHandler(this.RightBox_Enter);
            //
            // panel1
            //
            this.panel1.Controls.Add(this.SaveCSharpFileButton);
            this.panel1.Controls.Add(this.SaveAllCSharpFilesButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 567);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panel1.Size = new System.Drawing.Size(450, 38);
            this.panel1.TabIndex = 11;
            //
            // SaveCSharpFileButton
            //
            this.SaveCSharpFileButton.AutoSize = true;
            this.SaveCSharpFileButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SaveCSharpFileButton.Location = new System.Drawing.Point(333, 4);
            this.SaveCSharpFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveCSharpFileButton.MaximumSize = new System.Drawing.Size(420, 30);
            this.SaveCSharpFileButton.Name = "SaveCSharpFileButton";
            this.SaveCSharpFileButton.Size = new System.Drawing.Size(117, 30);
            this.SaveCSharpFileButton.TabIndex = 8;
            this.SaveCSharpFileButton.Text = "Save C# File";
            this.SaveCSharpFileButton.UseVisualStyleBackColor = true;
            this.SaveCSharpFileButton.Click += new System.EventHandler(this.SaveCSharpFileButton_Click);
            //
            // SaveAllCSharpFilesButton
            //
            this.SaveAllCSharpFilesButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SaveAllCSharpFilesButton.Location = new System.Drawing.Point(0, 4);
            this.SaveAllCSharpFilesButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAllCSharpFilesButton.MaximumSize = new System.Drawing.Size(120, 30);
            this.SaveAllCSharpFilesButton.Name = "SaveAllCSharpFilesButton";
            this.SaveAllCSharpFilesButton.Size = new System.Drawing.Size(120, 30);
            this.SaveAllCSharpFilesButton.TabIndex = 8;
            this.SaveAllCSharpFilesButton.Text = "Save All C# Files";
            this.SaveAllCSharpFilesButton.UseVisualStyleBackColor = true;
            this.SaveAllCSharpFilesButton.Click += new System.EventHandler(this.SaveAllCSharpFilesButton_Click);
            //
            // CSharpLabel
            //
            this.CSharpLabel.AutoSize = true;
            this.CSharpLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CSharpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.CSharpLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.CSharpLabel.Location = new System.Drawing.Point(4, 4);
            this.CSharpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CSharpLabel.Name = "CSharpLabel";
            this.CSharpLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.CSharpLabel.Size = new System.Drawing.Size(25, 21);
            this.CSharpLabel.TabIndex = 10;
            this.CSharpLabel.Text = "C#";
            this.CSharpLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CSharpLabel_LinkClicked);
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
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SetupToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(969, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            //
            // FileToolStripMenuItem
            //
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveCurrentApexFileToolStripMenuItem,
            this.SaveAllApexFilesToolStripMenuItem,
            this.toolStripSeparator1,
            this.SaveCurrentCSharpFileToolStripMenuItem,
            this.SaveAllCSharpFilesToolStripMenuItem,
            this.toolStripSeparator2,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            //
            // SaveCurrentApexFileToolStripMenuItem
            //
            this.SaveCurrentApexFileToolStripMenuItem.Name = "SaveCurrentApexFileToolStripMenuItem";
            this.SaveCurrentApexFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.SaveCurrentApexFileToolStripMenuItem.Text = "Save current Apex file";
            this.SaveCurrentApexFileToolStripMenuItem.Click += new System.EventHandler(this.SaveApexFileButton_Click);
            //
            // SaveAllApexFilesToolStripMenuItem
            //
            this.SaveAllApexFilesToolStripMenuItem.Name = "SaveAllApexFilesToolStripMenuItem";
            this.SaveAllApexFilesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.SaveAllApexFilesToolStripMenuItem.Text = "Save all Apex files";
            this.SaveAllApexFilesToolStripMenuItem.Click += new System.EventHandler(this.SaveAllApexFilesButton_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            //
            // SaveCurrentCSharpFileToolStripMenuItem
            //
            this.SaveCurrentCSharpFileToolStripMenuItem.Name = "SaveCurrentCSharpFileToolStripMenuItem";
            this.SaveCurrentCSharpFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.SaveCurrentCSharpFileToolStripMenuItem.Text = "Save current C# file";
            this.SaveCurrentCSharpFileToolStripMenuItem.Click += new System.EventHandler(this.SaveCSharpFileButton_Click);
            //
            // SaveAllCSharpFilesToolStripMenuItem
            //
            this.SaveAllCSharpFilesToolStripMenuItem.Name = "SaveAllCSharpFilesToolStripMenuItem";
            this.SaveAllCSharpFilesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.SaveAllCSharpFilesToolStripMenuItem.Text = "Save all C# files";
            this.SaveAllCSharpFilesToolStripMenuItem.Click += new System.EventHandler(this.SaveAllCSharpFilesButton_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            //
            // ExitToolStripMenuItem
            //
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            //
            // SetupToolStripMenuItem
            //
            this.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem";
            this.SetupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.SetupToolStripMenuItem.Text = "Setup";
            this.SetupToolStripMenuItem.Click += new System.EventHandler(this.SetupButton_Click);
            //
            // AboutToolStripMenuItem
            //
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.AboutToolStripMenuItem.Text = "About";
            //
            // MainPanel
            //
            this.MainPanel.Controls.Add(this.SplitContainer);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(8);
            this.MainPanel.Size = new System.Drawing.Size(969, 625);
            this.MainPanel.TabIndex = 4;
            //
            // DemoForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 649);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DemoForm";
            this.Text = "ApexSharp UI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ApexSplitContainer.Panel1.ResumeLayout(false);
            this.ApexSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApexSplitContainer)).EndInit();
            this.ApexSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApexTextBox)).EndInit();
            this.LeftButtonPanel.ResumeLayout(false);
            this.LeftButtonPanel.PerformLayout();
            this.CSharpSplitContainer.Panel1.ResumeLayout(false);
            this.CSharpSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CSharpSplitContainer)).EndInit();
            this.CSharpSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CSharpTextBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.SplitContainer ApexSplitContainer;
        private System.Windows.Forms.LinkLabel ApexLabel;
        private System.Windows.Forms.Button SaveAllApexFilesButton;
        private System.Windows.Forms.Button SaveApexFileButton;
        private System.Windows.Forms.LinkLabel CSharpLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveCurrentApexFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAllApexFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SaveCurrentCSharpFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAllCSharpFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.Panel LeftButtonPanel;
        private System.Windows.Forms.SplitContainer CSharpSplitContainer;
        private System.Windows.Forms.ListBox CSharpFilesBox;
        private FastColoredTextBoxNS.FastColoredTextBox CSharpTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SaveCSharpFileButton;
        private System.Windows.Forms.Button SaveAllCSharpFilesButton;
        private System.Windows.Forms.ListBox ApexFilesBox;
        private FastColoredTextBoxNS.FastColoredTextBox ApexTextBox;
        private System.Windows.Forms.Panel MainPanel;
    }
}

