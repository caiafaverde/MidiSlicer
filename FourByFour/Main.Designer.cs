namespace FourByFour
{
	partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.BeatsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SaveMidiFile = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.TempoUpDown = new FourByFour.NumericUpDownToolStripItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.BarsUpDown = new FourByFour.NumericUpDownToolStripItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.StepsUpDown = new FourByFour.NumericUpDownToolStripItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.PatternComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.PlayButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.OutputComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BeatsPanel
            // 
            this.BeatsPanel.AutoScroll = true;
            this.BeatsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BeatsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.BeatsPanel.Location = new System.Drawing.Point(0, 26);
            this.BeatsPanel.Name = "BeatsPanel";
            this.BeatsPanel.Size = new System.Drawing.Size(750, 284);
            this.BeatsPanel.TabIndex = 2;
            // 
            // SaveMidiFile
            // 
            this.SaveMidiFile.Filter = "MIDI files|*.mid|All files|*.*";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.TempoUpDown,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.BarsUpDown,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.StepsUpDown,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.PatternComboBox,
            this.toolStripSeparator4,
            this.toolStripButton1,
            this.PlayButton,
            this.toolStripButton3,
            this.OutputComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(750, 26);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 23);
            this.toolStripLabel1.Text = "Tempo";
            // 
            // TempoUpDown
            // 
            this.TempoUpDown.Name = "TempoUpDown";
            this.TempoUpDown.Size = new System.Drawing.Size(41, 23);
            this.TempoUpDown.Text = "100";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(29, 23);
            this.toolStripLabel2.Text = "Bars";
            // 
            // BarsUpDown
            // 
            this.BarsUpDown.Name = "BarsUpDown";
            this.BarsUpDown.Size = new System.Drawing.Size(41, 23);
            this.BarsUpDown.Text = "1";
            this.BarsUpDown.ValueChanged += new System.EventHandler(this.BarsUpDown_ValueChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(35, 23);
            this.toolStripLabel3.Text = "Steps";
            // 
            // StepsUpDown
            // 
            this.StepsUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StepsUpDown.Name = "StepsUpDown";
            this.StepsUpDown.Size = new System.Drawing.Size(35, 23);
            this.StepsUpDown.Text = "16";
            this.StepsUpDown.ValueChanged += new System.EventHandler(this.StepsUpDown_ValueChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(45, 23);
            this.toolStripLabel4.Text = "Pattern";
            // 
            // PatternComboBox
            // 
            this.PatternComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PatternComboBox.Items.AddRange(new object[] {
            "(None)",
            "Basic Empty",
            "Break",
            "House"});
            this.PatternComboBox.Name = "PatternComboBox";
            this.PatternComboBox.Size = new System.Drawing.Size(121, 26);
            this.PatternComboBox.SelectedIndexChanged += new System.EventHandler(this.PatternComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 23);
            this.toolStripButton1.Text = "Add";
            this.toolStripButton1.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlayButton.Image = ((System.Drawing.Image)(resources.GetObject("PlayButton.Image")));
            this.PlayButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(33, 23);
            this.PlayButton.Text = "Play";
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(58, 23);
            this.toolStripButton3.Text = "Save as...";
            this.toolStripButton3.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // OutputComboBox
            // 
            this.OutputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputComboBox.Name = "OutputComboBox";
            this.OutputComboBox.Size = new System.Drawing.Size(140, 26);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 310);
            this.Controls.Add(this.BeatsPanel);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(631, 140);
            this.Name = "Main";
            this.Text = "Ex-4x4 Beats";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void ToolStripDropDownButton2_ValueChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
		private System.Windows.Forms.FlowLayoutPanel BeatsPanel;
		private System.Windows.Forms.SaveFileDialog SaveMidiFile;
		//private System.Windows.Forms.ComboBox PatternComboBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private FourByFour.NumericUpDownToolStripItem TempoUpDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private FourByFour.NumericUpDownToolStripItem BarsUpDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private FourByFour.NumericUpDownToolStripItem StepsUpDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox PatternComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton PlayButton;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripComboBox OutputComboBox;
    }
}

