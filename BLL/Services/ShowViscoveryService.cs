using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViscoveryDemo.BLL.Services
{
    public class ShowViscoveryService : IShowViscoveryService
    {
        private Process _visAgentProcess;
        public async Task StartVisAgent()
        {
            try
            {
                if (_visAgentProcess != null && !_visAgentProcess.HasExited)
                {
                    // 已經啟動，不需重複
                    return;
                }

                //目前寫死執行檔路徑
                var visAgentPath = @"C:\Program Files (x86)\VisAgent2\VisAgent.exe";

                var startInfo = new ProcessStartInfo
                {
                    FileName = visAgentPath,
                    UseShellExecute = true,
                    CreateNoWindow = true, // 不顯示 console 視窗
                    WindowStyle = ProcessWindowStyle.Minimized // 啟動時最小化
                };

                _visAgentProcess = Process.Start(startInfo);
                var isReady = await WaitForVisAgentReadyAsync();
                if (!isReady)
                {
                    MessageBox.Show("VisAgent 啟動逾時，請確認是否成功啟動。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async Task<bool> WaitForVisAgentReadyAsync(int timeoutSeconds = 10)
        {
            var timeout = DateTime.Now.AddSeconds(timeoutSeconds);
            using (var client = new HttpClient())
            {
                while (DateTime.Now < timeout)
                {
                    try
                    {
                        // 健康檢查 API（建議問 VisAgent 是否有支援）
                        var response = await client.GetAsync("http://127.0.0.1:1688/api/v2/health");
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        // 尚未啟動，不處理
                    }

                    await Task.Delay(500); // 每 0.5 秒重試
                }
            }
            return false; // Timeout
        }
    }
}
