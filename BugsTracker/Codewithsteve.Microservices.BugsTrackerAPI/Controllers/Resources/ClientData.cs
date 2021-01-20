using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers.Resources
{
    public class ClientData
    {
        public string ClientId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
