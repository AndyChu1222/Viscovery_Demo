using System.Collections.Generic;
using System.Linq;
using ViscoveryDemo.DAL.Repositories;
using ViscoveryDemo.BLL.Models;
using System.Windows.Controls;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Order>> LoadOrdersAsync(string orderType)
        {
            // VisAgent 跳出並等待辨識結果
            var orders = _repository.GetOrders(orderType).Select(o => new Order
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Price = o.Price
            }).ToList();
            //因發現會需要等待使用者按下按鈕，若超過時間會Timeout，故暫時不使用await
            var response =  _recognitionRepository.UnifiedRecognition(orderType);
            var recognized = response?.Data?.Order?.Plates?
                .SelectMany(p => p.Instances)
                .Select(i => i.Product.ProductCode)
                .ToList() ?? new List<string>();

            foreach (var order in orders)
            {
                order.Status = recognized.Contains(order.Code) ? OrderStatus.Confirm : OrderStatus.Undetected;
            }

            return orders;
        }
    }
}
