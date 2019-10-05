using System;
using System.Linq;

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

        //Fixed Array of 10'000 Entries
       static String[] expenses = new string[10000];

        static void Main(string[] args)
        {
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

            int input = int.Parse(Console.ReadLine());

            menu mainMenu = (menu)input;

            switch (mainMenu)
            {
                case menu.add:
                    addExpense();
                    break;
                case menu.displayAll:
                    break;
                case menu.search:
                    break;
                case menu.modify:
                    break;
                case menu.delete:
                    break;
                case menu.sort:
                    break;
                case menu.normalize:
                    break;
                default:
                    break;
            }
            displayMenu();

        }

        public static void addExpense()
        {
            int totalLeft = 10000 - expenses.Count(s => s != null);
            int curTotal = expenses.Count(s => s != null);
            Console.WriteLine("---------------------------------");

            Console.WriteLine("Let's add a new expense!\nHow many entries do you want to add?");
            int totalEntries = int.Parse(Console.ReadLine());
            if (totalLeft - totalEntries <= 0)
            {
                Console.WriteLine("You can only store a max of 10'000 Expenses In this program!");
            }
            else
            {
               
                for (int i = curTotal; i < curTotal + totalEntries; i++)
                {
                    Console.WriteLine("Adding Entry #" + (i + 1)+ " Into The List");
                    String date = "";
                    while (date.Length != 8) //validating the length of 8
                    {
                        Console.WriteLine("Please enter the purchase date in format YYYYMMDD (8 Charaacters)");
                        date = Console.ReadLine();
                    }
                    Console.WriteLine("Please enter the Description of the Expenditure or Item");
                    String desc = Console.ReadLine();
                    Console.WriteLine("What category is this?");
                    String category = Console.ReadLine();
                    Console.WriteLine("What is the amount for this Expenditure or Item?");
                    String amount = Console.ReadLine();
                    expenses[i] = date + "☺" + desc + "☺" + category + "☺" + amount;
                }

            }
            Console.WriteLine("---------------------------------");


        }
    }
}
