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

        private CINode BuildNodesForCi(CINode node, int level)
        {
            var children = _dbContext.Dependencies.Where(di => di.BaseCIName == node.Value.Name)
                .Select(di => new CINode {Value = di.DependencyCI}).ToList();

            if (children.Count() > 0)
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
    }
}