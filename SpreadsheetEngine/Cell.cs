namespace SpreadsheetEngine
{
    using System.ComponentModel;

    public abstract class Cell : INotifyPropertyChanged
    {
        protected int columnIndex;
        protected int rowIndex;
        protected string text;
        protected string cellValue;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
                else
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }


            }
        }

        public string Value
        {
            get
            {
                return this.cellValue;
            }
        }

        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        protected Cell(int row, int column)
        {
            this.rowIndex = row;
            this.columnIndex = column;
            this.text = string.Empty;
            this.cellValue = this.text;
        }
    }
}
