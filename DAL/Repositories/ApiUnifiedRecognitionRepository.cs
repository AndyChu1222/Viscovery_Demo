using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using ViscoveryDemo.BLL.Models;
using ViscoveryDemo.Properties;
using Newtonsoft.Json;

namespace ViscoveryDemo.DAL.Repositories
{
    public class ApiUnifiedRecognitionRepository : IUnifiedRecognitionRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url = Settings.Default.UnifiedRecognitionUrl;

        public ResponseAPIModel<UnifiedRecognitionData> UnifiedRecognition(string orderType)
        {
            var requestBody = new
            {
                switch_to_visagent = true, // 重點，讓 VisAgent 畫面跳出
                response_fields = new string[] { "combo" } // 可選要回傳的資料
            };

            // 封裝成 JSON
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var requestUrl = string.IsNullOrEmpty(orderType) ? _url : $"{_url}?orderType={orderType}";
            var httpContent = jsonContent;
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

