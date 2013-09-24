using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        [Test]
        public void TestBulkAdd()
        {
            int loops = 1000000;
            Deque<int> deque = new Deque<int>();
            for (int i = 0; i < loops; i++)
            {
                deque.Add(i);
            }

            for (int expected = 0; expected < loops; expected++)
            {
                int actual = deque.RemoveFront();

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
