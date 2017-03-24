using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            //string[] testingData = { "4 4", "1000 1", "2000 2", "500 2", "1200 0" };
            //string[] testingData = { "3 4", "1000 0", "2000 1", "500 1" };
            string[] testingData = { "4 4", "5 0", "25 1", "10 2", "15 2" };



            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();


            string line;
            int lineCount = 0;
            int timeLeft = 0;


            //while ((line = Console.ReadLine()) != null)
            for (int i = 0; i < testingData.Length; i++)
            {
                //string[] data = line.Split(null);
                string[] data = testingData.ElementAt(i).Split(null);

                if (lineCount == 0)
                {
                    timeLeft = Int32.Parse(data[1]);
                    lineCount++;
                    continue;
                }
                    

                

                int amount = Int32.Parse(data[0]);
                int time = Int32.Parse(data[1]);


                if (!dict.ContainsKey(time))
                {
                    dict.Add(time, new List<int>());
                }

                dict[time].Add(amount);   

                lineCount++;
            }

            //p.printDictionary(dict);

            Console.Write(p.solution(dict, timeLeft));

            Console.ReadLine();
        }



        private int solution(Dictionary<int, List<int>> dict, int time)
        {
            int maxValue = 0;
            int timeLeft = time;
            int timeUsed = 0;

          
        }

        //private int solution(Dictionary<int, List<int>> dict, int time)
        //{
        //    int maxValue = 0;
        //    int timeLeft = time;
        //    int timeUsed = 0;

        //    for (int i = 0; i < dict.Count; i++)
        //    {
        //        if (timeUsed > time)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            List<int> sorted = new List<int>();
        //            if (dict.ContainsKey(timeLeft))
        //            {
        //                sorted = dict[timeLeft];
        //                sorted.Sort();
        //                maxValue += sorted[sorted.Count - 1];
        //            }
        //        }

        //        timeLeft--;
        //        timeUsed++;
        //    }

        //    return maxValue;
        //}



        private void printDictionary(Dictionary<int, List<int>> dict)
        {
            foreach (KeyValuePair<int, List<int>> pair in dict)
            {
                Console.Write(pair.Key + ": ");
                foreach (int amount in pair.Value)
                {
                    Console.Write(amount + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
