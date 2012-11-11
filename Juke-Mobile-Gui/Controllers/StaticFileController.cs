using Juke_Mobile_Gui.Properties;
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
        private Dictionary<string, string> mimeTypes = new Dictionary<string, string>(){
            { ".htm", "text/html"},
            { ".html", "text/html"},
            { ".js", "application/javascript"},
            { ".css", "text/css"},
            { ".less", "text/css"},
            { ".png", "image/png"},
            { ".jpg", "image/jpeg"},
            { ".jpeg", "image/jpeg"},
            { ".gif", "image/gif"}
        };

        public HttpResponseMessage Get(string res = "index.html")
        {
            var basePath = new FileInfo(Settings.Default.ServerClientPath);
            var file = new FileInfo(basePath.FullName + "/" + res);
            if (!file.Exists)
                file = new FileInfo(basePath.FullName + "/error.html");

            var content = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(file.OpenRead())
            };
            content.Content.Headers.Add("Content-Type", GetMimeType(file.Extension));
            return content;
        }


        private string GetMimeType(string ext)
        {
            if (mimeTypes.ContainsKey(ext))
            {
                return mimeTypes[ext];
            }
            else
            {
                return "unknown/unknown";
            }
        }
    }
}