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


            long d = 0;
            int count = 0;
            string line;

            string[] testingData = new string[8];
            testingData[0] = "20 7";
            testingData[1] = "1 1";
            testingData[2] = "100 100";
            testingData[3] = "1 3";
            testingData[4] = "101 101";
            testingData[5] = "3 1";
            testingData[6] = "102 102";
            testingData[7] = "3 3";

            //string[] testingData = new string[5];
            //testingData[0] = "10 4";
            //testingData[1] = "45 46";
            //testingData[2] = "90 47";
            //testingData[3] = "45 54";
            //testingData[4] = "90 43";

            while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Length; i++)
            {
                string[] xy = line.Split(null);
                //string[] xy = testingData.ElementAt(i).Split(null);
                if (count == 0)
                {
                    long.TryParse(xy[0], out d); // get the diameter
                }
                else
                {

                    long x;
                    long y;
                    long.TryParse(xy[0], out x);
                    long.TryParse(xy[1], out y);
                    //stars.Add(x, y);
                    allStars.Add(new Star(x, y));
                }
                count++;
            }

            Star s1 = findMajority(allStars, d);

            int total = 0;
            if (s1 == null)
                Console.WriteLine("NO");
            else
            {
                foreach (Star s2 in allStars)
                {
                    if (distanceBetweenStars(s1, s2, d))
                    {
                        total++;
                    }
                }
            }

            if (total != 0)
                Console.WriteLine(total);
            //Console.ReadLine();
        }

        public static Star findMajority(List<Star> A, long d)
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
                // Split into halves and recursively solve them
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
                Star x = findMajority(firstHalf, d);
                Star y = findMajority(secondHalf, d);



                if (x == null && y == null)
                    return null;
                else if (x == null)
                {
                    // count occurences of y in A, return y or NO
                    int count = 0;
                    foreach (Star s2 in A)
                    {
                        if (distanceBetweenStars(y, s2, d))
                        {
                            count++;
                        }
                    }

                    if (count > A.Count / 2)
                        return y;
                    else
                        return null;
                }
                else if (y == null)
                {
                    // count occurences of x in A, return x or NO
                    int count = 0;
                    foreach (Star s2 in A)
                    {
                        if (distanceBetweenStars(x, s2, d))
                        {
                            count++;
                        }
                    }

                    if (count > A.Count / 2)
                        return x;
                    else
                        return null;
                }
                else
                {
                    // count occurrences of x and y in A, return x, y, or NO
                    int ycount = 0;
                    int xcount = 0;
                    foreach (Star s2 in A)
                    {
                        if (distanceBetweenStars(y, s2, d))
                        {
                            ycount++;
                        }
                        if (distanceBetweenStars(x, s2, d))
                        {
                            xcount++;
                        }
                    }

                    if (xcount > A.Count / 2)
                        return x;

                    else if (ycount > A.Count / 2)
                        return y;
                    else
                        return null;
                }
            }
        }

        public static Star findMajorityFaster(List<Star> A, long d)
        {
            if (A.Count == 0)
                return null;
            else if (A.Count == 1)
                return A.ElementAt(0);
            else
            {
                List<Star> APrime = new List<Star>();
                Star y = null;

                // Find A' and y
                for (int i = 0; i < A.Count; i++)
                {

                    if (i + 1 < A.Count)
                    {
                        Star s1 = A.ElementAt(i);
                        Star s2 = A.ElementAt(i + 1);
                        if (distanceBetweenStars(s1, s2, d))
                        {
                            APrime.Add(s1); // if the elements are are same, keep 1 of them in APrime
                        }
                    }
                    else
                    {
                        y = A.ElementAt(i); // if there's a leftover 
                    }


                }

                Star x = findMajorityFaster(APrime, d);

                if (x == null)
                {
                    // if A is odd, count occurrences of y in A, return y or NO as appropriate
                    if (A.Count % 2 != 0)
                    {
                        // count occurences of y in A, return y or NO
                        int count = 0;
                        foreach (Star s2 in A)
                        {
                            if (distanceBetweenStars(y, s2, d))
                            {
                                count++;
                            }
                        }

                        if (count > A.Count / 2)
                            return y;
                        else
                            return null;
                    }
                    else
                    {
                        return null; // else return NO
                    }
                }
                else
                {
                    // count occurences of x in A, return x or NO
                    int count = 0;
                    foreach (Star s2 in A)
                    {
                        if (distanceBetweenStars(x, s2, d))
                        {
                            count++;
                        }
                    }

                    if (count > A.Count / 2)
                        return x;
                    else
                        return null;
                }
            }
        }


        /**
         * Return true if s1 and s2 are with d of each other, false otherwise
         **/
        public static bool distanceBetweenStars(Star s1, Star s2, long d)
        {
            return (((s1.x - s2.x) * (s1.x - s2.x)) + ((s1.y - s2.y) * (s1.y - s2.y))) <= (d * d);
        }
    }

    class Star
    {
        public long x;
        public long y;

        public Star(long _x, long _y)
        {
            x = _x;
            y = _y;
        }
    }

    class Galaxy
    {
        public List<Star> galaxyStars;
    }
}
