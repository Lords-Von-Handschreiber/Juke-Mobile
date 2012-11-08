using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.IO;
using System.Collections.ObjectModel;

namespace Juke_Mobile_Gui.Controllers
{
    public class FileUploadController : ApiController
    {        
        [HttpPost]
        public async Task UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(Settings.Default.ServerUploadPath);
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            foreach (MultipartFileData data in streamProvider.FileData)
            {
                string strFileName = data.Headers.ContentDisposition.FileName.Trim('"');
                File.Move(data.LocalFileName, Settings.Default.ServerUploadPath + "\\" + strFileName);
                //TODO update database with ID3 Info
            }                        
        }
    }
}