using System;
using System.Collections;
using System.Collections.Generic;

namespace HashDictionary
{
    public class HashDictionary<K, V> : IDictionary<K,V>
    {
        private class Node
        {
            public Node(K key, V value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }

            public K Key { get; set; }
            public V Value { get; set; }
            public Node Next { get; set; }
        }

        private const int INITIAL_HASH_TABLE_SIZE = 8;

        private Node[] hashTable = new Node[INITIAL_HASH_TABLE_SIZE];
        private int size;

        public ICollection<K> Keys
        {
            get
            {
                var list = new List<K>(this.Count);
                foreach (KeyValuePair<K,V> pair in this)
                {
                    list.Add(pair.Key);
                }

                return list;
            }

            
        }

        public ICollection<V> Values
        {
            get
            {
                var list = new List<V>(this.Count);
                foreach (KeyValuePair<K, V> pair in this)
                {
                    list.Add(pair.Value);
                }

                return list;
            }
        }

        public int Count => this.size;

        public bool IsReadOnly => false;

        public V this[K key]
        {
            get
            {
                Node n = FindNode(key);
                if (n != null)
                {
                    return n.Value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }

            set
            {
                Node n = FindNode(key);
                if (n == null)
                {
                    int index = IndexFor(key);
                    hashTable[index] = new Node(key, value, hashTable[index]);
                    size++;
                }
                else
                {
                    n.Value = value;
                }
            }
        }

        public void Add(K key, V value)
        {
            if (!ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                throw new ArgumentException("Key already exists");
            }
        }

        public bool ContainsKey(K key)
        {
            return FindNode(key) != null;
        }

        private Node FindNode(K key)
        {
            int index = IndexFor(key);
            Node n = hashTable[index];
            while(n != null)
            {
                if (n.Key.Equals(key))
                {
                    return n;
                }
                else
                {
                    n = n.Next;
                }
            }

            return null;
        }

        private int IndexFor(K key)
        {
            return Math.Abs(key.GetHashCode()) % hashTable.Length;
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            Node n = FindNode(key);
            value = n != null ? n.Value : default(V);
            return n != null;
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            for(int i = 0; i < hashTable.Length; ++i)
            {
                hashTable[i] = null;
            }

            size = 0;
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return this.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int index)
        {
            foreach (var pair in this)
            {
                array[index++] = pair;
            }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for(int i = 0; i < hashTable.Length; ++i)
            {
                for(Node n = hashTable[i]; n != null; n = n.Next) 
                {
                    
                    yield return new KeyValuePair<K, V>(n.Key, n.Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


}
