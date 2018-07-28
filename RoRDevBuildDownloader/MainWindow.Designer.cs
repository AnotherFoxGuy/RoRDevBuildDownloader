namespace RoRDevBuildDownloader
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
			this.LogWindow = new System.Windows.Forms.TextBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LogWindow
			// 
			this.LogWindow.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.LogWindow.Location = new System.Drawing.Point(12, 12);
			this.LogWindow.Multiline = true;
			this.LogWindow.Name = "LogWindow";
			this.LogWindow.ReadOnly = true;
			this.LogWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.LogWindow.Size = new System.Drawing.Size(560, 301);
			this.LogWindow.TabIndex = 0;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 319);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(460, 30);
			this.progressBar.TabIndex = 1;
			// 
			// UpdateButton
			// 
			this.UpdateButton.Enabled = false;
			this.UpdateButton.Location = new System.Drawing.Point(482, 319);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(90, 30);
			this.UpdateButton.TabIndex = 3;
			this.UpdateButton.Text = "Update";
			this.UpdateButton.UseVisualStyleBackColor = true;
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.UpdateButton);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.LogWindow);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "RoR dev build downloader";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox LogWindow;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button UpdateButton;
	}
}

