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
            this.searchBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "QuickDex";
            this.notifyIcon.Visible = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchBtn);
            this.groupBox1.Controls.Add(this.searchBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search by Pokémon name or National Dex number";
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
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(412, 131);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Location = new System.Drawing.Point(305, 19);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(97, 20);
            this.searchBtn.TabIndex = 1;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(439, 212);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "QuickDex";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

