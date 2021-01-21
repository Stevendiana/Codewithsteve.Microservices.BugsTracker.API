using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Codewithsteve.Microservices.BugsTracker.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

       
        public TEntity Get(string id, string companyId)
        {
            return Context.Set<TEntity>().Find(id, companyId);
        }


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

      

        public void Detach(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Detached;
        }

        public void Modified(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Added(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Added;
        }

        public void NotModified(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public void Entry(TEntity entity)
        {
            Context.Entry<TEntity>(entity);
        }

        public EntityState EntryState(TEntity entity)
        {
           return Context.Entry<TEntity>(entity).State;
        }

        
        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }
        public void AttachRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AttachRange(entities);
        }

        public void AddObject(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
        }



        // Here we are working with a DbContext, not ApplicationDbContext. So we don't have DbSets 
        // such as Courses or Authors, and we need to use the generic Set() method to access them.

        //public IEnumerable<TEntity> GetAll()
        //{
        //    // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
        //    // too much noise. I could get a reference to the DbSet returned from this method in the 
        //    // constructor and store it in a private field like _entities. This way, the implementation
        //    // of our methods would be cleaner:
        //    // 
        //    // _entities.ToList();
        //    // _entities.Where();
        //    // _entities.SingleOrDefault();
        //    // 
        //    // I didn't change it because I wanted the code to look like the videos. But feel free to change
        //    // this on your own.
        //    return Context.Set<TEntity>().ToList();
        //}
    }
}
