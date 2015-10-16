namespace Gmod_Dupe_Downloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnpnl = new System.Windows.Forms.Panel();
            this.downloadbtn = new System.Windows.Forms.Button();
            this.instructionslbl = new System.Windows.Forms.Label();
            this.txtbx = new System.Windows.Forms.TextBox();
            this.keepdeletedchkbx = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnpnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnpnl
            // 
            this.btnpnl.Controls.Add(this.downloadbtn);
            this.btnpnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnpnl.Location = new System.Drawing.Point(0, 132);
            this.btnpnl.Margin = new System.Windows.Forms.Padding(2);
            this.btnpnl.Name = "btnpnl";
            this.btnpnl.Size = new System.Drawing.Size(384, 22);
            this.btnpnl.TabIndex = 0;
            // 
            // downloadbtn
            // 
            this.downloadbtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.downloadbtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.downloadbtn.Location = new System.Drawing.Point(0, 0);
            this.downloadbtn.Margin = new System.Windows.Forms.Padding(2);
            this.downloadbtn.Name = "downloadbtn";
            this.downloadbtn.Size = new System.Drawing.Size(384, 22);
            this.downloadbtn.TabIndex = 0;
            this.downloadbtn.Text = "&Download Dupe";
            this.downloadbtn.UseVisualStyleBackColor = true;
            this.downloadbtn.Click += new System.EventHandler(this.downloadbtn_Click);
            // 
            // instructionslbl
            // 
            this.instructionslbl.AutoSize = true;
            this.instructionslbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionslbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.instructionslbl.Location = new System.Drawing.Point(43, 6);
            this.instructionslbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.instructionslbl.Name = "instructionslbl";
            this.instructionslbl.Size = new System.Drawing.Size(334, 13);
            this.instructionslbl.TabIndex = 2;
            this.instructionslbl.Text = "Paste a dupe\'s workshop ID or URL and click \"Download Dupe.\"";
            // 
            // txtbx
            // 
            this.txtbx.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbx.ForeColor = System.Drawing.Color.DimGray;
            this.txtbx.Location = new System.Drawing.Point(6, 27);
            this.txtbx.Margin = new System.Windows.Forms.Padding(2);
            this.txtbx.Name = "txtbx";
            this.txtbx.Size = new System.Drawing.Size(371, 22);
            this.txtbx.TabIndex = 3;
            this.txtbx.Text = "E.G. \"https://steamcommunity.com/sharedfiles/filedetails/?id=517308353\"";
            this.txtbx.Click += new System.EventHandler(this.txtbx_Click);
            this.txtbx.TextChanged += new System.EventHandler(this.txtbx_TextChanged);
            // 
            // keepdeletedchkbx
            // 
            this.keepdeletedchkbx.AutoSize = true;
            this.keepdeletedchkbx.Checked = true;
            this.keepdeletedchkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepdeletedchkbx.Location = new System.Drawing.Point(4, 111);
            this.keepdeletedchkbx.Margin = new System.Windows.Forms.Padding(2);
            this.keepdeletedchkbx.Name = "keepdeletedchkbx";
            this.keepdeletedchkbx.Size = new System.Drawing.Size(376, 17);
            this.keepdeletedchkbx.TabIndex = 4;
            this.keepdeletedchkbx.Text = "Remove un-needed, temporary files after dupe is installed (Recommended)";
            this.keepdeletedchkbx.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DimGray;
            this.textBox1.Location = new System.Drawing.Point(6, 75);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(371, 22);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\GarrysMod\\garrysmod\\dupes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Garry\'s Mod folder";
            // 
            // MainForm
            // 
            this.AcceptButton = this.downloadbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 154);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.keepdeletedchkbx);
            this.Controls.Add(this.txtbx);
            this.Controls.Add(this.instructionslbl);
            this.Controls.Add(this.btnpnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "GMod Dupe Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.btnpnl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel btnpnl;
        private System.Windows.Forms.Button downloadbtn;
        private System.Windows.Forms.Label instructionslbl;
        private System.Windows.Forms.TextBox txtbx;
        private System.Windows.Forms.CheckBox keepdeletedchkbx;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

