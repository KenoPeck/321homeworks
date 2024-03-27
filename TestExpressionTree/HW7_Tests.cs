// <copyright file="HW7_Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Tests have documentation above each test set which weren't being detected by stylecop
#pragma warning disable SA1600 // Elements should be documented

namespace HW7.Tests
{
    using System.Globalization;
    using System.Linq.Expressions;
    using ExpressionTree;
    using SpreadsheetEngine;

    [TestFixture]
    public class HW7_Tests
    {
        private Form1 testForm = new Form1();

        [Test]
        public void TestDisplayValue()
        {
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1+5";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("10"));
        }

        [Test]
        public void TestDisplayText()
        {
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "1";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Text, Is.EqualTo("1"));
        }

        // ExpressionTree Tests ----------------------------------------------------------------------------------------------------

        [Test]
        [TestCase("11+22", ExpectedResult = "11 22 + ")] // addition expression
        [TestCase("22-11", ExpectedResult = "22 11 - ")] // subtraction expression
        [TestCase("3+3-3", ExpectedResult = "3 3 + 3 - ")] // addition and substraction mixed expression
        [TestCase("3+3*3", ExpectedResult = "3 3 3 * + ")] // addition and multiplication mixed expression
        [TestCase("11*22", ExpectedResult = "11 22 * ")] // multiplication expression
        [TestCase("11*22 + 5", ExpectedResult = "11 22 * 5 + ")] // multiplication and addditon mixed expression
        [TestCase("22/11", ExpectedResult = "22 11 / ")] // division expression
        [TestCase("A+1", ExpectedResult = "A 1 + ")] // Variable addition expression
        [TestCase("A+B", ExpectedResult = "A B + ")] // Pure Variable addition expression
        [TestCase("A+B*C", ExpectedResult = "A B C * + ")] // Pure Variable addition and multiplication expression
        [TestCase("A-B+C", ExpectedResult = "A B C + - ")] // Pure Variable subtraction and addition expression
        [TestCase("A*B+C", ExpectedResult = "A B * C + ")] // Pure Variable multiplication and addition expression
        [TestCase("A*(B+C)", ExpectedResult = "A B C + * ")] // Pure Variable multiplication and addition expression with parentheses
        [TestCase("A* (B + C* D) + E", ExpectedResult = "A B C D * + * E + ")] // mixed expression with parentheses

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
        [TestCase("5/2", ExpectedResult = 2.5)] // division expression
        [TestCase("5-22/11", ExpectedResult = 3.0)] // mixed (-,/) expression
        [TestCase("(10-6/2)/2", ExpectedResult = 3.5)] // mixed (-,/) expression with parentheses
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

        [Test]
        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        [TestCase("2%3")]
        [TestCase("2^3")]
        public void TestEvaluateUnsupportedOperator(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<UnsupportedOperatorException>());
        }

        [Test]
        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        [TestCase("(()")]
        [TestCase("3+(9+(5+44/(6+2))")]
        [TestCase("5+(3/7))")]
        public void TestUnequalParentheses(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<System.Exception>());
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
        /// Test expression with variable value.
        /// </summary>
        public void TestExpressionWithVariableValue()
        {
            ExpressionTree expression = new ExpressionTree("B1+7");
            expression.SetVariable("B1", 23.0);
            Assert.That(expression.Evaluate(), Is.EqualTo(30.0));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestAdditionWith2VariableValues()
        {
            ExpressionTree expression = new ExpressionTree("B2+A1+3");
            expression.SetVariable("A1", 9);
            expression.SetVariable("B2", 1);
            Assert.That(expression.Evaluate(), Is.EqualTo(13));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestMixedWith2VariableValues()
        {
            ExpressionTree expression = new ExpressionTree("(B2+5)*A1+3");
            expression.SetVariable("A1", 1);
            expression.SetVariable("B2", 2);
            Assert.That(expression.Evaluate(), Is.EqualTo(10));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestExpressionWith3VariableValues()
        {
            ExpressionTree expression = new ExpressionTree("(A1+B2)/CD55");
            expression.SetVariable("A1", 11.5);
            expression.SetVariable("B2", 9.5);
            expression.SetVariable("CD55", 2);
            Assert.That(expression.Evaluate(), Is.EqualTo(10.5));
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented