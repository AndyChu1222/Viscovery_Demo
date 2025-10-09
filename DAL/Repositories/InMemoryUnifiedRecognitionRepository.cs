using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViscoveryDemo.BLL.Models;

namespace ViscoveryDemo.DAL.Repositories
{
    public class InMemoryUnifiedRecognitionRepository : IUnifiedRecognitionRepository
    {
        public ResponseAPIModel<UnifiedRecognitionData> UnifiedRecognition(string orderType)
        {
            var instances = GetInstances(orderType);
            var data = new UnifiedRecognitionData
            {
                Order = new RecognitionOrder
                {
                    Plates = new List<Plate>
                    {
                        new Plate { Instances = instances }
                    }
                }
            };

            return new ResponseAPIModel<UnifiedRecognitionData>
            {
                Code = "200",
                Message = "Success",
                IsSuccess = true,
                Data = data
            };
        }
        /// <summary>
        /// Inmemory的答案，模擬API辨識結果
        /// 可調整回傳答案，產生三種辨識結果
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private List<Instance> GetInstances(string orderType)
        {
            var all = new List<Instance>
            {
                new Instance { Product = new Product { ProductName = "圓麵包", ProductCode = "b104" } },
                new Instance { Product = new Product { ProductName = "三峽金牛角",   ProductCode = "b102" } },
                new Instance { Product = new Product { ProductName = "小圓麵包",     ProductCode = "b103" } }
            };

            switch (orderType)
            {
                case "1":
                    return all;
                case "2":
                    return new List<Instance>();
                case "3":
                    return all.Take(2).ToList();
                default:
                    return new List<Instance>();
            }
        }
    }
}
