using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome2845();
            welcome5562();
            Console.ReadKey();
        }
        static partial void welcome5562();  
        private static void welcome2845()
        {
            Console.WriteLine("enter your name:");
            string username = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", username);
        }
    }
}
