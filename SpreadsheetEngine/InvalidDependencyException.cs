// <copyright file="InvalidDependencyException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Exception for missing dependency.
    /// </summary>
    public class InvalidDependencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDependencyException"/> class.
        /// </summary>
        public InvalidDependencyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDependencyException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Message to be displayed with exception.</param>
        public InvalidDependencyException(string exceptionMessage)
            : base(exceptionMessage)
        {
            Console.WriteLine(exceptionMessage);
        }
    }
}