namespace AutomatedXMLTestFileGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtXsdFilePath = new TextBox();
            label1 = new Label();
            txtOutputTestFilePath = new TextBox();
            label3 = new Label();
            txtNumberOfFiles = new NumericUpDown();
            label2 = new Label();
            btnGenerateTestFiles = new Button();
            ((System.ComponentModel.ISupportInitialize)txtNumberOfFiles).BeginInit();
            SuspendLayout();
            // 
            // txtXsdFilePath
            // 
            txtXsdFilePath.Location = new Point(138, 15);
            txtXsdFilePath.Name = "txtXsdFilePath";
            txtXsdFilePath.Size = new Size(311, 31);
            txtXsdFilePath.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 1;
            label1.Text = "XSD File Path:";
            // 
            // txtOutputTestFilePath
            // 
            txtOutputTestFilePath.Location = new Point(138, 58);
            txtOutputTestFilePath.Name = "txtOutputTestFilePath";
            txtOutputTestFilePath.Size = new Size(311, 31);
            txtOutputTestFilePath.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 58);
            label3.Name = "label3";
            label3.Size = new Size(112, 25);
            label3.TabIndex = 2;
            label3.Text = "Output Path:";
            // 
            // txtNumberOfFiles
            // 
            txtNumberOfFiles.Location = new Point(138, 108);
            txtNumberOfFiles.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtNumberOfFiles.Name = "txtNumberOfFiles";
            txtNumberOfFiles.Size = new Size(180, 31);
            txtNumberOfFiles.TabIndex = 4;
            txtNumberOfFiles.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 108);
            label2.Name = "label2";
            label2.Size = new Size(105, 25);
            label2.TabIndex = 5;
            label2.Text = "No. of Files:";
            // 
            // btnGenerateTestFiles
            // 
            btnGenerateTestFiles.Location = new Point(163, 169);
            btnGenerateTestFiles.Name = "btnGenerateTestFiles";
            btnGenerateTestFiles.Size = new Size(183, 52);
            btnGenerateTestFiles.TabIndex = 6;
            btnGenerateTestFiles.Text = "Generate";
            btnGenerateTestFiles.UseVisualStyleBackColor = true;
            btnGenerateTestFiles.Click += btnGenerateTestFiles_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 233);
            Controls.Add(btnGenerateTestFiles);
            Controls.Add(label2);
            Controls.Add(txtNumberOfFiles);
            Controls.Add(label3);
            Controls.Add(txtOutputTestFilePath);
            Controls.Add(label1);
            Controls.Add(txtXsdFilePath);
            MaximizeBox = false;
            Name = "Form1";
            Text = "CyberFinn XML Generator";
            ((System.ComponentModel.ISupportInitialize)txtNumberOfFiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtXsdFilePath;
        private Label label1;
        private TextBox txtOutputTestFilePath;
        private Label label3;
        private NumericUpDown txtNumberOfFiles;
        private Label label2;
        private Button btnGenerateTestFiles;
    }
}
