// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp
{
    using ExpressionTree;

    /// <summary>
    /// Console app Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Accepts user input and displays the result of the expression.
        /// </summary>
        public static void Main()
        {
            string? expression = string.Empty; // string to store expression input.
            string? variableName; // string to store variable name input.
            string? variableValue; // string to store variable value input.
            string? userInput; // string to store user input.
            ExpressionTree expressionTree = new ExpressionTree("A1 + B1 + C1"); // expression tree object for storing actual expression.
            while (true) // loop for accepting user input and displaying result.
            {
                Console.WriteLine($"Menu (Current Expression = \"{expression}\")");
                Console.WriteLine("     1 = Enter a new Expression");
                Console.WriteLine("     2 = Set Variable");
                Console.WriteLine("     3 = Evaluate Tree");
                Console.WriteLine("     4 = Quit");
                userInput = Console.ReadLine(); // get user input.

                switch (userInput)
                {
                    case "1": // case for entering new expression.
                        Console.Write("Enter expression: ");
                        expression = Console.ReadLine();
                        if (expression == null)
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }
                        else
                        {
                            try
                            {
                                expressionTree = new ExpressionTree(expression); // create new expression tree object for new expression.
                                break;
                            }
                            catch (UnsupportedOperatorException)
                            {
                                Console.WriteLine("Unsupported Operator!");
                                break;
                            }
                        }

                    case "2": // case for setting variable.
                        Console.WriteLine("Enter variable name: ");
                        variableName = Console.ReadLine();
                        Console.WriteLine("Enter variable value: ");
                        variableValue = Console.ReadLine();
                        if (variableName == null)
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }
                        else if (variableValue == null || variableValue == string.Empty)
                        {
                            expressionTree.SetVariable(variableName, 0); // default variable value of 0 if empty.
                            break;
                        }
                        else
                        {
                            try
                            {
                                double double_val = double.Parse(variableValue); // parse variable value to double.
                                expressionTree.SetVariable(variableName, double_val); // add variable to variables dictionary.
                                break;
                            }
                            catch (FormatException) // catch exception if variable value is not a valid double.
                            {
                                Console.WriteLine("Invalid Variable Value!");
                                break;
                            }
                        }

                    case "3": // case for evaluating expression.
                        Console.Write("Result: ");
                        Console.WriteLine(expressionTree.Evaluate().ToString("0.###")); // display result of expression.
                        break;
                    case "4": // case for quitting the program.
                        return;
                    default: // default case for invalid input.
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
}
