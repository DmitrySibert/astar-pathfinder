using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStarImplementation.Data.Structure;

namespace AStarImplementation
{
    public interface IHeuristicStrategy<Location>
    {
        double calculate(Location l1, Location l2);
    }

    class AStar
    {
        public static Dictionary<Location2D, Location2D> Run(
            Graph<Location2D> graph, Location2D start, Location2D goal, IHeuristicStrategy<Location2D> heuristic)
        {
            PriorityQueue<Location2D> open = new PriorityQueue<Location2D>();
            open.Add(0, start);
            Dictionary<Location2D, double> g_cost = new Dictionary<Location2D,double>();                   //cost from start vertex to current
            g_cost[start] = 0;
            Dictionary<Location2D, Location2D> came_from = new Dictionary<Location2D, Location2D>();
            while(!open.IsEmpty())
            {
                Location2D current = open.Top();
                if (current.Equals(goal))
                {
                    break;
                }
                List<Location2D> neighbours = graph.GetNeighbors(current);
                foreach (Location2D item in neighbours)
	            {
                    //magic number is cost of transition from current to its neighbor
                    double cost = g_cost[current] + 1; 		 
                    if (!g_cost.ContainsKey(item) || cost < g_cost[item])
                    {
                        g_cost[item] = cost;   
                        double priority = heuristic.calculate(item, goal);
                        open.Add(priority, item);
                        came_from[item] = current;
                    }
	            }
            }

            return came_from;
        }
    }
}
