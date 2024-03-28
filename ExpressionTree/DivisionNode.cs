// <copyright file="DivisionNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Division node class.
    /// </summary>
    internal class DivisionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionNode"/> class.
        /// </summary>
        /// <param name="c">operator char value.</param>
        public DivisionNode()
        {
        }

        /// <summary>
        /// gets operator symbol.
        /// </summary>
        public static new char Operator => '/';

        /// <summary>
        /// gets operator precedence.
        /// </summary>
        public static new int Precedence => 2;

        /// <summary>
        /// gets AssociativityVals Enum of operator.
        /// </summary>
        public static new AssociativityVals Associativity => AssociativityVals.L;

        /// <summary>
        /// Evaluates the division node.
        /// </summary>
        /// <returns>evaluated double value of self + children.</returns>
        public override double Evaluate()
        {
            if (this.Right == null || this.Left == null)
            {
                throw new System.Exception("Invalid expression");
            }

            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
