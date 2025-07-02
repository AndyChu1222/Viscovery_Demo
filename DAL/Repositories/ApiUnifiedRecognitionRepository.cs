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
                    switch_to_visagent = true, // ���I�A�� VisAgent �e�����X
                };

                // �ʸ˦� JSON
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
                    //MessageBox.Show("api success �ǳƦ^��");
                    return (ResponseAPIModel<UnifiedRecognitionData>)serializer.ReadObject(stream);
                }
            }
            catch (HttpRequestException ex)
            {
                // �o�̳B�z�����s�u���~
                // �i�H�^�� null �ΩߥX�۩w�q�ҥ~
                MessageBox.Show("�s�u���ѡA�нT�{API�O�_�ҰʡC���~�T���G" + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                string errorMessage;

                if (ex is AggregateException aggEx)
                {
                    // �i�} AggregateException ���X�Ĥ@�ӿ��~�T��
                    errorMessage = aggEx.InnerExceptions.FirstOrDefault()?.Message ?? "�������~";
                }
                else if (ex.InnerException != null)
                {
                    // ���X�_�����~
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show("�o�Ϳ��~�G" + errorMessage);
                return null;
            }

        }
    }
}

