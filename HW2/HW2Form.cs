using System.Collections.Immutable;
using System.Diagnostics;
using HW2.Methods;


namespace HW2.Forms
{
    public partial class HW2Form : Form
    {
        /// <summary>
        /// string used for output in winform text box
        /// </summary>
        internal static string? outputString;
        /// <summary>
        /// integer list used for storing random integers from 0-20000
        /// </summary>
        public static List<int> numList;

        // CONSTRUCTOR
        public HW2Form()
        {
            InitializeComponent();

            numList = new List<int>(); // creating empty list
            HW2Random.fillList(numList); // filling list with random integers

            RunDistinctIntegers();
            outputString = outputString.Replace("\n", Environment.NewLine); // converting newlines for winform textbox
            outputBox.Text = outputString; // displaying output string in textbox
        }


        /// <summary>
        /// Determines number of distinct integers in numList using 3 different methods
        /// </summary>
        private static void RunDistinctIntegers()
        {
            // OUTPUT: -------------------------------------------------------------------------------------------------------
            outputString = $"1. HASHSET METHOD: {HashMethod.Method1(numList)} unique numbers\n"
                           + $"    HASHSET METHOD TIME COMPLEXITY: O(n)\n"
                           + "    Reasoning: C# List has O(1) access, and C# HashSet has O(1) access, so the conversion between list to hashset is O(n),\n    and then the hashset count property is also O(1), making the full method O(n)\n"
                           + $"2. O(1) STORAGE METHOD: {FixedStorageMethod.Method2(numList)} unique numbers\n"
                           + $"3. SORTED METHOD: {0} unique numbers";

        }

    }
}
