//-----------------------------------------------------------------------
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
        protected int columnIndex;

        /// <summary>
        /// index of cell row.
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// text/unevaluated value of cell.
        /// </summary>
        protected string text;

        /// <summary>
        /// true/evaluated value of cell.
        /// </summary>
        protected string cellValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row"> row index for new cell.</param>
        /// <param name="column"> column index for new cell.</param>
        protected Cell(int row, int column)
        {
            this.rowIndex = row;
            this.columnIndex = column;
            this.text = string.Empty;
            this.cellValue = this.text;
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
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                if (value[0] != '=')
                {
                    this.cellValue = value;
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
                return this.cellValue;
            }
        }

        /// <summary>
        /// Gets row index of cell.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets column index of cell.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }
    }
}
