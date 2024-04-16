// <copyright file="CellBGCEditCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HW10
{
    using SpreadsheetEngine;

    /// <summary>
    /// Command for editing cell text.
    /// </summary>
    internal class CellBGCEditCommand : ICommand
    {
        private readonly Cell[] cells;
        private readonly uint[] oldColors;
        private readonly uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellBGCEditCommand"/> class.
        /// </summary>
        /// <param name="cells">Cells being edited.</param>
        /// <param name="oldColors">Colors of cells before edit.</param>
        /// <param name="newColor">Color of cell after edit.</param>
        public CellBGCEditCommand(Cell[] cells, uint[] oldColors, uint newColor)
        {
            this.cells = cells;
            this.oldColors = oldColors;
            this.newColor = newColor;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BGColor = this.newColor; // set the new color
            }
        }

        /// <summary>
        /// Undoes the command.
        /// </summary>
        public void Undo()
        {
            for (int i = 0; i < this.cells.Length; i++)
            {
                this.cells[i].BGColor = this.oldColors[i]; // set the old colors
            }
        }

        /// <summary>
        /// Gets the title of the command.
        /// </summary>
        /// <returns>string containing the title of the command type.</returns>
        public string GetTitle()
        {
            return "Background Color Edit";
        }
    }
}
