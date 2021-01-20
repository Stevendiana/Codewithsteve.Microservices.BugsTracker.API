using Myairops.Tech.Test.ClientDatabase.Models;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Data
{
    public interface IBugRepository : IRepository<Bug>
    {
        Bug GetOneBug(string id);
        IEnumerable<Bug> GetAllBugs();
    }
}
