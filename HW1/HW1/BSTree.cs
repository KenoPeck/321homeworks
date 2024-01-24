using System; using HW1_BSTNode;


namespace HW1_BSTree
{
    internal class BSTree
    {
        // DEFAULT CONSTRUCTOR
        public BSTree()
        {
            root = null!; // setting root to null
            count = 0; // setting initial count to 0
        }

        // ROOT OF TREE
        private BSTNode? root;

        // NUMBER OF NODES IN TREE
        private int count;

        // NODE INSERTION METHOD
        public void Insert(int numInput)
        {
            if (root == null) // checking if tree is empty
            {
                root = new BSTNode(numInput); // creating root node
                count += 1; // adding root node to count
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
                    count += 1; // incrementing count
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
                    count += 1; // incrementing count
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
    }
}
