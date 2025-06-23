using System.Collections.Generic;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(string orderType);
    }
}
