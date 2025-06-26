using System.Collections.Generic;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder(string code);
        IEnumerable<Order> GetOrders(string orderType);
    }
}
