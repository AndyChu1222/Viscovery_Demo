using System;
using System.Collections.Generic;
using System.Linq;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetOrders(string orderType)
        {
            return OrderBuilder.Build(orderType);
        }
        public Order GetOrder(string code)
        {
            var all = OrderBuilder.GetAllProduct();

            var temp = all.Where(x => x.Code == code).FirstOrDefault();
            //現有的資料庫沒有，沒有API傳來的資料
            if (temp != null)
            {
                return temp;
            }
            return ErrorOrder(code);
        }

        private Order ErrorOrder(string code)
        {
            Order order = new Order
            {
                Code = code,
                Status = OrderStatus.Undetected,
            };
            return order;
        }
    }
}
