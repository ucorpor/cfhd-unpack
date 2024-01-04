namespace control_unpack
{
    partial class MainForm
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.rmdpPage = new System.Windows.Forms.TabPage();
            this.rmdpProgressLbl = new System.Windows.Forms.Label();
            this.rmdpPathBtn = new System.Windows.Forms.Button();
            this.rmdpPathTxt = new System.Windows.Forms.TextBox();
            this.rmdpUnpackBtn = new System.Windows.Forms.Button();
            this.stringPage = new System.Windows.Forms.TabPage();
            this.stringsPackBtn = new System.Windows.Forms.Button();
            this.stringPathBtn = new System.Windows.Forms.Button();
            this.stringPathTxt = new System.Windows.Forms.TextBox();
            this.stringUnpackBtn = new System.Windows.Forms.Button();
            this.txt2xlsxBtn = new System.Windows.Forms.Button();
            this.xlsx2txtBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.rmdpPage.SuspendLayout();
            this.stringPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(181, 165);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(201, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/ucorpor/cfhd-unpack";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.rmdpPage);
            this.tabControl1.Controls.Add(this.stringPage);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(375, 150);
            this.tabControl1.TabIndex = 5;
            // 
            // rmdpPage
            // 
            this.rmdpPage.Controls.Add(this.rmdpProgressLbl);
            this.rmdpPage.Controls.Add(this.rmdpPathBtn);
            this.rmdpPage.Controls.Add(this.rmdpPathTxt);
            this.rmdpPage.Controls.Add(this.rmdpUnpackBtn);
            this.rmdpPage.Location = new System.Drawing.Point(4, 22);
            this.rmdpPage.Name = "rmdpPage";
            this.rmdpPage.Padding = new System.Windows.Forms.Padding(3);
            this.rmdpPage.Size = new System.Drawing.Size(367, 124);
            this.rmdpPage.TabIndex = 0;
            this.rmdpPage.Text = "rmdp";
            this.rmdpPage.UseVisualStyleBackColor = true;
            // 
            // rmdpProgressLbl
            // 
            this.rmdpProgressLbl.AutoSize = true;
            this.rmdpProgressLbl.Location = new System.Drawing.Point(6, 37);
            this.rmdpProgressLbl.Name = "rmdpProgressLbl";
            this.rmdpProgressLbl.Size = new System.Drawing.Size(85, 13);
            this.rmdpProgressLbl.TabIndex = 6;
            this.rmdpProgressLbl.Text = "rmdpProgressLbl";
            // 
            // rmdpPathBtn
            // 
            this.rmdpPathBtn.Location = new System.Drawing.Point(262, 6);
            this.rmdpPathBtn.Name = "rmdpPathBtn";
            this.rmdpPathBtn.Size = new System.Drawing.Size(96, 23);
            this.rmdpPathBtn.TabIndex = 3;
            this.rmdpPathBtn.Text = "Choose file...";
            this.rmdpPathBtn.UseVisualStyleBackColor = true;
            this.rmdpPathBtn.Click += new System.EventHandler(this.rmdpPathBtn_Click);
            // 
            // rmdpPathTxt
            // 
            this.rmdpPathTxt.Location = new System.Drawing.Point(6, 6);
            this.rmdpPathTxt.Name = "rmdpPathTxt";
            this.rmdpPathTxt.Size = new System.Drawing.Size(250, 20);
            this.rmdpPathTxt.TabIndex = 4;
            // 
            // rmdpUnpackBtn
            // 
            this.rmdpUnpackBtn.Location = new System.Drawing.Point(171, 32);
            this.rmdpUnpackBtn.Name = "rmdpUnpackBtn";
            this.rmdpUnpackBtn.Size = new System.Drawing.Size(187, 23);
            this.rmdpUnpackBtn.TabIndex = 5;
            this.rmdpUnpackBtn.Text = "Unpack rmdp";
            this.rmdpUnpackBtn.UseVisualStyleBackColor = true;
            this.rmdpUnpackBtn.Click += new System.EventHandler(this.rmdpUnpackBtn_Click);
            // 
            // stringPage
            // 
            this.stringPage.Controls.Add(this.xlsx2txtBtn);
            this.stringPage.Controls.Add(this.txt2xlsxBtn);
            this.stringPage.Controls.Add(this.stringPathBtn);
            this.stringPage.Controls.Add(this.stringPathTxt);
            this.stringPage.Controls.Add(this.stringUnpackBtn);
            this.stringPage.Controls.Add(this.stringsPackBtn);
            this.stringPage.Location = new System.Drawing.Point(4, 22);
            this.stringPage.Name = "stringPage";
            this.stringPage.Padding = new System.Windows.Forms.Padding(3);
            this.stringPage.Size = new System.Drawing.Size(367, 124);
            this.stringPage.TabIndex = 1;
            this.stringPage.Text = "string_table.bin";
            this.stringPage.UseVisualStyleBackColor = true;
            // 
            // stringsPackBtn
            // 
            this.stringsPackBtn.Location = new System.Drawing.Point(6, 92);
            this.stringsPackBtn.Name = "stringsPackBtn";
            this.stringsPackBtn.Size = new System.Drawing.Size(355, 26);
            this.stringsPackBtn.TabIndex = 0;
            this.stringsPackBtn.Text = "Create string_table.bin...";
            this.stringsPackBtn.UseVisualStyleBackColor = true;
            this.stringsPackBtn.Click += new System.EventHandler(this.stringsPackBtn_Click);
            // 
            // stringPathBtn
            // 
            this.stringPathBtn.Location = new System.Drawing.Point(265, 6);
            this.stringPathBtn.Name = "stringPathBtn";
            this.stringPathBtn.Size = new System.Drawing.Size(96, 23);
            this.stringPathBtn.TabIndex = 3;
            this.stringPathBtn.Text = "Choose file...";
            this.stringPathBtn.UseVisualStyleBackColor = true;
            // 
            // stringPathTxt
            // 
            this.stringPathTxt.Location = new System.Drawing.Point(6, 6);
            this.stringPathTxt.Name = "stringPathTxt";
            this.stringPathTxt.Size = new System.Drawing.Size(253, 20);
            this.stringPathTxt.TabIndex = 4;
            // 
            // stringUnpackBtn
            // 
            this.stringUnpackBtn.Location = new System.Drawing.Point(6, 32);
            this.stringUnpackBtn.Name = "stringUnpackBtn";
            this.stringUnpackBtn.Size = new System.Drawing.Size(355, 23);
            this.stringUnpackBtn.TabIndex = 5;
            this.stringUnpackBtn.Text = "Unpack string_table.bin";
            this.stringUnpackBtn.UseVisualStyleBackColor = true;
            // 
            // txt2xlsxBtn
            // 
            this.txt2xlsxBtn.Location = new System.Drawing.Point(6, 61);
            this.txt2xlsxBtn.Name = "txt2xlsxBtn";
            this.txt2xlsxBtn.Size = new System.Drawing.Size(175, 23);
            this.txt2xlsxBtn.TabIndex = 6;
            this.txt2xlsxBtn.Text = "txt to xlsx";
            this.txt2xlsxBtn.UseVisualStyleBackColor = true;
            this.txt2xlsxBtn.Click += new System.EventHandler(this.txt2xlsxBtn_Click);
            // 
            // xlsx2txtBtn
            // 
            this.xlsx2txtBtn.Location = new System.Drawing.Point(186, 61);
            this.xlsx2txtBtn.Name = "xlsx2txtBtn";
            this.xlsx2txtBtn.Size = new System.Drawing.Size(175, 23);
            this.xlsx2txtBtn.TabIndex = 7;
            this.xlsx2txtBtn.Text = "xlsx to txt";
            this.xlsx2txtBtn.UseVisualStyleBackColor = true;
            this.xlsx2txtBtn.Click += new System.EventHandler(this.xlsx2txtBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 187);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cfhd-unpack";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.rmdpPage.ResumeLayout(false);
            this.rmdpPage.PerformLayout();
            this.stringPage.ResumeLayout(false);
            this.stringPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage rmdpPage;
        private System.Windows.Forms.TabPage stringPage;
        private System.Windows.Forms.Button stringsPackBtn;
        private System.Windows.Forms.Button rmdpPathBtn;
        private System.Windows.Forms.TextBox rmdpPathTxt;
        private System.Windows.Forms.Button rmdpUnpackBtn;
        public System.Windows.Forms.Label rmdpProgressLbl;
        private System.Windows.Forms.Button stringPathBtn;
        private System.Windows.Forms.TextBox stringPathTxt;
        private System.Windows.Forms.Button stringUnpackBtn;
        private System.Windows.Forms.Button txt2xlsxBtn;
        private System.Windows.Forms.Button xlsx2txtBtn;
    }
}

