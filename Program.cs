using System;

namespace Project_Milestone
{
    class Program
    {
        //Daniel Van Stryp Programming 152 - Project Milestone
        enum menu
        {
            add = 1,
            displayAll,
            search,
            modify,
            delete,
            sort,
            normalize,
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Daniiel's Milestone Project!");
            displayMenu(); //Displaying menu to the user
        }

        private static void displayMenu()
        {
            Console.WriteLine("Please select one of the following");
        }
    }
}
