﻿using Codewithsteve.Microservices.BugsTracker.API.Data;
using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codewithsteve.Microservices.BugsTracker.Models;

namespace Codewithsteve.Microservices.BugsTracker.API.Repositories
{
    public class BugRepository : Repository<Bug>, IBugRepository
    {
        public BugRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Bug GetOneBug(string id)
        {
            return ApplicationDbContext.Bugs.SingleOrDefault(d => d.BugId == id);
        }

        public IEnumerable<Bug> GetAllBugs()
        {
            return ApplicationDbContext.Bugs
                .Include(r => r.Client)
                .OrderByDescending(d => d.DateCreated).ToList();
        }

       
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
