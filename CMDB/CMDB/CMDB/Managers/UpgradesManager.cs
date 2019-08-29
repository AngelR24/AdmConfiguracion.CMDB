using System;
using System.Linq;
using System.Text.RegularExpressions;
using CMDB.Models;
using CMDB.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CMDB.Managers
{
    public class UpgradesManager
    {
        private readonly AppDbContext _dbContext;
        private readonly MenuManager _menuManager;
        private readonly ConfigurationItemManager _configurationItemManager;
        private readonly ReportsManager _reportsManager;
        private readonly string _semVerPattern;

        public UpgradesManager(
            AppDbContext dbContext, 
            MenuManager menuManager,
            ConfigurationItemManager configurationItemManager,
            ReportsManager reportsManager)
        {
            _dbContext = dbContext;
            _menuManager = menuManager;
            _configurationItemManager = configurationItemManager;
            _reportsManager = reportsManager;
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
                _menuManager.PerformConsolePause("The given configuration item was not found in the database");
                return;
            }

            bool versionValid = false;
            string writtenVersion = "0.0.0";

            while (!versionValid)
            {
                Console.Write($"Enter new version ({ci.Version}): ");
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

            int[] newVersionChunks = writtenVersion.Split('.').Select(str => Int32.Parse(str)).ToArray();
            int[] oldVersionChunks = ci.Version.Split('.').Select(str => Int32.Parse(str)).ToArray();

            CINode node = _reportsManager.BuildNodesForCi(ci);
            //0 => Major //1 => Minor //2 => Patch
            if (oldVersionChunks[0] != newVersionChunks[0])
            {
                Console.WriteLine("Detected Major Change in versions");
                Console.WriteLine("Dangerous Action!");
                Console.WriteLine("Changes were not saved !");
                Console.WriteLine("HERE IS THE HIERARCHY TREE");
                _reportsManager.PrintNodeTree(node);
            }
            else if (oldVersionChunks[1] != newVersionChunks[1])
            {
                Console.WriteLine("Detected Minor Change in versions");
                Console.WriteLine("It is possible that certain Items may be affected");
                ci.Version = writtenVersion;
                _dbContext.Entry(ci).State = EntityState.Modified;
                _dbContext.SaveChanges();
                Console.WriteLine("HERE IS THE HIERARCHY TREE");
                _reportsManager.PrintNodeTree(node);
            }
            else if (oldVersionChunks[2] != newVersionChunks[2])
            {
                Console.WriteLine("Detected a patch change");
                ci.Version = writtenVersion;
                _dbContext.Entry(ci).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("No changes detected at all");
            }
            
            _menuManager.PerformConsolePause("Press any key to continue...");

        }
        
        
    }
}