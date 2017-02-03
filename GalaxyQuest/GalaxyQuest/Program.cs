using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Star> allStars = new List<Star>();
            Dictionary<long, long> stars = new Dictionary<long, long>();


            long d;
            int count = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] xy = line.Split(null);
                if (count == 0)
                {
                    long.TryParse(xy[0], out d);
                }
                else
                {
                    
                    long x;
                    long y;
                    long.TryParse(xy[0], out x);
                    long.TryParse(xy[1], out y);
                    //stars.Add(x, y);
                    allStars.Add(new Star((x, y)));
                }
                count++; // this skips the first line
            }

            findMajority(allStars);
        }

        public static Star findMajority(List<Star> A)
        {
            if (A.Count == 0)
            {
                return null;
            }
            else if (A.Count == 1)
            {
                return A.ElementAt(0);
            }
            else
            {
                List<Star> firstHalf = new List<Star>();
                for (int i = 0; i < A.Count / 2; i++)
                {
                    firstHalf.Add(A.ElementAt(i));
                }
                List<Star> secondHalf = new List<Star>();
                for (int i = A.Count / 2; i < A.Count; i++)
                {
                    secondHalf.Add(A.ElementAt(i));
                }
                Star x = findMajority(firstHalf);
                Star y = findMajority(secondHalf);

                if (x == null && y == null)
                    return null;
                else if (x == null)
                {
                    // count occurences of y in A, return y or NO
                }
                else if (y == null)
                {
                    // count occurences of x in A, return x or NO
                }
                else
                {
                    // count occurrences of x and y in A, return x, y, or NO
                }
            }

            return null;
        }

        public long distance(Star s1, Star s2)
        {
            return Convert.ToInt64(Math.Pow(s1.x - s2.x, 2) + Math.Pow(s1.y - s2.y, 2));
        }
    }

    class Star
    {
        public long x;
        public long y;

        Star(long _x, long _y)
        {
            x = _x;
            y = _y;
        }
    }
}
