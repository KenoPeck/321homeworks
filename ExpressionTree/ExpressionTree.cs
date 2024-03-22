// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System.Collections.Generic;
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
        /// Stack for converting to postfix.
        /// </summary>
        private Stack<char> treeStack = new Stack<char>();

        /// <summary>
        /// StringBuilder for generating/storing postfix expression.
        /// </summary>
        private StringBuilder postfixExp = new StringBuilder("");

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
        }

        /// <summary>
        /// Gets generated postfix expression.
        /// </summary>
        /// <returns>postfix expression string.</returns>
        public string GetPostFix()
        {
            return this.postfixExp.ToString();
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
                throw new Exception("Cannot Evaluate Empty Tree!");
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
