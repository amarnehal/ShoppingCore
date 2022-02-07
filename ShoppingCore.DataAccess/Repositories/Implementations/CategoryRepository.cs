using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class CategoryRepository : BaseRepositoryImplementation<Category>, ICatageoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category obj)
        {
            _dbContext.Set<Category>().Update(obj);
        }
    }
}
