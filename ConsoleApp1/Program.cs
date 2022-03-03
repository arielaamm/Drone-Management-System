using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> amounts = new List<int>{ 5000, 1500, 9000, 8000,
                    6500, 4000, 1500, 1500 };

            
            Console.WriteLine(amounts.ToList());
            Console.WriteLine(amounts.Count(i=>i==1500));
            

            /*
             This code produces the following output:

             4000
             1500
             5500
            */
        }
    }
}
