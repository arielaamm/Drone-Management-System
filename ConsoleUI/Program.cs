using System;

namespace ConsoleUI
{
    class Program   
    {
        static void Main(string[] args)
        {
            string Option;
            Console.WriteLine("Hey you got to ariel&babauv drone's");
            Console.WriteLine("Choose what you want to do");
            Console.WriteLine("Type what you want to do from the list below");
            Console.WriteLine("Addition, Updating, Display, List view or Exit");
            Option = Console.ReadLine();
            switch (Option)
            {
                case "Addition":
                    FunAddition();
                    break;
                case "Updating":
                    FunUpdating();
                    break;
                case "Display":
                    FunDisplay();
                    break;
                case "List view":
                    FunListview();
                    break;
                case "Exit":
                    Console.WriteLine("Thank you have a good day");
                    break;
                default:
                    break;
            }
        }
    }
}
