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
            this.stringPathBtn = new System.Windows.Forms.Button();
            this.stringPathTxt = new System.Windows.Forms.TextBox();
            this.stringUnpackBtn = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.unpackPage = new System.Windows.Forms.TabPage();
            this.rmdpPathBtn = new System.Windows.Forms.Button();
            this.rmdpPathTxt = new System.Windows.Forms.TextBox();
            this.rmdpUnpackBtn = new System.Windows.Forms.Button();
            this.repackPage = new System.Windows.Forms.TabPage();
            this.stringsRepackBtn = new System.Windows.Forms.Button();
            this.rmdpProgressLbl = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.unpackPage.SuspendLayout();
            this.repackPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // stringPathBtn
            // 
            this.stringPathBtn.Location = new System.Drawing.Point(262, 70);
            this.stringPathBtn.Name = "stringPathBtn";
            this.stringPathBtn.Size = new System.Drawing.Size(96, 23);
            this.stringPathBtn.TabIndex = 0;
            this.stringPathBtn.Text = "Choose file...";
            this.stringPathBtn.UseVisualStyleBackColor = true;
            this.stringPathBtn.Click += new System.EventHandler(this.stringPathBtn_Click);
            // 
            // stringPathTxt
            // 
            this.stringPathTxt.Location = new System.Drawing.Point(6, 70);
            this.stringPathTxt.Name = "stringPathTxt";
            this.stringPathTxt.Size = new System.Drawing.Size(250, 20);
            this.stringPathTxt.TabIndex = 1;
            // 
            // stringUnpackBtn
            // 
            this.stringUnpackBtn.Location = new System.Drawing.Point(171, 96);
            this.stringUnpackBtn.Name = "stringUnpackBtn";
            this.stringUnpackBtn.Size = new System.Drawing.Size(187, 23);
            this.stringUnpackBtn.TabIndex = 2;
            this.stringUnpackBtn.Text = "Unpack string_table.bin";
            this.stringUnpackBtn.UseVisualStyleBackColor = true;
            this.stringUnpackBtn.Click += new System.EventHandler(this.stringUnpackBtn_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(170, 165);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(212, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/ucorpor/control-unpack";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.unpackPage);
            this.tabControl1.Controls.Add(this.repackPage);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(375, 150);
            this.tabControl1.TabIndex = 5;
            // 
            // unpackPage
            // 
            this.unpackPage.Controls.Add(this.rmdpProgressLbl);
            this.unpackPage.Controls.Add(this.rmdpPathBtn);
            this.unpackPage.Controls.Add(this.rmdpPathTxt);
            this.unpackPage.Controls.Add(this.rmdpUnpackBtn);
            this.unpackPage.Controls.Add(this.stringPathBtn);
            this.unpackPage.Controls.Add(this.stringPathTxt);
            this.unpackPage.Controls.Add(this.stringUnpackBtn);
            this.unpackPage.Location = new System.Drawing.Point(4, 22);
            this.unpackPage.Name = "unpackPage";
            this.unpackPage.Padding = new System.Windows.Forms.Padding(3);
            this.unpackPage.Size = new System.Drawing.Size(367, 124);
            this.unpackPage.TabIndex = 0;
            this.unpackPage.Text = "Unpack";
            this.unpackPage.UseVisualStyleBackColor = true;
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
            // repackPage
            // 
            this.repackPage.Controls.Add(this.stringsRepackBtn);
            this.repackPage.Location = new System.Drawing.Point(4, 22);
            this.repackPage.Name = "repackPage";
            this.repackPage.Padding = new System.Windows.Forms.Padding(3);
            this.repackPage.Size = new System.Drawing.Size(367, 124);
            this.repackPage.TabIndex = 1;
            this.repackPage.Text = "Repack";
            this.repackPage.UseVisualStyleBackColor = true;
            // 
            // stringsRepackBtn
            // 
            this.stringsRepackBtn.Location = new System.Drawing.Point(6, 6);
            this.stringsRepackBtn.Name = "stringsRepackBtn";
            this.stringsRepackBtn.Size = new System.Drawing.Size(355, 113);
            this.stringsRepackBtn.TabIndex = 0;
            this.stringsRepackBtn.Text = "Repack string_table.bin...";
            this.stringsRepackBtn.UseVisualStyleBackColor = true;
            this.stringsRepackBtn.Click += new System.EventHandler(this.stringsRepackBtn_Click);
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
            this.Text = "control-unpack";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.unpackPage.ResumeLayout(false);
            this.unpackPage.PerformLayout();
            this.repackPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stringPathBtn;
        private System.Windows.Forms.TextBox stringPathTxt;
        private System.Windows.Forms.Button stringUnpackBtn;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage unpackPage;
        private System.Windows.Forms.TabPage repackPage;
        private System.Windows.Forms.Button stringsRepackBtn;
        private System.Windows.Forms.Button rmdpPathBtn;
        private System.Windows.Forms.TextBox rmdpPathTxt;
        private System.Windows.Forms.Button rmdpUnpackBtn;
        public System.Windows.Forms.Label rmdpProgressLbl;
    }
}

