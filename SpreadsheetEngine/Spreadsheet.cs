namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public class Spreadsheet : Cell
    {
        private Cell[,] cells;
        private int rowCount;
        private int colCount;

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
                    cells[i, j] = CreateCell(i, j);
                }
            }
        }

        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        public int ColumnCount
        {
            get
            {
                return this.colCount;
            }
        }

        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        internal Cell CreateCell(int row, int col)
        {
            Cell newCell = new ConcreteCell(row, col);
            newCell.PropertyChanged += Cell_PropertyChanged;
            return newCell;
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CellPropertyChanged(this, e);
            if (e.PropertyName == "Text")
            {
                ConcreteCell cell = (ConcreteCell)sender;
                if (cell.Text[0] != '=')
                {
                    cell.UpdateValue(cell.Text);
                }
                else
                {
                    return;
                }
            }
        }

        public Cell GetCell(int row, int col)
        {
            return cells[row, col];
        }
    }
}
