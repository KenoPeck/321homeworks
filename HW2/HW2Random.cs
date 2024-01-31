using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2.Methods
{
    internal class HW2Random
    {
        /// <summary>
        /// Adds random integers to numList
        /// </summary>
        /// <param name="numList"> HW2's numList </param>
        /// <returns> list of 10000 random integers from 0-20000 </returns>
        internal static List<int> FillList(List<int> numList)
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
