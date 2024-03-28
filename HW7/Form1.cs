// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Form1.cs
// contains implementation of UI class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace HW7
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
            var input = this.dataGridView1.Rows[rowIndex].Cells[columnIndex].Value;
            if (input != null)
            {
                #pragma warning disable CS8601 // Possible null reference assignment.
                this.spreadsheet.GetCell(rowIndex, columnIndex).Text = input.ToString();
                #pragma warning restore CS8601 // Possible null reference assignment.
            }
            else
            {
                #pragma warning disable CS8601 // Possible null reference assignment.
                this.spreadsheet.GetCell(rowIndex, columnIndex).Text = string.Empty;
                #pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Cell cell = (Cell)sender;
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
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
