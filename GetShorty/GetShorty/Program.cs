using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetShorty
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            List<Graph> list = new List<Graph>();

            //List<Node2> graph = new List<Node2>();



            Dictionary<long, LinkedList<Node3>> graph = new Dictionary<long, LinkedList<Node3>>();
            Dictionary<Node3, LinkedList<Node3>> graph2 = new Dictionary<Node3, LinkedList<Node3>>();

            //8 13
            //0 1 0.1
            //1 2 0.2
            //2 3 0.1
            //0 5 0.8
            //1 6 0.6
            //3 6 0.1
            //0 4 0.4
            //1 5 0.6
            //2 6 0.2
            //3 7 0.4
            //4 5 0.5
            //6 5 0.1
            //6 7 0.1

            string[] testingData = { "3 3", "0 1 0.9", "1 2 0.9", "0 2 0.8" };
            //string[] testingData = { "8 13", "0 1 0.1", "1 2 0.2", "2 3 0.1", "0 5 0.8", "1 6 0.6" };

            string line;
            int lineCount = 0;
            int areaCount = 0;
            int numOfIntersections = 0;
            int numOfCorridors = 0;
            long outResult;

            //while ((line = Console.ReadLine()) != null)
            for (int i = 0; i < testingData.Length; i++)
            {
                //string[] data = line.Split(null);
                string[] data = testingData.ElementAt(i).Split(null);


                if (lineCount == 0)
                {

                    numOfIntersections = int.Parse(data[0]);
                    numOfCorridors = int.Parse(data[1]);
                }
                else
                {



                    long first = long.Parse(data[0]);
                    long second = long.Parse(data[1]);
                    double factor = double.Parse(data[2]);

                    if (!graph.ContainsKey(first))
                    {
                        graph.Add(first, new LinkedList<Node3>());
                    }
                    if (!graph.ContainsKey(second))
                    {
                        graph.Add(second, new LinkedList<Node3>());
                    }
                    graph[first].AddLast(new Node3(first, second, factor));  // create the edges.
                    graph[second].AddLast(new Node3(second, first, factor)); // they are undirected, so create it both ways.


                    //if (!graph2.ContainsKey(first))
                    //{
                    //    graph2.Add(first, new LinkedList<Node3>());
                    //}
                    //if (!graph2.ContainsKey(second))
                    //{
                    //    graph2.Add(second, new LinkedList<Node3>());
                    //}
                    //graph2[first].AddLast(new Node3(first, second, factor));  // create the edges.
                    //graph2[second].AddLast(new Node3(second, first, factor)); // they are undirected, so create it both ways.
                }
                lineCount++;
            }

            p.printGraph(graph);






            p.dijkstra2(graph, 0, numOfIntersections - 1);


           

            Console.ReadLine();

        }


        public void printGraph(Dictionary<long, LinkedList<Node3>> g)
        {
            foreach(KeyValuePair<long, LinkedList<Node3>> entry in g)
            {
                Console.WriteLine(entry.Key + ": ");
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    Console.Write("(me: " + entry.Value.ElementAt(i).myName + ",  neighbor: " + entry.Value.ElementAt(i).next + " , factor: " + entry.Value.ElementAt(i).factor + ")");
                }
                Console.WriteLine();
            }
        }



        public void dijkstra(Dictionary<long, LinkedList<Node3>> g, Node3 s)
        {
            Dictionary<long, double> dist = new Dictionary<long, double>();
            Dictionary<long, Node3> prev = new Dictionary<long, Node3>();

            foreach(KeyValuePair<long, LinkedList<Node3>> entry in g)
            {
                dist.Add(entry.Key, double.MaxValue);
                prev.Add(entry.Key, null);
            }
            dist[s.myName] = 1;

            PriorityQueue pq = new PriorityQueue();
            pq.insertOrChange(s, 0);

            while(!pq.isEmpty())
            {
                Node3 u = pq.deleteMax();
                LinkedList<Node3> edges = g[u.myName];
                foreach (Node3 v in edges)
                {
                    if (dist[v.myName] > dist[u.myName] * v.factor)
                    {
                        dist[v.myName] = dist[u.myName] * v.factor;
                        prev[v.myName] = u;
                        pq.insertOrChange(v, dist[v.myName]);
                    }
                }

            }
        }









        public void dijkstra2(Dictionary<long, LinkedList<Node3>> g, long start, long end)
        {
            Dictionary<long, double> dist = new Dictionary<long, double>();
            Dictionary<long, Node3> prev = new Dictionary<long, Node3>();

            foreach (KeyValuePair<long, LinkedList<Node3>> entry in g)
            {
                dist.Add(entry.Key, double.MaxValue);
                prev.Add(entry.Key, null);
            }
            dist[start] = 1.0;

            PriorityQueue pq = new PriorityQueue();
            pq.insertOrChange(new Node3(start), 1.0);

            while (!pq.isEmpty())
            {
                Node3 u = pq.deleteMax();
                LinkedList<Node3> edges = g[u.myName];
                foreach (Node3 v in edges)
                {
                    if (dist[v.next] > dist[u.myName] * v.factor)
                    {
                        dist[v.next] = dist[u.myName] * v.factor;
                        prev[v.next] = u;
                        pq.insertOrChange(v, dist[v.next]);
                    }
                }

            }

            Console.WriteLine("Final answer: " + dist[end]);
        }

    }






    public class PriorityQueue
    {
        List<Node3> list = new List<Node3>();
        HashSet<Node3> hash = new HashSet<Node3>();

        public PriorityQueue()
        {
            
        }

        public bool isEmpty()
        {
            return list.Count == 0;
        }

        public Node3 deleteMax()
        {
            double maxVal = -1;
            Node3 maxN = new Node3();
            foreach(Node3 n in list)
            {
                if (n.factor > maxVal)
                {
                    maxVal = n.factor;
                    maxN = n;
                }
            }
            list.Remove(maxN);
            hash.Remove(maxN);
            return maxN;
        }

        public void insertOrChange(Node3 v, double w)
        {
            if (hash.Contains(v))
            {
                v.factor = w;
            }
            else
            {
                v.factor = w;
                list.Add(v);
                hash.Add(v);
            }
        }
    }








    public class Edge
    {
        double factor;
        Node neighbor;

        public Edge(Node _next, double _factor)
        {
            factor = _factor;
            neighbor = _next;
        }
    }

    public class Node
    {
        List<Edge> edges;
        long name;
        public Node(long _name)
        {
            name = _name;
        }
    }










    public class Node3
    {
        public double factor;
        public long next;
        public long myName;

        public Node3()
        {
            // empty node
        }
        
        public Node3(long me)
        {
            myName = me;
        }

        public Node3(long me, double _fact)
        {
            myName = me;
            factor = _fact;
        }

        public Node3(long me, long n, double _fact)
        {
            factor = _fact;
            next = n;
            myName = me;
        }
    }






    public class Intersection
    {
        long name;
        public Intersection(long _name)
        {
            name = _name;
        }

        public long getName()
        {
            return name;
        }
    }


    public class Corridor
    {
        long factor;
        Intersection i1;
        Intersection i2;

        public Corridor(Intersection _i1, Intersection _i2, long _factor)
        {
            i1 = _i1;
            i2 = _i2;
            factor = _factor;
        }

        public long getFactor()
        {
            return factor;
        }

        public Intersection getI1()
        {
            return i1;
        }

        public Intersection getI2()
        {
            return i2;
        }
    }


    public class Dungeon
    {
        List<Corridor> corridors;

        public Dungeon()
        {
            // create a new dungeon
        }

        public void createCorridor(Corridor c)
        {
            corridors.Add(c);

        }

        public Intersection containsIntersection(long name)
        {
            foreach (Corridor c in corridors)
            {
                if (c.getI1().getName() == name)
                {
                    return c.getI1();
                }
                else if (c.getI2().getName() == name)
                {
                    return c.getI2();
                }
            }
            return null;
        }
    }












    public class Edge2
    {
        long end;
        double factor;

        public Edge2(long _end, double _factor)
        {
            end = _end;
            factor = _factor;
        }


    }

    public class Node2
    {
        List<Edge2> neighbors;

        public Node2()
        {

        }

        public List<Edge2> getNeighbors()
        {
            return neighbors;
        }
    }






























































    public class Graph
    {
        private List<Vertex> vertice = new List<Vertex>();
        //private Dictionary<Vertex, Vertex> vertices = new Dictionary<Vertex, Vertex>();

        public Graph()
        {
            // don't know what to do here yet
        }

        public void addVertex(Vertex v)
        {
            vertice.Add(v);
        }

        public void addEdgeToVertex(Vertex v1, Vertex v2)
        {
            if (vertice.Contains(v1))
            {
                v1.addEdge(v2);
            }
        }

        public void drawGraph()
        {
            foreach (Vertex v in vertice)
            {
                Console.WriteLine("");
                Console.WriteLine("Vertex " + v.getVertexName() + "(" + v.getPreVisit() + ", " + v.getPostVisit() + ")" + " has edges: ");
                foreach (Vertex v2 in v.getEdges())
                {
                    Console.Write(v2.getVertexName() + "(" + v2.getPreVisit() + ", " + v2.getPostVisit() + ")" + " > ");
                }
            }
        }

        public Vertex getVertexWithName(long s)
        {
            foreach (Vertex v in vertice)
            {
                if (v.getVertexName().Equals(s))
                    return v;
            }

            return null;
        }

        public List<Vertex> getVertices()
        {
            return vertice;
        }
    }

    public class Vertex
    {
        //private Dictionary<string, int> vertex = new Dictionary<string, int>();
        private List<Vertex> edges = new List<Vertex>();
        private long name;
        private int price;
        private int dist;
        private int preVisit;
        private int postVisit;
        private bool visited;
        private long factor;
        private Vertex prev;

        public Vertex(long _name)
        {
            name = _name;
        }

        public Vertex(long _name, int dist)
        {
            name = _name;
            this.dist = dist;
        }

        public void addEdge(Vertex v)
        {
            edges.Add(v);
        }

        public List<Vertex> getEdges()
        {
            return edges;
        }

        public long getVertexName()
        {
            return name;
        }

        public void givePreVisit(int num)
        {
            preVisit = num;
        }
        public int getPreVisit()
        {
            return preVisit;
        }

        public void givePostVisit(int num)
        {
            postVisit = num;
        }
        public int getPostVisit()
        {
            return postVisit;
        }

        public void setVisited(bool vis)
        {
            visited = vis;
        }

        public bool getVisited()
        {
            return visited;
        }

        public void setPrice(int price)
        {
            this.price = price;
        }
        public int getPrice()
        {
            return price;
        }

        public void setDist(int dist)
        {
            this.dist = dist;
        }
        public int getDist()
        {
            return dist;
        }

        public void setPrev(Vertex v)
        {
            prev = v;
        }
    }
}
