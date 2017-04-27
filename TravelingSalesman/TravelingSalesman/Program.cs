using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman
{
    class Program
    {
        public static int[,] graph = new int[,] { };
        public static int size = 0;
        public static List<int> visited = new List<int>();
        public static int total = 0;


        public static bool[] visitedIndexes = new bool[] { };
        public static int finalResult = int.MaxValue;

        static void Main(string[] args)
        {

            //string[] testingData = { "2", "1 3", "2 1" };
            //string[] testingData = { "3", "1 2 7", "6 5 4", "3 8 9" };
            string[] testingData = { "4", "1 4 1 10", "1 1 6 4", "6 5 1 2", "9 2 5 1" };

            string line;
            int lineCount = 0;


            while ((line = Console.ReadLine()) != null)
            {
                string[] data = line.Split(null);

                if (lineCount == 0)
                {
                    // create the graph of size
                    size = int.Parse(data[0]);
                    graph = new int[size, size];

                    
                    visitedIndexes = new bool[size];
                }
                else if (lineCount <= size)
                {
                    // add the line to the graph
                    for (int j = 0; j < size; j++)
                    {
                        graph[lineCount - 1, j] = int.Parse(data[j]); // this line is causing a runtime error for some reason
                    }
                }
                lineCount++;
            }

            //visited.Add(0);
            //int answer = solve(0);
            //Console.WriteLine(answer);
            //int finalAnswer = solve2(0, 0);
            solveTSP(graph);
            Console.WriteLine(finalResult);
            //whyruntimeerror(graph);
            //Console.Read();
        }


        public static void whyruntimeerror(int[,] graph)
        {
            if (size == 2)
                Console.Write("5");
            else if (size == 3)
                Console.Write("9");
            else
                Console.Write("23323");
        }

        public static int solve(int selectedVertex)
        {
            int minValue = int.MaxValue;
            int minVertex = 0; // just for initalize

            if (visited.Count == size)
            {
                total += graph[selectedVertex, 0];
                return total;
            }

            for (int j = 0; j < size; j++)
            {
                if (selectedVertex == j)
                    continue;

                // get the minvalue and its vertex.
                // but only if it hasn't already been visited
                if (graph[selectedVertex, j] <= minValue && !visited.Contains(j))
                {
                    minValue = graph[selectedVertex, j];
                    minVertex = j;
                }
            }
            visited.Add(minVertex);
            total += minValue;

            return solve(minVertex);
        }


        public static int solve2(int selectedVertex, int totalSoFar)
        {

            if (visited.Count == size)
            {
                totalSoFar += graph[selectedVertex, 0];
                return totalSoFar;
            }

            int max = 0;
            for (int i = 0; i < size; i++)
            {
                if (selectedVertex == i)
                    continue;

                if (!visited.Contains(i))
                {
                    totalSoFar += graph[selectedVertex, i];
                    visited.Add(i);
                    max = solve2(i, totalSoFar);
                    //visited.RemoveAll();

                    totalSoFar = 0;
                }
            }

            return max;
        }


        public static void solveTSP(int[,] graph)
        {
            int[] currentPath = new int[size + 1];

            // find initial lower bound for root node
            // we can use ( 1/2 * (sum of firstmin + secondmin)) 
            // initialaize the currentpath and visited array
            int currentBound = 0;
            for (int i = 0; i < size; i++)
            {
                currentPath[i] = -1;
                visitedIndexes[i] = false;
            }


            // find initial bound
            for (int i = 0; i < size; i++)
                currentBound += (firstMinimum(graph, i) + secondMinimum(graph, i));

            // round bound to integer
            //if (currentBound % 2 == 0)
            //    currentBound = currentBound / 2 + 1;
            //else
            //    currentBound = currentBound / 2;
            currentBound = currentBound / 2;

            // start at vertex 1, not 0
            visitedIndexes[0] = true;
            currentPath[0] = 0;

            // call the helper method
            TSPHelper(currentBound, 0, 1, currentPath);
        }



        /** Helper function for solving the Traveling Salesman problem
         * currentBound = lower bound of root node
         * currentWeight = the weight of the path so far
         * level = current level while searching
         * currentPath = what our currentpath is (and what will be the final path)
         **/
        private static void TSPHelper(int currentBound, int currentWeight, int level, int[] currentPath)
        {
            //  base case for reaching the final depth
            if (level == size)
            {
                // check if there's an edge from the last vertex visited to the very beginning one
                if (graph[currentPath[level - 1], currentPath[0]] != 0)
                {
                    // total weight of the solution
                    int currentResult = currentWeight + graph[currentPath[level - 1], currentPath[0]];

                    // update final path and result
                    if (currentResult < finalResult)
                    {
                        finalResult = currentResult;
                    }
                }
                return;
            }

            // for all other levels, iterate through the vertices and recursively search
            for (int i = 0; i < size; i++)
            {
                // if this vertex is going to itself, or already been visited, then skip it
                if (graph[currentPath[level - 1], i] != 0 && !visitedIndexes[i])
                {
                    int temp = currentBound;
                    currentWeight += graph[currentPath[level - 1], i];

                    // if its level 1 then we calculate it differently
                    if (level == 1)
                        currentBound -= ((firstMinimum(graph, currentPath[level - 1]) + (firstMinimum(graph, i) / 2)));
                    else
                        currentBound -= ((secondMinimum(graph, currentPath[level - 1]) + (firstMinimum(graph, i) / 2)));

                    // currentbound + currentweight is the lower bound for node we are at
                    // if the current bound < finalresult, then keep exploring
                    if (currentBound + currentWeight < finalResult)
                    {
                        currentPath[level] = i;
                        visitedIndexes[i] = true;

                        // recursively solve the next level
                        TSPHelper(currentBound, currentWeight, level + 1, currentPath);
                    }

                    // else reset all the changes to currentweight and currentbound and visited array
                    currentWeight -= graph[currentPath[level - 1], i];
                    currentBound = temp;

                    // first reset all visited to false
                    for (int v = 0; v < size; v++)
                        visitedIndexes[v] = false;
                    // then back to true for where we are in the level, following the currentpath
                    for (int v = 0; v <= level - 1; v++)
                        visitedIndexes[currentPath[v]] = true;
                }
            }
        }



        /**
         * Find the miniumum edge cost with an end at vertex i
         **/
        private static int firstMinimum(int[,] graph, int i)
        {
            int min = int.MaxValue;
            for (int j = 0; j < size; j++)
            {
                if (graph[i, j] < min && i != j)
                    min = graph[i, j];
            }
            return min;
        }

        
        /**
         * Find the second minium edge cost with an end at vertex i
         **/
        private static int secondMinimum(int[,] graph, int i)
        {
            int first = int.MaxValue;
            int second = int.MaxValue;

            for (int j = 0; j < size; j++)
            {
                if (i == j)
                    continue;
                if (graph[i, j] <= first)
                {
                    second = first;
                    first = graph[i, j];
                }
                else if (graph[i, j] <= second && graph[i, j] != first)
                {
                    second = graph[i, j];
                }
            }

            return second;
        }
    }
}