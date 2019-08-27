using CMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.Clear();
            Console.WriteLine("Add Dependency");
            ListItems();

            Console.WriteLine(new string('-', 20));
            Console.Write("Main Configuration Item: ");
            string mainCI = Console.ReadLine();
            var mainItem = _dbContext.ConfigurationItems.Find(mainCI.ToUpper());
            if (mainItem == null)
            {
                _menuManager.InvalidInputMessage("CI was not found");
                return;
            }
            Console.Write("Dependant Configuration Item: ");
            string dependantCI = Console.ReadLine();
            var dependantItem = _dbContext.ConfigurationItems.Find(dependantCI.ToUpper());
            if (dependantItem == null)
            {
                _menuManager.InvalidInputMessage("CI was not found");
                return;
                
            }

            var dependency = new Dependency
            {
                BaseCIName = mainCI.ToUpper(),
                DependencyCIName = dependantCI.ToUpper()
            };


            if (_menuManager.ConfirmMessage("Are you sure you want to add this Dependency"))
            {
                _dbContext.Dependencies.Add(dependency);
                _dbContext.SaveChanges();
            }

            
        }

        public void ListItems()
        {
            var items = _dbContext.ConfigurationItems.ToList();

            foreach (var item in items)
            {
                Console.WriteLine($"*{item.Name}  {item.Version}");
            }
        }

        public void ListDependencies()
        {
            ListItems();
            string BaseCI = Console.ReadLine();
            var configurationItem = _dbContext.ConfigurationItems.Find(BaseCI.ToUpper());
            if (string.IsNullOrEmpty(BaseCI) || configurationItem == null)
            {
                _menuManager.InvalidInputMessage("CI was not found");
                return;
            }
            var items = _dbContext.Dependencies.Where(q => q.BaseCIName == BaseCI.ToUpper()).ToList();          

            if (items.Count == 0)
            {
                Console.WriteLine("This CI has no dependant CIs");
            }
            else
            {
                Console.WriteLine($"The next CI are dependant of {BaseCI.ToUpper()}:");
                foreach (var item in items)
                {
                    Console.WriteLine($"*{item.DependencyCIName} {item.DependencyCI.Version}");
                }
            }            
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
        }
    }
}
