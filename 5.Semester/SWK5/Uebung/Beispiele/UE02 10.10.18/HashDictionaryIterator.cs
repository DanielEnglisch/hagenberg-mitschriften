        private class HashDictionaryIterator : IEnumerator<KeyValuePair<K, V>>
        {
            private HashDictionary<K, V> dictionary;
            private int currentIndex;
            private Node currentNode;

            public HashDictionaryIterator (HashDictionary<K, V> dictionary)
	        {
                this.dictionary = dictionary;
                currentIndex = -1;
                currentNode = null;
	        }

            public KeyValuePair<K, V> Current
            {
                get { return new KeyValuePair<K,V>(currentNode.Key, currentNode.Value); }
            }

            public bool MoveNext()
            {
                if (currentNode != null)
                {
                    currentNode = currentNode.Next;
                }

                while (currentNode == null)
                {
                    currentIndex++;
                    if (currentIndex >= dictionary.hashTable.Length)
                        return false;

                    currentNode = dictionary.hashTable[currentIndex];
                }

                return true;
            }

            public void Reset()
            {
                this.currentIndex = -1;
                this.currentNode = null;
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
            }
        }