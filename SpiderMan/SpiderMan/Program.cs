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


            //string[] testingData = { "3", "1", "4", "31", "43 32 48 25 25 39 34 7 30 29 32 5 26 6 62 14 19 8 28 35 8 32 1 31 18 17 34 52 35 17 41", "30", "59 43 21 10 9 4 24 1 42 14 19 47 4 15 2 15 59 46 37 11 4 10 7 14 2 6 6 12 51 58" };

            List<Tuple<int, string>> answers = new List<Tuple<int, string>>();

            string line;
            int lineCount = 0;
            int numOfScenarios = 0;
            int numOfDistances = 0;

            int[] distances = { };

            //while ((line = Console.ReadLine()) != null)
            for (int i = 0; i < testingData.Length; i++)
            {
                //string[] data = line.Split(null);
                string[] data = testingData.ElementAt(i).Split(null);

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
                    // put data into array
                    for (int j = 0; j < data.Length; j++)
                    {
                        distances[j] = int.Parse(data[j]);
                    }

                    // solve the problem
                    solve(distances, 0, true, 0, 0, "U", answers);


                    // print out results
                    if (answers.Count == 0)
                    {
                        Console.WriteLine("IMPOSSIBLE");
                    }
                    else
                    {
                        int min = int.MaxValue;

                        // get the miniumum height
                        foreach (Tuple<int, string> val in answers)
                        {
                            min = Math.Min(val.Item1, min);
                        }

                        // print the path with the minimum height
                        foreach (Tuple<int, string> val in answers)
                        {
                            if (val.Item1 == min)
                            {
                                Console.WriteLine(val.Item2);
                                break;
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



        /// <summary>
        /// Recursively solve it by going up and down at each point.
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="start"></param>
        /// <param name="up"></param>
        /// <param name="sum"></param>
        /// <param name="maxHeight"></param>
        /// <param name="path"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static int solve(int[] dist, int start, bool up, int sum, int maxHeight, string path, List<Tuple<int, string>> answers)
        {
            int currentSum;
            if (start == dist.Length - 1 && sum - dist[start] == 0 && !up)
            {
                Console.WriteLine("YES" + " " + maxHeight + " " + path);
                answers.Add(new Tuple<int, string>(maxHeight, path)); // found a legal path so add it to the list of answers
                return -1;
            }
            else if (start == dist.Length - 1)
            {
                Console.WriteLine("NO " + maxHeight + " " + path);
                return -1;
            }
            if (up)
            {
                currentSum = sum + dist[start];
                maxHeight = Math.Max(currentSum, maxHeight);
                string tmp = path + "U";
                solve(dist, start + 1, true, currentSum, maxHeight, tmp, answers);
                if (start + 1 <= dist.Length && (currentSum - dist[start + 1] >= 0)) // only go down if its legal
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
                if (start + 1 <= dist.Length && (currentSum - dist[start + 1] >= 0)) // only go down if its legal
                {
                    string tmp2 = path + "D";
                    solve(dist, start + 1, false, currentSum, maxHeight, tmp2, answers);
                }

            }

            return -1;
        }
    }
}
