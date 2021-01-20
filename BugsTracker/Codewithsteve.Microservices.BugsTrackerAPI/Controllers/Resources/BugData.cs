using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers.Resources
{
    public class BugData
    {
        public string BugId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Status { get; set; }
        public string ProjectId { get; set; }
        [Required]
        public string ClientId { get; set; }
        public DateTime ResolveByDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public string Impact { get; set; }
        [Required]
        public int Severity { get; set; }
        public string severitylevel { get; set; }
        public string Notes { get; set; }
        public string ResolvedBy { get; set; }
        public string SignedoffBy { get; set; }
    }
}
