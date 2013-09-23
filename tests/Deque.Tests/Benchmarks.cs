using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque.Tests
{
    class Benchmarks
    {
        Deque<int> d;
        Queue<int> q;
        Stack<int> s;
        List<int> l;
        LinkedList<int> ll;

        int loops = 10000000;

        [TestFixtureSetUp]
        public void SetUp()
        {
            d = new Deque<int>(loops);
            q = new Queue<int>(loops);
            s = new Stack<int>(loops);
            l = new List<int>(loops);
            ll = new LinkedList<int>();
        }

        [Test]
        public void TestPerformanceDequeAdd()
        {
            for (int i = 0; i < loops; i++)
            {
                d.Add(i);
            }
        }

        [Test]
        public void TestPerformanceDequeAddFront()
        {
            d = new Deque<int>(loops);
            for (int i = 0; i < loops; i++)
            {
                d.AddFront(i);
            }
        }

        [Test]
        public void TestPerformanceDequeIterate()
        {
            foreach (var item in d)
            {
            }
        }

        [Test]
        public void TestPerformanceDequeRemove()
        {
            for (int i = 0; i < loops; i++)
            {
                d.RemoveBack();
            }
        }

        [Test]
        public void TestPerformanceStackAdd()
        {
            for (int i = 0; i < loops; i++)
            {
                s.Push(i);
            }
        }

        [Test]
        public void TestPerformanceStackIterate()
        {
            foreach (var item in s)
            {
            }
        }

        [Test]
        public void TestPerformanceStackRemove()
        {
            for (int i = 0; i < loops; i++)
            {
                s.Pop();
            }
        }

        [Test]
        public void TestPerformanceQueueAdd()
        {
            for (int i = 0; i < loops; i++)
            {
                q.Enqueue(i);
            }
        }

        [Test]
        public void TestPerformanceQueueIterate()
        {
            foreach (var item in q)
            {
            }
        }

        [Test]
        public void TestPerformanceQueueRemove()
        {
            for (int i = 0; i < loops; i++)
            {
                q.Dequeue();
            }
        }

        [Test]
        public void TestPerformanceListAdd()
        {
            for (int i = 0; i < loops; i++)
            {
                l.Add(i);
            }
        }

        [Test]
        public void TestPerformanceListIterate()
        {
            foreach (var item in l)
            {
            }
        }

        [Test]
        public void TestPerformanceListRemove()
        {
            for (int i = 0; i < loops; i++)
            {
                l.RemoveAt(l.Count - 1);
            }
        }

        [Test]
        public void TestPerformanceLinkedListAdd()
        {
            for (int i = 0; i < loops; i++)
            {
                ll.AddLast(i);
            }
        }

        [Test]
        public void TestPerformanceLinkedListIterate()
        {
            foreach (var item in ll)
            {
            }
        }

        [Test]
        public void TestPerformanceLinkedListRemove()
        {
            for (int i = 0; i < loops; i++)
            {
                ll.RemoveLast();
            }
        }
    }
}
