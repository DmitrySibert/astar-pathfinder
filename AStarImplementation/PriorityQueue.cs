using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarImplementation.Data.Structure
{
    class PriorityQueue<Element>
    {
        private SortedList<double, Element> queue;

        private class DuplicateKeyOrder : Comparer<double>
        {
            public override int Compare(double x, double y)
            {
                if (x > y)
                {
                    return 1;
                } 
                else if (x < y)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public PriorityQueue()
        {
            this.queue = new SortedList<double, Element>(new DuplicateKeyOrder());
        }

        public void Add(double priority, Element element)
        {
            this.queue.Add(priority, element);
        }

        /// <summary>
        /// Push out top element of queue
        /// </summary>
        /// <returns></returns>
        public Element Top()
        {
            Element top = this.queue.Values[0];
            this.queue.RemoveAt(0);

            return top;
        }

        public bool IsEmpty()
        {
            return this.queue.Count == 0;
        }
    }
}
