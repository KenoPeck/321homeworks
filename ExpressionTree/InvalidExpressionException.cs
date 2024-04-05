// <copyright file="InvalidExpressionException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Exception for Invalid expression.
    /// </summary>
    public class InvalidExpressionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExpressionException"/> class.
        /// </summary>
        public InvalidExpressionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExpressionException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Message to be displayed with exception.</param>
        public InvalidExpressionException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}