// <copyright file="MultiplicationNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Multiplication node class.
    /// </summary>
    internal class MultiplicationNode : OperatorNode
    {
        private static AssociativityVals associativity = AssociativityVals.L;

        private static int precedence = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="c">operator char value.</param>
        public MultiplicationNode(char c)
            : base(c)
        {
        }

        /// <summary>
        /// Evaluates the multiplication node.
        /// </summary>
        /// <returns>evaluated double value of self + children.</returns>
        public override double Evaluate()
        {
            if (this.Right == null || this.Left == null)
            {
                throw new System.Exception("Invalid expression");
            }

            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
