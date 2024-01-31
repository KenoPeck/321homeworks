namespace HW2.Forms
{
    partial class HW2Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            outputBox = new TextBox();
            SuspendLayout();
            // 
            // outputBox
            // 
            outputBox.Dock = DockStyle.Fill;
            outputBox.Location = new Point(0, 0);
            outputBox.Multiline = true;
            outputBox.Name = "outputBox";
            outputBox.Size = new Size(800, 450);
            outputBox.TabIndex = 0;
            // 
            // HW2Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(outputBox);
            Name = "HW2Form";
            Text = "Cornelius Peck - 11780145";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox outputBox;
    }
}
