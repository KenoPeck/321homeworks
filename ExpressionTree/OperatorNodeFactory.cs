// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System.Reflection;

    /// <summary>
    /// Factory class for creating operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// List of supported operators.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) },
        };

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
                    return new AdditionNode();
                case '-': // subtraction
                    return new SubtractionNode();
                case '*': // multiplication
                    return new MultiplicationNode();
                case '/': // division
                    return new DivisionNode();
                default:
                    throw new UnsupportedOperatorException("Invalid operator");
            }
        }

        /// <summary>
        /// Checks if a character is an operator.
        /// </summary>
        /// <param name="c">operator character.</param>
        /// <returns>T/F for if c is a supported operator.</returns>
        public bool IsOperator(char c)
        {
            return this.operators.ContainsKey(c);
        }

        /// <summary>
        /// Gets the precedence value of an operator.
        /// </summary>
        /// <param name="c">operator character.</param>
        /// <returns>operator precedence int value.</returns>
        /// <exception cref="Exception">operator has no precedence value.</exception>
        /// <exception cref="Exception">operator has no get precedence method.</exception>
        /// <exception cref="UnsupportedOperatorException">operator does not exist.</exception>
        public int GetPrecedence(char c)
        {
            if (this.IsOperator(c))
            {
                int precedence = 0;
                Type myType = this.operators[c];
                PropertyInfo? propertyInfo = myType.GetProperty("Precedence");
                if (propertyInfo != null)
                {
                    var temp = propertyInfo.GetValue(null, null);
                    if (temp != null)
                    {
                        precedence = (int)temp;
                    }
                    else
                    {
                        throw new Exception("Operator Has No Precedence Value");
                    }

                    return precedence;
                }
                else
                {
                    throw new Exception("Operator Has No Precedence Property");
                }
            }
            else
            {
                throw new UnsupportedOperatorException("Invalid operator");
            }
        }

        /// <summary>
        /// Gets the associativity value of an operator.
        /// </summary>
        /// <param name="c">operator character.</param>
        /// <returns>operator associativity enum value.</returns>
        /// <exception cref="Exception">operator has no associativity value.</exception>
        /// <exception cref="Exception">operator has no get associativity method.</exception>
        /// <exception cref="UnsupportedOperatorException">operator does not exist.</exception>
        public OperatorNode.AssociativityVals GetAssociativity(char c)
        {
            if (this.IsOperator(c))
            {
                OperatorNode.AssociativityVals associativity = 0;
                MethodInfo? methodInfo = this.operators[c].GetMethod("Associativity");
                if (methodInfo != null)
                {
                    var temp = methodInfo.Invoke(null, null);
                    if (temp != null)
                    {
                        associativity = (OperatorNode.AssociativityVals)temp;
                    }
                    else
                    {
                        throw new Exception("Operator Has No Associativity Value");
                    }

                    return associativity;
                }
                else
                {
                    throw new Exception("Operator Has No Associativity Property");
                }
            }
            else
            {
                throw new UnsupportedOperatorException("Invalid operator");
            }
        }
    }
}
