using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace AStarImplementation
{
   
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<Location2D> queue = new PriorityQueue<Location2D>();

            Location2D A = new Location2D(1, 1);
            Location2D B = new Location2D(2, 2);
            Location2D C = new Location2D(3, 3);
            Location2D D = new Location2D(4, 3);

            queue.Add(1.1, A);
            queue.Add(1.1, D);
            queue.Add(2.1, B);
            queue.Add(1.5, C);

            Location2D location = queue.Top();

            int k = 10;
        }

        static void GraphTest()
        {
            Location2D A = new Location2D(1, 1);
            Location2D B = new Location2D(2, 2);
            Location2D C = new Location2D(3, 3);
            Location2D D = new Location2D(4, 4);

            Graph<Location2D> graph = new Graph<Location2D>();
            graph.AddNode(A);
            graph.AddNode(B);
            graph.AddNode(C);
            graph.AddNode(D);
            graph.AddEdge(A, B);
            graph.AddEdge(A, C);
            graph.AddEdge(A, D);
            graph.AddEdge(D, C);

            List<Location2D> neighborsA = graph.GetNeighbors(A);
            List<Location2D> neighborsC = graph.GetNeighbors(C);

            int k = 10;
        }
    }
}
