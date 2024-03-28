// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System.Reflection;
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

    /// <summary>
    /// Factory class for creating operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// List of supported operators.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Creates an operator node based on the operator char value.
        /// </summary>
        /// <param name="op">operator char value.</param>
        /// <returns>new OperatorNode.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
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

        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object value = operatorField.GetValue(type);
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }

    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

}
