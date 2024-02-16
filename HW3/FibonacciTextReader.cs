using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HW3
{
    public class FibonacciTextReader : System.IO.TextReader
    {
        private int max;
        private int lines;
        private BigInteger fibNum1;
        private BigInteger fibNum2;
        public FibonacciTextReader(int maxLines)
        {
            max = maxLines;
            lines = 0; // line counter for iterative labels in output
            fibNum1 = -1;
            fibNum2 = -1;
        }
        
        /// <summary>
        /// Single line of fibonacci number calculation
        /// </summary>
        /// <returns>
        /// returns a string value of the next fibonacci number, or null if the max number of lines has been reached
        /// </returns>
        public override string? ReadLine()
        {
            if (fibNum1 == -1) // 1st call
            {
                fibNum1 = 0;
                lines++;
                return "0";
            }
            else if (fibNum2 == -1) // second call
            {
                fibNum2 = 1;
                lines++;
                return "1";
            }
            else if (lines < max) // 3rd call and beyond
            {
                BigInteger temp = fibNum1;
                fibNum1 = fibNum2;
                fibNum2 = temp + fibNum1;
                lines++;
                return fibNum2.ToString();
            }
            else return null; // final call

        }

        /// <summary>
        /// wrapper function for readline that reads fibonacci lines until the max number of lines is reached
        /// </summary>
        /// <returns>
        /// returns a string value of all fibonacci numbers up to the max number of lines, with line numbers and newlines
        /// </returns>
        public override string ReadToEnd()
        {
            StringBuilder finalResult = new("");
            string? line;
            int i = 1;
            while (true)
            {
                line = ReadLine();
                if (line != null)
                {
                    finalResult.Append(i.ToString() + ": ");
                    finalResult.Append(line);
                    finalResult.Append(Environment.NewLine);
                    i++;
                }
                else
                {
                    break;
                }
            }
            return finalResult.ToString();
        }
    }
}
