namespace QuickDex
{
    partial class MainWnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.settingsLink = new System.Windows.Forms.LinkLabel();
            this.genSelect = new System.Windows.Forms.ComboBox();
            this.generationLbl = new System.Windows.Forms.Label();
            this.searchSourceLbl = new System.Windows.Forms.Label();
            this.searchSrcSelect = new System.Windows.Forms.ComboBox();
            this.msgDisplay = new System.Windows.Forms.RichTextBox();
            this.resultsLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "QuickDex";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchBtn);
            this.groupBox1.Controls.Add(this.searchBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search by Pokémon name or National Dex number";
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Location = new System.Drawing.Point(305, 19);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(97, 20);
            this.searchBtn.TabIndex = 3;
            this.searchBtn.TabStop = false;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(6, 19);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(285, 20);
            this.searchBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.settingsLink);
            this.groupBox2.Controls.Add(this.genSelect);
            this.groupBox2.Controls.Add(this.generationLbl);
            this.groupBox2.Controls.Add(this.searchSourceLbl);
            this.groupBox2.Controls.Add(this.searchSrcSelect);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(139, 131);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // settingsLink
            // 
            this.settingsLink.AutoSize = true;
            this.settingsLink.Location = new System.Drawing.Point(6, 107);
            this.settingsLink.Name = "settingsLink";
            this.settingsLink.Size = new System.Drawing.Size(95, 13);
            this.settingsLink.TabIndex = 3;
            this.settingsLink.Text = "QuickDex Settings";
            this.settingsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.settingsLink_LinkClicked);
            // 
            // genSelect
            // 
            this.genSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genSelect.FormattingEnabled = true;
            this.genSelect.Location = new System.Drawing.Point(5, 77);
            this.genSelect.Name = "genSelect";
            this.genSelect.Size = new System.Drawing.Size(121, 21);
            this.genSelect.TabIndex = 2;
            // 
            // generationLbl
            // 
            this.generationLbl.AutoSize = true;
            this.generationLbl.Location = new System.Drawing.Point(6, 63);
            this.generationLbl.Name = "generationLbl";
            this.generationLbl.Size = new System.Drawing.Size(62, 13);
            this.generationLbl.TabIndex = 2;
            this.generationLbl.Text = "Generation:";
            // 
            // searchSourceLbl
            // 
            this.searchSourceLbl.AutoSize = true;
            this.searchSourceLbl.Location = new System.Drawing.Point(5, 18);
            this.searchSourceLbl.Name = "searchSourceLbl";
            this.searchSourceLbl.Size = new System.Drawing.Size(81, 13);
            this.searchSourceLbl.TabIndex = 1;
            this.searchSourceLbl.Text = "Search Source:";
            // 
            // searchSrcSelect
            // 
            this.searchSrcSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchSrcSelect.FormattingEnabled = true;
            this.searchSrcSelect.Location = new System.Drawing.Point(6, 32);
            this.searchSrcSelect.Name = "searchSrcSelect";
            this.searchSrcSelect.Size = new System.Drawing.Size(121, 21);
            this.searchSrcSelect.TabIndex = 1;
            // 
            // msgDisplay
            // 
            this.msgDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.msgDisplay.CausesValidation = false;
            this.msgDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.msgDisplay.Location = new System.Drawing.Point(159, 85);
            this.msgDisplay.Name = "msgDisplay";
            this.msgDisplay.ReadOnly = true;
            this.msgDisplay.ShortcutsEnabled = false;
            this.msgDisplay.Size = new System.Drawing.Size(256, 120);
            this.msgDisplay.TabIndex = 99;
            this.msgDisplay.TabStop = false;
            this.msgDisplay.Text = "";
            this.msgDisplay.WordWrap = false;
            // 
            // resultsLbl
            // 
            this.resultsLbl.AutoSize = true;
            this.resultsLbl.Location = new System.Drawing.Point(159, 69);
            this.resultsLbl.Name = "resultsLbl";
            this.resultsLbl.Size = new System.Drawing.Size(42, 13);
            this.resultsLbl.TabIndex = 2;
            this.resultsLbl.Text = "Results";
            // 
            // MainWnd
            // 
            this.AcceptButton = this.searchBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(439, 212);
            this.Controls.Add(this.resultsLbl);
            this.Controls.Add(this.msgDisplay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "QuickDex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWnd_FormClosing);
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWnd_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox searchSrcSelect;
        private System.Windows.Forms.RichTextBox msgDisplay;
        private System.Windows.Forms.Label resultsLbl;
        private System.Windows.Forms.Label searchSourceLbl;
        private System.Windows.Forms.Label generationLbl;
        private System.Windows.Forms.ComboBox genSelect;
        private System.Windows.Forms.LinkLabel settingsLink;
    }
}

