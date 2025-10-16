using System.Collections.Generic;
using System.Linq;

namespace ViscoveryDemo.BLL.Models
{
    public static class OrderBuilder
    {
        public static List<Order> GetAllProduct()
        {
            var allBurgerKingItemList = new List<Order>
            {
                new Order { Id = 1, Code = "2025101401", Name = "Whopper", Price = 144m },
                new Order { Id = 2, Code = "2025101402", Name = "Whopper Jr.", Price = 119m },
                new Order { Id = 3, Code = "2025101403", Name = "Coke", Price = 38m },
                new Order { Id = 4, Code = "2025101404", Name = "Double Cheeseburger", Price = 99m },
                new Order { Id = 5, Code = "2025101405", Name = "Fish'N Crisp", Price = 74m },
            };

            var allItemList = new List<Order>
            {
                new Order { Id = 1, Code = "b101", Name = "小麵包", Price = 12.99m },
                new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
                new Order { Id = 3, Code = "b103", Name = "小圓麵包", Price = 12.99m },
                new Order { Id = 4, Code = "b104", Name = "圓麵包", Price = 7.99m }
                
            };
            return allBurgerKingItemList;
        }
        private static readonly Dictionary<string, List<Order>> _menus = new Dictionary<string, List<Order>>()
        {
            #region 漢堡王版本
            ["a"] = new List<Order>
            {
                new Order { Id = 1, Code = "2025101401", Name = "Whopper", Price = 144m },
                new Order { Id = 2, Code = "2025101402", Name = "Whopper Jr.", Price = 119m },
                new Order { Id = 4, Code = "2025101404", Name = "Double Cheeseburger", Price = 99m },
                new Order { Id = 5, Code = "2025101405", Name = "Fish'N Crisp", Price = 74m },
            },
            ["b"] = new List<Order>
            {
                new Order { Id = 1, Code = "2025101401", Name = "Whopper", Price = 144m },
                new Order { Id = 3, Code = "2025101403", Name = "Coke", Price = 38m }
            },
            ["c"] = new List<Order>
            {
                new Order { Id = 1, Code = "2025101401", Name = "Whopper", Price = 144m },
                new Order { Id = 4, Code = "2025101404", Name = "Double Cheeseburger", Price = 99m }
            },
            #endregion

            #region 開發測試版本
            //["1"] = new List<Order>
            //{
            //    new Order { Id = 1, Code = "b103", Name = "小圓麵包", Price = 12.99m },
            //    new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
            //    new Order { Id = 3, Code = "b104", Name = "圓麵包", Price = 7.99m }
            //},
            //["2"] = new List<Order>
            
            //{
            //    new Order { Id = 1, Code = "b103", Name = "小圓麵包", Price = 12.99m },
            //    new Order {Id = 2, Code = "b101", Name = "小麵包", Price = 12.99m}
            //},
            //["3"] = new List<Order>
            //{
            //    new Order { Id = 1, Code = "b101", Name = "小麵包", Price = 12.99m },
            //    new Order { Id = 2, Code = "b102", Name = "三峽金牛角", Price = 4.99m },
            //    new Order { Id = 3, Code = "b103", Name = "小圓麵包", Price = 12.99m }
            //}
            #endregion
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
