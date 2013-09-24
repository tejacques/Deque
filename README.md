Deque
=====

A C# Deque class


Methods
-------

```csharp
public Class Deque<T> : IList<T>
{
    public void Add;
    public void AddFront(T item);
    public void AddBack(T item);
    public void AddRange(IEnumerable<T> collection);
    public void AddFrontRange(IEnumerable<T> collection);
    public void AddFrontRange(IEnumerable<T> collection, int fromIndex, int count);
    public void AddBackRange(IEnumerable<T> collection);
    public void AddBackRange(IEnumerable<T> collection, int fromIndex, int count);
    
    public bool Contains(T item);
    public void CopyTo(T[] array, int arrayIndex);
    public int Count;

    public int IndexOf(T item);
    public void Insert(int index, T item);
    public void InsertRange(int index, IEnumerable<T> collection);
    public void InsertRange(int index, IEnumerable<T> collection, int fromIndex, int count);

    public void Remove(int index);
    public T RemoveFront();
    public T RemoveBack();
    public void RemoveRange(int index, int count);

    public T Get(int index);
    public void Set(int index, T item);
    
    public T this[index];
}
```
