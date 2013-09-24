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

Without preallocating space
```
Class	    Operation	Iterations	Time (ms)	Time Per Operation (ns)	Compared To Deque

Deque	    Insert	    30000000	228	         7.6	                    100.00%
Deque	    Remove	    30000000	145	         4.8        	            100.00%
Deque	    Iterate	    30000000	181	         6.0        	            100.00%
Deque	    Total	    90000000	554	         6.1           	            100.00%

List	    Insert	    30000000	348	         11.6   	                152.63%
List	    Remove	    30000000	113	          3.7       	             77.93%
List	    Iterate	    30000000	76	          2.5       	             41.98%
List	    Total	    90000000	537	          5.9       	             96.93%

Stack	    Insert	    30000000	191	          6.3       	             83.77%
Stack	    Remove	    30000000	112	          3.7       	             77.24%
Stack	    Iterate	    30000000	145	          4.8       	             80.11%
Stack	    Total	    90000000	448	          4.9       	             80.86%

Queue	    Insert	    30000000	450	         15 	                    197.36%
Queue	    Remove	    30000000	218	          7.2       	            150.34%
Queue	    Iterate	    30000000	190	          6.3       	            104.97%
Queue	    Total	    90000000	858	          9.5       	            154.87%

Linked List	Insert	    30000000	3902	    130.1       	           1711.40%
Linked List	Remove	    30000000	258	          8.6	                    177.91%
Linked List	Iterate	    30000000	147	          4.9	                     81.21%
Linked List	Total	    90000000	4307	     47.8       	            777.43%
```

With preallocating space
```
Class	    Operation	Iterations	Time (ms)	Time Per Operation (ns)	Compared To Deque

Deque	    Insert	    30000000	175	         5.8	                    100.00%
Deque	    Remove	    30000000	145	         4.8	                    100.00%
Deque	    Iterate	    30000000	185	         6.1	                    100.00%
Deque	    Total	    90000000	505	         5.6	                    100.00%

List	    Insert	    30000000	116	         3.8	                     66.28%
List	    Remove	    30000000	122	         4.0	                     84.13%
List	    Iterate	    30000000	76	         2.5	                     41.08%
List	    Total	    90000000	314	         3.4	                     62.17%

Stack	    Insert	    30000000	126	         4.2	                     72.00%
Stack	    Remove	    30000000	108	         3.6	                     74.48%
Stack	    Iterate	    30000000	146	         4.8	                     78.91%
Stack	    Total	    90000000	380	         4.2	                     75.24%

Queue	    Insert	    30000000	245	         8.1	                    140.00%
Queue	    Remove	    30000000	229	         7.6	                    157.93%
Queue	    Iterate	    30000000	189	         6.3	                    102.16%
Queue	    Total	    90000000	663	         7.3	                    131.28%

Linked List	Insert	    30000000	3854	    128.4                      2202.28%
Linked List	Remove	    30000000	271	         9.0	                    186.89%
Linked List	Iterate	    30000000	149	         4.9	                     80.54%
Linked List	Total	    90000000	4274	     47.4	                    846.33%
```

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
