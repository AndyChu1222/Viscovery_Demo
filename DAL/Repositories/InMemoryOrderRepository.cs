using System.Collections.Generic;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetOrders(string orderType)
        {
            return OrderBuilder.Build(orderType);
        }
    }
}
