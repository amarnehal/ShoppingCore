using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface BaseRepository<TEntity> where TEntity:class
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter= null, string ? includeproperties = null);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity GetFirstOrDefault(Expression<Func<TEntity,bool>>filter, string? includeproperties = null);
    }
}
