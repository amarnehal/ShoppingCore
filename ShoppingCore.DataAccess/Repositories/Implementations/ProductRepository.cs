using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class ProductRepository : BaseRepositoryImplementation<Product>, IProductInterface
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Product obj)
        {
           var objToUpdate= _dbContext.Products.FirstOrDefault(x => x.Id == obj.Id);
            if(objToUpdate != null)
            {
                objToUpdate.Name = obj.Name;
                objToUpdate.Description = obj.Description;
                objToUpdate.Price = obj.Price;
                objToUpdate.CategoryId = obj.CategoryId;
                objToUpdate.DateCreated = DateTime.Now;
                if (obj.ImageUrl != null)
                {
                    objToUpdate.ImageUrl = obj.ImageUrl;
                }
                 
            }

        }
    }
}
