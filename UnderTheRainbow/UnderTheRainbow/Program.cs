using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] testingData = new string[] { "3", "0", "350", "450", "825" };

            int[] dist = new int[0];
            int lineCount = 0;
            string line;

            //while ((line = Console.ReadLine()) != null)
            for (int i = 0; i < testingData.Length; i++)
            {
                //string[] data = line.Split(null);
                string[] data = testingData.ElementAt(i).Split(null);

                if (lineCount == 0)
                {
                    dist = new int[int.Parse(data[0]) + 1];
                }
                else
                {
                    int distance = int.Parse(data[0]);
                    dist[lineCount - 1] = distance;
                }


                lineCount++;
            }

            Dictionary<long, long> cache = new Dictionary<long, long>();

            Console.WriteLine(dynamicPenaltyF(dist, dist.Length - 1, cache));

            Console.ReadLine();
        }

        private static long penalty(int[] dist)
        {
            long minPenalty = long.MaxValue;
            for (int i = dist.Length - 1; i >= 0; i--)
            {
                minPenalty = Math.Min(minPenalty, penaltyF(dist, i));
            }
            return minPenalty;
        }

        private static long penaltyF(int[] dist, int n)
        {
            long minPenalty = long.MaxValue;
            for (int i = n; i >= 0; i--)
            {
                if (dist.Length - 1 == i)
                {
                    minPenalty = 0;
                }
                long calculate = (400 - ((dist[n-i] - dist[i])*(dist[n-i] - dist[i])) + penaltyF(dist, n-i));
                minPenalty = Math.Min(minPenalty, calculate);
            }

            return minPenalty;
        }

        private static long dynamicPenaltyF(int[] dist, int n, Dictionary<long, long> cache)
        {
            long result;
            if (cache.TryGetValue(n, out result)) {
                return result;
            }

            long minPenalty = long.MaxValue;
            for (int i = n; i >= 0; i--)
            {
                if (dist.Length - 1 == i)
                {
                    minPenalty = 0;
                }
                long calculate = (400 - ((dist[n - i] - dist[i]) * (dist[n - i] - dist[i])) + dynamicPenaltyF(dist, n - i, cache));
                minPenalty = Math.Min(minPenalty, calculate);
            }

            cache[n] = minPenalty;
            return minPenalty;
        }


        private static long dynamicPenalty(int[] dist)
        {
            var cache = new Dictionary<long, long>();
            long minPenalty = 0;
            for (int i = dist.Length -1; i >= 0; i--)
            {
                if (dist.Length - 1 == i)
                {
                    minPenalty = 0;
                }
                long calculate = (400 - ((dist[n - i] - dist[i]) * (dist[n - i] - dist[i])) + dynamicPenaltyF(dist, n - i, cache));
                minPenalty = Math.Min(minPenalty, calculate);
            }
            
        }
    }
}
