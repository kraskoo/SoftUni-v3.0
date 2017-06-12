namespace BasicTree.Trees.BinaryTree
{
    using System;

    public class BinaryTree<T>
    {
        public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public void PrintIndentedPreOrder(int indent = 0)
        {
            Console.Write(new string(' ', 2 * indent));
            Console.WriteLine(this.Value);
            this.LeftChild?.PrintIndentedPreOrder(indent + 1);
            this.RightChild?.PrintIndentedPreOrder(indent + 1);
        }

        public void EachInOrder(Action<T> action)
        {
            this.LeftChild?.EachInOrder(action);
            action(this.Value);
            this.RightChild?.EachInOrder(action);
        }

        public void EachPostOrder(Action<T> action)
        {
            this.LeftChild?.EachPostOrder(action);
            this.RightChild?.EachPostOrder(action);
            action(this.Value);
        }
    }
}