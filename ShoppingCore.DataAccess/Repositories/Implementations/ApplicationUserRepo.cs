using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class ApplicationUserRepo:BaseRepositoryImplementation<ApplicationUser>,IApplicationUser
    {
        private readonly AppDbContext _dbContext; 
        public ApplicationUserRepo(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
