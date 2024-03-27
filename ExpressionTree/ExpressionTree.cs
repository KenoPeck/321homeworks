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
    public class ExpressionTree : OperatorNode
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
        private StringBuilder postfixExp = new StringBuilder(string.Empty);

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
            for (int i = 0; i < expression.Length; i++)
            {
                while (i < expression.Length && char.IsWhiteSpace(expression[i])) // skipping whitespace
                {
                    i++;
                }

                if (char.IsLetter(expression[i])) // if character is part of a variable ----------------------------------------------------------------------------------
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

                    i--; // decrement i to account for increment in for loop
                    this.postfixExp.Append(variableName.ToString() + " "); // add variable to postfix expression
                }
                else if (char.IsNumber(expression[i])) // if character is a constant ----------------------------------------------------------------------------------
                {
                    StringBuilder constant = new StringBuilder();
                    while (i < expression.Length && (char.IsNumber(expression[i]) || expression[i] == '.'))
                    {
                        constant.Append(expression[i]);
                        i++;
                    }

                    i--; // decrement i to account for increment in for loop
                    this.postfixExp.Append(constant.ToString() + " "); // add variable to postfix expression
                }
                else if (expression[i] == '(') // if character is a left parenthesis ----------------------------------------------------------------------------------
                {
                    this.treeStack.Push('('); // push left parenthesis to stack
                }
                else if (expression[i] == ')') // if character is a right parenthesis ----------------------------------------------------------------------------------
                {
                    while (this.treeStack.Count > 0 && this.treeStack.Peek() != '(') // while top of stack is not left parenthesis
                    {
                        this.postfixExp.Append(this.treeStack.Pop() + " "); // pop stack symbols and add to postfix expression
                    }

                    if (this.treeStack.Count > 0 && this.treeStack.Peek() == '(') // if top of stack is left parenthesis
                    {
                        this.treeStack.Pop(); // pop left parenthesis
                    }
                    else
                    {
                        throw new Exception("Invalid Parentheses!"); // throw exception if parentheses are invalid
                    }
                }
                else if (factory.IsOperator(expression[i])) // if character is an operator ----------------------------------------------------------------------------------
                {
                    if (this.treeStack.Count == 0 || this.treeStack.Peek() == '(') // if stack is empty or contains left parenthesis on top
                    {
                        this.treeStack.Push(expression[i]); // push operator to stack
                    }
                    else if (factory.GetPrecedence(expression[i]) > factory.GetPrecedence(this.treeStack.Peek())) // if operator has higher precedence than top of stack
                    {
                        this.treeStack.Push(expression[i]); // push operator to stack
                    }
                    else if (factory.GetPrecedence(expression[i]) <= factory.GetPrecedence(this.treeStack.Peek())) // if operator has equal or lower precedence than top of stack
                    {
                        while (this.treeStack.Count > 0 && factory.GetPrecedence(expression[i]) <= factory.GetPrecedence(this.treeStack.Peek())) // while equal or lower than top of stack
                        {
                            this.postfixExp.Append(this.treeStack.Pop() + " "); // pop stack symbols and add to postfix expression
                        }

                        this.treeStack.Push(expression[i]); // push operator to stack
                    }
                }
                else
                {
                    throw new UnsupportedOperatorException($"\n{expression[i]} is not a supported operator!\n");
                }
            }

            while (this.treeStack.Count > 0) // while stack is not empty
            {
                if (this.treeStack.Peek() == '(') // if stack still contains parentheses
                {
                    throw new Exception("Invalid Parentheses!"); // throw exception if parentheses are still on stack
                }

                this.postfixExp.Append(this.treeStack.Pop() + " "); // add all operators on stack to postfix expression
            }

            string postfix = this.postfixExp.ToString();
            Stack<OperatorNode> nodeStack = new Stack<OperatorNode>();

            for (int i = 0; i < postfix.Length - 1; i++)
            {
                if (char.IsWhiteSpace(postfix[i])) // skip whitespace
                {
                    continue;
                }
                else if (factory.IsOperator(postfix[i]) == true) // if character is an operator
                {
                    if (nodeStack.Count > 1)
                    {
                        OperatorNode temp = factory.CreateOperatorNode(postfix[i]);
                        temp.Right = nodeStack.Pop();
                        temp.Left = nodeStack.Pop();
                        nodeStack.Push(temp);
                    }
                    else
                    {
                        throw new Exception("Invalid Expression!");
                    }
                }
                else if (char.IsLetter(postfix[i])) // if character is beginning of a variable
                {
                    StringBuilder elementName = new StringBuilder();
                    while (i < postfix.Length - 1 && char.IsLetter(postfix[i]))
                    {
                        elementName.Append(postfix[i]);
                        i++;
                    }

                    if (i < postfix.Length - 1 && char.IsNumber(postfix[i]))
                    {
                        while (i < postfix.Length - 1 && char.IsNumber(postfix[i]))
                        {
                            elementName.Append(postfix[i]);
                            i++;
                        }
                    }

                    i--;
                    this.variables[elementName.ToString()] = 0;
                    OperatorNode temp = factory.CreateOperatorNode('+');
                    temp.Left = new VariableNode(elementName.ToString(), 0);
                    temp.Right = new ConstantNode(0);
                    nodeStack.Push(temp);
                }
                else if (char.IsNumber(postfix[i])) // if character is beginning of a constant
                {
                    StringBuilder constant = new StringBuilder();
                    while (i < postfix.Length - 1 && (char.IsNumber(postfix[i]) || postfix[i] == '.'))
                    {
                        constant.Append(postfix[i]);
                        i++;
                    }

                    i--;
                    OperatorNode temp = factory.CreateOperatorNode('+');
                    temp.Left = new ConstantNode(double.Parse(constant.ToString()));
                    temp.Right = new ConstantNode(0);
                    nodeStack.Push(temp);
                }
                else // if character is not a valid character
                {
                    throw new Exception("Invalid Expression!");
                }
            }

            if (nodeStack.Count > 1 || nodeStack.Count == 0) // if stack is empty or contains more than one node
            {
                throw new Exception("Invalid Expression!");
            }
            else
            {
                this.root = nodeStack.Pop(); // set root of tree
            }
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
        public override double Evaluate()
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
