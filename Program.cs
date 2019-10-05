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
            Console.WriteLine("1) Add A New Expense");
            Console.WriteLine("2) Show All Expenses");
            Console.WriteLine("3) Search Item");
            Console.WriteLine("4) Modify Item");
            Console.WriteLine("5) Delete Item");
            Console.WriteLine("6) Sort Items Alphabetically");
            Console.WriteLine("7) Normalize Descriptions");

        }
    }
}
