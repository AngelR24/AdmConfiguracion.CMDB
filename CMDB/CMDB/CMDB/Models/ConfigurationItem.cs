using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMDB.Models
{
    public class ConfigurationItem
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Responsible { get; set; }

        public List<Dependency> DependencyItems { get; set; }
        public List<Dependency> BaseItems { get; set; }
    }
}
