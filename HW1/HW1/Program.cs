using System;
using HW1_BSTree;

namespace HW1
{ 
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                BSTree tree = new BSTree(); // creating a new tree
                Console.WriteLine("Enter a unique list of numbers between 0-100 separated by spaces");
                Console.WriteLine("Or enter q to quit:");
                string? userInput = Console.ReadLine(); // reading user input
                if (userInput == "q") // checking if user wants to quit
                {
                    break;
                }
                if (userInput != "") // checking that user input is not empty before splitting
                {
                    char delimiter = ' ';
                    string[] tokens = userInput.Split(delimiter); // splitting input string into tokens
                    foreach (string token in tokens)
                    {
                        tree.Insert(int.Parse(token)); // inserting each token into tree
                    }
                }
                tree.InOrderTraversal(); // displaying tree in sorted order
                tree.Count(); // displaying number of nodes in tree
                tree.Levels(); // displaying number of levels in tree
                tree.MinLevels(); // displaying theoretical minimum number of levels tree could have
                Console.WriteLine("\n");
            } while (true); // running until user quits
        }
    }


}