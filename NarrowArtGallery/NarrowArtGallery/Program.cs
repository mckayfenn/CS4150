using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarrowArtGallery
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] testingData = { "6 4", "3 1", "2 1", "1 2", "1 3", "3 3", "0 0", "0 0" };
            string[] testingData = { "4 3", "3 4", "1 1", "1 1", "5 6", "0 0" };
            //string[] testingData = { "10 5", "7 8", "4 9", "3 7", "5 9", "7 2", "10 3", "0 10", "3 2", "6 3", "7 9", "0 0" };

            string line;
            int lineCount = 0;
            int numOfRows = 0;
            int numOfClosing = 0;

            int[,] gallery = new int[0, 0];
            int row = 0;
 

            while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Length; i++)
            {
                string[] data = line.Split(null);
                //string[] data = testingData.ElementAt(i).Split(null);

                if (lineCount == 0)
                {
                    numOfRows = int.Parse(data[0]);
                    numOfClosing = int.Parse(data[1]);
                    gallery = new int[numOfRows, 2];
                }
                else if (lineCount > numOfRows)
                {
                    break;
                }
                else
                {
                    gallery[row, 0] = int.Parse(data[0]);
                    gallery[row, 1] = int.Parse(data[1]);
                    row++;
                }

                lineCount++;
                
            }


            Console.WriteLine(maxValue(gallery, numOfRows, numOfClosing, numOfRows));
            
            Console.ReadLine();
        }

        private static int maxValue(int[,] g, int r, int k, int numRows)
        {   
            Tuple<int, int, int> tuple = new Tuple<int, int, int>(0,0,0);
            return maxValue(g, 0, -1, k, numRows, new Dictionary<Tuple<int, int, int>, int>());

        }

        private static int maxValue(int[,] gallery, int r, int uncloseableRoom, int k, int N, Dictionary<Tuple<int, int, int>, int> cache)
        {
           
            int result = 0;

            if (cache.TryGetValue(new Tuple<int, int, int>(r, uncloseableRoom, k), out result)) {
                return result;
            }

            if (r >= gallery.Length / 2)
                return result;
            if (k == N - r)
            {
                if (uncloseableRoom == 0)
                    result = gallery[r, 0] + maxValue(gallery, r + 1, 0, k - 1, N, cache);
                else if (uncloseableRoom == 1)
                    result = gallery[r, 1] + maxValue(gallery, r + 1, 1, k - 1, N, cache);
                else if (uncloseableRoom == -1)
                    result = Math.Max((gallery[r, 0] + maxValue(gallery, r + 1, 0, k - 1, N, cache)), (gallery[r, 1] + maxValue(gallery, r + 1, 1, k - 1, N, cache)));
            }
            else
            {
                if (uncloseableRoom == 0)
                    result = Math.Max((gallery[r, 0] + maxValue(gallery, r + 1, 0, k - 1, N, cache)), (gallery[r, 0] + gallery[r, 1] + maxValue(gallery, r + 1, -1, k, N, cache)));
                else if (uncloseableRoom == 1)
                    result = Math.Max((gallery[r, 1] + maxValue(gallery, r + 1, 1, k - 1, N, cache)), (gallery[r, 0] + gallery[r, 1] + maxValue(gallery, r + 1, -1, k, N, cache)));
                else if (uncloseableRoom == -1)
                {
                    int temp = Math.Max((gallery[r, 0] + maxValue(gallery, r + 1, 0, k - 1, N, cache)), (gallery[r, 1] + maxValue(gallery, r + 1, 1, k - 1, N, cache)));
                    result = Math.Max(temp, (gallery[r, 0] + gallery[r, 1] + maxValue(gallery, r + 1, -1, k, N, cache)));
                }
            }

            cache[new Tuple<int, int, int>(r, uncloseableRoom, k)] = result;

            return result;
        }
    }
}
