using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers.Resources
{
    public class ProjectData
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string ProjectOwnerId { get; set; }
        public string ProjectManagerId { get; set; }
    }
}
