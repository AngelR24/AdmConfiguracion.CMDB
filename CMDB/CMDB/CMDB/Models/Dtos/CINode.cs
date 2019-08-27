namespace CMDB.Models.Dtos
{
    /// <summary>
    /// This class represents a Configuration Item node
    /// </summary>
    public class CINode
    {
        public ConfigurationItem Value { get; set; }
        
        public CINode Node { get; set; }
    }
}