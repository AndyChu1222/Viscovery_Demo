using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViscoveryDemo.BLL.Services
{
    public interface IShowViscoveryService
    {
        /// <summary>
        /// StartUp的時候自動開啟VisAgent
        /// </summary>
        void StartVisAgent();
    }
}
