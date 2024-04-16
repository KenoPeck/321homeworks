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
    using System.Xml;
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
        private List<(int, int)> editedCells = new ();

        private Stack<ICommand> undoStack = new ();
        private Stack<ICommand> redoStack = new ();

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
        /// Function for resetting spreadsheet.
        /// </summary>
        public void WipeSpreadSheet()
        {
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.colCount; j++)
                {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                    this.cells[i, j].PropertyChanged -= this.Cell_PropertyChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                }
            }

            this.cells = new Cell[this.rowCount, this.colCount];
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.colCount; j++)
                {
                    this.cells[i, j] = this.CreateCell(i, j);
                }
            }

            foreach (ConcreteCell cell in this.dependencies.Keys)
            {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                cell.PropertyChanged -= this.SourceUpdateHandler;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            }

            this.dependencies = new Dictionary<ConcreteCell, List<ConcreteCell>>();
            this.values = new Dictionary<string, double>();
            this.editedCells = new List<(int, int)>();
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Function for saving spreadsheet to file.
        /// </summary>
        /// <param name="filestream">filestream spreadsheet is to be saved in.</param>
        /// <exception cref="NotImplementedException">placeholder function for TDD.</exception>
        public void SaveSpreadSheet(Stream filestream)
        {
            var spreadSheetXmlDoc = new XmlDocument(); // Create new XML document.
            var root = spreadSheetXmlDoc.CreateElement("Spreadsheet"); // Create root element.
            spreadSheetXmlDoc.AppendChild(root); // Append root element to XML document.
            foreach ((int, int) cellIndex in this.editedCells) // Iterate through edited cells and add them to XML document.
            {
                Cell theCell = this.cells[cellIndex.Item1, cellIndex.Item2]; // Get cell at index.
                if (theCell.Text == string.Empty && theCell.BGColor == 0xFFFFFFFF) // If cell is empty, skip it.
                {
                    continue;
                }

                var cell = spreadSheetXmlDoc.CreateElement("Cell"); // Create cell element.
                var index = spreadSheetXmlDoc.CreateAttribute("Index"); // Create index attribute.
                index.Value = cellIndex.Item1.ToString() + "," + cellIndex.Item2.ToString(); // Set index attribute value.
                var cellValue = spreadSheetXmlDoc.CreateElement("Text"); // Create text element.
                cellValue.InnerText = theCell.Text; // Set text element value.
                var cellBGC = spreadSheetXmlDoc.CreateElement("BGColor"); // Create BGColor element.
                cellBGC.InnerText = theCell.BGColor.ToString(); // Set Background Color element value.
                cell.AppendChild(cellValue); // Append text element to cell element.
                cell.AppendChild(cellBGC); // Append BGColor element to cell element.
                cell.Attributes.Append(index); // Append index attribute to cell element.
                root.AppendChild(cell); // Append cell element to root element.
            }

            spreadSheetXmlDoc.Save(filestream);
        }

        /// <summary>
        /// Function for loading spreadsheet from a file.
        /// </summary>
        /// <param name="filestream">filestream spreadsheet is to be loaded from.</param>
        /// <exception cref="NotImplementedException">placeholder function for TDD.</exception>
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public void LoadSpreadSheet(Stream filestream)
        {
            var spreadSheetXmlDoc = new XmlDocument(); // Create new XML document.
            spreadSheetXmlDoc.Load(filestream); // Load XML document from filestream.
            XmlNodeList cells = spreadSheetXmlDoc.GetElementsByTagName("Cell"); // Get all cell elements from XML document.
            foreach (XmlNode cell in cells) // Iterate through cell elements and update spreadsheet cells.
            {
                string[] index = cell.Attributes["Index"].Value.Split(','); // Split cell row/col indexes from index attribute.
                int rowIndex = int.Parse(index[0]); // Get row index.
                int colIndex = int.Parse(index[1]); // Get column index.
                Cell theCell = this.cells[rowIndex, colIndex]; // Get cell at specified index.
                theCell.Text = cell["Text"].InnerText; // Set cell text.
                theCell.BGColor = uint.Parse(cell["BGColor"].InnerText); // Set cell Background Color.
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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
        /// Function for adding command to undo stack.
        /// </summary>
        /// <param name="command">command to be added.</param>
        public void AddUndo(ICommand command)
        {
            this.undoStack.Push(command); // Add command to undo stack.
            this.redoStack.Clear(); // Clear redo stack.
        }

        /// <summary>
        /// Function for undoing a command.
        /// </summary>
        /// <returns>Tuple containing (updated_undo_type, updated_redo_type).</returns>
        /// <exception cref="InvalidOperationException">exception thrown if stack is empty when function called.</exception>
        public (ICommand?, ICommand) Undo()
        {
            if (this.undoStack.Count > 0) // If undo stack is not empty
            {
                ICommand redoCommand = this.undoStack.Pop(); // Pop command from undo stack.
                ICommand? undoCommand = this.undoStack.Count > 0 ? this.undoStack.Peek() : null; // Peek at next command in undo stack to see if another exists.
                redoCommand.Undo(); // Undo command.
                this.redoStack.Push(redoCommand); // Push command to redo stack.
                return (undoCommand, redoCommand);
            }
            else
            {
                throw new InvalidOperationException("No commands to undo.");
            }
        }

        /// <summary>
        /// Function for redoing a command.
        /// </summary>
        /// <returns>Tuple containing (updated_redo_type, updated_undo_type).</returns>
        /// <exception cref="InvalidOperationException">exception thrown if stack is empty when function called.</exception>
        public (ICommand, ICommand?) Redo()
        {
            if (this.redoStack.Count > 0) // If redo stack is not empty
            {
                ICommand undoCommand = this.redoStack.Pop(); // Pop command from redo stack.
                ICommand? redoCommand = this.redoStack.Count > 0 ? this.redoStack.Peek() : null; // Peek at next command in redo stack to see if another exists.
                undoCommand.Execute(); // Execute command.
                this.undoStack.Push(undoCommand); // Push command to undo stack.
                return (undoCommand, redoCommand);
            }
            else
            {
                throw new InvalidOperationException("No commands to redo.");
            }
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
            ConcreteCell changedCell = (ConcreteCell)sender;
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append((char)(changedCell.ColumnIndex + 'A'));
            nameBuilder.Append(changedCell.RowIndex + 1);
            string cellName = nameBuilder.ToString();
            if (!this.editedCells.Contains((changedCell.RowIndex, changedCell.ColumnIndex)))
            {
                this.editedCells.Add((changedCell.RowIndex, changedCell.ColumnIndex));
            }

            if (e.PropertyName == "Value") // If cell value is changed, update cell value and dependencies.
            {
                string source = changedCell.Text.Substring(1);
                string result = string.Empty;
                try
                {
                    changedCell.ExpressionTree = new ExpressionTree(source); // create expression tree with formula from cell.
                    changedCell.ExpressionTree.SetVariables(this.values); // set variables in expression tree to values in spreadsheet.
                    try
                    {
                        result = changedCell.ExpressionTree.Evaluate().ToString(); // try to evaluate expression tree.
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

                if (double.TryParse(result, out double evaluatedResult))
                {
                    this.values[cellName] = evaluatedResult; // If result is a number, add it to the dictionary.
                }
                else
                {
                    this.values.Remove(cellName); // If result is not a number, remove it from the dictionary.
                }

                changedCell.UpdateValue(result);
                foreach (ConcreteCell reference in this.dependencies.Keys)
                {
                    if (this.dependencies[reference].Contains(changedCell)) // If cell was dependent on another cell, unsubscribe it from the source cell changes.
                    {
                        this.dependencies[reference].Remove(changedCell);
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
                        int isCell = 0;
                        while (i < source.Length && char.IsDigit(source[i]))
                        {
                            isCell = 1;
                            rowBuilder.Append(source[i]);
                            i++;
                        }

                        if (i < source.Length && char.IsLetter(source[i]))
                        {
                            continue;
                        }

                        if (isCell == 1)
                        {
                            sourceRowIndex = int.Parse(rowBuilder.ToString()) - 1;
                            if (sourceRowIndex < 0 || sourceRowIndex >= this.rowCount || sourceColumnIndex < 0 || sourceColumnIndex >= this.colCount)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (isCell == 1)
                        {
                            i--;
                            ConcreteCell sourceCell = (ConcreteCell)this.cells[sourceRowIndex, sourceColumnIndex];
                            if (this.dependencies.ContainsKey(sourceCell)) // If source cell is already in dependencies, add cell to list.
                            {
                                this.dependencies[sourceCell].Add(changedCell);
                            }
                            else // If source cell is not in dependencies, add it to dictionary and add cell to list.
                            {
                                this.dependencies.Add(sourceCell, new List<ConcreteCell>());
                                this.dependencies[sourceCell].Add(changedCell);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate
                                sourceCell.PropertyChanged += this.SourceUpdateHandler;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate
                            }
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
                if (double.TryParse(changedCell.Text, out double result))
                {
                    this.values[cellName] = result;
                }
                else
                {
                    this.values.Remove(cellName);
                }

                changedCell.UpdateValue(changedCell.Text);
            }
            else if (e.PropertyName == "Empty") // If cell is cleared, remove it from dictionary and update dependents.
            {
                this.values.Remove(cellName);
                changedCell.UpdateValue(string.Empty);
                foreach (ConcreteCell reference in this.dependencies.Keys)
                {
                    if (this.dependencies[reference].Contains(changedCell)) // If cell was dependent on another cell, unsubscribe it from the source cell changes.
                    {
                        this.dependencies[reference].Remove(changedCell);
                        if (this.dependencies[reference].Count == 0) // If source cell has no more dependents, remove it from dictionary and unsubscribe updater.
                        {
                            this.dependencies.Remove(reference);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                            reference.PropertyChanged -= this.SourceUpdateHandler;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                        }
                    }
                }
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
            if (e.PropertyName == "Value" || e.PropertyName == "Text" || e.PropertyName == "Empty")
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
