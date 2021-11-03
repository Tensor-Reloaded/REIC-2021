using System;
using System.Collections.Generic;

namespace REIC
{   //followed a written tutorial and modified it so that it works with my data collections, namely, float lists

    public class Item
    {
        float value;
        // Constructor
        public Item(float value)
        {
            this.value = value;
        }
        public float Value
        {
            get { return value; }
        }
    }

    public interface IAbstractCollection
    {
        Iterator CreateIterator();
    }
   
    public class Collection : IAbstractCollection
    {
        List<Item> items = new List<Item>();
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
        // Gets item count
        public int Count
        {
            get { return items.Count; }
        }
        // Indexer
        public Item this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }
    }

    /// The 'Iterator' interface
    public interface IAbstractIterator
    {
        Item First();
        Item Next();
        bool IsDone { get; }
        Item CurrentItem { get; }
    }

    /// The 'ConcreteIterator' class
    public class Iterator : IAbstractIterator
    {
        Collection collection;
        int current = 0;
        int step = 1;
        // Constructor
        public Iterator(Collection collection)
        {
            this.collection = collection;
        }
        // Gets first item
        public Item First()
        {
            current = 0;
            return collection[current] as Item;
        }
        // Gets next item
        public Item Next()
        {
            current += step;
            if (!IsDone)
                return collection[current] as Item;
            else
                return null;
        }
        // Gets or sets stepsize
        public int Step
        {
            get { return step; }
            set { step = value; }
        }
        // Gets current iterator item
        public Item CurrentItem
        {
            get { return collection[current] as Item; }
        }
        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return current >= collection.Count; }
        }
    }
}
