using System.Collections.Immutable;
using System.Diagnostics;
using HW2.Methods;


namespace HW2.Forms
{
    public partial class HW2Form : Form
    {
        internal static string? outputString;
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


        // RUNDISTINCTINTEGERS METHOD
        // RUNS 3 METHODS TO COUNT DISTINCT INTEGERS AND PUTS RESULTS IN OUTPUTSTRING
        private static void RunDistinctIntegers()
        {
            // OUTPUT: -------------------------------------------------------------------------------------------------------
            outputString = $"1. HASHSET METHOD: {HashMethod.Method1(numList)} unique numbers\n"
                           + $"    HASHSET METHOD TIME COMPLEXITY: O(n)\n"
                           + "    Reasoning: C# List has O(1) access, and C# HashSet has O(1) access, so the conversion between list to hashset is O(n),\n    and then the hashset count property is also O(1), making the full method O(n)\n"
                           + $"2. O(1) STORAGE METHOD: {0} unique numbers\n"
                           + $"3. SORTED METHOD: {0} unique numbers";

        }

    }
}
