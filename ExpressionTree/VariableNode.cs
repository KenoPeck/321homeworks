// <copyright file="VariableNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Node for storing variables.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Variable Name.</param>
        /// <param name="value">Variable Value.</param>
        public VariableNode(string name, double value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Evaluates self.
        /// </summary>
        /// <returns> double value of variable.</returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
