// <copyright file="AdditionNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Addition node class.
    /// </summary>
    internal class AdditionNode : OperatorNode
    {
        private static AssociativityVals associativity = AssociativityVals.L;

        private static int precedence = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="c">operator char value.</param>
        public AdditionNode(char c)
            : base(c)
        {
        }

        /// <summary>
        /// Evaluates the addition node.
        /// </summary>
        /// <returns>evaluated double value of self and children.</returns>
        public override double Evaluate()
        {
            if (this.Right == null || this.Left == null)
            {
                throw new System.Exception("Invalid expression");
            }

            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
