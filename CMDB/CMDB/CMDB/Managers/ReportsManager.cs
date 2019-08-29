using System;
using System.Collections.Generic;
using System.Linq;
using CMDB.Models;
using CMDB.Models.Dtos;

namespace CMDB.Managers
{
    public class ReportsManager
    {
        private readonly AppDbContext _dbContext;
        private readonly ConfigurationItemManager _configurationItemManager;

        public ReportsManager(AppDbContext dbContext, ConfigurationItemManager configurationItemManager)
        {
            _dbContext = dbContext;
            _configurationItemManager = configurationItemManager;
        }

        public CINode BuildNodesForCi(ConfigurationItem item)
        {
            CINode node = new CINode
            {
                Value = item
            };
            return BuildNodesForCi(node, 0);
        }

        public void PrintNodeTree(CINode node)
        {
            PrintNodeTree(node, 0);
        }

        public void ListDependencies()
        {
            Console.Clear();
            Console.WriteLine("List Dependencies");
            _configurationItemManager.ListItems();

            Console.WriteLine(new string('-', 20));
            Console.Write("Main Configuration Item: ");
            string mainCI = Console.ReadLine();
            var mainItem = _dbContext.ConfigurationItems.Find(mainCI.ToUpper());
            if (mainItem == null)
            {
                Console.WriteLine("CI not found");
                Console.ReadKey();
                return;
            }

            var node = BuildNodesForCi(mainItem);

            PrintNodeTree(node);
            Console.ReadKey();
        }

        private CINode BuildNodesForCi(CINode node, int level)
        {
            var children = _dbContext.Dependencies.Where(di => di.DependencyCIName == node.Value.Name)
                .Select(di => new CINode {Value = di.BaseCI}).ToList();

            if (children.Count > 0)
            {
                var resultNodes = new List<CINode>();

                foreach (var child in children)
                {
                    var foundNode = BuildNodesForCi(child, level + 1);
                    if (foundNode != null)
                    {
                        resultNodes.Add(foundNode);
                    }
                    else
                    {
                        resultNodes.Add(child);
                    }

                    node.Nodes = resultNodes;
                }
            }

            if (level != 0)
            {
                return null;
            }

            return node;
        }

        private void PrintNodeTree(CINode node, int level)
        {
            if (node == null)
            {
                return;
            }
            
            Console.Write('|');
            Console.Write(new string('-', level * 2));
            Console.WriteLine($"{node.Value.Name}        PERSON RESPONSIBLE IS: {node.Value.Responsible}");

            if (node.Nodes != null && !node.Nodes.Any())
            {
                return;
            }

            if (node.Nodes != null)
            {
                foreach (var childNode in node.Nodes)
                {
                    PrintNodeTree(childNode, level + 1);
                }
            }
        }
    }
}