// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// Node for storing constants.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value"> double value to be stored in node.</param>
        public ConstantNode(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the double value stored in node.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Evaluate self.
        /// </summary>
        /// <returns> double value stored in node.</returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
