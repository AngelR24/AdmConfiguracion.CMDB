using System.Collections.Generic;

namespace CMDB.Models.Dtos
{
    /// <summary>
    /// This class represents a Configuration Item node
    /// </summary>
    public class CINode
    {
        public ConfigurationItem Value { get; set; }
        
        public IEnumerable<CINode> Nodes { get; set; }
    }
}