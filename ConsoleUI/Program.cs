using System;

namespace ConsoleUI
{
    class Program   
    {
        public void FunAddition()
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station \nadd drone \nadd customer \nadd parcel");
        }
        public void FunUpdating()
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("parcel to drone \npackage collection \ndelivery package \nsend for loadingor \nrelease from charging");
        }
        public void FunDisplay()
        {

        }
        public void FunListview()
        {

        }
        static void Main(string[] args)
        {
            var P = new Program();
            string Option;
            Console.WriteLine("Hey you got to ariel&babauv drone's");
            Console.WriteLine("Choose what you want to do");
            Console.WriteLine("Type what you want to do from the list below");
            Console.WriteLine("Addition, Updating, Display, List view or Exit");
            Option = Console.ReadLine();
            switch (Option)
            {
                case "Addition":
                    P.FunAddition();
                    break;
                case "Updating":
                    P.FunUpdating();
                    break;
                case "Display":
                    P.FunDisplay();
                    break;
                case "List view":
                    P.FunListview();
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
