﻿using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
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
        public IQueryable<PlayRequest> Get()
        {
            return Db.Query<PlayRequest>();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {            

        }

        public void Vote(string idMusicInfo, string userName)
        {
            
            
            PlayRequest req = new PlayRequest()
            {
                MusicInfo = Db.Load<MusicInfo>(idMusicInfo),
                RequestDateTime = DateTime.Today,
                Username = userName,
                PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue
            };

            PlayRequestManager.SavePlayRequest(req);
        }
    }
}