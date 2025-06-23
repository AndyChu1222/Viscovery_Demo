using System.Collections.Generic;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.BLL.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> LoadOrders(string orderType);
    }
}
