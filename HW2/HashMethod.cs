using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Immutable;

namespace HW2.Methods
{
    
    public class HashMethod
    {
        /// <summary>
        /// Determines the number of distinct integers in a list using a hashset
        /// </summary>
        /// <param name="numList"> HW2's numList of 10000 random integers from 0-20000 </param>
        /// <returns> returns a long integer representing the number of distinct integers in numList </returns>
        public static long Method1(List<int> numList)
        {
            var hashSet = numList.ToImmutableHashSet<int>(); // creating hashset from integer list
            long method1 = hashSet.Count(); // getting count of distinct integers from hash set
            return method1;
        }
    }
}
