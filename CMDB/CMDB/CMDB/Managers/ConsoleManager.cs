using CMDB.Models;
using System;
using System.Collections.Generic;

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
        private readonly AppDbContext _dbContext;
        private readonly ConfigurationItemManager _configItemManager;

        public ConsoleManager()
        {
            _menuManager = new MenuManager(_menuItems);
            _menuActions = new Dictionary<int, Action>
            {
                {1, AddNewCI },
                {2, AddCIDependency }
            };
            _dbContext = new AppDbContext();
            _configItemManager = new ConfigurationItemManager(_dbContext, _menuManager);
        }

        public void Start()
        {
            Console.WriteLine("Application Started");
            LoadMainScreen();
        }

        public void LoadMainScreen()
        {
            Console.Clear();
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
            Console.Clear();
            Console.WriteLine("ADD NEW CI");
            _menuManager.InvalidInputMessage("Press key to continue..");
            LoadMainScreen();
        }

        private void AddCIDependency()
        {
            Console.Clear();
            Console.WriteLine("Add dependency");
            _menuManager.InvalidInputMessage("Press key to continue..");
            LoadMainScreen();
        }
    }
}
