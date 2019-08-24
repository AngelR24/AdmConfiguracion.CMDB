using CMDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDB.Managers
{ 
    public class ConfigurationItemManager
    {
        private readonly AppDbContext _dbContext;
        private readonly MenuManager _menuManager;

        public ConfigurationItemManager(AppDbContext dbContext, MenuManager menuManager)
        {
            _dbContext = dbContext;
            _menuManager = menuManager;
        }


        public void Create()
        {
            Console.Clear();
            Console.WriteLine("Add new Configuration Item");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Responsible: ");
            string responsible = Console.ReadLine();
            Console.Write("Version (0.0.0): ");
            string version = Console.ReadLine();

            ConfigurationItem item = new ConfigurationItem
            {
                Name = name.ToUpper(),
                Description = description.ToUpper(),
                Responsible = responsible.ToUpper(),
                Version = version
            }; 

            if (_menuManager.ConfirmMessage("Are you sure you want to add this Configuration Item"))
            {
                _dbContext.ConfigurationItems.Add(item);
                _dbContext.SaveChanges();
            }
        }

        public void AddDependency()
        {

        }

        public void ListItems()
        {

        }

    }
}
