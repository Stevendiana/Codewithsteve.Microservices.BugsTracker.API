﻿using AutoMapper;
using System.Threading.Tasks;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Data;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        //private readonly IUserService _userService;
        private readonly IMapper _mapper;
        


        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;


            Bugs = new BugRepository(_context);
            Clients = new ClientRepository(_context);

        }

        public IBugRepository Bugs { get; private set; }
        public IClientRepository Clients { get; private set; }
        

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
