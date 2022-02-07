using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface IShoppingCart:BaseRepository<ShoppingCart>
    {
        int incrementCount(ShoppingCart shoppingCart, int count);
        int decrementCount(ShoppingCart shoppingCart, int count);
    }
}
