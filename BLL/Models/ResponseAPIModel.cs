using System.Runtime.Serialization;

namespace ViscoveryDemo.BLL.Models
{
    public interface IResponseModel<T>
    {
        T Data { get; set; }
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }

    [DataContract]
    public class ResponseAPIModel<T> : IResponseModel<T>
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
