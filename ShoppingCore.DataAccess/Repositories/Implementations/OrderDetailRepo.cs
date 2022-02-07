using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class OrderDetailRepo : BaseRepositoryImplementation<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderDetailRepo(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(OrderDetail obj)
        {
            _dbContext.Update(obj);
        }
    }
}
