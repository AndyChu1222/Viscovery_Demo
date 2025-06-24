using System.Collections.Generic;
using System.Threading.Tasks;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.BLL.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> LoadOrdersAsync(string orderType);
    }
}
