using OrderingDomain.Entities;
using OrderingDomain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderingApplication.Handlers
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName);
    }
}