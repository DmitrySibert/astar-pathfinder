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
        double Calculate(Location l1, Location l2);
    }

    public interface IPathBuilder<Location>
    {
        List<Location> Build(Dictionary<Location, Location> came_from, Location start, Location goal);
    }

    public class AStar<Location>
        where Location : IEquatable<Location>
    {
        private IHeuristicStrategy<Location> heuristic;
        private IPathBuilder<Location> pathBuilder;

        public AStar(IHeuristicStrategy<Location> heuristic, IPathBuilder<Location> pathBuilder)
        {
            this.heuristic = heuristic;
            this.pathBuilder = pathBuilder;
        }

        public List<Location> Run(IWeightedGraph<Location> graph, Location start, Location goal)
        {
            PriorityQueue<Location> open = new PriorityQueue<Location>();
            open.Add(0, start);
            Dictionary<Location, double> g_cost = new Dictionary<Location, double>();                   //cost from start vertex to current
            g_cost[start] = 0;
            Dictionary<Location, Location> came_from = new Dictionary<Location, Location>();
            while(!open.IsEmpty())
            {
                Location current = open.Top();
                if (current.Equals(goal))
                {
                    break;
                }
                IEnumerable<Location> neighbours = graph.GetNeighbors(current);
                foreach (Location item in neighbours)
	            {
                    //magic number is cost of transition from current to its neighbor
                    double cost = g_cost[current] + graph.Cost(current, item); 		 
                    if (!g_cost.ContainsKey(item) || cost < g_cost[item])
                    {
                        g_cost[item] = cost;   
                        double priority = this.heuristic.Calculate(item, goal);
                        open.Add(priority, item);
                        came_from[item] = current;
                    }
	            }
            }

            return this.pathBuilder.Build(came_from, start, goal);
        }
    }

    public class SimplePathBuilder<Location> : IPathBuilder<Location>
        where Location : IEquatable<Location>
    {
        public List<Location> Build(Dictionary<Location, Location> came_from, Location start, Location goal)
        {
            List<Location> path = new List<Location>();
            if (!came_from.ContainsKey(goal))
            {
                return path;
            }
            path.Add(goal);
            Location cur = came_from[goal];
            path.Add(cur);
            Location from;
            while (true)
            {
                from = came_from[cur];
                path.Add(from);
                if (from.Equals(start))
                {
                    break;
                }
                cur = from;
            }

            return path;
        }
    }

    public class ChebishevDist2D : IHeuristicStrategy<Location2D>
    {
        public double Calculate(Location2D l1, Location2D l2)
        {
            double dx = Math.Abs(l1.x - l2.x);
            double dy = Math.Abs(l1.y - l2.y);

            return Math.Max(dx, dy);
        }
    }

    public class ManhattanDist2D : IHeuristicStrategy<Location2D>
    {
        public double Calculate(Location2D l1, Location2D l2)
        {
            double dx = Math.Abs(l1.x - l2.x);
            double dy = Math.Abs(l1.y - l2.y);

            return dx + dy;
        }
    } 
}
