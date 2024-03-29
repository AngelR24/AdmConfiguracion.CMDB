﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMDB.Managers
{
    public class MenuManager
    {
        private readonly List<string> _menuItems;
        
        public MenuManager(List<string> menuItems)
        {
            _menuItems = menuItems;
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("Main menu");
            for (int i = 0; i < _menuItems.Count; i++)
            {
                Console.WriteLine($"{i+1}- {_menuItems[i]}");
            }
            Console.WriteLine($"{_menuItems.Count + 1}- Exit");  
        }

        public int SelectOption()
        {
            Console.Write("Select an option: ");
            string answer = Console.ReadLine();
            int option = -1;
            bool result = Int32.TryParse(answer, out option);

            if (!result)
            {
                PerformConsolePause();
                return -1;
            }

            if (option < 1 || option > _menuItems.Count + 1)
            {
                PerformConsolePause();
                return -1;
            }

            if (option == _menuItems.Count + 1)
            {
                ExitApplication();
                return -1;
            }

            return option;
          
        }

        public void PerformConsolePause(string message = "Invalid input..")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public bool ConfirmMessage(string message = "Are you sure you want to continue?")
        {
            Console.Write($"{message} (Y/N) -->");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Y")
            {
                return true;
            }
            return false;
        }

        private void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
