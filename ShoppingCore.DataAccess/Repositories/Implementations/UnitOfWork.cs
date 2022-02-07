using ShoppingCore.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
            Catageory = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            ProductType = new ProductTypeRepository(_dbContext);
            ApplicationUser = new ApplicationUserRepo(_dbContext);
            ShoppingCart = new ShoppingCartRepo(_dbContext);
            OrderHeader = new OrderHeaderRepo(_dbContext);
            OrderDetail = new OrderDetailRepo(_dbContext);
        }
        public ICatageoryRepository Catageory { get; private set; }

        public IProductInterface Product { get; private set; }

        public IProductType ProductType { get; private set; }
        public IApplicationUser ApplicationUser { get; private set; }
        public IShoppingCart ShoppingCart { get; private set; }
        public IOrderDetailRepository  OrderDetail { get;private set; }
        public IOrderHeaderRepository OrderHeader { get; set; }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
