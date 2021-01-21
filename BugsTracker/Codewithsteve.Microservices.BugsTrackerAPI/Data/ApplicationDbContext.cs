
using Microsoft.EntityFrameworkCore;
using Codewithsteve.Microservices.BugsTracker.Models;
using System;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}