// <copyright file="Node.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// abstract base node class.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// abstract evaluate method.
        /// </summary>
        /// <returns> double value of evaluated node.</returns>
        public abstract double Evaluate();
    }
}
