// <copyright file="ExpressionTreeTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Tests have documentation above each test set which weren't being detected by stylecop
#pragma warning disable SA1600 // Elements should be documented

namespace ExpressionTree.Tests
{
    using System.Globalization;

    [TestFixture]
    public class ExpressionTreeTests
    {
        [Test]
        [TestCase("11+22", ExpectedResult = "11 22 + ")] // addition expression
        [TestCase("22-11", ExpectedResult = "22 11 - ")] // subtraction expression
        [TestCase("3+3-3", ExpectedResult = "3 3 + 3 - ")] // addition and substraction mixed expression
        [TestCase("11*22", ExpectedResult = "11 22 * ")] // multiplication expression
        [TestCase("22/11", ExpectedResult = "22 11 / ")] // division expression

        /// <summary>
        /// Test postfix expression generation.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        public string TestPostFixExpressions(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.GetPostFix();
        }

        [Test]
        [TestCase("11+22", ExpectedResult = 33.0)] // addition expression
        [TestCase("22-11", ExpectedResult = 11.0)] // subtraction expression
        [TestCase("11*22", ExpectedResult = 242.0)] // multiplication expression
        [TestCase("22/11", ExpectedResult = 2.0)] // division expression
        [TestCase("1/0", ExpectedResult = double.PositiveInfinity)] // division by zero

        /// <summary>
        /// Test expressions with normal cases.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        // TODO: Refactor to something more descriptive such as UnsupportedOperatorException

        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        [TestCase("2^3")]
        public void TestEvaluateUnsupportedOperator(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<UnsupportedOperatorException>());
        }

        [Test]

        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
        }

        [Test]
        /// <summary>
        /// Test expressions with variable values.
        /// </summary>
        public void TestExpressionsWithVariableValues()
        {
            ExpressionTree expression = new ExpressionTree("B1+7");
            expression.SetVariable("B1", 23.0);
            Assert.That(expression.Evaluate(), Is.EqualTo(30.0));

            expression = new ExpressionTree("B2+A1+3");
            expression.SetVariable("A1", 9);
            expression.SetVariable("B2", 1);
            Assert.That(expression.Evaluate(), Is.EqualTo(13));
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented