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
    using ExpressionTree;

    /// <summary>
    /// Spreadsheet class for managing cells, inherits abstract cell class.
    /// </summary>
    public class Spreadsheet : Cell
    {
        private Cell[,] cells;
        private int rowCount;
        private int colCount;

        private Dictionary<ConcreteCell, List<ConcreteCell>> dependencies = new Dictionary<ConcreteCell, List<ConcreteCell>>();
        private Dictionary<string, double> values = new Dictionary<string, double>();

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
            if (e.PropertyName == "Value") // If cell value is changed, update cell value and dependencies.
            {
                ConcreteCell cell = (ConcreteCell)sender;
                string source = cell.Text.Substring(1);
                string result = string.Empty;
                try
                {
                    cell.ExpressionTree = new ExpressionTree(source); // create expression tree with formula from cell.
                    cell.ExpressionTree.SetVariables(this.values); // set variables in expression tree to values in spreadsheet.
                    try
                    {
                        result = cell.ExpressionTree.Evaluate().ToString(); // try to evaluate expression tree.
                    }
                    catch (ArgumentException)
                    {
                        result = "RefError"; // If a variable is not found in the dictionary, set result to error.
                    }
                }
                catch (InvalidAssociativityException)
                {
                    result = "AError"; // If operator associativity is invalid, set result to AError.
                }
                catch (InvalidPrecedenceException)
                {
                    result = "PError"; // If operator precedence is invalid, set result to PError.
                }
                catch (UnsupportedOperatorException)
                {
                    result = "OpError"; // If an unknown operator is used, set result to OpError.
                }

                StringBuilder newCellBuilder = new StringBuilder();
                newCellBuilder.Append((char)(cell.ColumnIndex + 'A'));
                newCellBuilder.Append(cell.RowIndex + 1);
                string newCell = newCellBuilder.ToString(); // create string representation of cell name.
                if (double.TryParse(result, out double evaluatedResult))
                {
                    this.values[newCell] = evaluatedResult; // If result is a number, add it to the dictionary.
                }
                else
                {
                    this.values.Remove(newCell); // If result is not a number, remove it from the dictionary.
                }

                cell.UpdateValue(result);
                foreach (ConcreteCell reference in this.dependencies.Keys)
                {
                    if (this.dependencies[reference].Contains(cell)) // If cell was dependent on another cell, unsubscribe it from the source cell changes.
                    {
                        this.dependencies[reference].Remove(cell);
                        if (this.dependencies[reference].Count == 0) // If source cell has no more dependents, remove it from dictionary and unsubscribe updater.
                        {
                            this.dependencies.Remove(reference);
                            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                            reference.PropertyChanged -= this.SourceUpdateHandler;
                            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                        }
                    }
                }

                int sourceColumnIndex = 0;
                int sourceRowIndex = 0;
                for (int i = 0; i < source.Length; i++) // Iterate through formula to find dependencies.
                {
                    if (char.IsLetter(source[i])) // If character is a letter, it is a column index/dependency.
                    {
                        StringBuilder rowBuilder = new StringBuilder();
                        sourceColumnIndex = source[i] - 'A';
                        i++;
                        while (i < source.Length && char.IsDigit(source[i]))
                        {
                            rowBuilder.Append(source[i]);
                            i++;
                        }

                        i--;
                        sourceRowIndex = int.Parse(rowBuilder.ToString()) - 1;
                        ConcreteCell sourceCell = (ConcreteCell)this.cells[sourceRowIndex, sourceColumnIndex];
                        if (this.dependencies.ContainsKey(sourceCell)) // If source cell is already in dependencies, add cell to list.
                        {
                            this.dependencies[sourceCell].Add(cell);
                        }
                        else // If source cell is not in dependencies, add it to dictionary and add cell to list.
                        {
                            this.dependencies.Add(sourceCell, new List<ConcreteCell>());
                            this.dependencies[sourceCell].Add(cell);
                            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate
                            sourceCell.PropertyChanged += this.SourceUpdateHandler;
                            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate
                        }
                    }
                    else
                    {
                        continue; // If character is not a letter, continue to next character.
                    }
                }
            }
            else if (e.PropertyName == "Text") // If cell text is changed, update cell text and value fields.
            {
                ConcreteCell cell = (ConcreteCell)sender;
                StringBuilder sourceBuilder = new StringBuilder();
                sourceBuilder.Append((char)(cell.ColumnIndex + 'A'));
                sourceBuilder.Append(cell.RowIndex + 1);
                string source = sourceBuilder.ToString();
                if (double.TryParse(cell.Text, out double result))
                {
                    this.values[source] = result;
                }
                else
                {
                    this.values.Remove(source);
                }

                cell.UpdateValue(cell.Text);
            }
            else if (e.PropertyName == "Empty") // If cell is cleared, remove it from dictionary and update dependents.
            {
                ConcreteCell cell = (ConcreteCell)sender;
                StringBuilder sourceBuilder = new StringBuilder();
                sourceBuilder.Append((char)(cell.ColumnIndex + 'A'));
                sourceBuilder.Append(cell.RowIndex + 1);
                string source = sourceBuilder.ToString();
                this.values.Remove(source);
                cell.UpdateValue(string.Empty);
            }

            this.CellPropertyChanged(sender, e);
        }

        /// <summary>
        /// Function for handling source cell updates.
        /// </summary>
        /// <param name="sender">Source cell which has been updated.</param>
        /// <param name="e">Changed property name.</param>
        private void SourceUpdateHandler(object sender, PropertyChangedEventArgs e) // Function for handling source cell updates.
        {
            ConcreteCell sourceCell = (ConcreteCell)sender;
            if (this.dependencies.ContainsKey(sourceCell)) // If source cell is in dependencies, update dependents.
            {
                List<ConcreteCell> dependencies = new List<ConcreteCell>(this.dependencies[sourceCell]);
                foreach (ConcreteCell dependentCell in dependencies)
                {
                    dependentCell.Refresh(); // Refresh dependent cell to match new value of sourceCell.
                }
            }
            else
            {
                throw new InvalidDependencyException("Source cell not found in dependencies dictionary.");
            }
        }

        private class ConcreteCell : Cell
        {
            private ExpressionTree expressionTree;

            /// <summary>
            /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
            /// </summary>
            /// <param name="row"> row index for new cell.</param>
            /// <param name="column"> column index for new cell.</param>
            public ConcreteCell(int row, int column)
                : base(row, column)
            {
                this.expressionTree = new ExpressionTree("0+0");
            }

            internal ExpressionTree ExpressionTree { get => this.expressionTree; set => this.expressionTree = value; }

            /// <summary>
            /// Internal function allowing spreadsheet to update cell value.
            /// </summary>
            /// <param name="value"> new value for cell.</param>
            internal void UpdateValue(string value) // Internal function allowing spreadsheet to update cell value.
            {
                this.CellValue = value;
            }
        }
    }
}
