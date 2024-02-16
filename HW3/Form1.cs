namespace HW3
{
    public partial class HW3Form : Form
    {
        public HW3Form()
        {
            InitializeComponent();
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadFibonacciNumbersFirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadFibonacciNumbersFirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(); // show dialog when user clicks on "Save to file" menu item
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, textBox1.Text); // write text from textBox1 to file
        }
    }
}
