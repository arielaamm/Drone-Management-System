using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public class MyClass
        {
            public int i { set; get; }
        }
        static void Main(string[] args)
        {
            void DisplayDefaultOf<T>()
            {
                var val = default(T);
                Console.WriteLine($"Default value of {typeof(T)} is {(val == null ? "null" : val.ToString())}.");
            }
            DisplayDefaultOf<MyClass>();
            Console.WriteLine(default(MyClass));
        }
    }
}
