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

            string[] testingData = { "4 4", "1000 1", "2000 2", "500 2", "1200 0" };
            //string[] testingData = { "3 4", "1000 0", "2000 1", "500 1" };
            //string[] testingData = { "4 4", "5 0", "25 1", "10 2", "15 2" };

            string line;
            int lineCount = 0;
            int timeLeft = 0;


            List<Customer> customers = new List<Customer>();


            while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Length; i++)
            {
                string[] data = line.Split(null);
                //string[] data = testingData.ElementAt(i).Split(null);

                if (lineCount == 0)
                {
                    timeLeft = Int32.Parse(data[1]);
                    lineCount++;
                    continue;
                }




                int amount = Int32.Parse(data[0]);
                int time = Int32.Parse(data[1]);


                customers.Add(new Customer(time, amount));

                lineCount++;
            }

            // Sort the people by amount
            Comparison<Customer> comparison = new Comparison<Customer>(compare);
            customers.Sort(comparison);


            // create an empty queue
            List<int> queue = new List<int>(timeLeft);
            for (int i = 0; i < timeLeft; i++)
            {
                queue.Add(0);
            }

            int k = 0;

            // Put customers into queue (list)
            while (k < customers.Count)
            {


                Customer c = customers[k];
                int i = c.waitTime;

                // Pu them into the queue if their time is not already occupied
                while (i >= 0)
                {
                    if (queue[i] == 0)
                    {
                        queue[i] = c.amount;
                        break;
                    }

                    i--;
                }

                k++;
            }


            int total = 0;

            for (int i = 0; i < queue.Count; i++)
            {
                total += queue[i];
            }

            Console.WriteLine(total);

            Console.ReadLine();

        }

        /// <summary>
        /// Comparison used for sorting
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static int compare(Customer c1, Customer c2)
        {
            return c2.amount.CompareTo(c1.amount);
        }
    }

    class Customer
    {
        public int waitTime;
        public int amount;

        public Customer(int time, int money)
        {
            waitTime = time;
            amount = money;
        }
    }
}
