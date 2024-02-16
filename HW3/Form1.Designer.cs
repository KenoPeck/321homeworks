namespace HW3
{
    partial class HW3Form
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
            textBox1 = new TextBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadFromFileToolStripMenuItem = new ToolStripMenuItem();
            loadFibonacciNumbersFirst50ToolStripMenuItem = new ToolStripMenuItem();
            loadFibonacciNumbersFirst100ToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.ExecutablePath;
            saveToFileToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.AcceptsReturn = true;
            textBox1.Location = new Point(1, 32);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(378, 415);
            textBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(387, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadFromFileToolStripMenuItem, loadFibonacciNumbersFirst50ToolStripMenuItem, loadFibonacciNumbersFirst100ToolStripMenuItem, saveToFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadFromFileToolStripMenuItem
            // 
            loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            loadFromFileToolStripMenuItem.Size = new Size(325, 26);
            loadFromFileToolStripMenuItem.Text = "Load from file...";
            loadFromFileToolStripMenuItem.Click += loadFromFileToolStripMenuItem_Click;
            // 
            // loadFibonacciNumbersFirst50ToolStripMenuItem
            // 
            loadFibonacciNumbersFirst50ToolStripMenuItem.Name = "loadFibonacciNumbersFirst50ToolStripMenuItem";
            loadFibonacciNumbersFirst50ToolStripMenuItem.Size = new Size(325, 26);
            loadFibonacciNumbersFirst50ToolStripMenuItem.Text = "Load Fibonacci Numbers (First 50)";
            loadFibonacciNumbersFirst50ToolStripMenuItem.Click += loadFibonacciNumbersFirst50ToolStripMenuItem_Click;
            // 
            // loadFibonacciNumbersFirst100ToolStripMenuItem
            // 
            loadFibonacciNumbersFirst100ToolStripMenuItem.Name = "loadFibonacciNumbersFirst100ToolStripMenuItem";
            loadFibonacciNumbersFirst100ToolStripMenuItem.Size = new Size(325, 26);
            loadFibonacciNumbersFirst100ToolStripMenuItem.Text = "Load Fibonacci Numbers (First 100)";
            loadFibonacciNumbersFirst100ToolStripMenuItem.Click += loadFibonacciNumbersFirst100ToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Title = "Save To File";
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // saveToFileToolStripMenuItem
            // 
            saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            saveToFileToolStripMenuItem.Size = new Size(325, 26);
            saveToFileToolStripMenuItem.Text = "Save to file...";
            saveToFileToolStripMenuItem.Click += saveToFileToolStripMenuItem_Click;
            // 
            // HW3Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 450);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "HW3Form";
            Text = "322 Notepad";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadFromFileToolStripMenuItem;
        private ToolStripMenuItem loadFibonacciNumbersFirst50ToolStripMenuItem;
        private ToolStripMenuItem loadFibonacciNumbersFirst100ToolStripMenuItem;
        private ToolStripMenuItem saveToFileToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}
