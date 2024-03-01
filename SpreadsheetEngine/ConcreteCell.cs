namespace SpreadsheetEngine
{
    internal class ConcreteCell : Cell
    {
        public ConcreteCell(int row, int column)
            : base(row, column)
        {
        }

        internal void UpdateValue(string value)
        {
            this.cellValue = value;
        }
    }
}
