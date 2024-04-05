// <copyright file="InvalidPrecedenceException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Exception for missing or invalid precedence value.
    /// </summary>
    public class InvalidPrecedenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPrecedenceException"/> class.
        /// </summary>
        public InvalidPrecedenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPrecedenceException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Message to be displayed with exception.</param>
        public InvalidPrecedenceException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}