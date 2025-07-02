using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using ViscoveryDemo.BLL.Models;
using ViscoveryDemo.Properties;
using Newtonsoft.Json;
using System.Windows;
using System.Linq;

namespace ViscoveryDemo.DAL.Repositories
{
    public class ApiUnifiedRecognitionRepository : IUnifiedRecognitionRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url = Settings.Default.UnifiedRecognitionUrl;

        public ResponseAPIModel<UnifiedRecognitionData> UnifiedRecognition(string orderType)
        {
            try
            {
                var requestBody = new
                {
                    switch_to_visagent = true, // 重點，讓 VisAgent 畫面跳出
                };

                // 封裝成 JSON
                StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var requestUrl = string.IsNullOrEmpty(orderType) ? _url : $"{_url}?orderType={orderType}";
                //MessageBox.Show(requestUrl);
                var httpContent = jsonContent;
                var response = _httpClient.PostAsync(requestUrl, httpContent).Result;
                //MessageBox.Show(response.Content.ToString());
                response.EnsureSuccessStatusCode();

                var serializer = new DataContractJsonSerializer(typeof(ResponseAPIModel<UnifiedRecognitionData>));
                using (var stream = response.Content.ReadAsStreamAsync().Result)
                {
                    //MessageBox.Show("api success 準備回傳");
                    return (ResponseAPIModel<UnifiedRecognitionData>)serializer.ReadObject(stream);
                }
            }
            catch (HttpRequestException ex)
            {
                // 這裡處理網路連線錯誤
                // 可以回傳 null 或拋出自定義例外
                MessageBox.Show("連線失敗，請確認API是否啟動。錯誤訊息：" + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                string errorMessage;

                if (ex is AggregateException aggEx)
                {
                    // 展開 AggregateException 取出第一個錯誤訊息
                    errorMessage = aggEx.InnerExceptions.FirstOrDefault()?.Message ?? "未知錯誤";
                }
                else if (ex.InnerException != null)
                {
                    // 取出巢狀錯誤
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show("發生錯誤：" + errorMessage);
                return null;
            }

        }
    }
}

