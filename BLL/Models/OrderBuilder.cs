using System.Collections.Generic;
using System.Linq;

namespace ViscoveryDemo.BLL.Models
{
    public static class OrderBuilder
    {
        public static List<Order> GetAllProduct()
        {
            var list1 = new List<Order>
            {
                new Order { Id = 1, Code = "b101", Name = "小麵包", Price = 12.99m },
                new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
                new Order { Id = 3, Code = "b103", Name = "小圓麵包", Price = 12.99m },
                new Order { Id = 3, Code = "b104", Name = "圓麵包", Price = 7.99m }
            };

            return list1;
        }
        private static readonly Dictionary<string, List<Order>> _menus = new Dictionary<string, List<Order>>()
        {
            ["1"] = new List<Order>
            {
                new Order { Id = 1, Code = "b103", Name = "小圓麵包", Price = 12.99m },
                new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
                new Order { Id = 3, Code = "b104", Name = "圓麵包", Price = 7.99m }
            },
            ["2"] = new List<Order>
            {
                new Order { Id = 1, Code = "b103", Name = "小圓麵包", Price = 12.99m },
                new Order {Id = 2, Code = "b101", Name = "小麵包", Price = 12.99m}
            },
            ["3"] = new List<Order>
            {
                new Order { Id = 1, Code = "b101", Name = "小麵包", Price = 12.99m },
                new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
                new Order { Id = 3, Code = "b103", Name = "小圓麵包", Price = 12.99m },
            }
        };

        public static void Register(string key, IEnumerable<Order> orders)
        {
            _menus[key] = orders.ToList();
        }

        public static List<Order> Build(string key)
        {
            return _menus.TryGetValue(key, out var orders)
                ? orders.Select(o => new Order { Id = o.Id, Code = o.Code, Name = o.Name, Price = o.Price }).ToList()
                : new List<Order>();
        }
    }
}
