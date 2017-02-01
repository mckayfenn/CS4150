using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] A = { 2, 5, 7, 22 };
            int[] B = { 7, 8, 9, 14};
            int k = 5;

            int[] C = { 22, 25, 31, 33, 48, 99, 104 };
            int[] D = { 1, 4, 23, 32, 55 };
            //k = 8;

            // 1, 4, 22, 23, 25, 31, 32, 33, 48, 55, 99, 104

            int answer = select(A, B, k);
            Console.WriteLine("Searching A: " + " and B: " + ", for index: " + k + ". We get value: " + answer);

            answer = select(C, D, k);
            Console.WriteLine(k);

            Console.ReadLine();
        }

        // A and B are each sorted into ascending order, and 0 <= k < |A|+|B| 
        // Returns the element that would be stored at index k if A and B were
        // combined into a single array that was sorted into ascending order.
        public static int select(int[] A, int[] B, int k)
        {
            return select(A, 0, A.Length - 1, B, 0, B.Length - 1, k);
        }


        public static int select(int[] A, int loA, int hiA, int[] B, int loB, int hiB, int k)
        {
            // A[loA..hiA] is empty
            if (hiA < loA)
                return B[k - loA];
            // B[loB..hiB] is empty
            if (hiB < loB)
                return A[k - loB];
            // Get the midpoints of A[loA..hiA] and B[loB..hiB]
            int i = (loA + hiA) / 2;
            int j = (loB + hiB) / 2;
            // Figure out which one of four cases apply
            if (A[i] > B[j])
                if (k <= i + j) 
                    return select(A, loA, hiA - 1, B, loB, hiB, k); // select(A, 0, A.Length - 1, B, 0, j, k);
                else
                    return select(A, loA, hiA, B, loB + 1, hiB, k); // select(A, 0, A.Length - 1, B, j, B.Length - 1, k);
            else
                if (k <= i + j)
                    return select(A, loA, hiA, B, loB, hiB - 1, k); // select(A, 0, i, B, 0, B.Length - 1, k);
            else
                    return select(A, loA + 1, hiA, B, loB, hiB, k); // select(A, i + 1, A.Length - 1, B, 0, B.Length - 1, k);
        }
    }
}
