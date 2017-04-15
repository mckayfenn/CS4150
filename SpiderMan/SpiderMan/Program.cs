using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderMan
{
    class Program
    {


        static void Main(string[] args)
        {
            //string[] testingData = { "1", "4", "20 20 20 20" }; // UDUD
            //string[] testingData = { "1", "6", "3 2 5 3 1 2" };
            //string[] testingData = { "1", "7", "3 4 2 1 6 4 5" };

            string[] testingData = { "3", "4", "20 20 20 20", "6", "3 2 5 3 1 2", "7", "3 4 2 1 6 4 5" };

            List<Tuple<int, string>> answers = new List<Tuple<int, string>>();

            string line;
            int lineCount = 0;
            int numOfScenarios = 0;
            int numOfDistances = 0;

            int[] distances = { };

            while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Length; i++)
            {
                string[] data = line.Split(null);
                //string[] data = testingData.ElementAt(i).Split(null);

                if (lineCount == 0)
                {
                    numOfScenarios = int.Parse(data[0]);
                }
                else if (lineCount % 2 != 0)
                {

                    answers = new List<Tuple<int, string>>();
                    numOfDistances = int.Parse(data[0]);
                    distances = new int[numOfDistances];
                }
                else
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        distances[j] = int.Parse(data[j]);
                    }
                    solve(distances, 0, true, 0, 0, "U", answers);

                    if (answers.Count == 0)
                    {
                        Console.WriteLine("IMPOSSIBLE");
                    }
                    else
                    {
                        int min = int.MaxValue;
                        foreach (Tuple<int, string> val in answers)
                        {
                            min = Math.Min(val.Item1, min);
                        }

                        bool hasAnswer = false;
                        foreach (Tuple<int, string> val in answers)
                        {
                            if (val.Item1 == min)
                            {
                                hasAnswer = true;
                                Console.WriteLine(val.Item2);
                            }
                        }
                    }


                }




                lineCount++;
            }

            //solve(distances, 0, true, 0, 0, "U", answers);

            //int min = int.MaxValue;
            //foreach (Tuple<int, string> val in answers)
            //{
            //    min = Math.Min(val.Item1, min);
            //}

            //bool hasAnswer = false;
            //foreach (Tuple<int, string> val in answers)
            //{
            //    if (val.Item1 == min)
            //    {
            //        hasAnswer = true;
            //        Console.WriteLine("Final answer: " + val.Item2);
            //    }
            //}

            //if (!hasAnswer)
            //{
            //    Console.WriteLine("IMPOSSIBLE");
            //}


            Console.Read();
        }




        public static int solve(int[] dist, int start, bool up, int sum, int maxHeight, string path, List<Tuple<int, string>> answers)
        {
            int currentSum;
            if (start == dist.Length - 1 && sum - dist[start] == 0 && !up)
            {
                //Console.WriteLine("YES" + " " + maxHeight + " " + path);
                answers.Add(new Tuple<int, string>(maxHeight, path));
                return -1;
            }
            else if (start == dist.Length - 1)
            {
                //Console.WriteLine("NO");
                return -1;
            }
            if (up)
            {
                currentSum = sum + dist[start];
                maxHeight = Math.Max(currentSum, maxHeight);
                string tmp = path + "U";
                solve(dist, start + 1, true, currentSum, maxHeight, tmp, answers);
                if (start + 1 <= dist.Length && (currentSum - dist[start + 1] >= 0))
                {
                    string tmp2 = path + "D";
                    solve(dist, start + 1, false, currentSum, maxHeight, tmp2, answers);
                }
            }
            else if (!up)
            {
                currentSum = sum - dist[start];
                maxHeight = Math.Max(currentSum, maxHeight);
                string tmp = path + "U";
                solve(dist, start + 1, true, currentSum, maxHeight, tmp, answers);
                if (start + 1 <= dist.Length && (currentSum - dist[start + 1] >= 0))
                {
                    string tmp2 = path + "D";
                    solve(dist, start + 1, false, currentSum, maxHeight, tmp2, answers);
                }

            }

            return -1;
        }
    }
}
