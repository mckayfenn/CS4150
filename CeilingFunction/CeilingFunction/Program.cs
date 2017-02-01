using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeilingFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tree> trees = new List<Tree>();

            int first = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Count; i++)
            {
                if (first != 0)
                {
                    Tree myTree = new Tree();
                    string[] numbers = line.Split(null);
                    //string[] numbers = testingData.ElementAt(i).Split(null);
                    foreach (string num in numbers)
                    {
                        myTree.insert(Int32.Parse(num));
                        
                    }

                    if (trees.Count == 0)
                    {
                        trees.Add(myTree);
                    }
                    else
                    {
                        foreach (Tree tree in trees)
                        {
                            if (myTree.equalsAnotherTree(tree))
                            {
                                myTree.sameAsOther = true;
                            }
                        }
                        trees.Add(myTree);
                    }
                }
                first++;
            }

            // Check if there are unique trees and remove them if they are
            for(int j = trees.Count - 1; j >= 0; j--)
            {
                Tree tree = trees.ElementAt(j);
                if(tree.sameAsOther == true)
                {
                    trees.Remove(tree);
                }
            }

           Console.Write(trees.Count);
            //Console.ReadLine();
        }
    }

    class Node
    {
        public int nodeValue;
        public Node left;
        public Node right;
    }

    class Tree
    {
        public Node root;
        public bool sameAsOther;

        public int leftCount;
        public int rightCount;

        public void insert(int value)
        {
            if (root == null)
            {
                //Console.WriteLine(value + ": created as the root node");
            }
            addNode(ref root, value);
        }

        public Node addNode(ref Node parent, int value)
        {
            if (parent == null)
            {
                parent = new Node();
                parent.nodeValue = value;
            }
            else if (value < parent.nodeValue)
            {
                //Console.WriteLine(value + ": created to the left of " + parent.nodeValue);
                leftCount++;
                parent.left = addNode(ref parent.left, value);
            }
            else
            {
                //Console.WriteLine(value + ": created to the right of " + parent.nodeValue);
                rightCount++;
                parent.right = addNode(ref parent.right, value);
            }
            return parent;
        }

        public bool equalsAnotherTree(Tree tree2)
        {
            return areEqualTrees(this.root, tree2.root);
        }

        private bool areEqualTrees(Node node1, Node node2)
        {
            // If the trees are empty then of course they are equal
            if (node1 == null && node2 == null)
                return true;

            // Check if branches are equal or not
            if ((node1 == null && node2 != null) || (node1 != null && node2 == null))
                return false;

            // else check recursively
            return areEqualTrees(node1.left, node2.left) && areEqualTrees(node1.right, node2.right);

        }
    }
}
