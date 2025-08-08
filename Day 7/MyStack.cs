namespace Day7;

using System;
using System.Collections.Generic;

public class MyStack<T>
{
    private List<T> _items = new List<T>(); // internal storage

    // Returns the number of items in the stack
    public int Count()
    {
        return _items.Count;
    }

    // Removes and returns the top item from the stack
    public T Pop()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        int lastIndex = _items.Count - 1;
        T item = _items[lastIndex];
        _items.RemoveAt(lastIndex);
        return item;
    }

    // Adds an item to the top of the stack
    public void Push(T item)
    {
        _items.Add(item);
    }
}
