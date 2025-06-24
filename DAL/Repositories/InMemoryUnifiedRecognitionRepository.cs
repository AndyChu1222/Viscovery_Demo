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

        private List<Instance> GetInstances(string orderType)
        {
            var all = new List<Instance>
            {
                new Instance { Product = new Product { ProductName = "小麵包", ProductCode = "b101" } },
                new Instance { Product = new Product { ProductName = "三峽金牛角",   ProductCode = "b102" } },
                new Instance { Product = new Product { ProductName = "Soft Drink",     ProductCode = "b103" } }
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
