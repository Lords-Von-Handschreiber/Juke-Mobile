using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.ServiceModel;
using Juke_Mobile_Gui.Properties;

namespace Juke_Mobile_Gui
{
    public static class HttpSelfHostConfigurationFactory
    {
        public static HttpSelfHostConfiguration CreateInstance()
        {
            var cfg = new HttpSelfHostConfiguration(Settings.Default.ServerURLAndPort);

            cfg.MaxReceivedMessageSize = 16L * 1024 * 1024 * 1024;
            cfg.TransferMode = TransferMode.StreamedRequest;
            cfg.ReceiveTimeout = TimeSpan.FromMinutes(20);

            cfg.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            cfg.Routes.MapHttpRoute(
                "Default", "{*res}",
                new { controller = "StaticFile", res = RouteParameter.Optional });

            return cfg;
        }
    }
}
