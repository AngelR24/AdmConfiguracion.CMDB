using System;
using System.Collections.Generic;
using System.Text;

namespace CMDB.Managers
{
    public class ConsoleManager
    {
        private readonly MenuManager _menuManager;
        private readonly List<string> _menuItems = new List<string>
        {
            "Add new CI", //Option 1
            "Add dependency CI" //Option 2
        };

        public ConsoleManager()
        {
            _menuManager = new MenuManager(_menuItems);  
        }

        public void Start()
        {
            Console.WriteLine("Application Started");

        }

    }
}
