// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Form1.cs
// contains implementation of UI class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace HW8
{
    using System.ComponentModel;
    using SpreadsheetEngine;

    /// <summary>
    /// UI class for spreadsheet.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// instantiated Spreadsheet object for housing cell array.
        /// </summary>
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
            this.spreadsheet = new (50, 26);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            this.spreadsheet.CellPropertyChanged += this.Spreadsheet_CellPropertyChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        }

        /// <summary>
        /// Gets private spreadsheet object.
        /// </summary>
        internal Spreadsheet Spreadsheet { get => this.spreadsheet; }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = this.spreadsheet.GetCell(rowIndex, columnIndex).Text;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            var input = this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value; // get input from cell
            var editedCell = this.spreadsheet.GetCell(rowIndex, columnIndex); // get cell to edit
            if (input != null)
            {
                string? inputStr = input.ToString();
                if (inputStr != null)
                {
                    var textCommand = new CellTextEditCommand(editedCell, editedCell.Text, inputStr); // create command from cell input
                    this.spreadsheet.AddUndo(textCommand); // add command to undo stack
                    textCommand.Execute(); // execute command
                    this.undoToolStripMenuItem.Enabled = true; // enable undo button
                    this.undoToolStripMenuItem.Text = "Undo Cell Text Edit"; // change undo button text
                    this.redoToolStripMenuItem.Enabled = false; // disable redo button
                    this.redoToolStripMenuItem.Text = "Redo Unavailable"; // change redo button text
                }
            }
            else
            {
                var textCommand = new CellTextEditCommand(editedCell, editedCell.Text, string.Empty); // create command from cell input
                this.spreadsheet.AddUndo(textCommand); // add command to undo stack
                textCommand.Execute(); // execute command
                this.undoToolStripMenuItem.Enabled = true; // enable undo button
                this.undoToolStripMenuItem.Text = "Undo Cell Text Edit"; // change undo button text
                this.redoToolStripMenuItem.Enabled = false; // disable redo button
                this.redoToolStripMenuItem.Text = "Redo Unavailable"; // change redo button text
            }
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value") // If cell value changes, update datagridview cell value.
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
            }
            else if (e.PropertyName == "Text") // If cell text changes, update datagridview cell text.
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Text;
            }
            else if (e.PropertyName == "Empty") // If cell text is empty string, update datagridview cell text to empty.
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = string.Empty;
            }
            else if (e.PropertyName == "BGColor") // If cell background color changes, update datagridview cell background color.
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb((int)cell.BGColor);
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

#pragma warning disable SA1300 // Element should begin with upper-case letter - winforms autogenerated code
        private void changeSelectedCellsBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
#pragma warning restore SA1300 // Element should begin with upper-case letter
        {
            var selectedCells = this.dataGridView1.SelectedCells; // get selected cells
            if (this.colorDialog1.ShowDialog() == DialogResult.OK) // if color dialog result is OK
            {
                uint newColor = (uint)this.colorDialog1.Color.ToArgb(); // get color from dialog
                Cell[] cells = new Cell[selectedCells.Count]; // create cell array for cells to edit
                uint[] oldColors = new uint[selectedCells.Count]; // create uint array for old colors
                int i = 0;
                foreach (DataGridViewCell cell in selectedCells)
                {
                    cells[i] = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex); // get cell from spreadsheet
                    oldColors[i] = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor; // get old color
                    i++;
                }

                var colorCommand = new CellBGCEditCommand(cells, oldColors, newColor); // create command from cell input
                colorCommand.Execute(); // execute command
                this.spreadsheet.AddUndo(colorCommand); // add command to undo stack
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo Cell Background Color Edit";
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo Unavailable";
            }
        }
#pragma warning disable SA1300 // Element should begin with upper-case letter - winforms autogenerated code

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (undoCommand, redoCommand) = this.spreadsheet.Undo(); // undo last command and get new undo and redo commands
            if (undoCommand != null) // if there is an undo command
            {
                this.undoToolStripMenuItem.Text = "Undo Cell " + undoCommand.GetTitle(); // change undo button text
                this.redoToolStripMenuItem.Enabled = true; // enable redo button
                this.redoToolStripMenuItem.Text = "Redo Cell " + redoCommand.GetTitle(); // change redo button text
            }
            else // if there is no more undo commands
            {
                this.undoToolStripMenuItem.Enabled = false; // disable undo button
                this.undoToolStripMenuItem.Text = "Undo Unavailable"; // change undo button text
                this.redoToolStripMenuItem.Enabled = true; // enable redo button
                this.redoToolStripMenuItem.Text = "Redo Cell " + redoCommand.GetTitle(); // change redo button text
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var (undoCommand, redoCommand) = this.spreadsheet.Redo(); // redo last command and get new undo and redo commands
            if (redoCommand != null) // if there is a redo command
            {
                this.redoToolStripMenuItem.Text = "Redo Cell " + redoCommand.GetTitle(); // change redo button text
                this.undoToolStripMenuItem.Enabled = true; // enable undo button
                this.undoToolStripMenuItem.Text = "Undo Cell " + undoCommand.GetTitle(); // change undo button text
            }
            else // if there is no more redo commands
            {
                this.redoToolStripMenuItem.Enabled = false; // disable redo button
                this.redoToolStripMenuItem.Text = "Redo Unavailable"; // change redo button text
                this.undoToolStripMenuItem.Enabled = true; // enable undo button
                this.undoToolStripMenuItem.Text = "Undo Cell " + undoCommand.GetTitle(); // change undo button text
            }
        }

#pragma warning restore SA1300 // Element should begin with upper-case letter
    }
}
