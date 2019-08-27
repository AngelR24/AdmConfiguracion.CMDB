using System;
using System.Linq;
using System.Text.RegularExpressions;
using CMDB.Models;

namespace CMDB.Managers
{
    public class UpgradesManager
    {
        private readonly AppDbContext _dbContext;
        private readonly MenuManager _menuManager;
        private readonly ConfigurationItemManager _configurationItemManager;
        private readonly string _semVerPattern;

        public UpgradesManager(
            AppDbContext dbContext, 
            MenuManager menuManager,
            ConfigurationItemManager configurationItemManager)
        {
            _dbContext = dbContext;
            _menuManager = menuManager;
            _configurationItemManager = configurationItemManager;
            _semVerPattern = @"^([0-9]+)\.([0-9]+)\.([0-9]+)(?:-([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?(?:\+[0-9A-Za-z-]+)?$";
        }

        public void PerfomCiUpgradeOrDowngrade()
        {
            Console.WriteLine("PERFOM UPGRADE/DOWNGRADE");
            Console.WriteLine(new string('-', 25));
            _configurationItemManager.ListCIWithVersion();
            Console.WriteLine(new string('-', 25));
            Console.Write("Select Configuration Item to Ugrade/Downgrade: ");

            string selectedCi = Console.ReadLine()?.ToUpper();

            var ci = _dbContext.ConfigurationItems.Find(selectedCi);
            if (ci == null)
            {
                _menuManager.InvalidInputMessage("The given configuration item was not found in the database");
                return;
            }

            bool versionValid = false;
            string writtenVersion = "0.0.0";

            while (!versionValid)
            {
                Console.Write("Enter new version (0.0.0): ");
                string version = Console.ReadLine()?.ToUpper();

                var match = Regex.Match(version, _semVerPattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    versionValid = true;
                    writtenVersion = version;
                }
                else
                {
                    Console.Write("      YOU ENTERED AN INVALID SEMANTIC VERSION NUMBER");
                    Console.WriteLine();
                }
            }

            int[] versionChunks = writtenVersion.Split('.').Select(str => Int32.Parse(str)).ToArray();
            
        }
        
        
    }
}