using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2.Methods
{
    internal class HW2Random
    {
        internal static List<int> fillList(List<int> numList)
        {
            var rand = new Random(); // instantiating random class
            for (int i = 0; i < 10000; i++)
            {
                numList.Add(rand.Next(20001)); // adding random integers to list
            }
            return numList;
        }
        
    }
}
