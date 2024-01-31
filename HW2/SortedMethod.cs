using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2.Methods
{
    public class SortedMethod
    {
        /// <summary>
        /// Determines the number of distinct integers in a list using a sorted fixed storage (O(1)), O(n)) time complexity method
        /// </summary>
        /// <param name="inputNumList"> HW2's numList of 10000 random integers from 0-20000 </param>
        /// <returns> returns a long integer representing the number of distinct integers in numList </returns>
        public static long Method3(List<int> inputNumList)
        {
            long method3 = 0; // initializing count to 0
            List<int> numList = new List<int>(inputNumList); // creating copy of static list
            numList.Sort(); // sorting the list with built in sorting method
            int buffer = -1;
            for (int i = 0; i < numList.Count(); i++) // iterating through each integer in list
            {
                if (numList[i] != buffer) // if the number at i has not been seen before
                {
                    method3 += 1; // increment count by 1
                    buffer = numList[i]; // set buffer to the number at i
                }
            }
            return method3;
        }
    }
}
