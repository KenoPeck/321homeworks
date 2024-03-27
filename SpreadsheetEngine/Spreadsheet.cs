// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Spreadsheet.cs
// contains implementation of spreadsheet class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    /// <summary>
    /// Spreadsheet class for managing cells, inherits abstract cell class.
    /// </summary>
    public class Spreadsheet : Cell
    {
        private Cell[,] cells;
        private int rowCount;
        private int colCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows"> number of rows for spreadsheet.</param>
        /// <param name="numCols"> number of columns for spreadsheet.</param>
        public Spreadsheet(int numRows, int numCols)
            : base(0, 0)
        {
            this.rowCount = numRows;
            this.colCount = numCols;
            this.cells = new Cell[numRows, numCols];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    this.cells[i, j] = this.CreateCell(i, j);
                }
            }
        }

        /// <summary>
        /// Event handler for updating UI/spreadsheet when any cell property is changed.
        /// </summary>
        #pragma warning disable SA1130
        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };
        #pragma warning restore SA1130

        /// <summary>
        /// Gets number of columns in spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.colCount;
            }
        }

        /// <summary>
        /// Gets number of rows in spreadsheet.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        /// Gets spreadsheet cell at specified row and column.
        /// </summary>
        /// <param name="row"> row index of target cell.</param>
        /// <param name="col"> column index of target cell.</param>
        /// <returns> cell at specified row and column.</returns>
        public Cell GetCell(int row, int col)
        {
            return this.cells[row, col];
        }

        /// <summary>
        /// Internal function for creating new concrete cell.
        /// </summary>
        /// <param name="row"> row index for new cell.</param>
        /// <param name="col"> column index for new cell.</param>
        /// <returns> new cell at specified row and column indexes.</returns>
        internal Cell CreateCell(int row, int col)
        {
            Cell newCell = new ConcreteCell(row, col);
            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            newCell.PropertyChanged += this.Cell_PropertyChanged;
            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            return newCell;
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                ConcreteCell cell = (ConcreteCell)sender;
                string source = cell.Text.Substring(1);
                int columnIndex = source[0] - 'A';
                int rowIndex = int.Parse(source.Substring(1)) - 1;
                cell.UpdateValue(this.cells[rowIndex, columnIndex].Value);
                this.UpdateDependents(cell);
            }
            else if (e.PropertyName == "Text")
            {
                ConcreteCell cell = (ConcreteCell)sender;
                cell.UpdateValue(cell.Text);
                this.UpdateDependents(cell);
            }

            this.CellPropertyChanged(sender, e);
        }

        private void UpdateDependents(ConcreteCell cell)
        {
            StringBuilder sourceBuilder = new StringBuilder();
            sourceBuilder.Append((char)(cell.ColumnIndex + 'A'));
            sourceBuilder.Append(cell.RowIndex + 1);
            string source = sourceBuilder.ToString();
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.colCount; j++)
                {
                    if (this.cells[i, j].Text.Length > 1 && this.cells[i, j].Text.Substring(1) == source && this.cells[i, j] is ConcreteCell concreteCell)
                    {
                        concreteCell.UpdateValue(cell.Value);
                        this.CellPropertyChanged(concreteCell, new PropertyChangedEventArgs("Value"));
                        this.UpdateDependents(concreteCell);
                    }
                }
            }
        }

        private class ConcreteCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
            /// </summary>
            /// <param name="row"> row index for new cell.</param>
            /// <param name="column"> column index for new cell.</param>
            public ConcreteCell(int row, int column)
                : base(row, column)
            {
            }

            /// <summary>
            /// Internal function allowing spreadsheet to update cell value.
            /// </summary>
            /// <param name="value"> new value for cell.</param>
            internal void UpdateValue(string value)
            {
                this.CellValue = value;
            }
        }
    }
}
