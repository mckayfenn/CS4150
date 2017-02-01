using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    class Program
    {
        /// <summary>
        /// The minimum duration of a timing eperiment (in msecs) in versions 4 and 5
        /// </summary>
        public const int DURATION = 1000;

        static void Main(string[] args)
        {
            int n = 2000;
            int k = 5;

            Console.WriteLine("\nSize\tTime (msec)");
            double previousTime = 0;

            /*HashSet<string> test = generateWords(n, k);
            foreach(string word in test)
            {
                Console.WriteLine(word);
            }*/

            // Report the average time for various sizes of n
            for (int i = 0; i < 100; i++)
            {

                double currentTime = timeMrAnaga(n, k);
                Console.WriteLine(k + "  \t" + (currentTime - previousTime).ToString("G3"));
               
                previousTime = currentTime;
                k = k * 2;
            }
            Console.Read();
        }

        public void originalAlgorithm()
        {
            HashSet<string> allWords = new HashSet<string>();

            int first = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (first != 0)
                    allWords.Add(line);
                first++;
            }

            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejected = new HashSet<string>();

            foreach (string word in allWords)
            {
                char[] newWord = word.ToCharArray();
                Array.Sort(newWord);
                string sortedWord = new string(newWord);
                if (solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (!rejected.Contains(sortedWord))
                {
                    solutions.Add(sortedWord);
                }
            }

            Console.WriteLine(solutions.Count);
        }

        public static HashSet<string> generateWords(int n, int k)
        {
            HashSet<string> words = new HashSet<string>();
            Random randIndex = new Random();
            string let = "abcdefghijklmnopqrstuvwxyz";
            char[] letters = let.ToCharArray();
            

            // Create n words
            for(int i = 0; i < n; i++)
            {
                char[] word = new char[k];
                // of length k
                for (int j = 0; j < k; j++)
                {
                    word[j] = letters[randIndex.Next(26)];
                }
                words.Add(new string(word));
            }
            return words;
        }

        public static void mrAnaga(HashSet<string> givenWords)
        {
            HashSet<string> allWords = givenWords;

            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejected = new HashSet<string>();

            foreach (string word in allWords)
            {
                char[] newWord = word.ToCharArray();
                Array.Sort(newWord);
                string sortedWord = new string(newWord);
                if (solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (!rejected.Contains(sortedWord))
                {
                    solutions.Add(sortedWord);
                }
            }

            //Console.WriteLine(solutions.Count);
        }

        public static double timeMrAnaga(int n, int k)
        {
            HashSet<string> generatedWords = generateWords(n, k);

            // Create a stopwatch
            Stopwatch sw = new Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < n; d++)
                    {
                        mrAnaga(generatedWords);
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / n;

            // Create a stopwatch
            sw = new Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < n; d++)
                    {
                        //BinarySearch(data, d);
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / n;

            // Return the difference
            return totalAverage - overheadAverage;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static double msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }
    }
}
