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
        public SubtractionNode()
        {
        }

        /// <summary>
        /// gets operator symbol.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// gets operator precedence.
        /// </summary>
        public static int Precedence => 0;

        /// <summary>
        /// gets AssociativityVals Enum of operator.
        /// </summary>
        public static AssociativityVals Associativity => AssociativityVals.L;

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
