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
        /// associativity enum value of operator.
        /// </summary>
        private AssociativityVals associativity;

        /// <summary>
        /// precedence value of operator.
        /// </summary>
        private int precedence;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="c"> operator char value.</param>
        public OperatorNode(char c)
        {
            this.Operator = c;
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
        /// Gets or sets the operator char value.
        /// </summary>
        public char Operator { get; set; }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        public Node? Left { get; set; }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        public Node? Right { get; set; }

        /// <summary>
        /// Gets or sets the precedence int value.
        /// </summary>
        protected int Precedence { get => this.precedence; set => this.precedence = value; }

        /// <summary>
        /// Gets or sets the associativity enum value.
        /// </summary>
        protected AssociativityVals Associativity { get => this.associativity; set => this.associativity = value; }
    }
}
