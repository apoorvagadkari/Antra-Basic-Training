using System;

public class MyList<T>
{
    private T[] _items;
    private int _count;

    public MyList()
    {
        _items = new T[4]; // initial capacity
        _count = 0;
    }

    // 1. Add element at the end
    public void Add(T element)
    {
        EnsureCapacity();
        _items[_count++] = element;
    }

    // 2. Remove element at index and return it
    public T Remove(int index)
    {
        ValidateIndex(index);
        T removedItem = _items[index];

        // Shift elements left
        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--;
        _items[_count] = default; // clear last

        return removedItem;
    }

    // 3. Check if element exists
    public bool Contains(T element)
    {
        for (int i = 0; i < _count; i++)
        {
            if (Equals(_items[i], element))
                return true;
        }
        return false;
    }

    // 4. Clear list
    public void Clear()
    {
        _items = new T[4];
        _count = 0;
    }

    // 5. Insert element at specific index
    public void InsertAt(T element, int index)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity();
        // Shift elements right
        for (int i = _count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }
        _items[index] = element;
        _count++;
    }

    // 6. Delete element at index (without returning it)
    public void DeleteAt(int index)
    {
        Remove(index); // reuse logic from Remove()
    }

    // 7. Find element at index
    public T Find(int index)
    {
        ValidateIndex(index);
        return _items[index];
    }

    public int Count => _count;

    // Resize array if needed
    private void EnsureCapacity()
    {
        if (_count == _items.Length)
        {
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
    }

    // Validate index
    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }
}
