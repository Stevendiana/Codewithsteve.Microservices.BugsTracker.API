using Microsoft.EntityFrameworkCore;
using Codewithsteve.Microservices.BugsTracker.Models;
using Codewithsteve.Microservices.BugsTracker.API.Data;
using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codewithsteve.Microservices.BugsTracker.API.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Client GetOneClient(string id)
        {
            return ApplicationDbContext.Clients.SingleOrDefault(d => d.ClientId == id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return ApplicationDbContext.Clients
                .Include(x=>x.Bugs).OrderByDescending(d => d.Name).ToList();
        }


        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
