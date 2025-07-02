using System.Collections.Generic;
using System.Linq;
using ViscoveryDemo.DAL.Repositories;
using ViscoveryDemo.BLL.Models;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;

namespace ViscoveryDemo.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnifiedRecognitionRepository _recognitionRepository;
        private List<Product> _apiList;
        private List<Order> _orderList;
        public OrderService(IOrderRepository repository, IUnifiedRecognitionRepository recognitionRepository)
        {
            _repository = repository;
            _recognitionRepository = recognitionRepository;
        }

        public async Task<IEnumerable<Order>> LoadOrdersAsync(string orderType)
        {
            var currentOrders = _repository.GetOrders(orderType).Select(o => new Order
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Price = o.Price
            }).ToList();
            //MessageBox.Show("取得currentOrders成功 該套餐項目數量:"+currentOrders.Count().ToString());


            //因發現會需要等待使用者按下按鈕，若超過時間會Timeout，故暫時不使用await
            var response = _recognitionRepository.UnifiedRecognition(orderType);

            _apiList = response?.Data?.Order?.Plates?
    .SelectMany(p => p.Instances)
    .Select(i => i.Product)
    .ToList() ?? new List<Product>();

            var apiReturnModel = ApiConvertToOrder(_apiList); //TODO 看顯示哪個model

            _orderList = currentOrders ?? new List<Order>();

            return UpdateOrders(apiReturnModel, currentOrders);
        }

        private List<Order> ApiConvertToOrder(List<Product> recognized)
        {
            List<Order> apiConvertModel = new List<Order>();
            foreach (var item in recognized)
            {
                Order convertModel = _repository.GetOrder(item.ProductCode);
                //一開始預設都是Undetected
                if (convertModel.Status == OrderStatus.Undetected)
                    convertModel.Name = item.ProductName; //DB 沒有的，就拿API的名字
                apiConvertModel.Add(convertModel);
            }
            return apiConvertModel;
        }
        private List<Order> UpdateOrders(List<Order> apiReturnModel, List<Order> currentOrders)
        {
            // 建立快速查詢的 HashSet
            var currentOrderCodes = new HashSet<string>(currentOrders.Select(o => o.Code));
            var apiReturnCodes = new HashSet<string>(apiReturnModel.Select(o => o.Code));

            var result = new List<Order>();

            // 1. 處理 apiReturnModel 資料
            foreach (var order in apiReturnModel)
            {
                if (currentOrderCodes.Contains(order.Code))
                {
                    // 雙方都有，設為 Confirm
                    order.Status = OrderStatus.Confirm;
                }
                else
                {
                    // apiReturnModel 有，但 currentOrders 沒有，設為 UnMatch
                    order.Status = OrderStatus.UnMatch;
                }

                // 加入結果
                result.Add(order);
            }

            // 2. 處理 currentOrders 資料（找出 apiReturnModel 沒有的）
            var missingOrders = currentOrders
                .Where(o => !apiReturnCodes.Contains(o.Code))
                .ToList();

            foreach (var missingOrder in missingOrders)
            {
                // currentOrders 有，但 apiReturnModel 沒有，設為 Undetected
                missingOrder.Status = OrderStatus.Undetected;
                result.Add(missingOrder);
            }

            // 3. 將結果回填到 apiReturnModel
            apiReturnModel.Clear();
            apiReturnModel.AddRange(result);
            return apiReturnModel;
        }
    }
}
