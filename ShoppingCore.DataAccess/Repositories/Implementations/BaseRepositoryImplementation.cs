using Microsoft.EntityFrameworkCore;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class BaseRepositoryImplementation<TEntity> : BaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepositoryImplementation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null,string? includeproperties = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
           IQueryable<TEntity> querry = dbSet;
            if(filter != null)
            {
                querry = querry.Where(filter);

            }
            
            if (includeproperties != null)
            {
                foreach(var includeProp in includeproperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(includeProp);
                }
            }
            return querry.ToList();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter,string? includeproperties = null)
        {
            IQueryable<TEntity> querry = dbSet;
            querry = querry.Where(filter);
            if (includeproperties != null)
            {
                foreach (var includeProp in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(includeProp);
                }
            }
            return querry.FirstOrDefault();
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
