using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMDB.Models
{
    public class Dependency
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string BaseCIName { get; set; }

        public string DependencyCIName { get; set; }

        public ConfigurationItem BaseCI { get; set; }

        public ConfigurationItem DependencyCI { get; set; }

    }
}
