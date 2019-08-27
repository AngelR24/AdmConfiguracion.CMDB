using System;
using CMDB.Models;

namespace CMDB.Managers
{
    public class UpgradesManager
    {
        private readonly AppDbContext _dbContext;
        private readonly MenuManager _menuManager;
        private readonly ConfigurationItemManager _configurationItemManager;

        public UpgradesManager(
            AppDbContext dbContext, 
            MenuManager menuManager,
            ConfigurationItemManager configurationItemManager)
        {
            _dbContext = dbContext;
            _menuManager = menuManager;
            _configurationItemManager = configurationItemManager;
        }

        public void PerfomCiUpgradeOrDowngrade()
        {
            Console.WriteLine("PERFOM UPGRADE/DOWNGRADE");
            Console.WriteLine(new string('-', 25));
            _configurationItemManager.ListCIWithVersion();
            Console.WriteLine(new string('-', 25));
            Console.Write("Select Configuration Item to Ugrade/Downgrade");
        }
        
        
    }
}