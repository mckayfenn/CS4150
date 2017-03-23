    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Numerics.BigInteger;

    namespace NumberTheory
    {
        class Program
        {
            static void Main(string[] args)
            {
                Program p = new Program();

                //p.isPrime(32416190071);

                string[] testingData = { "gcd 6 15", "gcd 2 13", "exp 6 5 7", "inverse 7 13", "inverse 6 9", "isprime 13", "isprime 10", "key 2 7", "key 5 3" };

                string line;

            //while ((line = Console.ReadLine()) != null)
            //for (int i = 0; i < testingData.Length; i++)
            //{
            //    //string[] data = line.Split(null);
            //    string[] data = testingData.ElementAt(i).Split(null);

            //    switch (data[0])
            //    {
            //        case "gcd":
            //            Console.WriteLine(p.gcd(System.Numerics.BigInteger.Parse(data[1]), System.Numerics.BigInteger.Parse(data[2])));
            //            break;
            //        case "exp":
            //            Console.WriteLine(p.modExp(System.Numerics.BigInteger.Parse(data[1]), System.Numerics.BigInteger.Parse(data[2]), System.Numerics.BigInteger.Parse(data[3])));
            //            break;
            //        case "inverse":
            //            System.Numerics.BigInteger inv = p.inverse(System.Numerics.BigInteger.Parse(data[1]), System.Numerics.BigInteger.Parse(data[2]));
            //            if (inv > 0)
            //                Console.WriteLine(inv);
            //            else
            //                Console.WriteLine("none");
            //            break;
            //        case "isprime":
            //            p.isPrime(System.Numerics.BigInteger.Parse(data[1]));
            //            break;
            //        case "key":
            //            p.rsaKey(System.Numerics.BigInteger.Parse(data[1]), System.Numerics.BigInteger.Parse(data[2]));
            //            break;
            //        default:
            //            // shouldn't do anything here
            //            break;
            //    }
            //}

            Console.WriteLine(p.inverse(5, 192));
            Console.WriteLine(p.modExp(6, 5, 221));
            Console.WriteLine(p.modExp(11, 5, 221));
            Console.WriteLine(p.modExp(72, 77, 221));
            Console.WriteLine(p.inverse(3, 616));

            Console.ReadLine();
        }

            /// <summary>
            /// Returns the greatest common divisor of a and b
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public System.Numerics.BigInteger gcd(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
            {
                while (b > 0)
                {
                    System.Numerics.BigInteger temp = a % b;
                    a = b;
                    b = temp;
                }
                return a;
            }

            public System.Numerics.BigInteger modExp(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger n)
            {
                if (y == 0)
                    return 1;
                else
                {
                    System.Numerics.BigInteger z = modExp(x, y / 2, n);
                    if (y % 2 == 0) // y is even
                        return (z * z) % n;
                    else // y is odd
                        return (x * z * z) % n;
                }
            }


            /// <summary>
            /// Returns [x, y, d] such that
            /// d = gcd(a,b) and
            /// ax + by = d
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger> extendedEuclid(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
            {
                if (b == 0)
                    return new Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger>(1, 0, a);
                else
                {
                    Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger> res = extendedEuclid(b, modHelper(a, b));
                    return new Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger>(res.Item2, res.Item1 - (a / b) * res.Item2, res.Item3);
                }
            }


            /// <summary>
            /// Returns a^-1 (mod n) or reports that no inverse exists
            /// </summary>
            /// <param name="a"></param>
            /// <param name="n"></param>
            /// <returns></returns>
            public System.Numerics.BigInteger inverse (System.Numerics.BigInteger a, System.Numerics.BigInteger n)
            {
                Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger> res = extendedEuclid(a, n);
                if (res.Item3 == 1)
                    //Console.WriteLine(res.Item1 % n);
                    return modHelper(res.Item1, n);
                else
                    return -1;
                    //.WriteLine("none");
            }


            /// <summary>
            /// Reports on whether or not n is prime.
            /// </summary>
            /// <param name="n"></param>
            public void isPrime(System.Numerics.BigInteger n)
            {
                Random rand = new Random();
                int r;
                if (n > 15)
                    r = 15; 
                else
                    r = rand.Next(int.Parse(n.ToString()));
                for (int i = 1; i < 10; i++)
                {
                    if (modExp(new System.Numerics.BigInteger(i), n - 1, n) == 1)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("no");
                        return;
                    }
                }
                Console.WriteLine("yes");
            }

            public void rsaKey(System.Numerics.BigInteger p, System.Numerics.BigInteger q)
            {
                System.Numerics.BigInteger n = p * q;  // call n the modulus
                System.Numerics.BigInteger phi = (p - 1) * (q - 1);

                bool gcdFound = false;
                System.Numerics.BigInteger e = 2;

                while(!gcdFound)
                {
                    if (gcd(e, phi) == 1)
                    {
                        gcdFound = true;
                        break;
                    }
                    e++;
                }

                System.Numerics.BigInteger d = inverse(e, phi);

                Console.WriteLine(n + " " + e + " " + d);
            }

            private System.Numerics.BigInteger modHelper(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
            {
                if (a % b < 0)
                    return a % b + b;
                else
                    return a % b;
            }
        }
    }
