using Myairops.Tech.Test.ClientDatabase.Models;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Data
{
   
    public interface IClientRepository : IRepository<Client>
    {
        Client GetOneClient(string id);
        IEnumerable<Client> GetAllClients();
    }
}
