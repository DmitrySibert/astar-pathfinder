using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarImplementation.Data.Structure
{
    public class Location2D : IEquatable<Location2D>
    {
        public readonly double x,y;

        public Location2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Location2D p = obj as Location2D;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (x == p.x) && (y == p.y);
        }

        public bool Equals(Location2D p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (x == p.x) && (y == p.y);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + x.GetHashCode();
            hash = hash * 31 + y.GetHashCode();

            return hash;
        }

    }
    public class Graph<Node>
        where Node : IEquatable<Node>
    {
        private Dictionary<Node, HashSet<Node>> edges;

        public Graph()
        {
            this.edges = new Dictionary<Node, HashSet<Node>>();  
        }

        public void AddNode(Node node)
        {
            HashSet<Node> edges = new HashSet<Node>();
            this.edges.Add(node, edges);
        }

        public void AddEdge(Node node1, Node node2)
        {
            this.edges[node1].Add(node2);
            this.edges[node2].Add(node1);
        }

        public List<Node> GetNeighbors(Node node)
        {
            return this.edges[node].ToList<Node>();
        }
    }
}
