namespace FourByFour
{
	partial class BeatControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.Instrument = new System.Windows.Forms.ComboBox();
            this.MenuButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intervalLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intervalRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StepControl = new FourByFour.StepControl();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Instrument
            // 
            this.Instrument.DisplayMember = "Key";
            this.Instrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Instrument.FormattingEnabled = true;
            this.Instrument.Location = new System.Drawing.Point(28, 6);
            this.Instrument.Name = "Instrument";
            this.Instrument.Size = new System.Drawing.Size(127, 21);
            this.Instrument.TabIndex = 0;
            // 
            // MenuButton
            // 
            this.MenuButton.Location = new System.Drawing.Point(3, 5);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Size = new System.Drawing.Size(19, 23);
            this.MenuButton.TabIndex = 2;
            this.MenuButton.Text = "M";
            this.MenuButton.UseVisualStyleBackColor = true;
            this.MenuButton.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.shiftToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 186);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // shiftToolStripMenuItem
            // 
            this.shiftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepLeftToolStripMenuItem,
            this.stepRightToolStripMenuItem,
            this.intervalLeftToolStripMenuItem,
            this.intervalRightToolStripMenuItem});
            this.shiftToolStripMenuItem.Name = "shiftToolStripMenuItem";
            this.shiftToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.shiftToolStripMenuItem.Text = "Shift";
            // 
            // stepLeftToolStripMenuItem
            // 
            this.stepLeftToolStripMenuItem.Name = "stepLeftToolStripMenuItem";
            this.stepLeftToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stepLeftToolStripMenuItem.Text = "1 step left";
            this.stepLeftToolStripMenuItem.Click += new System.EventHandler(this.stepLeftToolStripMenuItem_Click);
            // 
            // stepRightToolStripMenuItem
            // 
            this.stepRightToolStripMenuItem.Name = "stepRightToolStripMenuItem";
            this.stepRightToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stepRightToolStripMenuItem.Text = "1 step right";
            this.stepRightToolStripMenuItem.Click += new System.EventHandler(this.stepRightToolStripMenuItem_Click);
            // 
            // intervalLeftToolStripMenuItem
            // 
            this.intervalLeftToolStripMenuItem.Name = "intervalLeftToolStripMenuItem";
            this.intervalLeftToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.intervalLeftToolStripMenuItem.Text = "1 interval left";
            this.intervalLeftToolStripMenuItem.Click += new System.EventHandler(this.intervalLeftToolStripMenuItem_Click);
            // 
            // intervalRightToolStripMenuItem
            // 
            this.intervalRightToolStripMenuItem.Name = "intervalRightToolStripMenuItem";
            this.intervalRightToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.intervalRightToolStripMenuItem.Text = "1 interval right";
            this.intervalRightToolStripMenuItem.Click += new System.EventHandler(this.intervalRightToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // StepControl
            // 
            this.StepControl.AutoSize = true;
            this.StepControl.Bars = 1;
            this.StepControl.Location = new System.Drawing.Point(161, 4);
            this.StepControl.MinimumSize = new System.Drawing.Size(272, 16);
            this.StepControl.Name = "StepControl";
            this.StepControl.Size = new System.Drawing.Size(272, 29);
            this.StepControl.StepCount = 16;
            this.StepControl.TabIndex = 1;
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.channelToolStripMenuItem.Text = "Channel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // BeatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.MenuButton);
            this.Controls.Add(this.StepControl);
            this.Controls.Add(this.Instrument);
            this.MinimumSize = new System.Drawing.Size(2, 36);
            this.Name = "BeatControl";
            this.Size = new System.Drawing.Size(436, 36);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox Instrument;
		private StepControl StepControl;
		private System.Windows.Forms.Button MenuButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intervalLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intervalRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
