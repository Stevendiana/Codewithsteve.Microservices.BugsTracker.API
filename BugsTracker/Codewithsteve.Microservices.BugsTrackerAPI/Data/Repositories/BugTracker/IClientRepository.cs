using Codewithsteve.Microservices.BugsTracker.Models;
using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codewithsteve.Microservices.BugsTracker.API.Data
{
   
    public interface IClientRepository : IRepository<Client>
    {
        Client GetOneClient(string id);
        IEnumerable<Client> GetAllClients();
    }
}
