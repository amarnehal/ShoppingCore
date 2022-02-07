using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICatageoryRepository Catageory { get; }
        IProductInterface Product { get; }
        IProductType ProductType { get; }
        IApplicationUser ApplicationUser { get; }
        IShoppingCart ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        void Save();
    }
}
