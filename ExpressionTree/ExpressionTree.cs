// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System.Text;

    /// <summary>
    /// Expression tree class for parsing and evaluating expressions.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Operator node factory for creating operator nodes.
        /// </summary>
        private static OperatorNodeFactory factory = new OperatorNodeFactory();

        /// <summary>
        /// Root node of the expression tree.
        /// </summary>
        private OperatorNode? root;

        /// <summary>
        /// Dictionary for storing variables and their values.
        /// </summary>
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Expression from which tree will be generated.</param>
        public ExpressionTree(string expression)
        {
            OperatorNode? current = this.root; // current node for tree generation.
            for (int i = 0; i < expression.Length; i++)
            {
                while (i < expression.Length && char.IsWhiteSpace(expression[i])) // skipping whitespace
                {
                    i++;
                }

                if (char.IsLetter(expression[i])) // if character is part of a variable
                {
                    StringBuilder variableName = new StringBuilder();
                    while (i < expression.Length && char.IsLetter(expression[i])) // add all letters in variable name
                    {
                        variableName.Append(expression[i]);
                        i++;
                    }

                    while (i < expression.Length && char.IsNumber(expression[i])) // add all numbers in variable name
                    {
                        variableName.Append(expression[i]);
                        i++;
                    }

                    while (i < expression.Length && char.IsWhiteSpace(expression[i])) // skip whitespace
                    {
                        i++;
                    }

                    if (current == null) // if current root has no operator yet
                    {
                        if (i < expression.Length && !char.IsLetter(expression[i]) && !char.IsNumber(expression[i])) // if next character is an operator
                        {
                            if (current == this.root)
                            {
                                this.root = factory.CreateOperatorNode(expression[i]); // create new operator node
                                current = this.root;
                            }
                            else
                            {
                                current = factory.CreateOperatorNode(expression[i]);
                            }

                            if (this.variables.ContainsKey(variableName.ToString()))
                            {
                                current.Left = new VariableNode(variableName.ToString(), this.variables[variableName.ToString()]);
                            }
                            else
                            {
                                current.Left = new VariableNode(variableName.ToString(), 0);
                                this.variables[variableName.ToString()] = 0;
                            }
                        }
                        else
                        {
                            throw new System.Exception("Invalid expression");
                        }
                    }
                    else
                    {
                        if (this.variables.ContainsKey(variableName.ToString()))
                        {
                            current.Right = new VariableNode(variableName.ToString(), this.variables[variableName.ToString()]);
                        }
                        else
                        {
                            current.Right = new VariableNode(variableName.ToString(), 0);
                            this.variables[variableName.ToString()] = 0;
                            i -= 1;
                        }
                    }
                }
                else if (char.IsNumber(expression[i]))
                {
                    StringBuilder constant = new StringBuilder();
                    while (i < expression.Length && (char.IsNumber(expression[i]) || expression[i] == '.'))
                    {
                        constant.Append(expression[i]);
                        i++;
                    }

                    while (i < expression.Length && char.IsWhiteSpace(expression[i]))
                    {
                        i++;
                    }

                    if (current == null)
                    {
                        if (i < expression.Length && !char.IsLetter(expression[i]) && !char.IsNumber(expression[i]))
                        {
                            if (current == this.root)
                            {
                                this.root = factory.CreateOperatorNode(expression[i]);
                                current = this.root;
                            }
                            else
                            {
                                current = factory.CreateOperatorNode(expression[i]);
                            }

                            current.Left = new ConstantNode(double.Parse(constant.ToString()));
                        }
                        else if (this.root == null)
                        {
                            this.root = factory.CreateOperatorNode('+');
                            this.root.Left = new ConstantNode(double.Parse(constant.ToString()));
                            this.root.Right = new ConstantNode(0);
                        }
                        else
                        {
                            throw new System.Exception("Invalid expression");
                        }
                    }
                    else
                    {
                        current.Right = new ConstantNode(double.Parse(constant.ToString()));
                    }
                }
                else if (!char.IsLetter(expression[i]) && !char.IsNumber(expression[i])) // if character is an operator
                {
                    if (current == null)
                    {
                        throw new System.Exception("Invalid expression");
                    }
                    else
                    {
                        if (current.Right == null)
                        {
                            throw new System.Exception("Invalid expression");
                        }
                        else
                        {
                            if (current.Right is ConstantNode || current.Right is VariableNode)
                            {
                                OperatorNode temp = factory.CreateOperatorNode(expression[i]); // set right to new operator node and move old value to new node's left
                                temp.Left = current.Right;
                                current.Right = temp;
                                current = temp;
                            }
                            else
                            {
                                throw new System.Exception("Invalid Tree");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets specified variable in variables dictionary.
        /// </summary>
        /// <param name="variableName"> Name of variable.</param>
        /// <param name="variableValue"> Value of Variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            if (this.variables.ContainsKey(variableName) && this.root != null)
            {
                this.UpdateVariable(variableName, variableValue, this.root);
            }

            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluates expression to a double value.
        /// </summary>
        /// <returns> double value which expression evaluated to.</returns>
        public double Evaluate()
        {
            if (this.root == null)
            {
                throw new System.Exception("Cannot Evaluate Empty Tree!");
            }

            return this.root.Evaluate();
        }

        /// <summary>
        /// Updates value of all matching variables in the expression tree.
        /// </summary>
        /// <param name="variableName">name of variable to be updated.</param>
        /// <param name="variableValue">value of variable to be updated.</param>
        /// <param name="root">root of tree to be updated.</param>
        private void UpdateVariable(string variableName, double variableValue, OperatorNode root)
        {
            if (root != null)
            {
                if (root.Left is VariableNode)
                {
                    if (((VariableNode)root.Left).Name == variableName)
                    {
                        ((VariableNode)root.Left).Value = variableValue;
                    }
                }
                else if (root.Left is OperatorNode)
                {
                    this.UpdateVariable(variableName, variableValue, (OperatorNode)root.Left);
                }

                if (root.Right is VariableNode)
                {
                    if (((VariableNode)root.Right).Name == variableName)
                    {
                        ((VariableNode)root.Right).Value = variableValue;
                    }
                }
                else if (root.Right is OperatorNode)
                {
                    this.UpdateVariable(variableName, variableValue, (OperatorNode)root.Right);
                }
            }
        }
    }
}
