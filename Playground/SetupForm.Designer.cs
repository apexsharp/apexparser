namespace Playground
{
    partial class SetupForm
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
            this.ApplyButton = new System.Windows.Forms.Button();
            this.ApexDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenApexDirectoryButton = new System.Windows.Forms.Button();
            this.OpenCSharpDirectoryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CSharpDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CSharpNamespaceTextBox = new System.Windows.Forms.TextBox();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            //
            // ApplyButton
            //
            this.ApplyButton.Location = new System.Drawing.Point(497, 356);
            this.ApplyButton.Margin = new System.Windows.Forms.Padding(2);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(110, 30);
            this.ApplyButton.TabIndex = 0;
            this.ApplyButton.Text = "Apply and Close";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            //
            // ApexDirectoryTextBox
            //
            this.ApexDirectoryTextBox.Location = new System.Drawing.Point(158, 52);
            this.ApexDirectoryTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ApexDirectoryTextBox.Name = "ApexDirectoryTextBox";
            this.ApexDirectoryTextBox.Size = new System.Drawing.Size(235, 20);
            this.ApexDirectoryTextBox.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "APEX Source Directory ";
            //
            // openFileDialog1
            //
            this.openFileDialog1.FileName = "openFileDialog1";
            //
            // OpenApexDirectoryButton
            //
            this.OpenApexDirectoryButton.Location = new System.Drawing.Point(408, 47);
            this.OpenApexDirectoryButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenApexDirectoryButton.Name = "OpenApexDirectoryButton";
            this.OpenApexDirectoryButton.Size = new System.Drawing.Size(93, 30);
            this.OpenApexDirectoryButton.TabIndex = 3;
            this.OpenApexDirectoryButton.Text = "Open Dir";
            this.OpenApexDirectoryButton.UseVisualStyleBackColor = true;
            this.OpenApexDirectoryButton.Click += new System.EventHandler(this.OpenApexDirectoryButton_Click);
            //
            // OpenCSharpDirectoryButton
            //
            this.OpenCSharpDirectoryButton.Location = new System.Drawing.Point(408, 101);
            this.OpenCSharpDirectoryButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenCSharpDirectoryButton.Name = "OpenCSharpDirectoryButton";
            this.OpenCSharpDirectoryButton.Size = new System.Drawing.Size(93, 30);
            this.OpenCSharpDirectoryButton.TabIndex = 6;
            this.OpenCSharpDirectoryButton.Text = "Open Dir";
            this.OpenCSharpDirectoryButton.UseVisualStyleBackColor = true;
            this.OpenCSharpDirectoryButton.Click += new System.EventHandler(this.OpenCSharpDirectoryButton_Click);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 109);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "C# Source Directory ";
            //
            // CSharpDirectoryTextBox
            //
            this.CSharpDirectoryTextBox.Location = new System.Drawing.Point(158, 106);
            this.CSharpDirectoryTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CSharpDirectoryTextBox.Name = "CSharpDirectoryTextBox";
            this.CSharpDirectoryTextBox.Size = new System.Drawing.Size(235, 20);
            this.CSharpDirectoryTextBox.TabIndex = 4;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 167);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "C# Name Space";
            //
            // CSharpNamespaceTextBox
            //
            this.CSharpNamespaceTextBox.Location = new System.Drawing.Point(158, 164);
            this.CSharpNamespaceTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CSharpNamespaceTextBox.Name = "CSharpNamespaceTextBox";
            this.CSharpNamespaceTextBox.Size = new System.Drawing.Size(235, 20);
            this.CSharpNamespaceTextBox.TabIndex = 7;
            //
            // SetupForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 404);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CSharpNamespaceTextBox);
            this.Controls.Add(this.OpenCSharpDirectoryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CSharpDirectoryTextBox);
            this.Controls.Add(this.OpenApexDirectoryButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApexDirectoryTextBox);
            this.Controls.Add(this.ApplyButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.TextBox ApexDirectoryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenApexDirectoryButton;
        private System.Windows.Forms.Button OpenCSharpDirectoryButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CSharpDirectoryTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CSharpNamespaceTextBox;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
    }
}