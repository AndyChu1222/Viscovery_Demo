using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using ViscoveryDemo.BLL.Models;
using ViscoveryDemo.Properties;

namespace ViscoveryDemo.DAL.Repositories
{
    public class ApiUnifiedRecognitionRepository : IUnifiedRecognitionRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url = Settings.Default.UnifiedRecognitionUrl;

        public ResponseAPIModel<UnifiedRecognitionData> UnifiedRecognition(string orderType)
        {
            var requestUrl = string.IsNullOrEmpty(orderType) ? _url : $"{_url}?orderType={orderType}";
            var httpContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(requestUrl, httpContent).Result;
            response.EnsureSuccessStatusCode();

            var serializer = new DataContractJsonSerializer(typeof(ResponseAPIModel<UnifiedRecognitionData>));
            using (var stream = response.Content.ReadAsStreamAsync().Result)
            {
                return (ResponseAPIModel<UnifiedRecognitionData>)serializer.ReadObject(stream);
            }
        }
    }
}

