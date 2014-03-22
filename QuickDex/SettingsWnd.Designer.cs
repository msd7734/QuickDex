namespace QuickDex
{
    partial class SettingsWnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWnd));
            this.defaultSrcSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defaultGenSelect = new System.Windows.Forms.ComboBox();
            this.applyBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shortcutSelect = new System.Windows.Forms.ComboBox();
            this.shortcutInfoLbl = new System.Windows.Forms.Label();
            this.shortcutLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultSrcSelect
            // 
            this.defaultSrcSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultSrcSelect.FormattingEnabled = true;
            this.defaultSrcSelect.Location = new System.Drawing.Point(32, 40);
            this.defaultSrcSelect.Name = "defaultSrcSelect";
            this.defaultSrcSelect.Size = new System.Drawing.Size(121, 21);
            this.defaultSrcSelect.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default search source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Default generation:";
            // 
            // defaultGenSelect
            // 
            this.defaultGenSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultGenSelect.FormattingEnabled = true;
            this.defaultGenSelect.Location = new System.Drawing.Point(186, 40);
            this.defaultGenSelect.Name = "defaultGenSelect";
            this.defaultGenSelect.Size = new System.Drawing.Size(121, 21);
            this.defaultGenSelect.TabIndex = 3;
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(187, 219);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 4;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(277, 219);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.defaultSrcSelect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.defaultGenSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 80);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.shortcutLbl);
            this.groupBox2.Controls.Add(this.shortcutInfoLbl);
            this.groupBox2.Controls.Add(this.shortcutSelect);
            this.groupBox2.Location = new System.Drawing.Point(13, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 117);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Options";
            // 
            // shortcutSelect
            // 
            this.shortcutSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutSelect.FormattingEnabled = true;
            this.shortcutSelect.Location = new System.Drawing.Point(6, 83);
            this.shortcutSelect.Name = "shortcutSelect";
            this.shortcutSelect.Size = new System.Drawing.Size(121, 21);
            this.shortcutSelect.TabIndex = 4;
            // 
            // shortcutInfoLbl
            // 
            this.shortcutInfoLbl.AutoSize = true;
            this.shortcutInfoLbl.Location = new System.Drawing.Point(7, 19);
            this.shortcutInfoLbl.Name = "shortcutInfoLbl";
            this.shortcutInfoLbl.Size = new System.Drawing.Size(296, 39);
            this.shortcutInfoLbl.TabIndex = 5;
            this.shortcutInfoLbl.Text = "Because custom shortcut creation will take time to implement,\r\nthis is a temporar" +
    "y solution for those who can\'t get the Win+Q\r\nshortcut to work.\r\n";
            // 
            // shortcutLbl
            // 
            this.shortcutLbl.AutoSize = true;
            this.shortcutLbl.Location = new System.Drawing.Point(7, 67);
            this.shortcutLbl.Name = "shortcutLbl";
            this.shortcutLbl.Size = new System.Drawing.Size(85, 13);
            this.shortcutLbl.TabIndex = 6;
            this.shortcutLbl.Text = "Search shortcut:";
            // 
            // SettingsWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 254);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.applyBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWnd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickDex Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox defaultSrcSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox defaultGenSelect;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label shortcutInfoLbl;
        private System.Windows.Forms.ComboBox shortcutSelect;
        private System.Windows.Forms.Label shortcutLbl;
    }
}