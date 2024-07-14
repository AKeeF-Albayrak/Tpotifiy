namespace Tpotifiy
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
            this.panelAlbums = new System.Windows.Forms.Panel();
            this.panelPlayer = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelAlbums
            // 
            this.panelAlbums.Location = new System.Drawing.Point(3, 1);
            this.panelAlbums.Name = "panelAlbums";
            this.panelAlbums.Size = new System.Drawing.Size(197, 550);
            this.panelAlbums.TabIndex = 0;
            // 
            // panelPlayer
            // 
            this.panelPlayer.Location = new System.Drawing.Point(3, 557);
            this.panelPlayer.Name = "panelPlayer";
            this.panelPlayer.Size = new System.Drawing.Size(1247, 85);
            this.panelPlayer.TabIndex = 1;
            // 
            // panelSearch
            // 
            this.panelSearch.Location = new System.Drawing.Point(198, 1);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(751, 44);
            this.panelSearch.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(948, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 44);
            this.panel3.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 643);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelPlayer);
            this.Controls.Add(this.panelAlbums);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAlbums;
        private System.Windows.Forms.Panel panelPlayer;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panel3;
    }
}