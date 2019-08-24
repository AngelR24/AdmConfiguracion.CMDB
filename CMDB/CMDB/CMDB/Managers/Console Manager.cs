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
        private readonly Dictionary<int, Action> _menuActions;

        public ConsoleManager()
        {
            _menuManager = new MenuManager(_menuItems);
            _menuActions = new Dictionary<int, Action>
            {
                {1, AddNewCI },
                {2, AddCIDependency }
            };
        }

        public void Start()
        {
            Console.WriteLine("Application Started");
            LoadMainScreen();
        }

        public void LoadMainScreen()
        {
            _menuManager.PrintMainMenu();
            int option = _menuManager.SelectOption();

            if (option == -1)
            {
                LoadMainScreen();
            }

            _menuActions[option]();

        }


        private void AddNewCI()
        {

        }

        private void AddCIDependency()
        {

        }
    }
}
