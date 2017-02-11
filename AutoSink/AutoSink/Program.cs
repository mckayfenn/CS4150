using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class Program
    {
        public static int clock = 1;

        public static List<Vertex> topologicalSort = new List<Vertex>();

        static void Main(string[] args)
        {
            Graph myGraph = new Graph();
            Vertex mv1 = new Vertex("Sourceville", 5);
            Vertex mv2 = new Vertex("SinkCity", 10);
            Vertex mv3 = new Vertex("Easton", 20);
            Vertex mv4 = new Vertex("Weston", 15);

            myGraph.addVertex(mv1);
            myGraph.addVertex(mv2);
            myGraph.addVertex(mv3);
            myGraph.addVertex(mv4);

            myGraph.addEdgeToVertex(mv1, mv3);
            myGraph.addEdgeToVertex(mv1, mv4);
            myGraph.addEdgeToVertex(mv4, mv2);
            myGraph.addEdgeToVertex(mv3, mv2);

            //myGraph.drawGraph();




            //string[] testingData = new string[17];
            //testingData[0] = "4";
            //testingData[1] = "Sourceville 5";
            //testingData[2] = "SinkCity 10";
            //testingData[3] = "Easton 20";
            //testingData[4] = "Weston 15";
            //testingData[5] = "4";
            //testingData[6] = "Sourceville Easton";
            //testingData[7] = "Sourceville Weston";
            //testingData[8] = "Weston SinkCity";
            //testingData[9] = "Easton SinkCity";
            //testingData[10] = "6";
            //testingData[11] = "Sourceville SinkCity";
            //testingData[12] = "Easton SinkCity";
            //testingData[13] = "SinkCity SinkCity";
            //testingData[14] = "Weston Weston";
            //testingData[15] = "Weston Sourceville";
            //testingData[16] = "SinkCity Sourceville";


            //string[] testingData = new string[10];
            //testingData[0] = "3";
            //testingData[1] = "Here 10";
            //testingData[2] = "There 20";
            //testingData[3] = "Over 9";
            //testingData[4] = "1";
            //testingData[5] = "Here Over";
            //testingData[6] = "3";
            //testingData[7] = "Here There";
            //testingData[8] = "There There";
            //testingData[9] = "Here Over";

            //string[] testingData = new string[23];
            //testingData[0] = "7";
            //testingData[1] = "A 8";
            //testingData[2] = "B 4";
            //testingData[3] = "D 2";
            //testingData[4] = "E 1";
            //testingData[5] = "C 6";
            //testingData[6] = "G 2";
            //testingData[7] = "F 2";
            //testingData[8] = "9";
            //testingData[9] = "A B";
            //testingData[10] = "A D";
            //testingData[11] = "D B";
            //testingData[12] = "D E";
            //testingData[13] = "B E";
            //testingData[14] = "E C";
            //testingData[15] = "E G";
            //testingData[16] = "C G";
            //testingData[17] = "G F";
            //testingData[18] = "4";
            //testingData[19] = "A E";
            //testingData[20] = "A B";
            //testingData[21] = "A D";
            //testingData[22] = "E F";


            string[] testingData = new string[5];
            testingData[0] = "1";
            testingData[1] = "Here 10";
            testingData[2] = "0";
            testingData[3] = "1";
            testingData[4] = "Here Here";


            Graph graph = new Graph();

            string line;
            int lineCount = 0;
            int areaCount = 0;
            int numOfCitites = 5000;
            int numOfHighways = 15000;
            int numOfTrips = 15000;

            //Dictionary<string, string> trips = new Dictionary<string, string>();
            List<Tuple<string, string>> trips = new List<Tuple<string, string>>();

            int result = 0;

            //while ((line = Console.ReadLine()) != null)
            for (int i = 0; i < testingData.Length; i++)
            {
                if (Int32.TryParse(testingData[i], out result))
                {
                    if (areaCount == 0)
                    {
                        //numOfCitites = Int32.Parse(line);
                        numOfCitites = result;
                    }
                    else if (areaCount == 1)
                    {
                        //numOfHighways = Int32.Parse(line);
                        numOfHighways = result;
                    }
                    else if (areaCount == 2)
                    {
                        //numOfTrips = Int32.Parse(line);
                        numOfTrips = result;
                    }

                    areaCount++;
                }
                else
                {
                    // it's a different line.
                    Vertex v1;
                    Vertex v2;
                    //string[] data = line.Split(null);
                    string[] data = testingData.ElementAt(i).Split(null);



                    if (areaCount == 1) // we are creating vertices
                    {
                        v1 = new Vertex(data[0], Int32.Parse(data[1]));
                        graph.addVertex(v1);
                    }
                    else if (areaCount == 2) // we are creating edges
                    {
                        v1 = graph.getVertexWithName(data[0]);
                        v2 = graph.getVertexWithName(data[1]);

                        if (v1 != null && v2 != null)
                            graph.addEdgeToVertex(v1, v2);
                        else
                            Console.WriteLine("There's a problem trying to add an edge to a vertex");
                    }
                    else if (areaCount == 3) // we are trying to see if a trip is valid
                    {
                        trips.Add(new Tuple<string, string>(data[0], data[1]));
                    }
                }

                lineCount++;
            }


            depthFirstSearch(graph); // this will also create the topological sort

            foreach (Tuple<string, string> entry in trips)
            {
                Vertex source = graph.getVertexWithName(entry.Item1);
                bfs(graph, source);
                Vertex sink = graph.getVertexWithName(entry.Item2);
                if (sink.getDist() == Int32.MaxValue)
                {
                    Console.WriteLine("NO");
                }
                else
                {
                    Console.WriteLine(sink.getDist());
                }
            }

            Console.ReadLine();
        }

        public static void printTopoSort(Graph g)
        {
            Console.WriteLine("Topological Sort: ");
            for (int i = topologicalSort.Count - 1; i >= 0; i--)
            {
                Console.Write(topologicalSort[i].getVertexName() + ",");
            }
        }

        public static void printbfs()
        {

        }

        public static void depthFirstSearch(Graph g)
        {
            foreach (Vertex v in g.getVertices())
            {
                v.setVisited(false);
            }
            foreach (Vertex v in g.getVertices())
            {
                if (!v.getVisited())
                {
                    explore(g, v);
                }
            }
        }

        public static void explore(Graph G, Vertex v)
        {
            v.setVisited(true);
            v.givePreVisit(clock);
            clock++;
            foreach (Vertex u in v.getEdges())
            {
                if (!u.getVisited())
                {
                    explore(G, u);
                }
            }
            v.givePostVisit(clock);
            clock++;
            topologicalSort.Add(v);
        }

        public static void bfs(Graph g, Vertex s)
        {
            Stack<Vertex> stack = new Stack<Vertex>();

            int inf = Int32.MaxValue;
            foreach (Vertex v in topologicalSort)
            {
                stack.Push(v);
            }
            foreach (Vertex u in g.getVertices())
            {
                u.setDist(inf);

            }

            s.setDist(0);

            while (stack.Count != 0)
            {
                // Get the next vertex from topological order
                Vertex u = stack.Pop();

                if (u.getDist() != inf)
                {
                    foreach (Vertex v in u.getEdges())
                    {
                        if (v.getDist() > u.getDist() + v.getPrice())
                        {
                            v.setDist(u.getDist() + v.getPrice());
                        }
                    }
                }
            }

            for (int i = topologicalSort.Count - 1; i >= 0; i--)
            {
                if (topologicalSort[i].getDist() == inf)
                {
                    //Console.WriteLine(s.getVertexName() + " to " + topologicalSort[i].getVertexName() + " is: " + "NO");
                }
                else
                {
                    //Console.WriteLine(s.getVertexName() + " to " + topologicalSort[i].getVertexName() + " is: " + topologicalSort[i].getDist());
                }
            }
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

        public Vertex getVertexWithName(string s)
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
        private LinkedList<Vertex> edges = new LinkedList<Vertex>();
        private string name;
        private int price;
        private int dist;
        private int preVisit;
        private int postVisit;
        private bool visited;

        public Vertex(string city, int price)
        {
            name = city;
            this.price = price;
        }

        public void addEdge(Vertex v)
        {
            edges.AddLast(v);
        }

        public LinkedList<Vertex> getEdges()
        {
            return edges;
        }

        public string getVertexName()
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
    }
}
