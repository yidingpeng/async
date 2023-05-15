using Microsoft.Extensions.DependencyInjection;
using RW.Position.WinForm.Extensions;
using RW.Position.websocketServers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RW.Position.Winform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var servicesPrvider = ConfigurationService.Injection();
            Application.Run(servicesPrvider.GetService<SocketClient>());
            
        }
    }
}
