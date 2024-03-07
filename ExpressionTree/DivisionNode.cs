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
        public DivisionNode(char c)
            : base(c)
        {
            this.Associativity = AssociativityVals.L;
            this.Precedence = 2;
        }

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
