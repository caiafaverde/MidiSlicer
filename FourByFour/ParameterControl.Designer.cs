
namespace FourByFour
{
    partial class ParameterControl
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
            this.Tracks = new System.Windows.Forms.ComboBox();
            this.MenuButton = new System.Windows.Forms.Button();
            this.StepControl = new FourByFour.ParameterStepControl();
            this.Parameters = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Tracks
            // 
            this.Tracks.DisplayMember = "Key";
            this.Tracks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tracks.FormattingEnabled = true;
            this.Tracks.Location = new System.Drawing.Point(28, 6);
            this.Tracks.Name = "Tracks";
            this.Tracks.Size = new System.Drawing.Size(127, 21);
            this.Tracks.TabIndex = 0;
            // 
            // MenuButton
            // 
            this.MenuButton.Location = new System.Drawing.Point(3, 5);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Size = new System.Drawing.Size(19, 23);
            this.MenuButton.TabIndex = 2;
            this.MenuButton.Text = "M";
            this.MenuButton.UseVisualStyleBackColor = true;
            // 
            // StepControl
            // 
            this.StepControl.AutoSize = true;
            this.StepControl.Location = new System.Drawing.Point(161, 4);
            this.StepControl.MinimumSize = new System.Drawing.Size(272, 16);
            this.StepControl.Name = "StepControl";
            this.StepControl.Size = new System.Drawing.Size(272, 29);
            this.StepControl.TabIndex = 1;
            // 
            // Parameters
            // 
            this.Parameters.DisplayMember = "Key";
            this.Parameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Parameters.FormattingEnabled = true;
            this.Parameters.Location = new System.Drawing.Point(28, 33);
            this.Parameters.Name = "Parameters";
            this.Parameters.Size = new System.Drawing.Size(127, 21);
            this.Parameters.TabIndex = 3;
            // 
            // ParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Parameters);
            this.Controls.Add(this.MenuButton);
            this.Controls.Add(this.StepControl);
            this.Controls.Add(this.Tracks);
            this.MinimumSize = new System.Drawing.Size(2, 110);
            this.Name = "ParameterControl";
            this.Size = new System.Drawing.Size(436, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ComboBox Tracks;
        private ParameterStepControl StepControl;
        private System.Windows.Forms.Button MenuButton;

        #endregion

        private System.Windows.Forms.ComboBox Parameters;
    }
}
