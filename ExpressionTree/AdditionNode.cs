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
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        public AdditionNode()
        {
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
