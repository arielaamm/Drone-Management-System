﻿using System;

namespace Targil0
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            welcome2845();
            welcome5562();
            Console.ReadKey();
        }
        //מה מצב הסחה בשטחים? המצב בסדר
        static partial void welcome5562();
        private static void welcome2845()
        {
            Console.WriteLine("enter your name:");
            string username = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", username);
        }
    }
}
