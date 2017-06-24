namespace Problem01_TextEditor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BigList<T> : IEnumerable<T>
    {
        private static readonly int[] Fibonacci = new int[46]
        {
            1,
            2,
            3,
            5,
            8,
            13,
            21,
            34,
            55,
            89,
            144,
            233,
            377,
            610,
            987,
            1597,
            2584,
            4181,
            6765,
            10946,
            17711,
            28657,
            46368,
            75025,
            121393,
            196418,
            317811,
            514229,
            832040,
            1346269,
            2178309,
            3524578,
            5702887,
            9227465,
            14930352,
            24157817,
            39088169,
            63245986,
            102334155,
            165580141,
            267914296,
            433494437,
            701408733,
            1134903170,
            1836311903,
            int.MaxValue
        };

        private const uint MAXITEMS = 2147483646;
        private const int MAXLEAF = 120;
        private const int BALANCEFACTOR = 6;
        private const int MAXFIB = 44;
        private Node root;
        private int changeStamp;

        public int Count
        {
            get
            {
                if (root == null)
                {
                    return 0;
                }

                return root.Count;
            }
        }

        public T this[int index]
        {
            get
            {
                if (root == null || index < 0 || index >= root.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                Node node = root;
                for (ConcatNode concatNode = node as ConcatNode; concatNode != null; concatNode = node as ConcatNode)
                {
                    int count = concatNode.left.Count;
                    if (index < count)
                    {
                        node = concatNode.left;
                    }
                    else
                    {
                        node = concatNode.right;
                        index -= count;
                    }
                }
                return ((LeafNode)node).items[index];
            }
            set
            {
                if (root == null || index < 0 || index >= root.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                StopEnumerations();
                if (root.Shared)
                {
                    root = root.SetAt(index, value);
                }

                Node node = root;
                for (ConcatNode concatNode = node as ConcatNode; concatNode != null; concatNode = node as ConcatNode)
                {
                    int count = concatNode.left.Count;
                    if (index < count)
                    {
                        node = concatNode.left;
                        if (node.Shared)
                        {
                            concatNode.left = node.SetAt(index, value);
                            return;
                        }
                    }
                    else
                    {
                        node = concatNode.right;
                        index -= count;
                        if (node.Shared)
                        {
                            concatNode.right = node.SetAt(index, value);
                            return;
                        }
                    }
                }
                
                ((LeafNode)node).items[index] = value;
            }
        }

        public BigList()
        {
            root = (Node)null;
        }

        public BigList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            root = NodeFromEnumerable(collection);
            CheckBalance();
        }

        public BigList(IEnumerable<T> collection, int copies)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            root = NCopiesOfNode(copies, NodeFromEnumerable(collection));
            CheckBalance();
        }

        public BigList(BigList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.root == null)
            {
                root = (Node)null;
            }
            else
            {
                list.root.MarkShared();
                root = list.root;
            }
        }

        public BigList(BigList<T> list, int copies)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.root == null)
            {
                root = (Node)null;
            }
            else
            {
                list.root.MarkShared();
                root = NCopiesOfNode(copies, list.root);
            }
        }

        private BigList(Node node)
        {
            root = node;
            CheckBalance();
        }

        public static BigList<T> operator +(BigList<T> first, BigList<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if ((uint)(first.Count + second.Count) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            if (first.Count == 0)
            {
                return second.Clone();
            }

            if (second.Count == 0)
            {
                return first.Clone();
            }

            BigList<T> bigList = new BigList<T>(first.root.Append(second.root, false));
            bigList.CheckBalance();
            return bigList;
        }

        private void StopEnumerations()
        {
            changeStamp = changeStamp + 1;
        }

        private void CheckEnumerationStamp(int startStamp)
        {
            if (startStamp != changeStamp)
            {
                throw new InvalidOperationException();
            }
        }

        public void Clear()
        {
            StopEnumerations();
            root = (Node) null;
        }

        public void Insert(int index, T item)
        {
            StopEnumerations();
            if ((uint)(Count + 1) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            if (index <= 0 || index >= Count)
            {
                if (index == 0)
                {
                    AddToFront(item);
                }
                else
                {
                    if (index != Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    Add(item);
                }
            }
            else if (root == null)
            {
                root = (Node)new LeafNode(item);
            }
            else
            {
                Node node = root.InsertInPlace(index, item);
                if (node == root)
                {
                    return;
                }

                root = node;
                CheckBalance();
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            StopEnumerations();
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index <= 0 || index >= Count)
            {
                if (index == 0)
                {
                    AddRangeToFront(collection);
                }
                else
                {
                    if (index != Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    AddRange(collection);
                }
            }
            else
            {
                Node node1 = NodeFromEnumerable(collection);
                if (node1 == null)
                {
                    return;
                }

                if (root == null)
                {
                    root = node1;
                }
                else
                {
                    if ((uint)(Count + node1.Count) > 2147483646U)
                    {
                        throw new InvalidOperationException();
                    }

                    Node node2 = root.InsertInPlace(index, node1, true);
                    if (node2 == root)
                    {
                        return;
                    }

                    root = node2;
                    CheckBalance();
                }
            }
        }

        public void InsertRange(int index, BigList<T> list)
        {
            StopEnumerations();
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if ((uint)(Count + list.Count) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            if (index <= 0 || index >= Count)
            {
                if (index == 0)
                {
                    AddRangeToFront(list);
                }
                else
                {
                    if (index != Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    AddRange(list);
                }
            }
            else
            {
                if (list.Count == 0)
                {
                    return;
                }

                if (root == null)
                {
                    list.root.MarkShared();
                    root = list.root;
                }
                else
                {
                    if (list.root == root)
                    {
                        root.MarkShared();
                    }

                    Node node = root.InsertInPlace(index, list.root, false);
                    if (node == root)
                    {
                        return;
                    }

                    root = node;
                    CheckBalance();
                }
            }
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            if (count == 0)
            {
                return;
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || count > Count - index)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            StopEnumerations();
            Node root = this.root;
            int first = index;
            int num = count;
            int last = first + num - 1;
            Node node = root.RemoveRangeInPlace(first, last);
            if (node == this.root)
            {
                return;
            }

            this.root = node;
            CheckBalance();
        }

        public void Add(T item)
        {
            if ((uint)(Count + 1) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            StopEnumerations();
            if (root == null)
            {
                root = (Node)new LeafNode(item);
            }
            else
            {
                Node node = root.AppendInPlace(item);
                if (node == root)
                {
                    return;
                }

                root = node;
                CheckBalance();
            }
        }

        public void AddToFront(T item)
        {
            if ((uint)(Count + 1) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            StopEnumerations();
            if (root == null)
            {
                root = (Node)new LeafNode(item);
            }
            else
            {
                Node node = root.PrependInPlace(item);
                if (node == root)
                {
                    return;
                }

                root = node;
                CheckBalance();
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            StopEnumerations();
            Node node1 = NodeFromEnumerable(collection);
            if (node1 == null)
            {
                return;
            }

            if (root == null)
            {
                root = node1;
                CheckBalance();
            }
            else
            {
                if ((uint)(Count + node1.count) > 2147483646U)
                {
                    throw new InvalidOperationException();
                }

                Node node2 = root.AppendInPlace(node1, true);
                if (node2 == root)
                {
                    return;
                }

                root = node2;
                CheckBalance();
            }
        }

        public void AddRangeToFront(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            StopEnumerations();
            Node node1 = NodeFromEnumerable(collection);
            if (node1 == null)
            {
                return;
            }

            if (root == null)
            {
                root = node1;
                CheckBalance();
            }
            else
            {
                if ((uint)(Count + node1.Count) > 2147483646U)
                {
                    throw new InvalidOperationException();
                }

                Node node2 = root.PrependInPlace(node1, true);
                if (node2 == root)
                {
                    return;
                }

                root = node2;
                CheckBalance();
            }
        }

        public BigList<T> Clone()
        {
            if (root == null)
            {
                return new BigList<T>();
            }

            root.MarkShared();
            return new BigList<T>(root);
        }

        public void AddRange(BigList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if ((uint)(Count + list.Count) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            if (list.Count == 0)
            {
                return;
            }

            StopEnumerations();
            if (root == null)
            {
                list.root.MarkShared();
                root = list.root;
            }
            else
            {
                Node node = root.AppendInPlace(list.root, false);
                if (node == root)
                {
                    return;
                }

                root = node;
                CheckBalance();
            }
        }

        public void AddRangeToFront(BigList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if ((uint)(Count + list.Count) > 2147483646U)
            {
                throw new InvalidOperationException();
            }

            if (list.Count == 0)
            {
                return;
            }

            StopEnumerations();
            if (root == null)
            {
                list.root.MarkShared();
                root = list.root;
            }
            else
            {
                Node node = root.PrependInPlace(list.root, false);
                if (node == root)
                {
                    return;
                }

                root = node;
                CheckBalance();
            }
        }

        public BigList<T> GetRange(int index, int count)
        {
            if (count == 0)
            {
                return new BigList<T>();
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || count > Count - index)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            Node root = this.root;
            int first = index;
            int num = count;
            int last = first + num - 1;
            return new BigList<T>(root.Subrange(first, last));
        }

        public IList<T> Range(int index, int count)
        {
            if (index < 0 || index > Count || index == Count && count != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || count > Count || count + index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return (IList<T>)new BigListRange(this, index, count);
        }

        private IEnumerator<T> GetEnumerator(int start, int maxItems)
        {
            int startStamp = changeStamp;
            if (root != null && maxItems > 0)
            {
                ConcatNode[] stack = new ConcatNode[root.Depth];
                bool[] leftStack = new bool[root.Depth];
                int stackPtr = 0;
                int startIndex = 0;
                Node current = root;
                ConcatNode currentConcat;
                if (start != 0)
                {
                    if (start < 0 || start >= root.Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(start));
                    }

                    currentConcat = current as ConcatNode;
                    startIndex = start;
                    for (; currentConcat != null; currentConcat = current as ConcatNode)
                    {
                        stack[stackPtr] = currentConcat;
                        int count = currentConcat.left.Count;
                        if (startIndex < count)
                        {
                            leftStack[stackPtr] = true;
                            current = currentConcat.left;
                        }
                        else
                        {
                            leftStack[stackPtr] = false;
                            current = currentConcat.right;
                            startIndex -= count;
                        }
                        ++stackPtr;
                    }
                }
                label_12:
                for (; (currentConcat = current as ConcatNode) != null; current = currentConcat.left)
                {
                    stack[stackPtr] = currentConcat;
                    leftStack[stackPtr] = true;
                    ++stackPtr;
                }
                LeafNode currentLeaf = (LeafNode)current;
                int limit = currentLeaf.Count;
                if (limit > startIndex + maxItems)
                {
                    limit = startIndex + maxItems;
                }

                for (int i = startIndex; i < limit; ++i)
                {
                    yield return currentLeaf.items[i];
                    CheckEnumerationStamp(startStamp);
                }
                maxItems -= limit - startIndex;
                if (maxItems > 0)
                {
                    startIndex = 0;
                    while (stackPtr != 0)
                    {
                        ConcatNode[] concatNodeArray = stack;
                        int num = stackPtr - 1;
                        stackPtr = num;
                        int index = num;
                        ConcatNode concatNode = concatNodeArray[index];
                        if (leftStack[stackPtr])
                        {
                            leftStack[stackPtr] = false;
                            ++stackPtr;
                            current = concatNode.right;
                            goto label_12;
                        }
                        else
                        {
                            current = (Node)concatNode;
                        }
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator(0, int.MaxValue);
        }

        private Node NodeFromEnumerable(IEnumerable<T> collection)
        {
            Node node = (Node)null;
            IEnumerator<T> enumerator = collection.GetEnumerator();
            LeafNode leafNode;
            while ((leafNode = LeafFromEnumerator(enumerator)) != null)
            {
                if (node == null)
                {
                    node = (Node)leafNode;
                }
                else
                {
                    if ((uint)(node.count + leafNode.count) > 2147483646U)
                    {
                        throw new InvalidOperationException();
                    }

                    node = node.AppendInPlace((Node)leafNode, true);
                }
            }
            return node;
        }

        private LeafNode LeafFromEnumerator(IEnumerator<T> enumerator)
        {
            int count = 0;
            T[] newItems = (T[])null;
            for (; count < 120 && enumerator.MoveNext(); newItems[count++] = enumerator.Current)
            {
                if (count == 0)
                {
                    newItems = new T[120];
                }
            }
            if (newItems != null)
            {
                return new LeafNode(count, newItems);
            }

            return (LeafNode)null;
        }

        private Node NCopiesOfNode(int copies, Node node)
        {
            if (copies < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(copies));
            }

            if (copies == 0 || node == null)
            {
                return (Node)null;
            }

            if (copies == 1)
            {
                return node;
            }

            if ((long)copies * (long)node.count > 2147483646L)
            {
                throw new InvalidOperationException();
            }

            int num1 = 1;
            Node node1 = node;
            Node node2 = (Node)null;
            while (copies > 0)
            {
                node1.MarkShared();
                if ((copies & num1) != 0)
                {
                    copies -= num1;
                    node2 = node2 != null ? node2.Append(node1, false) : node1;
                }
                num1 *= 2;
                Node node3 = node1;
                int num2 = 0;
                node1 = node3.Append(node3, num2 != 0);
            }
            return node2;
        }

        private void CheckBalance()
        {
            if (root == null || root.Depth <= 6 || root.Depth - 6 <= 44 && Count >= Fibonacci[root.Depth - 6])
            {
                return;
            }

            Rebalance();
        }

        internal void Rebalance()
        {
            if (root == null || root.Depth <= 1 || root.Depth - 2 <= 44 && Count >= Fibonacci[root.Depth - 2])
            {
                return;
            }

            int length = 0;
            while (length <= 44 && root.Count >= Fibonacci[length])
            {
                ++length;
            }

            Node[] rebalanceArray = new Node[length];
            AddNodeToRebalanceArray(rebalanceArray, root, false);
            Node node1 = (Node)null;
            for (int index = 0; index < length; ++index)
            {
                Node node2 = rebalanceArray[index];
                if (node2 != null)
                {
                    if (node1 == null)
                    {
                        node1 = node2;
                    }
                    else
                    {
                        Node node3 = node1;
                        Node node4 = node2;
                        int num = !node4.Shared ? 1 : 0;
                        node1 = node3.PrependInPlace(node4, num != 0);
                    }
                }
            }
            root = node1;
        }

        private void AddNodeToRebalanceArray(Node[] rebalanceArray, Node node, bool shared)
        {
            if (node.Shared)
            {
                shared = true;
            }

            if (node.IsBalanced())
            {
                if (shared)
                {
                    node.MarkShared();
                }

                AddBalancedNodeToRebalanceArray(rebalanceArray, node);
            }
            else
            {
                ConcatNode concatNode = (ConcatNode)node;
                AddNodeToRebalanceArray(rebalanceArray, concatNode.left, shared);
                AddNodeToRebalanceArray(rebalanceArray, concatNode.right, shared);
            }
        }

        private void AddBalancedNodeToRebalanceArray(Node[] rebalanceArray, Node balancedNode)
        {
            Node node1 = (Node)null;
            int count = balancedNode.Count;
            int index;
            for (index = 0; count >= Fibonacci[index + 1]; ++index)
            {
                Node rebalance = rebalanceArray[index];
                if (rebalance != null)
                {
                    rebalanceArray[index] = (Node)null;
                    if (node1 == null)
                    {
                        node1 = rebalance;
                    }
                    else
                    {
                        Node node2 = node1;
                        Node node3 = rebalance;
                        int num = !node3.Shared ? 1 : 0;
                        node1 = node2.PrependInPlace(node3, num != 0);
                    }
                }
            }
            if (node1 != null)
            {
                Node node2 = balancedNode;
                Node node3 = node1;
                int num = !node3.Shared ? 1 : 0;
                balancedNode = node2.PrependInPlace(node3, num != 0);
            }
            while (true)
            {
                Node rebalance = rebalanceArray[index];
                if (rebalance != null)
                {
                    rebalanceArray[index] = (Node)null;
                    Node node2 = balancedNode;
                    Node node3 = rebalance;
                    int num = !node3.Shared ? 1 : 0;
                    balancedNode = node2.PrependInPlace(node3, num != 0);
                }
                if (balancedNode.Count >= Fibonacci[index + 1])
                {
                    ++index;
                }
                else
                {
                    break;
                }
            }
            rebalanceArray[index] = balancedNode;
        }

        private abstract class Node
        {
            public int count;
            protected volatile bool shared;
            
            public int Count
            {
                get
                {
                    return count;
                }
            }

            public bool Shared
            {
                get
                {
                    return shared;
                }
            }

            public abstract int Depth { get; }

            public void MarkShared()
            {
                shared = true;
            }

            public abstract T GetAt(int index);

            public abstract Node Subrange(int first, int last);

            public abstract Node SetAt(int index, T item);

            public abstract Node SetAtInPlace(int index, T item);

            public abstract Node Append(Node node, bool nodeIsUnused);

            public abstract Node AppendInPlace(Node node, bool nodeIsUnused);

            public abstract Node AppendInPlace(T item);

            public abstract Node RemoveRange(int first, int last);

            public abstract Node RemoveRangeInPlace(int first, int last);

            public abstract Node Insert(int index, Node node, bool nodeIsUnused);

            public abstract Node InsertInPlace(int index, T item);

            public abstract Node InsertInPlace(int index, Node node, bool nodeIsUnused);

            public Node Prepend(Node node, bool nodeIsUnused)
            {
                if (nodeIsUnused)
                {
                    return node.AppendInPlace(this, false);
                }

                return node.Append(this, false);
            }

            public Node PrependInPlace(Node node, bool nodeIsUnused)
            {
                if (nodeIsUnused)
                {
                    return node.AppendInPlace(this, !shared);
                }

                return node.Append(this, !shared);
            }

            public abstract Node PrependInPlace(T item);

            public bool IsBalanced()
            {
                if (Depth <= 44)
                {
                    return Count >= Fibonacci[Depth];
                }

                return false;
            }

            public bool IsAlmostBalanced()
            {
                if (Depth == 0)
                {
                    return true;
                }

                if (Depth - 1 <= 44)
                {
                    return Count >= Fibonacci[Depth - 1];
                }

                return false;
            }
        }

        private sealed class LeafNode : Node
        {
            public T[] items;

            public override int Depth
            {
                get
                {
                    return 0;
                }
            }

            public LeafNode(T item)
            {
                count = 1;
                items = new T[120];
                items[0] = item;
            }

            
            
            
            
            
            
            public LeafNode(int count, T[] newItems)
            {
                this.count = count;
                items = newItems;
            }

            public override T GetAt(int index)
            {
                return items[index];
            }

            public override Node SetAtInPlace(int index, T item)
            {
                if (shared)
                {
                    return SetAt(index, item);
                }

                items[index] = item;
                return (Node)this;
            }

            public override Node SetAt(int index, T item)
            {
                T[] newItems = (T[])items.Clone();
                newItems[index] = item;
                return (Node)new LeafNode(count, newItems);
            }

            private bool MergeLeafInPlace(Node other)
            {
                LeafNode leafNode = other as LeafNode;
                int num;
                if (leafNode == null || (num = leafNode.Count + count) > 120)
                {
                    return false;
                }

                if (num > items.Length)
                {
                    T[] objArray = new T[120];
                    Array.Copy((Array)items, 0, (Array)objArray, 0, count);
                    items = objArray;
                }
                Array.Copy((Array)leafNode.items, 0, (Array)items, count, leafNode.count);
                count = num;
                return true;
            }

            private Node MergeLeaf(Node other)
            {
                LeafNode leafNode = other as LeafNode;
                int count;
                if (leafNode == null || (count = leafNode.Count + this.count) > 120)
                {
                    return (Node)null;
                }

                T[] newItems = new T[120];
                Array.Copy((Array)items, 0, (Array)newItems, 0, this.count);
                Array.Copy((Array)leafNode.items, 0, (Array)newItems, this.count, leafNode.count);
                return (Node)new LeafNode(count, newItems);
            }

            public override Node PrependInPlace(T item)
            {
                if (shared)
                {
                    return Prepend((Node)new LeafNode(item), true);
                }

                if (count >= 120)
                {
                    return (Node)new ConcatNode((Node)new LeafNode(item), (Node)this);
                }

                if (count == items.Length)
                {
                    T[] objArray = new T[120];
                    Array.Copy((Array)items, 0, (Array)objArray, 1, count);
                    items = objArray;
                }
                else
                {
                    Array.Copy((Array)items, 0, (Array)items, 1, count);
                }

                items[0] = item;
                count = count + 1;
                return (Node)this;
            }

            public override Node AppendInPlace(T item)
            {
                if (shared)
                {
                    return Append((Node)new LeafNode(item), true);
                }

                if (count >= 120)
                {
                    return (Node)new ConcatNode((Node)this, (Node)new LeafNode(item));
                }

                if (count == items.Length)
                {
                    T[] objArray = new T[120];
                    Array.Copy((Array)items, 0, (Array)objArray, 0, count);
                    items = objArray;
                }
                items[count] = item;
                count = count + 1;
                return (Node)this;
            }

            public override Node AppendInPlace(Node node, bool nodeIsUnused)
            {
                if (shared)
                {
                    return Append(node, nodeIsUnused);
                }

                if (MergeLeafInPlace(node))
                {
                    return (Node)this;
                }

                ConcatNode concatNode = node as ConcatNode;
                if (concatNode != null && MergeLeafInPlace(concatNode.left))
                {
                    if (!nodeIsUnused)
                    {
                        concatNode.right.MarkShared();
                    }

                    return (Node)new ConcatNode((Node)this, concatNode.right);
                }
                if (!nodeIsUnused)
                {
                    node.MarkShared();
                }

                return (Node)new ConcatNode((Node)this, node);
            }

            public override Node Append(Node node, bool nodeIsUnused)
            {
                Node node1;
                if ((node1 = MergeLeaf(node)) != null)
                {
                    return node1;
                }

                ConcatNode concatNode = node as ConcatNode;
                Node left;
                if (concatNode != null && (left = MergeLeaf(concatNode.left)) != null)
                {
                    if (!nodeIsUnused)
                    {
                        concatNode.right.MarkShared();
                    }

                    return (Node)new ConcatNode(left, concatNode.right);
                }
                if (!nodeIsUnused)
                {
                    node.MarkShared();
                }

                MarkShared();
                return (Node)new ConcatNode((Node)this, node);
            }

            public override Node InsertInPlace(int index, T item)
            {
                if (shared)
                {
                    return Insert(index, (Node)new LeafNode(item), true);
                }

                if (count < 120)
                {
                    if (count == items.Length)
                    {
                        T[] objArray = new T[120];
                        if (index > 0)
                        {
                            Array.Copy((Array)items, 0, (Array)objArray, 0, index);
                        }

                        if (count > index)
                        {
                            Array.Copy((Array)items, index, (Array)objArray, index + 1, count - index);
                        }

                        items = objArray;
                    }
                    else if (count > index)
                    {
                        Array.Copy((Array)items, index, (Array)items, index + 1, count - index);
                    }

                    items[index] = item;
                    count = count + 1;
                    return (Node)this;
                }
                if (index == count)
                {
                    return (Node)new ConcatNode((Node)this, (Node)new LeafNode(item));
                }

                if (index == 0)
                {
                    return (Node)new ConcatNode((Node)new LeafNode(item), (Node)this);
                }

                T[] newItems1 = new T[120];
                Array.Copy((Array)items, 0, (Array)newItems1, 0, index);
                newItems1[index] = item;
                LeafNode leafNode = new LeafNode(index + 1, newItems1);
                T[] newItems2 = new T[count - index];
                Array.Copy((Array)items, index, (Array)newItems2, 0, count - index);
                Node right = (Node)new LeafNode(count - index, newItems2);
                return (Node)new ConcatNode((Node)leafNode, right);
            }

            public override Node InsertInPlace(int index, Node node, bool nodeIsUnused)
            {
                if (shared)
                {
                    return Insert(index, node, nodeIsUnused);
                }

                LeafNode leafNode1 = node as LeafNode;
                int num1;
                if (leafNode1 != null && (num1 = leafNode1.Count + count) <= 120)
                {
                    if (num1 > items.Length)
                    {
                        T[] objArray = new T[120];
                        Array.Copy((Array)items, 0, (Array)objArray, 0, index);
                        Array.Copy((Array)leafNode1.items, 0, (Array)objArray, index, leafNode1.Count);
                        Array.Copy((Array)items, index, (Array)objArray, index + leafNode1.Count, count - index);
                        items = objArray;
                    }
                    else
                    {
                        Array.Copy((Array)items, index, (Array)items, index + leafNode1.Count, count - index);
                        Array.Copy((Array)leafNode1.items, 0, (Array)items, index, leafNode1.count);
                    }
                    count = num1;
                    return (Node)this;
                }
                if (index == 0)
                {
                    return PrependInPlace(node, nodeIsUnused);
                }

                if (index == count)
                {
                    return AppendInPlace(node, nodeIsUnused);
                }

                T[] newItems1 = new T[index];
                Array.Copy((Array)items, 0, (Array)newItems1, 0, index);
                LeafNode leafNode2 = new LeafNode(index, newItems1);
                T[] newItems2 = new T[count - index];
                Array.Copy((Array)items, index, (Array)newItems2, 0, count - index);
                Node node1 = (Node)new LeafNode(count - index, newItems2);
                Node node2 = node;
                int num2 = nodeIsUnused ? 1 : 0;
                return leafNode2.AppendInPlace(node2, num2 != 0).AppendInPlace(node1, true);
            }

            public override Node Insert(int index, Node node, bool nodeIsUnused)
            {
                LeafNode leafNode1 = node as LeafNode;
                int count;
                if (leafNode1 != null && (count = leafNode1.Count + this.count) <= 120)
                {
                    T[] newItems = new T[120];
                    Array.Copy((Array)items, 0, (Array)newItems, 0, index);
                    Array.Copy((Array)leafNode1.items, 0, (Array)newItems, index, leafNode1.Count);
                    Array.Copy((Array)items, index, (Array)newItems, index + leafNode1.Count, this.count - index);
                    return (Node)new LeafNode(count, newItems);
                }
                if (index == 0)
                {
                    return Prepend(node, nodeIsUnused);
                }

                if (index == this.count)
                {
                    return Append(node, nodeIsUnused);
                }

                T[] newItems1 = new T[index];
                Array.Copy((Array)items, 0, (Array)newItems1, 0, index);
                LeafNode leafNode2 = new LeafNode(index, newItems1);
                T[] newItems2 = new T[this.count - index];
                Array.Copy((Array)items, index, (Array)newItems2, 0, this.count - index);
                Node node1 = (Node)new LeafNode(this.count - index, newItems2);
                Node node2 = node;
                int num = nodeIsUnused ? 1 : 0;
                return leafNode2.AppendInPlace(node2, num != 0).AppendInPlace(node1, true);
            }

            public override Node RemoveRangeInPlace(int first, int last)
            {
                if (shared)
                {
                    return RemoveRange(first, last);
                }

                if (first <= 0 && last >= count - 1)
                {
                    return (Node)null;
                }

                if (first < 0)
                {
                    first = 0;
                }

                if (last >= count)
                {
                    last = count - 1;
                }

                int num = first + (count - last - 1);
                if (count > last + 1)
                {
                    Array.Copy((Array)items, last + 1, (Array)items, first, count - last - 1);
                }

                for (int index = num; index < count; ++index)
                {
                    items[index] = default(T);
                }

                count = num;
                return (Node)this;
            }

            public override Node RemoveRange(int first, int last)
            {
                if (first <= 0 && last >= this.count - 1)
                {
                    return (Node)null;
                }

                if (first < 0)
                {
                    first = 0;
                }

                if (last >= this.count)
                {
                    last = this.count - 1;
                }

                int count = first + (this.count - last - 1);
                T[] objArray = new T[count];
                if (first > 0)
                {
                    Array.Copy((Array)items, 0, (Array)objArray, 0, first);
                }

                if (this.count > last + 1)
                {
                    Array.Copy((Array)items, last + 1, (Array)objArray, first, this.count - last - 1);
                }

                T[] newItems = objArray;
                return (Node)new LeafNode(count, newItems);
            }

            public override Node Subrange(int first, int last)
            {
                if (first <= 0 && last >= count - 1)
                {
                    MarkShared();
                    return (Node)this;
                }
                if (first < 0)
                {
                    first = 0;
                }

                if (last >= count)
                {
                    last = count - 1;
                }

                int length = last - first + 1;
                T[] newItems = new T[length];
                Array.Copy((Array)items, first, (Array)newItems, 0, length);
                return (Node)new LeafNode(length, newItems);
            }
        }

        private sealed class ConcatNode : Node
        {
            
            public Node left;
            
            public Node right;

            private short depth;

            public override int Depth => (int)depth;

            public ConcatNode(Node left, Node right)
            {
                this.left = left;
                this.right = right;
                count = left.Count + right.Count;
                if (left.Depth > right.Depth)
                {
                    depth = (short)(left.Depth + 1);
                }
                else
                {
                    depth = (short)(right.Depth + 1);
                }
            }

            private Node NewNode(Node newLeft, Node newRight)
            {
                if (left == newLeft)
                {
                    if (right == newRight)
                    {
                        MarkShared();
                        return (Node)this;
                    }
                    left.MarkShared();
                }
                else if (right == newRight)
                {
                    right.MarkShared();
                }

                if (newLeft == null)
                {
                    return newRight;
                }

                if (newRight == null)
                {
                    return newLeft;
                }

                return (Node)new ConcatNode(newLeft, newRight);
            }

            private Node NewNodeInPlace(Node newLeft, Node newRight)
            {
                if (newLeft == null)
                {
                    return newRight;
                }

                if (newRight == null)
                {
                    return newLeft;
                }

                left = newLeft;
                right = newRight;
                count = left.Count + right.Count;
                depth = left.Depth <= right.Depth ? (short)(right.Depth + 1) : (short)(left.Depth + 1);
                return (Node)this;
            }

            public override T GetAt(int index)
            {
                int count = left.Count;
                if (index < count)
                {
                    return left.GetAt(index);
                }

                return right.GetAt(index - count);
            }

            public override Node SetAtInPlace(int index, T item)
            {
                if (shared)
                {
                    return SetAt(index, item);
                }

                int count = left.Count;
                if (index < count)
                {
                    Node newLeft = left.SetAtInPlace(index, item);
                    if (newLeft != left)
                    {
                        return NewNodeInPlace(newLeft, right);
                    }

                    return (Node)this;
                }
                Node newRight = right.SetAtInPlace(index - count, item);
                if (newRight != right)
                {
                    return NewNodeInPlace(left, newRight);
                }

                return (Node)this;
            }

            public override Node SetAt(int index, T item)
            {
                int count = left.Count;
                if (index < count)
                {
                    return NewNode(left.SetAt(index, item), right);
                }

                return NewNode(left, right.SetAt(index - count, item));
            }

            public override Node PrependInPlace(T item)
            {
                if (shared)
                {
                    return Prepend((Node)new LeafNode(item), true);
                }

                LeafNode left;
                if (this.left.Count >= 120 || this.left.Shared || (left = this.left as LeafNode) == null)
                {
                    return (Node)new ConcatNode((Node)new LeafNode(item), (Node)this);
                }

                int count = left.Count;
                if (count == left.items.Length)
                {
                    T[] objArray = new T[120];
                    Array.Copy((Array)left.items, 0, (Array)objArray, 1, count);
                    left.items = objArray;
                }
                else
                {
                    Array.Copy((Array)left.items, 0, (Array)left.items, 1, count);
                }

                left.items[0] = item;
                ++left.count;
                this.count = this.count + 1;
                return (Node)this;
            }

            public override Node AppendInPlace(T item)
            {
                if (shared)
                {
                    return Append((Node)new LeafNode(item), true);
                }

                LeafNode right;
                if (this.right.Count >= 120 || this.right.Shared || (right = this.right as LeafNode) == null)
                {
                    return (Node)new ConcatNode((Node)this, (Node)new LeafNode(item));
                }

                int count = right.Count;
                if (count == right.items.Length)
                {
                    T[] objArray = new T[120];
                    Array.Copy((Array)right.items, 0, (Array)objArray, 0, count);
                    right.items = objArray;
                }
                right.items[count] = item;
                ++right.count;
                this.count = this.count + 1;
                return (Node)this;
            }

            public override Node AppendInPlace(Node node, bool nodeIsUnused)
            {
                if (shared)
                {
                    return Append(node, nodeIsUnused);
                }

                if (right.Count + node.Count <= 120 && right is LeafNode && node is LeafNode)
                {
                    return NewNodeInPlace(left, right.AppendInPlace(node, nodeIsUnused));
                }

                if (!nodeIsUnused)
                {
                    node.MarkShared();
                }

                return (Node)new ConcatNode((Node)this, node);
            }

            public override Node Append(Node node, bool nodeIsUnused)
            {
                if (right.Count + node.Count <= 120 && right is LeafNode && node is LeafNode)
                {
                    return NewNode(left, right.Append(node, nodeIsUnused));
                }

                MarkShared();
                if (!nodeIsUnused)
                {
                    node.MarkShared();
                }

                return (Node)new ConcatNode((Node)this, node);
            }

            public override Node InsertInPlace(int index, T item)
            {
                if (shared)
                {
                    return Insert(index, (Node)new LeafNode(item), true);
                }

                int count = left.Count;
                if (index <= count)
                {
                    return NewNodeInPlace(left.InsertInPlace(index, item), right);
                }

                return NewNodeInPlace(left, right.InsertInPlace(index - count, item));
            }

            public override Node InsertInPlace(int index, Node node, bool nodeIsUnused)
            {
                if (shared)
                {
                    return Insert(index, node, nodeIsUnused);
                }

                int count = left.Count;
                if (index < count)
                {
                    return NewNodeInPlace(left.InsertInPlace(index, node, nodeIsUnused), right);
                }

                return NewNodeInPlace(left, right.InsertInPlace(index - count, node, nodeIsUnused));
            }

            public override Node Insert(int index, Node node, bool nodeIsUnused)
            {
                int count = left.Count;
                if (index < count)
                {
                    return NewNode(left.Insert(index, node, nodeIsUnused), right);
                }

                return NewNode(left, right.Insert(index - count, node, nodeIsUnused));
            }

            public override Node RemoveRangeInPlace(int first, int last)
            {
                if (shared)
                {
                    return RemoveRange(first, last);
                }

                if (first <= 0 && last >= this.count - 1)
                {
                    return (Node)null;
                }

                int count = left.Count;
                Node newLeft = left;
                Node newRight = right;
                if (first < count)
                {
                    newLeft = left.RemoveRangeInPlace(first, last);
                }

                if (last >= count)
                {
                    newRight = right.RemoveRangeInPlace(first - count, last - count);
                }

                return NewNodeInPlace(newLeft, newRight);
            }

            public override Node RemoveRange(int first, int last)
            {
                if (first <= 0 && last >= this.count - 1)
                {
                    return (Node)null;
                }

                int count = left.Count;
                Node newLeft = left;
                Node newRight = right;
                if (first < count)
                {
                    newLeft = left.RemoveRange(first, last);
                }

                if (last >= count)
                {
                    newRight = right.RemoveRange(first - count, last - count);
                }

                return NewNode(newLeft, newRight);
            }

            public override Node Subrange(int first, int last)
            {
                if (first <= 0 && last >= this.count - 1)
                {
                    MarkShared();
                    return (Node)this;
                }
                int count = this.left.Count;
                Node left = (Node)null;
                Node right = (Node)null;
                if (first < count)
                {
                    left = this.left.Subrange(first, last);
                }

                if (last >= count)
                {
                    right = this.right.Subrange(first - count, last - count);
                }

                if (left == null)
                {
                    return right;
                }

                if (right == null)
                {
                    return left;
                }

                return (Node)new ConcatNode(left, right);
            }
        }

        private class BigListRange
        {
            private readonly BigList<T> wrappedList;
            private readonly int start;
            private int count;

            public int Count => Math.Min(count, wrappedList.Count - start);

            public T this[int index]
            {
                get
                {
                    if (index < 0 || index >= count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    return wrappedList[start + index];
                }
                set
                {
                    if (index < 0 || index >= count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    wrappedList[start + index] = value;
                }
            }

            public BigListRange(BigList<T> wrappedList, int start, int count)
            {
                this.wrappedList = wrappedList;
                this.start = start;
                this.count = count;
            }

            public void Clear()
            {
                if (wrappedList.Count - start < count)
                {
                    count = wrappedList.Count - start;
                }

                for (; count > 0; count = count - 1)
                {
                    wrappedList.RemoveAt(start + count - 1);
                }
            }

            public void Insert(int index, T item)
            {
                if (index < 0 || index > count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                wrappedList.Insert(start + index, item);
                count = count + 1;
            }

            public void RemoveAt(int index)
            {
                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                wrappedList.RemoveAt(start + index);
                count = count - 1;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return wrappedList.GetEnumerator(start, count);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}