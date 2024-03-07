// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Expression tree class for parsing and evaluating expressions.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Expression from which tree will be generated.
        /// </summary>
        private string expression;

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
            this.expression = expression;
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the variables dictionary.
        /// </summary>
        public Dictionary<string, double> Variables { get => this.variables; set => this.variables = value; }

        /// <summary>
        /// Sets specified variable in variables dictionary.
        /// </summary>
        /// <param name="variableName"> Name of variable.</param>
        /// <param name="variableValue"> Value of Variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Evaluates expression to a double value.
        /// </summary>
        /// <returns> double value which expression evaluated to.</returns>
        public double Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}
