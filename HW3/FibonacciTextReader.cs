using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    internal class FibonacciTextReader : System.IO.TextReader
    {
        private int lines;
        public FibonacciTextReader(int maxLines)
        {
            lines = maxLines;
        }
        
        
        public override string ReadLine()
        {
            return "";
        }
    }
}
