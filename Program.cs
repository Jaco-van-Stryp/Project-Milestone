﻿using System;
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
        static bool allowModifications = true;
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
                    searchItem();
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

            int curTotal = getCurTotal();

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
        
        public static void searchItem()
        {
            Console.WriteLine("---------------------------------");

            Console.WriteLine("You can now search for an expense! Do you want to search using the Description or Caterogy?");
            String searchTermG = Console.ReadLine();
            while(!searchTermG.ToLower().Equals("description") || !searchTermG.ToLower().Equals("category"))
            {
                Console.WriteLine("Please either enter Description or Category as your input");
                searchTermG = Console.ReadLine();
            }
            int curTotal = getCurTotal();
            int generalSearchValue;
            if(searchTermG.ToLower().Equals("description"))
            {
               generalSearchValue = 1; //for Description
            }
            else
            {
                generalSearchValue = 2; //For Category
            }

            Console.WriteLine("Please enter the search term that we need to search for in the " + searchTermG + "'s");
            String userSearchTerm = Console.ReadLine().ToLower();

            Console.WriteLine("\nDisplaying Summary\n");
            for (int i = 0; i < curTotal; i++)
            {
                String[] arrayData = expenses[i].Split("☺");
                if(arrayData[generalSearchValue].ToLower().Contains(userSearchTerm))
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Item Number - " + (i + 1));
                    Console.WriteLine("Date Of Purchase - " + convertStringToDate(arrayData[0]));
                    Console.WriteLine("Description - " + arrayData[1]); //TODO - Display in sixth truncated blank, if any spaces six or more
                }
            }


            Console.WriteLine("---------------------------------");
        }

        public static void modifyItem()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Modification");
            Console.WriteLine("Please enter the item number you wish to modify");
            int itemNum = int.Parse(Console.ReadLine()) - 1;
            while(itemNum > getCurTotal())
            {
                Console.WriteLine("Please enter a number within the range 1 - " + getCurTotal());
                itemNum = int.Parse(Console.ReadLine()) - 1;
            }
            String[] itemModification = expenses[itemNum].Split("☺");
            Console.WriteLine("You have selected Item Number - " + (itemNum + 1) + "\nThis is what it currently contains");
            Console.WriteLine("Date Purchased - " + convertStringToDate(itemModification[0]));
            Console.WriteLine("Item Description - " + itemModification[1]);
            Console.WriteLine("Category - " + itemModification[2]);
            Console.WriteLine("Amount - R" + itemModification[3]);
            Console.WriteLine("\nPress Enter to disable modification of any data");
            do
            {
                Console.WriteLine("Modification has been disabled");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.WriteLine("---------------------------------");

        }


        //extra methods to help with existing methods
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

        public static int getCurTotal()
        {
            int curTotal = expenses.Count(s => s != null);//total valid entries in array
            return curTotal;
        }

    }
}

/* Question Contents
Maggie’s family loves shopping and earning Smart Shopper’s points. They earn points when they buy
any item at selected local stores. As a developer see how they can backup and record their purchases
into a Household Account system to keep track of their expenditures.
Milestone 1 - Planning
In the first milestone, you are required to plan out exactly how you will provide a solution for the
required. Below its set of prerequisites for implementing all the requirements of this project.
The system should be able to store up to 10000 costs and revenues. For each
expense it should be allowed to save the following information:
• Date (8 characters: YYYYMMDD format) of purchase
• Description of expenditure or item
• Category
• Amount
Using a menu allowing the user to perform the following operations:
1 - Add a new expense. This option allows the user to capture the purchase date (date: YYYYMMDD
format), the description of the item purchased and the category of an item.
2 - Show all expenses. This option allows the user to specify the category (e.g. devices), a period in
terms of start date and end date when the purchase is made for that category. Finally display the
summary as item number, date (format DD / MM / YYYY) description, a category in parentheses,
and amount to two decimal places, all in the same line, separated by hyphens. At the end of all the
data show the total amount of data displayed.
3 – Search item. This option allows the user to specify the description or category without
distinguishing case sensitive or not). Finally, the summary is displayed as item number, the date of
purchase and item description (the description is displayed in the sixth truncated blank, if any
spaces six or more).
4 - Modify item. This option allows the user to specify the item by its number, and show the
previous value of each field (date purchased, item description, category and amount) and display
the message “press Enter to disable modification of any data” otherwise allow the user to make
necessary modifications to the item description, category, date and amount of purchase.
5 - Delete item. This option allows the user to capture the item number and clear the item from
the history of the record. Be advised that every time item is removed from the list, the list is reordered.
6 - Sort items alphabetically. This option sort the items in the list by date (if matched) description.
7 - Normalize descriptions: remove trailing spaces, spaces and mirror sites. If a description is all
uppercase, will be converted to lowercase (except for the first letter, kept in uppercase). 
 */