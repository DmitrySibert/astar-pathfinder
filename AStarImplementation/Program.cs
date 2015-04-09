using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStarImplementation.Data.Structure;

namespace AStarImplementation
{
    struct Limit2D
    {
        public int start;
        public int end;
        public Limit2D(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    };
   
    class Program
    {
        static byte[,] matrix = new byte[5, 5]
        {
            {1,0,1,1,1},
            {1,0,1,1,1},
            {1,0,1,0,1},
            {1,1,1,0,1},
            {1,1,1,0,1}
        };

        static void setNeighboursAround(
            int l_i, int l_j, Graph<Location2D> grid, Limit2D limit_i, Limit2D limit_j
        )
        {
            Location2D current = new Location2D(l_i,l_j);
            for (int i = l_i - limit_i.start; i <= l_i + limit_i.end; i++)
            {
                for (int j = l_j - limit_j.start; j <= l_j + limit_j.end; j++)
                {
                    if (!(i == l_i && j == l_j))
                    {
                        if (matrix[i,j] == 1)
                        {
                            grid.AddEdge(current, new Location2D(i, j));
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Graph<Location2D> grid = new Graph<Location2D>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        grid.AddNode(new Location2D(i, j));
                    }
                }
            }
            //top raw without corners
            int topRowNum = 0;
            for (int j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                Limit2D limit_i = new Limit2D(0,1);
                Limit2D limit_j = new Limit2D(1,1);
                if (matrix[topRowNum, j] == 1)
                {
                    setNeighboursAround(topRowNum, j, grid, limit_i, limit_j);
                }
            }
            
            //bottom line without corners
            int botRawNum = matrix.GetLength(0) - 1;
            for (int j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                Limit2D limit_i = new Limit2D(1, 0);
                Limit2D limit_j = new Limit2D(1, 1);
                if (matrix[botRawNum, j] == 1)
                {
                    setNeighboursAround(botRawNum, j, grid, limit_i, limit_j);
                }
            }

            //left column without corners
            int leftColNum = 0;
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                Limit2D limit_i = new Limit2D(1, 1);
                Limit2D limit_j = new Limit2D(0, 1);
                if (matrix[i, leftColNum] == 1)
                {
                    setNeighboursAround(i, leftColNum, grid, limit_i, limit_j);
                }
            }

            //right column without corners
            int rightColNum = matrix.GetLength(1) - 1;
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                Limit2D limit_i = new Limit2D(1, 1);
                Limit2D limit_j = new Limit2D(1, 0);
                if (matrix[i, rightColNum] == 1)
                {
                    setNeighboursAround(i, rightColNum, grid, limit_i, limit_j);
                }
            }

            Limit2D limitI = new Limit2D(0, 1);
            Limit2D limitJ = new Limit2D(0, 1);
            setNeighboursAround(0, 0, grid, limitI, limitJ);
            limitI = new Limit2D(0, 1);
            limitJ = new Limit2D(1, 0);
            setNeighboursAround(0, matrix.GetLength(0) - 1, grid, limitI, limitJ);
            limitI = new Limit2D(1, 0);
            limitJ = new Limit2D(0, 1);
            setNeighboursAround(matrix.GetLength(0) - 1, 0, grid, limitI, limitJ);
            limitI = new Limit2D(1, 0);
            limitJ = new Limit2D(1, 0);
            setNeighboursAround(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1, grid, limitI, limitJ);

            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        limitI = new Limit2D(1, 1);
                        limitJ = new Limit2D(1, 1);
                        setNeighboursAround(i, j, grid, limitI, limitJ);
                    }
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] == 1)
                    {
                        IEnumerable<Location2D> list = grid.GetNeighbors(new Location2D(i, j));
                        Console.Write("x={0};y={1} ::", i, j);
                        foreach(Location2D item in list)
                        {
                            Console.Write("({0};{1}) ", item.x, item.y);
                            
                        }
                        Console.WriteLine();
                    }
                }
            }

           
            Location2D start = new Location2D(0,0);
            Location2D goal = new Location2D(4,4);
            AStar<Location2D> astar = new AStar<Location2D>(new ChebishevDist2D(), new SimplePathBuilder<Location2D>());
            List<Location2D> path = astar.Run(grid, start, goal);

            int kk = 1;
        }

        static void priorityQueueTest()
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
            location = queue.Top();
            location = queue.Top();
            location = queue.Top();
            location = queue.Top();

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

            IEnumerable<Location2D> neighborsA = graph.GetNeighbors(A);
            IEnumerable<Location2D> neighborsC = graph.GetNeighbors(C);

            int k = 10;
        }
    }
}
