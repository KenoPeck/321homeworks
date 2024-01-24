using System; using HW1_BSTNode;


namespace HW1_BSTree
{
    internal class BSTree
    {
        // DEFAULT CONSTRUCTOR
        public BSTree()
        {
            root = null!; // setting root to null
        }

        // ROOT OF TREE
        private BSTNode? root;

        // NODE INSERTION METHOD
        public void Insert(int numInput)
        {
            if (root == null) // checking if tree is empty
            {
                root = new BSTNode(numInput); // creating root node
            }
            else
            {
                InsertHelper(root, numInput); // calling private helper method
            }

        }

        // PRIVATE HELPER METHOD FOR NODE INSERTION
        private void InsertHelper(BSTNode? iRoot, int numInput)
        {
            if (numInput == iRoot.Num) // checking for duplicate number
            {
                Console.WriteLine($"{numInput} ignored to avoid duplicate");
            }
            else if (numInput < iRoot.Num)
            {
                if (iRoot.Left == null)
                {
                    iRoot.Left = new BSTNode(numInput); // setting left child to input number
                }
                else
                {
                    InsertHelper(iRoot.Left, numInput); // recursively calling self on left child
                }
            }
            else if (numInput > iRoot.Num)
            {
                if (iRoot.Right == null)
                {
                    iRoot.Right = new BSTNode(numInput); // setting right child to input number
                }
                else
                {
                    InsertHelper(iRoot.Right, numInput); // recursively calling self on right child
                }
            }
        }

        // INORDER TRAVERSAL METHOD
        public void InOrderTraversal()
        {
            if (root == null) // checking if tree is empty
            {
                Console.WriteLine("Error: Tree empty");
            }
            else
            {
                InOrderTraversalHelper(root); // calling private helper method
            }
        }

        // PRIVATE HELPER METHOD FOR INORDER TRAVERSAL
        private void InOrderTraversalHelper(BSTNode? iRoot)
        {
            if (iRoot.Left != null) // checking if left child exists
            {
                InOrderTraversalHelper(iRoot.Left); // recursively calling self on left child
            }
            Console.WriteLine(iRoot.Num); // printing num value of current node
            if (iRoot.Right != null)
            {
                InOrderTraversalHelper(iRoot.Right); // recursively calling self on right child
            }
        }

        // NODE COUNT METHOD
        public int Count()
        {
            if (root == null) // checking if tree is empty
            {
                return 0;
            }
            else
            {
                int count = CountHelper(root); // calling private helper method
                return count + 1; // adding root to count
            }
        }

        // PRIVATE HELPER METHOD FOR LEVEL COUNT
        private int CountHelper(BSTNode? cRoot)
        {
            int count = 0;
            if (cRoot.Left != null) // checking if left child exists
            {
                count += 1; // incrementing count
                count += CountHelper(cRoot.Left); // recursively calling self on left child and incrementing count by result
            }
            if (cRoot.Right != null)
            {
                count += 1; // incrementing count
                count += CountHelper(cRoot.Right); // recursively calling self on right child and incrementing count by result
            }
            return count;
        }

        // LEVEL COUNT METHOD
        public void Levels()
        {
            if (root == null) // checking if tree is empty
            {
                Console.WriteLine("Tree has 0 levels");
            }
            else
            {
                int levels = LevelsHelper(root, 1); // calling private helper method
                Console.WriteLine($"Tree has {levels} levels");
            }
        }

        // PRIVATE HELPER METHOD FOR LEVEL COUNT
        private int LevelsHelper(BSTNode? iRoot, int numLevels)
        {
            int lMax = numLevels; int rMax = numLevels;
            if (iRoot.Left != null) // checking if left child exists
            {
                lMax = LevelsHelper(iRoot.Left, (numLevels + 1)); // recursively calling self on left child and incrementing numLevels
            }
            if (iRoot.Right != null) // checking if right child exists
            {
                rMax = LevelsHelper(iRoot.Right, (numLevels + 1)); // recursively calling self on right child and incrementing numLevels
            }
            return Math.Max(lMax, rMax); // returning max of left and right child level values
        }


        // THEORETICAL MINIMUM LEVEL COUNT METHOD
        public void MinLevels()
        {
            int minLevels = (int)Math.Ceiling(Math.Log2(Count()+1)); // taking second log of count + 1 and rounding up, then explicitly casting to int
            Console.WriteLine($"Theoretical minimum number of levels is {minLevels}");
        }
    }
}
