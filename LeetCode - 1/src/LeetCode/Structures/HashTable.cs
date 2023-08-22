using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Structures
{
    class HashTable
    {
        private class KeyValue
        {
            public string Key;
            public int Value;
        }
        private const int SIZE = 100;
        private LinkedList<KeyValue>[] data;
        public HashTable()
        {
            data = new LinkedList<KeyValue>[SIZE];
        }

        public void Put(string key, int value)
        {
            int hashcode = key.GetHashCode();
            int index = convertToIndex(hashcode);
            Insert(index, key, value);
        }

        internal void Remove(string key)
        {
            int hashcode = key.GetHashCode();
            int index = convertToIndex(hashcode);
            var list = data[index];
            remove(list, key);
        }

        private void remove(LinkedList<KeyValue> list, string key)
        {
            foreach (var item in list)
                if (item.Key == key)
                {
                    list.Remove(item);
                    return;
                }

        }

        public int Get(string key)
        {
            int hashcode = key.GetHashCode();
            int index = convertToIndex(hashcode);
            var list = data[index];
            var keyValue = FindValue(list, key);
            if (keyValue == null)
                throw new KeyNotFoundException();
            return keyValue.Value;
        }

        private void Insert(int index, string key, int value)
        {
            var list = data[index];
            if (list == null)
            {
                list = new LinkedList<KeyValue>();
                data[index] = list;
                list.AddLast(new KeyValue { Key = key, Value = value });
            }
            else
            {
                var duplicate = FindValue(list, key);
                if (duplicate != null)
                    duplicate.Value = value;
                else
                    list.AddLast(new KeyValue { Key = key, Value = value });
            }
        }


        private KeyValue FindValue(LinkedList<KeyValue> list, string key)
        {
            foreach (var item in list)
                if (item.Key == key)
                    return item;
            return null;
        }

        private int convertToIndex(int hashcode) => (hashcode & 0x7FFFFFFF) % SIZE;


    }

    public class HashTableTests
    {
        private readonly HashTable ht;

        public HashTableTests()
        {
            ht = new HashTable();
        }

        [Fact]
        public void insert()
        {
            ht.Put("test", 10);
            Assert.Equal(10, ht.Get("test"));
        }

        [Fact]
        public void insert2()
        {
            ht.Put("test", 10);
            ht.Put("test", 12);
            Assert.Equal(12, ht.Get("test"));
        }

        [Fact]
        public void insert3()
        {
            ht.Put("test1", 10);
            ht.Put("test2", 12);
            ht.Put("test3", 14);
            Assert.Equal(10, ht.Get("test1"));
            Assert.Equal(12, ht.Get("test2"));
            Assert.Equal(14, ht.Get("test3"));
        }

        [Fact]
        public void remove()
        {
            ht.Put("test1", 10);
            Assert.Equal(10, ht.Get("test1"));
            ht.Remove("test1");
            Assert.Throws<KeyNotFoundException>(() => { ht.Get("test1"); });

        }
    }
}
