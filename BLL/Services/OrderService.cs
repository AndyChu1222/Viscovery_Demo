using System.Collections.Generic;
using System.Linq;
using ViscoveryDemo.DAL.Repositories;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnifiedRecognitionRepository _recognitionRepository;

        public OrderService(IOrderRepository repository, IUnifiedRecognitionRepository recognitionRepository)
        {
            _repository = repository;
            _recognitionRepository = recognitionRepository;
        }

        public IEnumerable<Order> LoadOrders(string orderType)
        {
            var orders = _repository.GetOrders(orderType).Select(o => new Order
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Price = o.Price
            }).ToList();

            //呼叫並檢核API是否符合當前的商品
            var response = _recognitionRepository.UnifiedRecognition(orderType);
            var recognized = response.Data?.Order?.Plates?
                .SelectMany(p => p.Instances)
                .Select(i => i.Product.ProductCode)
                .ToList() ?? new List<string>();

            //檢核商品是否符合條件
            foreach (var order in orders)
            {
                order.Status = recognized.Contains(order.Code) ? OrderStatus.Confirm : OrderStatus.Undetected;
            }

            return orders;
        }
    }
}
