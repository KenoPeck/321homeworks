// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Cell.cs
// contains implementation of Cell abstract class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Abstract class for spreadsheet cell.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// index of cell column.
        /// </summary>
        private int columnIndex1;

        /// <summary>
        /// index of cell row.
        /// </summary>
        private int rowIndex1;

        /// <summary>
        /// text/unevaluated value of cell.
        /// </summary>
        private string text1;

        /// <summary>
        /// true/evaluated value of cell.
        /// </summary>
        private string cellValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row"> row index for new cell.</param>
        /// <param name="column"> column index for new cell.</param>
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Cell(int row, int column)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.RowIndex1 = row;
            this.ColumnIndex1 = column;
            this.Text1 = string.Empty;
            this.CellValue = this.Text1;
        }

        /// <summary>
        /// Event handler for cell property being changed.
        /// </summary>
        #pragma warning disable SA1130
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
        #pragma warning restore SA1130

        /// <summary>
        /// Gets or sets text value of cell.
        /// </summary>
        public string Text
        {
            get
            {
                return this.Text1;
            }

            set
            {
                if (this.Text1 == value)
                {
                    return;
                }

                this.Text1 = value;
                if (value[0] != '=')
                {
                    this.CellValue = value;
                    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                    #pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                else
                {
                    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                    #pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }
        }

        /// <summary>
        /// Gets value of cell.
        /// </summary>
        public string Value
        {
            get
            {
                return this.CellValue;
            }
        }

        /// <summary>
        /// Gets row index of cell.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.RowIndex1;
            }
        }

        /// <summary>
        /// Gets column index of cell.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.ColumnIndex1;
            }
        }

        /// <summary>
        /// Gets or sets Column index of cell.
        /// </summary>
        protected int ColumnIndex1 { get => this.columnIndex1; set => this.columnIndex1 = value; }

        /// <summary>
        /// Gets or sets Row index of cell.
        /// </summary>
        protected int RowIndex1 { get => this.rowIndex1; set => this.rowIndex1 = value; }

        /// <summary>
        /// Gets or sets text value of cell.
        /// </summary>
        protected string Text1 { get => this.text1; set => this.text1 = value; }

        /// <summary>
        /// Gets or sets value of cell.
        /// </summary>
        protected string CellValue { get => this.cellValue; set => this.cellValue = value; }
    }
}
