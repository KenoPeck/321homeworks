// <copyright file="UnsupportedOperatorException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Exception for unsupported operator.
    /// </summary>
    public class UnsupportedOperatorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedOperatorException"/> class.
        /// </summary>
        public UnsupportedOperatorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedOperatorException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Message to be displayed with exception.</param>
        public UnsupportedOperatorException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }
    }
}