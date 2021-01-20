using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id, string companyId);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        bool Any(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Attach(TEntity entity);
        void AttachRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Detach(TEntity entity);
        void Modified(TEntity entity);
        void NotModified(TEntity entity);
        void Entry(TEntity entity);
        void Added(TEntity entity);
        EntityState EntryState(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);


    }
}
