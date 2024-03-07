// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Factory class for creating operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
        }

        /// <summary>
        /// Creates an operator node based on the operator char value.
        /// </summary>
        /// <param name="op">operator char value.</param>
        /// <returns>new OperatorNode.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '+': // addition
                    return new AdditionNode(op);
                case '-': // subtraction
                    return new SubtractionNode(op);
                case '*': // multiplication
                    return new MultiplicationNode(op);
                case '/': // division
                    return new DivisionNode(op);
                default:
                    throw new UnsupportedOperatorException("Invalid operator");
            }
        }
    }
}
