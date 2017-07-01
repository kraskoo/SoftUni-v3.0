namespace LimitedMemory
{
    using System.Collections.Generic;
    using System.Collections;

    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private readonly LinkedList<Pair<K, V>> pairNodes;
        private readonly Dictionary<K, LinkedListNode<Pair<K, V>>> nodeByKey;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.pairNodes = new LinkedList<Pair<K, V>>();
            this.nodeByKey = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            return this.pairNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; }

        public int Count { get; private set; }

        public void Set(K key, V value)
        {
            if (this.Count == this.Capacity)
            {
                if (this.nodeByKey.ContainsKey(key))
                {
                    var node = this.nodeByKey[key];
                    this.pairNodes.Remove(node);
                    this.nodeByKey.Remove(key);
                    node.Value.Value = value;
                    this.nodeByKey.Add(key, node);
                    this.pairNodes.AddFirst(node);
                }
                else
                {
                    var last = this.pairNodes.Last;
                    this.pairNodes.RemoveLast();
                    this.nodeByKey.Remove(last.Value.Key);
                    var newPair = new Pair<K, V>(key, value);
                    var newNodePair = this.pairNodes.AddFirst(newPair);
                    this.nodeByKey.Add(key, newNodePair);
                }
            }
            else
            {
                if (this.nodeByKey.ContainsKey(key))
                {
                    var existingNode = this.nodeByKey[key];
                    this.pairNodes.Remove(existingNode);
                    this.nodeByKey.Remove(existingNode.Value.Key);
                    existingNode.Value.Value = value;
                    var newLinkedNode = this.pairNodes.AddFirst(existingNode.Value);
                    this.nodeByKey.Add(key, newLinkedNode);
                }
                else
                {
                    var pair = new Pair<K, V>(key, value);
                    var nodePair = this.pairNodes.AddFirst(pair);
                    this.nodeByKey.Add(key, nodePair);
                    this.Count++;
                }
            }
        }

        public V Get(K key)
        {
            if (!this.nodeByKey.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var requestedNode = this.nodeByKey[key];
            this.nodeByKey.Remove(requestedNode.Value.Key);
            this.pairNodes.Remove(requestedNode);
            var newPositionedNode = this.pairNodes.AddFirst(requestedNode.Value);
            this.nodeByKey.Add(requestedNode.Value.Key, newPositionedNode);
            return this.nodeByKey[key].Value.Value;
        }
    }
}