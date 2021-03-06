﻿using System;
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
using Juke_Mobile_Gui.Properties;
using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using Juke_Mobile_Core;

namespace Juke_Mobile_Gui.Controllers
{
    /// <summary>
    /// Controller that handles fileuploads.
    /// Multiple files possible
    /// Because files get uploaded with GUID as name we have to rename them.
    /// After renaming the file information is to be stored in the database.
    /// </summary>
    public class FileUploadController : RavenController
    {
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [HttpPost]
        public async Task UploadWithUserName(string id)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(Settings.Default.ServerUploadPath);
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            MusicInfoImporter importer = new MusicInfoImporter();
            foreach (MultipartFileData data in streamProvider.FileData)
            {
                string strFileName = data.Headers.ContentDisposition.FileName.Trim('"');
                string strNewFileFullName = Settings.Default.ServerUploadPath + "\\" + Guid.NewGuid() + "_" + strFileName;
                File.Move(data.LocalFileName, strNewFileFullName);
                FileInfo fi = new FileInfo(strNewFileFullName);
                MusicInfo info = importer.ImportMusic(fi);
                if (info != null)
                {
                    PlayRequest req = new PlayRequest()
                    {
                        MusicInfo = info,
                        RequestDateTime = DateTime.Today,
                        Username = id,
                        PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue
                    };
                    PlayRequestManager.SavePlayRequest(req);
                }                
            }
        }        
    }
}