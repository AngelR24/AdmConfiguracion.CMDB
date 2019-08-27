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
            "Add dependency CI", //Option 2
            "List dependencies", //Option 3
            "Upgrading CI"
        };
        private readonly Dictionary<int, Action> _menuActions;
        private readonly AppDbContext _dbContext;
        private readonly ConfigurationItemManager _configItemManager;
        private readonly UpgradesManager _upgradesManager;
        private readonly ReportsManager _reportsManager;

        public ConsoleManager()
        {
            _menuManager = new MenuManager(_menuItems);
            _menuActions = new Dictionary<int, Action>
            {
                {1, AddNewCI },
                {2, AddCIDependency },
                {3, ListDependencies },
                {4, UpgradeConfigurationItem}
            };
            _dbContext = new AppDbContext();
            _configItemManager = new ConfigurationItemManager(_dbContext, _menuManager);
            _reportsManager = new ReportsManager(_dbContext, _configItemManager);
            _upgradesManager = new UpgradesManager(_dbContext, _menuManager, _configItemManager, _reportsManager);
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
            _configItemManager.Create();
            LoadMainScreen();
        }

        private void AddCIDependency()
        {
            Console.Clear();
            _configItemManager.AddDependency();
            LoadMainScreen();
        }

        private void ListDependencies()
        {
            Console.Clear();
            _configItemManager.ListDependencies();
            LoadMainScreen();
        }

        private void UpgradeConfigurationItem()
        {
            Console.Clear();
            _upgradesManager.PerfomCiUpgradeOrDowngrade();
            LoadMainScreen();
        }
    }
}
