using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Project_Milestone
{
    class Program
    {
        //Daniel Van Stryp Programming 152 - Project Milestone
        enum menu
        {
            exit = 0,
            add,
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
            Console.WriteLine("0) Exit Application");


            int input = int.Parse(Console.ReadLine());

            menu mainMenu = (menu)input;

            switch (mainMenu)
            {
                case menu.exit:
                    Environment.Exit(0);
                    break;
                case menu.add:
                    addExpense();
                    break;
                case menu.displayAll:
                    showAllExpenses();
                    break;
                case menu.search:
                    searchItem();
                    break;
                case menu.modify:
                    modifyItem();
                    break;
                case menu.delete:
                    deleteItem();
                    break;
                case menu.sort:
                    expenses = sort(expenses);
                    break;
                case menu.normalize:
                    normalizeDesctiptions();
                    break;
                default:
                    Console.WriteLine("Unkown Input, Please Try Again");
                    break;
            }
            displayMenu();

        }

        public static void addExpense()
        {
            int totalLeft = 10000 - expenses.Count(s => s != null);
            int curTotal = getCurTotal();
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
                        Console.WriteLine("Please enter the purchase date in format YYYYMMDD (8 Characters)");
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

            int curTotal = getCurTotal();

            for (int i = 0; i < curTotal; i++)
            {
                String[] shortData = expenses[i].Split('☺');
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
                        Console.WriteLine("Date - " + formatDateString(shortData[0]));
                        Console.WriteLine("Description - " + shortData[1]);
                        Console.WriteLine("Category - " + shortData[2]);
                        Console.WriteLine("Amount - R" + get2Decimal(shortData[3]));

                        Console.WriteLine("\n");

                    }


                }
            }


            Console.WriteLine("---------------------------------");

        }

        public static void searchItem()
        {
            Console.WriteLine("---------------------------------");

            Console.WriteLine("You can now search for an expense! Do you want to search using the Description or Category?");
            String searchTermG = Console.ReadLine();
            while (!searchTermG.ToLower().Equals("description") && !searchTermG.ToLower().Equals("category"))
            {
                Console.WriteLine("Please either enter Description or Category as your input");
                searchTermG = Console.ReadLine();
            }
            int curTotal = getCurTotal();
            int generalSearchValue;
            if (searchTermG.ToLower().Equals("description"))
            {
                generalSearchValue = 1; //for Description
            }
            else
            {
                generalSearchValue = 2; //For Category
            }
            bool found = false;
            Console.WriteLine("Please enter the search term that we need to search for in the " + searchTermG + "s");
            String userSearchTerm = Console.ReadLine().ToLower();

            for (int i = 0; i < curTotal; i++)
            {
                String[] arrayData = expenses[i].Split('☺');
                if (arrayData[generalSearchValue].ToLower().Contains(userSearchTerm))
                {
                    found = true;
                    Console.WriteLine("\n");
                    Console.WriteLine("Item Number - " + (i + 1));
                    Console.WriteLine("Date Of Purchase - " + formatDateString(arrayData[0]));
                    String[] truncated = arrayData[1].Split(' ');
                    if (truncated.Length > 6)
                    {
                        Console.WriteLine("Description - " + truncated[0] + " " + truncated[1] + " " + truncated[2] + " " + truncated[3] + " " + truncated[4] + " " + truncated[5] + "...");
                    }
                    else
                    {
                        Console.WriteLine("Description - " + arrayData[1]);
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("Could not find your search term!");
            }

            Console.WriteLine("---------------------------------");
        }

        public static void modifyItem()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Modification");
            Console.WriteLine("Please enter the item number you wish to modify");
            int itemNum = int.Parse(Console.ReadLine()) - 1;
            while (itemNum > getCurTotal() - 1)
            {
                Console.WriteLine("Please enter a number within the range 1 - " + getCurTotal());
                itemNum = int.Parse(Console.ReadLine()) - 1;
            }
            String[] itemModification = expenses[itemNum].Split('☺');
            Console.WriteLine("You have selected Item Number - " + (itemNum + 1) + "\nThis is what it currently contains");
            Console.WriteLine("Date Purchased - " + formatDateString(itemModification[0]));
            Console.WriteLine("Item Description - " + itemModification[1]);
            Console.WriteLine("Category - " + itemModification[2]);
            Console.WriteLine("Amount - R" + itemModification[3]);
            Console.WriteLine("\nPress Enter to disable modification of any data or any other key to edit the current data");

            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();
            if (key == ConsoleKey.Enter)
            {
                Console.WriteLine("Modification Disabled, No changes were made");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Modifications Enabled");
                Console.WriteLine("Please fill in the updated infromation for item number " + (itemNum + 1));

                String date = "";
                while (date.Length != 8) //validating the length of 8
                {
                    Console.WriteLine("Please enter the purchase date in format YYYYMMDD (8 Characters)");
                    date = Console.ReadLine();
                }
                Console.WriteLine("Please enter the Description of the Expenditure or Item");
                String desc = Console.ReadLine();
                Console.WriteLine("What category is this?");
                String category = Console.ReadLine();
                Console.WriteLine("What is the amount for this Expenditure or Item?");
                String amount = Console.ReadLine();
                expenses[itemNum] = date + "☺" + desc + "☺" + category + "☺" + amount;
                Console.WriteLine("Item Number " + (itemNum + 1) + " has been updated");
            }
            Console.WriteLine("---------------------------------");

        }


        public static void deleteItem()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Deletion");
            Console.WriteLine("Enter a item number that you want to delete");
            int itemNum = int.Parse(Console.ReadLine()) - 1;
            while (itemNum > getCurTotal() - 1)
            {
                Console.WriteLine("Please enter a number within the range 1 - " + getCurTotal());
                itemNum = int.Parse(Console.ReadLine()) - 1;
            }
            expenses[itemNum] = null;
            expenses = reOrderArray(expenses);
            Console.WriteLine("Deletion Complete!");
            Console.WriteLine("---------------------------------");

        }

        public static String[] sort(String[] input)
        {
            //Sorting by description first then doing date after so description is on order
            Console.WriteLine("Sorting...");

            //Sorting Descriptions
            String temp;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != null)
                {

                    String[] splitI = input[i].Split('☺');
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        String[] splitJ = input[i].Split('☺');
                        //[0] would be Date
                        //[1] would be Description
                        //[2] would be Category
                        //[3] would be Amount
                        if (splitI[1][0] > splitJ[1][0]) //so the words can be sorted aswell
                        {
                            temp = input[i];
                            input[i] = input[j];
                            input[j] = temp;
                        }
                    }
                }

            }

            //Sorting dates

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != null)
                {
                    String[] splitI = input[i].Split('☺');
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        String[] splitJ = input[i].Split('☺');
                        //[0] would be Date
                        //[1] would be Description
                        //[2] would be Category
                        //[3] would be Amount
                        if (convertStringToDate(splitI[0]) > convertStringToDate(splitJ[0]))
                        {
                            temp = input[i];
                            input[i] = input[j];
                            input[j] = temp;
                        }
                    }
                }
            }
            Console.WriteLine("Sorting Complete");


            return input;
        }

        public static void normalizeDesctiptions()
        {
            Console.WriteLine("Normalising...");
            for (int i = 0; i < getCurTotal(); i++)
            {
                String[] split = expenses[i].Split('☺');
                String description = split[1];
                description = description.Trim(); //removing trail spaces
                description = description.ToLower();
                description = FirstLetterToUpper(description);
                expenses[i] = split[0] + "☺" + description + "☺" + split[2] + "☺" + split[3];
            }
            Console.WriteLine("Done Normalising");

        }


        //extra methods to help with existing methods
        public static string FirstLetterToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static DateTime convertStringToDate(String date)
        {

            String year = date.Substring(0, 4);
            String month = date.Substring(4, 2);
            String day = date.Substring(6, 2);
            DateTime dt = DateTime.ParseExact(day + "/" + month + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }
        public static String formatDateString(String date)
        {


            String year = date.Substring(0, 4);
            String month = date.Substring(4, 2);
            String day = date.Substring(6, 2);

            return day + "/" + month + "/" + year;

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
                    hasDec = true;
                    break;
                }
                else
                {
                    smartS += sInput[i];
                }
            }
            if (hasDec == false)
            {
                smartS += ".00";
            }
            return smartS;
        }

        public static int getCurTotal()
        {
            int curTotal = expenses.Count(s => s != null); //total valid entries in array
            return curTotal;
        }
        public static String[] reOrderArray(String[] input)
        {
            List<String> temp = new List<String>();
            temp = input.ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                input[i] = temp[i];
            }
            for (int i = temp.Count; i < input.Length; i++)
            {
                input[i] = null;
            }
            return input;
        }

    }
}


