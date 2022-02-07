using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Implementations
{
    public class OrderHeaderRepo : BaseRepositoryImplementation<OrderHeader>, IOrderHeaderRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderHeaderRepo(AppDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(OrderHeader obj)
        {
            _dbContext.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string paymentStatus = null)
        {
            var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if(orderFromDb!= null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus!=null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }

            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentItemId)
        {
            var orderFromdb = _dbContext.OrderHeaders.FirstOrDefault(x => x.Id == id);
            orderFromdb.SessionId = sessionId;
            orderFromdb.PaymentIntentId = paymentItemId;
             
        }
    }
}
