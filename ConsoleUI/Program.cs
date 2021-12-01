using IDAL.DO;
using System;
using System.Collections.Generic;
using DAL;
namespace ConsoleUI
{
    class Program   
    {
        public static void FunAddition()
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station(= 1) \nadd drone(= 2) \nadd customer(= 3) \nadd parcel(= 4)");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                case "add station" or "1":
                    Console.WriteLine("enter station name and how meny charge slots are");
                    string name;
                    name = Console.ReadLine();
                    int num = Convert.ToInt32(Console.ReadLine());
                    DAL.DalObject.AddStation(name,num);
                    break;
                case "add drone" or "2":
                    Console.WriteLine("enter Model name, weight(1,2,3), buttery");
                    string m = Console.ReadLine();
                    int w = Convert.ToInt32(Console.ReadLine());
                    double b = Convert.ToDouble(Console.ReadLine());
                    DAL.DalObject.AddDrone(m, (WEIGHT)w, b);
                    break;
                case "add customer" or "3":
                    Console.WriteLine("enter customer name and a phone numer");
                    string n = Console.ReadLine();
                    string p = Console.ReadLine();
                    DAL.DalObject.AddCustomer(n, p);
                    break;
                case "add parcel" or "4":
                    Console.WriteLine("enter customer name and a phone numer");
                    break;

            }
        }
        public static void FunUpdating()
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("parcel to drone \npackage collection \ndelivery package \nsend for loadingor \nrelease from charging");
        }
        public static void FunDisplay()
        {

        }
        public static void FunListview()
        {

        }
        static void Main(string[] args)
        {
            var P = new DataSource();
            DataSource.Initialize();
            string Option = "0";
            Console.WriteLine("Hey you got to ariel&babauv drone's");
            Console.WriteLine("Choose what you want to do");
            Console.WriteLine("Type what you want to do from the list below");
            do
            {
                Console.WriteLine("Addition(= 1), Updating(= 2), Display(= 3), List view(= 4) or Exit(= 5)");
                Option = Console.ReadLine();
                switch (Option)
                {
                    case "Addition" or "1":
                        FunAddition();
                        break;
                    case "Updating" or "2":
                        FunUpdating();
                        break;
                    case "Display" or "3":
                        FunDisplay();
                        break;
                    case "List view" or "4":
                        FunListview();
                        break;
                    case "Exit" or "5":
                        Console.WriteLine("Thank you have a good day");
                        break;
                    default:
                        break;
                }
            }
            while (Option != "5");
        }
    }
}
