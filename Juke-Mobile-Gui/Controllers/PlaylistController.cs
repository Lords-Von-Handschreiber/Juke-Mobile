﻿using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Juke_Mobile_Gui.Controllers
{
    public class PlaylistController : RavenController
    {
        // GET api/<controller>
        public dynamic Get()
        {
            var pl = PlayRequestManager.GetPlayList(PlayRequest.PlayRequestTypeEnum.Queue);
            var aaData = pl.Skip(1).Select(pr => new DTPlayRequest(pr));

            return new
            {
                sEcho = 1,
                iTotalRecords = aaData.Count(),
                iTotalDisplayRecords = aaData.Count(),
                aaData = aaData
            };
        }

        public void Vote(JObject jsonData)
        {
            dynamic json = jsonData;
            string idMusicInfo = json.idMusicInfo;
            string userName = json.userName;
            MusicInfo infotemp = Db.Load<MusicInfo>(idMusicInfo);
            if (infotemp != null)
            {
                PlayRequest req = new PlayRequest()
                {
                    MusicInfo = infotemp,
                    RequestDateTime = DateTime.Today,
                    Username = userName,
                    PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue
                };

                PlayRequestManager.SavePlayRequest(req);
            }
        }
    }
}