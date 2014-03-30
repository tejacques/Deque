using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deque.Tests
{
    [TestFixture]
    public class DequeTests
    {

        Deque<int> d;
        Queue<int> q;
        Stack<int> s;
        List<int> l;
        LinkedList<int> ll;

        [TestFixtureSetUp]
        public void SetUp()
        {
            d = new Deque<int>();
            q = new Queue<int>();
            s = new Stack<int>();
            l = new List<int>();
            ll = new LinkedList<int>();
        }

        [Test]
        public void TestConstructor()
        {
            Deque<int> deque;

            deque = new Deque<int>();
            Assert.AreEqual(16, deque.Capacity);

            deque = new Deque<int>(8);
            Assert.AreEqual(8, deque.Capacity);

            int[] arr = new [] {0,1,2,3,4,5,6,7};
            deque = new Deque<int>(arr);
            Assert.AreEqual(arr.Length, deque.Capacity);
            foreach (int item in arr)
            {
                Assert.IsTrue(deque.Contains(item));
            }
        }

        [Test]
        public void TestAdd()
        {
            Deque<int> deque = new Deque<int>();

            Assert.IsTrue(deque.IsEmpty);

            int item = 1;

            Assert.IsFalse(deque.Contains(item));
            deque.Add(item);

            int actualBack;
            actualBack = deque[0];

            Assert.IsTrue(deque.Contains(item));
            Assert.AreEqual(item, actualBack);

            int itemNewBack = 2;
            Assert.IsFalse(deque.Contains(itemNewBack));
            deque.AddBack(itemNewBack);

            Assert.IsTrue(deque.Contains(itemNewBack));
            actualBack = deque[1];
            Assert.AreEqual(itemNewBack, actualBack);

            actualBack = deque.RemoveBack();
            Assert.AreEqual(itemNewBack, actualBack);

            int itemNewFront = -1;
            Assert.IsFalse(deque.Contains(itemNewFront));
            deque.AddFront(itemNewFront);

            int actualFront;
            Assert.IsTrue(deque.Contains(itemNewFront));

            actualFront = deque[0];
            Assert.AreEqual(itemNewFront, actualFront);

            actualFront = deque.RemoveFront();
            Assert.AreEqual(itemNewFront, actualFront);

            int[] itemRange = new[] { 3, 4, 5, 6 };
            int offset = deque.Count;
            deque.AddRange(itemRange);

            foreach (var itm in itemRange)
            {
                Assert.AreEqual(itm, deque[offset]);
                offset++;
            }

            int[] itemBackRange = new[] { 7, 8, 9, 10 };
            offset = deque.Count;
            deque.AddBackRange(itemBackRange);
            foreach (var itm in itemBackRange)
            {
                Assert.AreEqual(itm, deque[offset]);
                offset++;
            }

            int[] itemFrontRange = new[] { 3, 4, 5, 6 };
            deque.AddFrontRange(itemFrontRange);

            for (int i = 0; i < itemFrontRange.Length; i++)
            {
                var itm = itemFrontRange[i];
                Assert.AreEqual(itm, deque[i]);
            }
        }

        [Test]
        public void TestBulkAdd()
        {
            int loops = 10000;
            Deque<int> deque = new Deque<int>();
            for (int i = 1; i < loops; i++)
            {
                deque.Add(i);
            }

            deque.AddFront(0);

            Deque<int> dequeCopy = new Deque<int>(deque);

            for (int expected = 0; expected < loops; expected++)
            {
                int actual = deque.RemoveFront();
                int actualCopy = dequeCopy[expected];

                Assert.AreEqual(expected, actual, "Original deque item differs from expected value");
                Assert.AreEqual(expected, actualCopy, "Copied deque item differs from expected value");
            }

        }

        [Test]
        public void TestBulkAddFront()
        {
            int loops = 10000;
            Deque<int> deque = new Deque<int>();
            for (int i = loops -1; i >= 0; i--)
            {
                deque.AddFront(i);
            }

            Deque<int> dequeCopy = new Deque<int>(deque);

            for (int expected = 0; expected < loops; expected++)
            {
                int actual = deque.RemoveFront();
                int actualCopy = dequeCopy[expected];

                Assert.AreEqual(expected, actual, "Original deque item differs from expected value");
                Assert.AreEqual(expected, actualCopy, "Copied deque item differs from expected value");
            }

        }

        [Test]
        public void TestBulkInsert()
        {
            int loops = 100000;
            Deque<int> deque = new Deque<int>();

            deque.AddFront(loops - 1);
            for (int i = 0; i < loops - 1; i++)
            {
                deque.Insert(deque.Count-1, i);
            }

            Deque<int> dequeCopy = new Deque<int>(deque);

            for (int expected = 0; expected < loops; expected++)
            {
                int actual = deque.RemoveFront();
                int actualCopy = dequeCopy[expected];

                Assert.AreEqual(expected, actual, "Original deque item differs from expected value");
                Assert.AreEqual(expected, actualCopy, "Copied deque item differs from expected value");
            }

        }

        [Test]
        public void TestRemove()
        {
            int[] items = new[] { 0, 1, 2, 3 };
            Deque<int> deque = new Deque<int>(items);

            Assert.IsTrue(deque.Contains(0));
            Assert.IsTrue(deque.Remove(0));
            Assert.IsFalse(deque.Contains(0));
            Assert.AreEqual(1, deque.RemoveFront());
            Assert.AreEqual(3, deque.RemoveBack());

            deque.Clear();
            deque.AddRange(items);
            Assert.IsTrue(deque.Contains(2));
            deque.RemoveAt(2);
            Assert.IsFalse(deque.Contains(2));

            deque.Clear();
            deque.AddRange(items);

            foreach (var item in items)
            {
                Assert.IsTrue(deque.Contains(item));
            }

            deque.RemoveRange(1, 2);

            Assert.IsTrue(deque.Contains(0));
            Assert.IsFalse(deque.Contains(1));
            Assert.IsFalse(deque.Contains(2));
            Assert.IsTrue(deque.Contains(3));
        }
    }
}
