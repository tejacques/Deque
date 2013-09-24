Deque
=====

What is it?
-----------

A C# Generic Deque class implemented in .NET 3.5. Deques have constant time insertion and removal from either end. This implementation is a circular-array backed Deque class to the System.Collections.Generic namespace. The tradeoff over a doubly-linked-list backed Deque is that arbitrary access into the deque is onstant time, but arbitrary insertion into the deque is O(n), whereas a doubly-linked-list backed deque is the opposite.

More information about Deques can be found here: http://en.wikipedia.org/wiki/Double-ended_queue

How can I get it?
-----------------

Deque is available as a NuGet package: https://nuget.org/packages/Deque/

```
PM> Install-Package Deque
```

Why was it made?
----------------

The Deque data structure is not part of the C# Standard Library, but it is nonetheless an incredibly useful data structure.

Performance Benchmarks
----------------------

This implementation of Deque was made to be highly performant, and uses several optimizations. Below are the results of benchmarks taken from several built-in .NET data structures compared to the Deque class, it can be seen that the Deque class is on par with them performance wise.

### Without preallocating space ###

|      Class     | Operation | Iterations | Time (ms) | Time Per Operation (ns) | Compared To Deque |
|:-------------- |:---------:| ----------:| ---------:| -----------------------:| -----------------:|
|   Deque        |  Insert   |   30000000 |	     192  |                   6.40  |          100.00%  |
|   Deque        |  Iterate  |   30000000 |	     184  |                   6.13  |          100.00%  |
|   Deque        |  Remove   |   30000000 |	     120  |                   4.00  |          100.00%  |
| **Deque**      | **Total** |   90000000 |	   **496**|                 **5.51**|        **100.00%**|
|   LinkedList   |  Insert   |   30000000 |	    4019  |                 133.97  |         2093.23%  |
|   LinkedList   |  Iterate  |   30000000 |	     168  |            	      5.60  |           91.30%  |
|   LinkedList   |  Remove   |   30000000 |	     394  |           	     13.13  |          328.33%  |
| **LinkedList** | **Total** |   90000000 |   **4581**|           	   **50.90**|        **923.59%**|
|   List         |  Insert   |   30000000 |      196  |           	      6.53  |          102.08%  |
|   List         |  Iterate  |   30000000 |	      79  |           	      4.63  |           42.93%  |
|   List         |  Remove   |   30000000 |	     116  |           	      3.87  |           96.67%  |
| **List**       | **Total** |   90000000 |	   **391**|           	    **4.34**|         **78.83%**|
|   Queue        |  Insert   |   30000000 |	     443  |           	     14.77  |          230.73%  |
|   Queue        |  Iterate  |   30000000 |      195  |           	      6.50  |          105.98%  |
|   Queue        |  Remove   |   30000000 |      224  |           	      7.47  |          186.67%  |
| **Queue**      | **Total** |   90000000 |    **862**|           	    **9.58**|        **173.79%**|
|   Stack        |  Insert   |   30000000 |      161  |           	      5.37  |           83.85%  |
|   Stack        |  Iterate  |   30000000 |      141  |           	      4.70  |           76.63%  |
|   Stack        |  Remove   |   30000000 |      112  |           	      3.73  |           93.33%  |
| **Stack**      | **Total** |   90000000 |    **414**|           	    **4.60**|         **83.47%**|

### With preallocating space ###

|      Class     | Operation | Iterations | Time (ms) | Time Per Operation (ns) | Compared To Deque |
|:-------------- |:---------:| ----------:| ---------:| -----------------------:| -----------------:|
|   Deque        |  Insert   |   30000000 |	     192  |                   4.77  |          100.00%  |
|   Deque        |  Iterate  |   30000000 |	     184  |                   6.27  |          100.00%  |
|   Deque        |  Remove   |   30000000 |	     120  |                   3.97  |          100.00%  |
| **Deque**      | **Total** |   90000000 |	   **496**|                 **5.00**|        **100.00%**|
|   LinkedList   |  Insert   |   30000000 |	    4019  |                 137.37  |         2881.82%  |
|   LinkedList   |  Iterate  |   30000000 |	     168  |            	      5.17  |           82.45%  |
|   LinkedList   |  Remove   |   30000000 |	     394  |           	     15.70  |          395.80%  |
| **LinkedList** | **Total** |   90000000 |   **4581**|           	   **52.74**|       **1054.89%**|
|   List         |  Insert   |   30000000 |      196  |           	      4.87  |          102.10%  |
|   List         |  Iterate  |   30000000 |	      79  |           	      2.90  |           46.26%  |
|   List         |  Remove   |   30000000 |	     116  |           	      3.80  |           95.80%  |
| **List**       | **Total** |   90000000 |	   **391**|           	    **3.86**|         **77.11%**|
|   Queue        |  Insert   |   30000000 |	     443  |           	      9.23  |          193.71%  |
|   Queue        |  Iterate  |   30000000 |      195  |           	      6.40  |          106.91%  |
|   Queue        |  Remove   |   30000000 |      224  |           	      7.87  |          198.32%  |
| **Queue**      | **Total** |   90000000 |    **862**|           	    **7.93**|        **158.67%**|
|   Stack        |  Insert   |   30000000 |      161  |           	      4.70  |           98.60%  |
|   Stack        |  Iterate  |   30000000 |      141  |           	      4.67  |           74.47%  |
|   Stack        |  Remove   |   30000000 |      112  |           	      3.73  |           94.12%  |
| **Stack**      | **Total** |   90000000 |    **414**|           	    **4.37**|         **87.33%**|


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
