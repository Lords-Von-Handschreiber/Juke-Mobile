using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Juke_Mobile_Gui.Controllers
{
    public class StaticFileController : ApiController
    {
        public HttpResponseMessage Get(string res = "index.html")
        {
            var file = new FileInfo("Juke-Mobile-Client/" + res);
            if (!file.Exists)
                file = new FileInfo("Juke-Mobile-Client/error.html");

            var content = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(file.OpenRead())
            };
            return content;
        }
    }
}