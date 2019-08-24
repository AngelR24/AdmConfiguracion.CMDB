using System;
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
    }
}
