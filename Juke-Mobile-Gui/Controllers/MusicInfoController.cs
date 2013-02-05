using Juke_Mobile_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Gui.Controllers
{
    public class MusicInfoController : RavenController
    {
        // GET api/<controller>
        public dynamic Get()
        {
            var mis = Db.Query<MusicInfo>();           

            return new
            {
                sEcho = 1,
                iTotalRecords = mis.Count(),
                iTotalDisplayRecords = mis.Count(),
                aaData = mis
            };
        }
    }
}
