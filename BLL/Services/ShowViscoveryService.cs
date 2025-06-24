using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViscoveryDemo.BLL.Services
{
    public class ShowViscoveryService : IShowViscoveryService
    {
        private Process _visAgentProcess;
        public void StartVisAgent()
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
                    Arguments = "--input EXTERNAL --output HTTP_REQUEST", // 初始參數（若需要）
                    UseShellExecute = true,
                    CreateNoWindow = true, // 不顯示 console 視窗
                    WindowStyle = ProcessWindowStyle.Minimized // 啟動時最小化
                };

                _visAgentProcess = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
