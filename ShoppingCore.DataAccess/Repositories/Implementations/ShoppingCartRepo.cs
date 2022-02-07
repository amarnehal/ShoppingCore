using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class ShoppingCartRepo:BaseRepositoryImplementation<ShoppingCart>,IShoppingCart
    {
        private readonly AppDbContext _dbContext;
        public ShoppingCartRepo(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int decrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int incrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
    }
}
