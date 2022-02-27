using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new();
            a.Add(1);
            a.Add(4);
            a.Add(-2);
            a.Add(-13);
            a.Add(11);
            a.Add(21);
            a.Add(-61);
            a.Add(-71);
            a.Add(17);
            a.Add(-15);
            a.Add(8);
            var t = a.AsEnumerable();
            var q = a.Where(i => i > 0).Select(i=>i);
            //from s in t
            //    let e = 0
            //    where s > e
            //    select s;
            List<int> w = q.ToList();
            Console.WriteLine(w);
        }
    }
}
