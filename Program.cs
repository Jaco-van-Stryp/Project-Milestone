using System;
using System.Globalization;
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
                    showAllExpenses();
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
                    Console.WriteLine("Adding Entry #" + (i + 1) + " Into The List");
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

        public static void showAllExpenses()
        {
            Console.WriteLine("---------------------------------");


            Console.WriteLine("Before we can display your expenses, Please enter a category");
            String category = Console.ReadLine();
            Console.WriteLine("Please enter a start date to display from in format YYYYMMDD");
            String startDate = Console.ReadLine();
            Console.WriteLine("Please enter a end date to display from in format YYYYMMDD");
            String endDate = Console.ReadLine();

            DateTime start = new DateTime();
            start = convertStringToDate(startDate);
            DateTime end = new DateTime();
            end = convertStringToDate(endDate);
            DateTime listed = new DateTime();

            int curTotal = expenses.Count(s => s != null);//tptal valid entries in array

            for (int i = 0; i < curTotal; i++)
            {
                String[] shortData = expenses[i].Split("☺");
                //[0] would be Date
                //[1] would be Description
                //[2] would be Category
                //[3] would be Amount
                if (category.ToLower().Equals(shortData[2]))
                {
                    listed = convertStringToDate(shortData[0]);
                    if (listed >= start && listed <= end)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("Date - " + convertStringToDate(shortData[0]));
                        Console.WriteLine("Description - " + shortData[1]);
                        Console.WriteLine("Category - " + shortData[2]);
                        Console.WriteLine("Amount - " + get2Decimal(shortData[3]));

                        Console.WriteLine("\n");

                    }


                }
            }


            Console.WriteLine("---------------------------------");

        }

        public static DateTime convertStringToDate(String date)
        {

            String year = date.Substring(0, 4);
            String month = date.Substring(4, 2);
            String day = date.Substring(6, 2);
            Console.WriteLine(day + "/" + month + "/" + year);
            DateTime newDate = new DateTime();
            DateTime dt = DateTime.ParseExact(day + "/" + month + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return newDate;
        }
        public static string get2Decimal(String input)
        {
            char[] sInput = input.ToCharArray();
            bool hasDec = false;
            String smartS = "";
            for (int i = 0; i < sInput.Length; i++)
            {
                if (sInput[i] == '.' || sInput[i] == ',')
                {
                    smartS += sInput[i];
                    smartS += sInput[i + 1];
                    smartS += sInput[i + 2];
                    smartS += sInput[i + 3];
                    hasDec = true;
                    break;
                }
                else
                {
                    smartS += sInput[i];
                }
            }
            if(hasDec == false)
            {
                smartS += ".00";
            }
            return smartS;
        }
    }
}
