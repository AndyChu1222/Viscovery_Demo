using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ViscoveryDemo.BLL.Models
{
    [DataContract]
    public class UnifiedRecognitionData
    {
        [DataMember(Name = "order")]
        public RecognitionOrder Order { get; set; }
    }

    [DataContract]
    public class RecognitionOrder
    {
        [DataMember(Name = "plates")]
        public List<Plate> Plates { get; set; }
    }

    [DataContract]
    public class Plate
    {
        [DataMember(Name = "instances")]
        public List<Instance> Instances { get; set; }
    }

    [DataContract]
    public class Instance
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }

    [DataContract]
    public class Product
    {
        [DataMember(Name = "product_name")]
        public string ProductName { get; set; }

        [DataMember(Name = "product_code")]
        public string ProductCode { get; set; }
    }
}
