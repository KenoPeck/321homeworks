//-----------------------------------------------------------------------
// Form1.cs
// contains implementation of UI class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace HW4
{
    using SpreadsheetEngine;
    using System.ComponentModel;
    /// <summary>
    /// UI class for spreadsheet.
    /// </summary>
    public partial class Form1 : Form
    {
        internal Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
            spreadsheet = new(26, 50);
            spreadsheet.CellPropertyChanged += this.Spreadsheet_CellPropertyChanged;
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        this.dataGridView1.Rows[i].Cells[j].Value = spreadsheet.GetCell(i,j).Value;
                    }
                }
            }
            else if (e.PropertyName == "Text")
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Text;
            }
        }

        private void InitializeDataGrid()
        {
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnCount = 26;
            this.dataGridView1.RowCount = 50;
            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns[i].HeaderText = ((char)('A' + i)).ToString();
                this.dataGridView1.Columns[i].Name = ((char)('A' + i)).ToString();
            }

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }
    }
}
