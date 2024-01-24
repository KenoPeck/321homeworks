using System;


namespace HW1_BSTNode
{
    internal class BSTNode
    {
        // NUMBER VALUE OF NODE
        public int Num { get; }

        // LEFT CHILD OF NODE
        public BSTNode Left { get; set; }

        // RIGHT CHILD OF NODE
        public BSTNode Right { get; set; }

        // DEFAULT CONSTRUCTOR
        public BSTNode(int numInput)
        {
            Num = numInput; // setting number value of node to input
            Left = null!; // setting left child to null
            Right = null!; // setting right child to null
        }
    }
}
