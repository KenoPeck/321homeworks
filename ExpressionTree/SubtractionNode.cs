// <copyright file="SubtractionNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Subtraction node class.
    /// </summary>
    internal class SubtractionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        /// <param name="c">operator char value.</param>
        public SubtractionNode(char c)
            : base(c)
        {
            this.Associativity = AssociativityVals.L;
            this.Precedence = 0;
        }

        /// <summary>
        /// Evaluates the Subtraction node.
        /// </summary>
        /// <returns>evaluated double value of self and children.</returns>
        public override double Evaluate()
        {
            if (this.Right == null || this.Left == null)
            {
                throw new System.Exception("Invalid expression");
            }

            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
