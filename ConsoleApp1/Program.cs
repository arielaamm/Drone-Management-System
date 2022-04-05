using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> enumerator = new() { 1, 2, 3, 4, 13 };
            var s = enumerator.GetEnumerator();
            while (s.MoveNext())
            {
                var item = s.Current;
                Console.WriteLine(item.ToString());
            }
            /*
             This code produces the following output:

             4000
             1500
             5500
            */
        }
    }
}
