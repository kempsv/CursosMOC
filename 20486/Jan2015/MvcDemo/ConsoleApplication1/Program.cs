using MvcDemo.Framework.Wcf.ServiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new ProxyClientManagement.ClientManagementServiceClient()) 
            {
                var r = service.FindAll();
            }

            var svc = ServiceLocator.GetInstance<MvcDemo.Services.ClientManagement.IClientManagementService>();

            var r1 = svc.FindAll();

        }
    }
}
