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
                }
                else
                {
                    // add the line to the graph
                    for (int j = 0; j < data.Length; j++)
                    {
                        graph[lineCount - 1, j] = int.Parse(data[j]);
                    }
                }
                lineCount++;
            }

            visited.Add(0);
            //Console.WriteLine(solve(0));
            int finalAnswer = solve2(0, 0);
            Console.WriteLine(finalAnswer);
            Console.Read();
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
                    totalSoFar = 0;
                }
            }

            return max;
        }
    }
}