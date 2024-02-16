namespace HW3
{
    public partial class HW3Form : Form
    {
        public HW3Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads text from a text reader into the user's textBox
        /// </summary>
        /// <param name="sr">
        /// a text/stream reader from which text is loaded into the textBox
        /// </param>
        public void loadText(System.IO.TextReader sr)
        {
            textBox1.Text = sr.ReadToEnd(); // read all text from TextReader and put it into textBox
        }

        public string getTextbox()
        {
            return textBox1.Text.ToString();
        }

        /// <summary>
        /// opens openfiledialog on user click, then loads text from file into user textbox
        /// </summary>
        internal void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opened = openFileDialog1.ShowDialog(); // show dialog when user clicks on "Load from file" menu item
            StreamReader sr = new(openFileDialog1.FileName); // create StreamReader from file selected by user
            loadText(sr); // load text into textBox
            sr.Close();
        }

        /// <summary>
        /// loads first 50 fibonacci numbers into user textbox
        /// </summary>
        internal void loadFibonacciNumbersFirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib50 = new(50); // create FibonacciTextReader with max of 50 lines
            loadText(fib50); // load text into textBox
        }

        /// <summary>
        /// loads first 100 fibonacci numbers into user textbox
        /// </summary>
        internal void loadFibonacciNumbersFirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib100 = new(100); // create FibonacciTextReader with max of 100 lines
            loadText(fib100); // load text into textBox
        }

        /// <summary>
        /// opens savefiledialog on user click
        /// </summary>
        public void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saved = saveFileDialog1.ShowDialog(); // show dialog when user clicks on "Save to file" menu item
        }

        /// <summary>
        /// saves text from user textbox to file
        /// </summary>
        internal void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, textBox1.Text); // write text from textBox1 to file
        }
    }
}
