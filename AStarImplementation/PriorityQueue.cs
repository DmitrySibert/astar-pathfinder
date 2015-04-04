using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarImplementation
{
    
    class PriorityQueue<Element>
    {
        //private SortedSet<KeyValuePair<double, Element>> queue;
        private SortedDictionary<double, Element> queue;

        //private class KeyOrder : Comparer<KeyValuePair<double, Element>>
        //{
        //    public override int Compare(KeyValuePair<double, Element> x, KeyValuePair<double, Element> y)
        //    {
        //        if (x.Key > y.Key)
        //        {
        //            return 1;
        //        }
        //        else if (x.Key < y.Key)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            return 1;
        //        }

        //    }
        //}

        private class KeyOrder : Comparer<double>
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
            //this.queue = new SortedSet<KeyValuePair<double, Element>>(new KeyOrder());
            this.queue = new SortedDictionary<double, Element>(new KeyOrder());
        }

        public void Add(double priority, Element element)
        {

            //this.queue.Add(new KeyValuePair<double, Element>(priority, element));
            this.queue.Add(priority, element);
        }

        /// <summary>
        /// Push out top element of queue
        /// </summary>
        /// <returns></returns>
        public Element Top()
        {
            //KeyValuePair<double, Element> top = this.queue.Min;
            //this.queue.Remove(top);

            Element top = this.queue.Keys[0];
            this.queue.Remove(top);

            return top.Value;
        }

        public bool IsEmpty()
        {
            return this.queue.Count == 0;
        }
    }
}
