using System;
using System.Collections;
using System.Collections.Generic;

namespace HashDictionary
{
    public class HashDictionary<K, V> : IDictionary<K, V>
    {
        private class Node
        {
            public Node(K key, V value, Node next)
            {
                this.key = key;
                this.value = value;
                Next = next;
            }

            public K key { get; set; }
            public V value { get; set; }

            public Node Next { get; set; }
        }

        private const int INITIAL_HASH_TABLE_SIZE = 8;

        private Node[] hashTable = new Node[INITIAL_HASH_TABLE_SIZE];

        private int size = 0;

        public ICollection<K> Keys
        {
            get
            {
                List<K> list = new List<K>(this.Count);
                foreach (KeyValuePair<K, V> pair in this)
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
                List<V> list = new List<V>();
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

            // get and Set use an implicit parameter called value,
            // whose type is the type of the property
            get
            {
                Node n = FindNode(key);
                if (n != null)
                    return n.value;
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                Node n = FindNode(key);
                if (n == null)
                {
                    // add new node
                    int index = IndexFor(key);
                    hashTable[index] = new Node(key, value, hashTable[index]);
                    ++size;
                }
                else
                {
                    // replace existing value
                    n.value = value;
                }
            }
        }

        public void Add(K key, V value)
        {
            if (!ContainsKey(key))
                this[key] = value;
            else
                throw new ArgumentException("Key already exists");
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this.Add(item.Key, item.Value);
        }

        public bool ContainsKey(K key)
        {
            return FindNode(key) != null;
        }

        private Node FindNode(K key)
        {
            int index = IndexFor(key);
            Node n = hashTable[index];
            while (n != null)
            {
                if (n.key.Equals(key))
                    return n;
                else
                    n = n.Next;
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

        // The out keyword causes arguments to be passed by reference. 
        // It is like the ref keyword, except that ref requires that the variable be 
        // initialized before it is passed. It is also like the in keyword,
        // except that in does not allow the called method to modify the argument value. 
        // To use an out parameter, both the method definition and the calling method
        // must explicitly use the out keyword.
        public bool TryGetValue(K key, out V value)
        {
            Node n = FindNode(key);
            value = n != null ? n.value : default(V);
            return n != null;
        }

        public void Clear()
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                hashTable[i] = null;
            }

            size = 0;
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            foreach (var pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for (int i = 0; i < hashTable.Length; i++)
                for (Node n = hashTable[i]; n != null; n = n.Next)
                    yield return new KeyValuePair<K, V>(n.key, n.value);
        }

        // gar nicht von ausßen aufrufbar außer es wird gecastet ((IEnumerable)dict).getEnumerator();
        // also nur wenn der statische typ dieses interface ist
        IEnumerator IEnumerable.GetEnumerator()
        {
            // keine endlosschleife weil
            // this vom statischen typ her HashDictionary ist 
            // und dabei IEnumerator getenumerator aufruft
            return this.GetEnumerator();
        }
    }
}
