using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Juke_Mobile_Gui.Controllers
{
    public class CurrentTrackController : RavenController
    {
        // GET api/<controller>
        public MusicInfo Get()
        {
            return Db.Query<MusicInfo>().First();
        }
    }
}