// <copyright file="CellTextEditCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HW9
{
    using SpreadsheetEngine;

    /// <summary>
    /// Command for editing cell text.
    /// </summary>
    internal class CellTextEditCommand : ICommand
    {
        private readonly Cell cell;
        private readonly string oldText;
        private readonly string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellTextEditCommand"/> class.
        /// </summary>
        /// <param name="cell">Cell being edited.</param>
        /// <param name="oldText">Text of cell before edit.</param>
        /// <param name="newText">Text of cell after edit.</param>
        public CellTextEditCommand(Cell cell, string oldText, string newText)
        {
            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Undoes the command.
        /// </summary>
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }

        /// <summary>
        /// Gets the title of the command.
        /// </summary>
        /// <returns>string containing the title of the command type.</returns>
        public string GetTitle()
        {
            return "Text Edit";
        }
    }
}
