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

        [TestFixtureSetUp]
        public void SetUp()
        {
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

            deque.Add(1);
        }
    }
}
