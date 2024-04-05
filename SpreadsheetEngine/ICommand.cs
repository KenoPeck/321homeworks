// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract class for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Undoes the command.
        /// </summary>
        public void Undo();

        /// <summary>
        /// Gets the title of the command.
        /// </summary>
        /// <returns>string containing the title of the command type.</returns>
        public string GetTitle();
    }
}
