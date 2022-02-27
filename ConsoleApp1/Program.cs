using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] amounts = { 5000, 2500, 9000, 8000,
                    6500, 4000, 1500, 5500 };

            IEnumerable<int> query = amounts.SkipWhile((amount, index) => amount > index * 1000);


                Console.WriteLine(amounts.ToList());
            

            /*
             This code produces the following output:

             4000
             1500
             5500
            */
        }
    }
}
