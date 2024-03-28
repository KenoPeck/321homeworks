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
        /// gets operator symbol.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// gets operator precedence.
        /// </summary>
        public static int Precedence => 1;

        /// <summary>
        /// gets AssociativityVals Enum of operator.
        /// </summary>
        public static AssociativityVals Associativity => AssociativityVals.L;

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        protected internal Node? Left { get; set; }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        protected internal Node? Right { get; set; }
    }
}
