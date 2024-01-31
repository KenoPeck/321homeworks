using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2.Methods
{
    public class FixedStorageMethod
    {
        /// <summary>
        /// Determines the number of distinct integers in a list using a fixed storage method of a single long integer (O(1))
        /// </summary>
        /// <param name="numList"> HW2's numList of 10000 random integers from 0-20000 </param>
        /// <returns> returns a long integer representing the number of distinct integers in numList </returns>
        public static long Method2(List<int> numList)
        {
            long method2 = 0; // initializing count to 0
            for (int i = 0; i < numList.Count(); i++) // looping through each integer in list
            {
                for (int j = 0; j <= i || i == 0; j++) // looping through each integer before the ith index
                {
                    if (i == j) // if the number at i is unique
                    {
                        method2 += 1; // increment count by 1
                        break; // break out of the loop
                    }
                    if (numList[i] == numList[j]) // if the number at i has been counted before
                    {
                        break; // break out of the loop
                    }
                }
            }
            return method2;
        }
    }
}
