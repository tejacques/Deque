using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deque
{
    public class Deque<T> : IList<T>
    {

        /// <summary>
        /// The default capacity of the deque.
        /// </summary>
        private const int defaultCapacity = 16;

        /// <summary>
        /// The first element offset from the beginning of the data array.
        /// </summary>
        private int startOffset;

        /// <summary>
        /// The last element offset from the beginning of the data array.
        /// </summary>
        private int endOffset;

        /// <summary>
        /// The circular array holding the items.
        /// </summary>
        private T[] buffer;

        /// <summary>
        /// Creates a new instance of the Deque class with the default capacity.
        /// </summary>
        public Deque() : this(defaultCapacity) { }

        /// <summary>
        /// Creates a new instance of the Deque class with the specified capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public Deque(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", "capacity is less than 0.");
            }

            capacity = Utility.ClosestPowerOfTwoGreaterThan(capacity);

            buffer = new T[capacity];
            this.startOffset = 0;
            this.endOffset = 0;
            this.shiftEndOffset(-1);
        }

        /// <summary>
        /// Create a new instance of the Deque class with the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The co</param>
        public Deque(IEnumerable<T> collection)
            : this(collection.Count())
        {
            InsertRange(0, collection);
        }

        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity
        {
            get
            {
                return buffer.Length;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Capacity is less than 0.");
                }
                else if (value < this.Count)
                {
                    throw new InvalidOperationException("Capacity cannot be set to a value less than Count");
                }
                else if (value == buffer.Length)
                {
                    return;
                }

                // Create a new array and copy the old values.
                T[] newBuffer = new T[value];
                this.CopyTo(newBuffer, 0);

                // Set up to use the new buffer.
                buffer = newBuffer;
                startOffset = 0;
                endOffset = 0;
            }
        }

        /// <summary>
        /// Gets whether or not the Deque is filled to capacity.
        /// </summary>
        public bool IsFull
        {
            get { return this.Count == this.Capacity; }
        }

        public bool IsEmpty
        {
            get { return 0 == this.Count; }
        }

        private void ensureCapacityFor(int numElements)
        {
            if (this.Count + numElements > this.Capacity)
            {
                this.Capacity = Utility.ClosestPowerOfTwoGreaterThan(
                    this.Count + numElements);
            }
        }

        private int toBufferIndex(int index)
        {
            return (index + startOffset) % Capacity;
        }

        private void checkIndexOutOfRange(int index)
        {
            if (index >= this.Count)
            {
                throw new IndexOutOfRangeException("The supplied index is greater than the Count");
            }
        }

        private static void checkArgumentsOutOfRange(int length, int offset, int count)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "offset", "Invalid offset " + offset);
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "count", "Invalid count " + count);
            }

            if (length - offset < count)
            {
                throw new ArgumentException(
                    String.Format(
                    "Invalid offset ({0}) or count + ({1}) for source length {2}",
                    offset, count, length));
            }
        }

        private int shiftStartOffset(int value)
        {
            this.startOffset = (this.startOffset + value) % this.Capacity;
            if (this.startOffset < 0)
            {
                this.startOffset += this.Capacity;
            }
            return this.startOffset;
        }

        private int preShiftStartOffset(int value)
        {
            int offset = this.startOffset;
            this.shiftStartOffset(value);
            return offset;
        }

        private int postShiftStartOffset(int value)
        {
            return shiftStartOffset(value);
        }

        private int shiftEndOffset(int value)
        {
            this.endOffset = (this.endOffset + value) % this.Capacity;
            if (this.endOffset < 0)
            {
                this.endOffset += this.Capacity;
            }
            return this.endOffset;
        }

        private int preShiftEndOffset(int value)
        {
            int offset = this.endOffset;
            this.shiftEndOffset(value);
            return offset;
        }

        private int postShiftEndOffset(int value)
        {
            return this.shiftEndOffset(value);
        }

        #region IEnumberable
        public IEnumerator<T> GetEnumerator()
        {
            int count = this.Count;

            // The below is done for performance reasons.
            // Rather than doing bounds checking and modulo arithmetic that would go along with
            // calls to Get(index), we can skip all of that by referencing the underlying array.

            if (startOffset > endOffset)
            {
                int lengthFromStart = Capacity - startOffset;
                int lengthFromEnd = count - lengthFromStart;    
            }
            else
            {
                for (int i = startOffset; i < startOffset +count; i++)
                {
                    yield return buffer[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollection
        /// <summary>
        /// Gets a value indicating whether the Deque is read-only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the number of elements contained in the Deque.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds an item to the Deque.
        /// </summary>
        /// <param name="item">The object to add to the Deque.</param>
        public void Add(T item)
        {
            AddBack(item);
        }

        /// <summary>
        /// Removes all items from the Deque.
        /// </summary>
        public void Clear()
        {
            this.Count = 0;
            this.startOffset = 0;
            this.endOffset = 0;
        }

        /// <summary>
        /// Determines whether the Deque contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the Deque.</param>
        /// <returns>true if item is found in the Deque; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }
        
        /// <summary>
        ///     Copies the elements of the Deque to a System.Array,
        ///     starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional System.Array that is the destination of
        ///     the elements copied from the Deque. The System.Array must
        ///     have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        ///     The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     array is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     arrayIndex is less than 0.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The number of elements in the source Deque is greater than
        ///     the available space from arrayIndex to the end of the
        ///     destination array.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array", "Array is null");

            checkArgumentsOutOfRange(array.Length, arrayIndex, this.Count);

            if (startOffset > endOffset)
            {
                int lengthFromStart = Capacity - startOffset;
                int lengthFromEnd = Count - lengthFromStart;

                Array.Copy(buffer, startOffset, array, 0, lengthFromStart);
                Array.Copy(buffer, 0, array, lengthFromStart, Count - lengthFromEnd);
            }
            else
            {
                Array.Copy(buffer, startOffset, array, 0, Count);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the Deque.
        /// </summary>
        /// <param name="item">The object to remove from the Deque.</param>
        /// <returns>
        ///     true if item was successfully removed from the Deque;
        ///     otherwise, false. This method also returns false if item
        ///     is not found in the original
        /// </returns>
        public bool Remove(T item)
        {
            bool result = true;
            int index = IndexOf(item);

            if (-1 == index)
            {
                result = false;
            }
            else
            {
                RemoveAt(index);
            }

            return result;
        }

        #endregion

        #region List<T>

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index of the element to get or set.
        /// </param>
        /// <returns>The element at the specified index</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     <paramref name="index"/> is not a valid index in this deque
        /// </exception>
        public T this[int index]
        {
            get
            {
                return this.Get(index);
            }

            set
            {
                this.Set(index, value);
            }
        }

        /// <summary>
        /// Inserts an item to the Deque at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the Deque.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the Deque.
        /// </exception>
        public void Insert(int index, T item)
        {
            ensureCapacityFor(1);

            if (index == 0)
            {
                AddFront(item);
                return;
            }
            else if (index == Count)
            {
                AddBack(item);
                return;
            }

            InsertRange(index, new[] { item });
        }

        /// <summary>
        /// Determines the index of a specific item in the deque.
        /// </summary>
        /// <param name="item">The object to locate in the deque.</param>
        /// <returns>The index of the item if found in the deque; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            int index = 0;
            foreach (var myItem in this)
            {
                if (myItem.Equals(item))
                {
                    break;
                }
                ++index;
            }

            if (index == this.Count)
            {
                index = -1;
            }

            return index;
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the Deque.
        /// </exception>
        public void RemoveAt(int index)
        {
            if (index == 0)
            {
                RemoveFront();
                return;
            }
            else if (index == Count - 1)
            {
                RemoveBack();
                return;
            }

            RemoveRange(index, 1);
        }
        #endregion

        public void AddFront(T item)
        {
            ensureCapacityFor(1);
            buffer[postShiftStartOffset(-1)] = item;
            Count++;
        }

        public void AddBack(T item)
        {
            ensureCapacityFor(1);
            buffer[postShiftEndOffset(1)] = item;
            Count++;
        }

        public T RemoveFront()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("The Deque is empty");
            }

            T result = buffer[preShiftStartOffset(1)];
            Count--;
            return result;
        }

        public T RemoveBack()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("The Deque is empty");
            }

            T result = buffer[preShiftEndOffset(-1)];
            Count--;
            return result;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            AddBackRange(collection);
        }

        public void AddFrontRange(IEnumerable<T> collection)
        {
            InsertRange(0, collection);
        }

        public void AddFrontRange(IEnumerable<T> collection, int fromIndex, int count)
        {
            InsertRange(0, collection, fromIndex, count);
        }

        public void AddBackRange(IEnumerable<T> collection)
        {
            InsertRange(this.Count, collection);
        }

        public void AddBackRange(IEnumerable<T> collection, int fromIndex, int count)
        {
            InsertRange(this.Count, collection, fromIndex, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            int count = collection.Count();
            this.InsertRange(index, collection, 0, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection, int fromIndex, int count)
        {
            checkIndexOutOfRange(index - 1);

            if (0 == count)
            {
                return;
            }

            // Make room
            ensureCapacityFor(count);

            int bufferIndex = toBufferIndex(index);

            if (index < Count / 2)
            {
                // Inserting into the first half of the list

                if (index > 0)
                {
                    // Move items down:
                    //  [0, index) -> 
                    //  [Capacity - count, Capacity - count + index)
                    int copyCount = index;
                    int shiftIndex = Capacity - count;
                    for (int j = 0; j < copyCount; j++)
                    {
                        buffer[toBufferIndex(shiftIndex + j)] = 
                            buffer[toBufferIndex(j)];
                    }
                }

                // shift the starting offset
                this.shiftStartOffset(-count);

            }
            else
            {
                // Inserting into the second half of the list

                if (index < Count - 1)
                {
                    // Move items up:
                    // [index, Count) -> [index + count, count + Count)
                    int copyCount = Count - index;
                    int shiftIndex = index + count;
                    for (int j = 0; j < copyCount; j++)
                    {
                        buffer[toBufferIndex(shiftIndex + j)] = 
                            buffer[toBufferIndex(index + j)];
                    }
                }

                // shift the ending offset
                this.shiftEndOffset(count);
            }

            // Copy new items into place
            int i = index;
            foreach (T item in collection)
            {
                buffer[toBufferIndex(i)] = item;
                ++i;
            }

            // Adjust valid count
            Count += count;
        }

        /// <summary>
        ///     Removes a range of elements from the view.
        /// </summary>
        /// <param name="index">
        ///     The index into the view at which the range begins.
        /// </param>
        /// <param name="count">
        ///     The number of elements in the range. This must be greater
        ///     than 0 and less than or equal to <see cref="Count"/>.
        /// </param>
        private void RemoveRange(int index, int count)
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("The Deque is empty");
            }

            if (index == 0)
            {
                // Removing from the beginning: shift the start offset
                this.shiftStartOffset(count);
                Count -= count;
                return;
            }
            else if (index == Count - count)
            {
                // Removing from the ending: trim the existing view
                Count -= count;
                return;
            }
            else if (index > Count - count)
            {
                throw new IndexOutOfRangeException("The supplied index is greater than the Count");
            }

            if ((index + (count / 2)) < Count / 2)
            {
                // Removing from first half of list

                // Move items up:
                //  [0, index) -> [count, count + index)
                int copyCount = index;
                int writeIndex = count;
                for (int j = 0; j < copyCount; j++)
                {
                    buffer[toBufferIndex(writeIndex + j)] = buffer[toBufferIndex(j)];
                }

                // Rotate to new view
                this.shiftStartOffset(count);
            }
            else
            {
                // Removing from second half of list

                // Move items down:
                // [index + collectionCount, count) -> [index, count - collectionCount)
                int copyCount = Count - count - index;
                int readIndex = index + count;
                for (int j = 0; j < copyCount; ++j)
                {
                    buffer[toBufferIndex(index + j)] = buffer[toBufferIndex(readIndex + j)];
                }

                this.shiftEndOffset(-count);
            }

            // Adjust valid count
            Count -= count;
        }

        public T Get(int index)
        {
            checkIndexOutOfRange(index);
            return buffer[toBufferIndex(index)];
        }

        public void Set(int index, T item)
        {
            checkIndexOutOfRange(index);
            buffer[toBufferIndex(index)] = item;
        }

    }
}
