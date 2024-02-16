namespace HW3
{
    public partial class HW3Form : Form
    {
        public HW3Form()
        {
            InitializeComponent();
        }

        public void loadText(System.IO.TextReader sr)
        {
            textBox1.Text = sr.ReadToEnd(); // read all text from TextReader and put it into textBox
        }

        internal void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opened = openFileDialog1.ShowDialog(); // show dialog when user clicks on "Load from file" menu item
            StreamReader sr = new(openFileDialog1.FileName); // create StreamReader from file selected by user
            loadText(sr); // load text into textBox
        }

        internal void loadFibonacciNumbersFirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        internal void loadFibonacciNumbersFirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        internal void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saved = saveFileDialog1.ShowDialog(); // show dialog when user clicks on "Save to file" menu item
        }

        internal void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, textBox1.Text); // write text from textBox1 to file
        }
    }
}
