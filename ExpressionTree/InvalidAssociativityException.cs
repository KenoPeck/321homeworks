// <copyright file="InvalidAssociativityException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Exception for missing or invalid associativity value.
    /// </summary>
    public class InvalidAssociativityException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAssociativityException"/> class.
        /// </summary>
        public InvalidAssociativityException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAssociativityException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Message to be displayed with exception.</param>
        public InvalidAssociativityException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}