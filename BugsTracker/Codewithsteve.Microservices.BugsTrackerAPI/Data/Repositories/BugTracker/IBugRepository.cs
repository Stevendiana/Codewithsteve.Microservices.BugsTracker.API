using Codewithsteve.Microservices.BugsTracker.Models;
using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codewithsteve.Microservices.BugsTracker.API.Data
{
    public interface IBugRepository : IRepository<Bug>
    {
        Bug GetOneBug(string id);
        IEnumerable<Bug> GetAllBugs();
    }
}
