//-----------------------------------------------------------------------
// ConcreteCell.cs
// contains implementation of Concrete Cell class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace SpreadsheetEngine
{
    /// <summary>
    /// Concrete class version of abstract spreadsheet cell, inherits abstract cell class.
    /// </summary>
    internal class ConcreteCell : Cell
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
            this.cellValue = value;
        }
    }
}
