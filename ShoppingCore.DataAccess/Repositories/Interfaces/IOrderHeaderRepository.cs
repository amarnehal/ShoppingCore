using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface IOrderHeaderRepository:BaseRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int id, string orderStatus, string paymentStatus = null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentItemId);
    }
}
