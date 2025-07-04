using System.Collections.Generic;
using System.Linq;

namespace ViscoveryDemo.BLL.Models
{
    public static class OrderBuilder
    {
        public static List<Order> GetAllProduct()
        {
            var allItemList = new List<Order>
            {
                new Order { Id = 1, Code = "2025052601", Name = "華堡", Price = 144m },
                new Order { Id = 2, Code = "2025052607", Name = "小華堡", Price = 119m },
                new Order { Id = 3, Code = "2025052605", Name = "中杯可樂", Price = 38m },
                new Order { Id = 4, Code = "2025070201", Name = "雙起士牛堡", Price = 99m },
                new Order { Id = 5, Code = "2025070204", Name = "中杯微糖紅茶", Price = 38m },
                new Order { Id = 6, Code = "2025052603", Name = "中薯條", Price = 59m },
                new Order { Id = 7, Code = "2025070203", Name = "雞塊", Price = 75m },
                new Order { Id = 8, Code = "2025070202", Name = "華鱈魚堡", Price = 74m }
            };

            return allItemList;
        }
        private static readonly Dictionary<string, List<Order>> _menus = new Dictionary<string, List<Order>>()
        {
            ["WhopperCombo"] = new List<Order>
            {
                new Order { Id = 1, Code = "2025052601", Name = "華堡", Price = 144m },
                new Order { Id = 7, Code = "2025070203", Name = "雞塊", Price = 75m },
                new Order { Id = 3, Code = "2025052605", Name = "中杯可樂", Price = 38m }
            },
            ["WhopperJuniorCombo"] = new List<Order>
            {
                new Order { Id = 2, Code = "2025052607", Name = "小華堡", Price = 119m },
                new Order { Id = 6, Code = "2025052603", Name = "中薯條", Price = 59m },
                new Order { Id = 5, Code = "2025070204", Name = "中杯微糖紅茶", Price = 38m }
            },
            ["FishBurgerCombo"] = new List<Order>
            {
                new Order { Id = 8, Code = "2025070202", Name = "華鱈魚堡", Price = 74m },
                new Order { Id = 5, Code = "2025070204", Name = "中杯微糖紅茶", Price = 38m },
                new Order { Id = 7, Code = "2025070203", Name = "雞塊", Price = 75m },
            },
            ["DoubleBurgerCombo"] = new List<Order>
            {
                new Order { Id = 8, Code = "2025070202", Name = "華鱈魚堡", Price = 74m },
                new Order { Id = 4, Code = "2025070201", Name = "雙起士牛堡", Price = 99m },
                new Order { Id = 7, Code = "2025070203", Name = "雞塊", Price = 75m },
                new Order { Id = 6, Code = "2025052603", Name = "中薯條", Price = 59m },
                new Order { Id = 5, Code = "2025070204", Name = "中杯微糖紅茶", Price = 38m },
                new Order { Id = 3, Code = "2025052605", Name = "中杯可樂", Price = 38m }
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
