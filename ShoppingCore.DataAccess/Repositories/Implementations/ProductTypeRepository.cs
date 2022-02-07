using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class ProductTypeRepository : BaseRepositoryImplementation<ProductType>, IProductType
    {
        private readonly AppDbContext _dbContext;
        public ProductTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(ProductType productType)
        {
            _dbContext.ProductType.Update(productType);
        }
    }
}
