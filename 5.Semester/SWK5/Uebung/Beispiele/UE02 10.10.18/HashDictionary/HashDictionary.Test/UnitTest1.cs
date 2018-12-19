
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace HashDictionary.Test
{
    [TestClass]
    public class HashDictionaryTests
    {
        [TestMethod]
        public void CountTest()
        {
            IDictionary<int, int> dictionary = new HashDictionary<int, int>();
            Assert.AreEqual(0, dictionary.Count);

            dictionary.Add(1, 10);
            Assert.AreEqual(1, dictionary.Count);

            dictionary.Add(2, 11);
            Assert.AreEqual(2, dictionary.Count);

            dictionary[3] = 20;
            Assert.AreEqual(3, dictionary.Count);

            dictionary[3] = 40;
            Assert.AreEqual(3, dictionary.Count);
        }

        [TestMethod]
        public void ContainsTest()
        {
            IDictionary<int, int> dictionary = new HashDictionary<int, int>();

            for (int i = 1; i < 20; i++)
            {
                dictionary.Add(i, 10 * i);
            }

            for (int i = 1; i < 20; i++)
            {
                Assert.IsTrue(dictionary.ContainsKey(i));
            }

            Assert.IsFalse(dictionary.ContainsKey(0));
            Assert.IsFalse(dictionary.ContainsKey(21));
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        public void KeyNotFoundTest()
        {
            var list = new List<int>() { 1, 2, 3 };

            IDictionary<int, int> dictionary = new HashDictionary<int, int>()
            {
                {1, 10 },
                {2, 20 }
            };

            try
            {
                int val = dictionary[3];
                Assert.Fail();
            }
            catch (KeyNotFoundException e)
            {
                
            }
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof(ArgumentException))]
        public void AddKeyTwiceTest()
        {
            IDictionary<int, int> dictionary = new HashDictionary<int, int>()
            {
                {1, 10 },
                {2, 20 }
            };

            dictionary.Add(2, 30);
        }
    }
}
