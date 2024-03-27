// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// abstract operator node class.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        public OperatorNode()
        {
            this.Left = this.Right = null;
        }

        /// <summary>
        /// enum for associativity values.
        /// </summary>
        public enum AssociativityVals
        {
            /// <summary>
            /// Left associativity.
            /// </summary>
            L = 1,

            /// <summary>
            /// Right associativity.
            /// </summary>
            R = 2,
        }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        protected Node? Left { get; set; }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        protected Node? Right { get; set; }
    }
}
