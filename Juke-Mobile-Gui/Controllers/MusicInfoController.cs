using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Juke_Mobile_Gui.Controllers
{
    public class MusicInfoController: ApiController
    {
        public void Vote(string res)
        {
            Db.Request(res);
        }

        public IEnumerable<MusicInfo> Get()
        {
            IEnumerable<MusicInfo> allItems = Db.Instance.Query<MusicInfo>();
            return allItems;
        }

        public MusicInfo Get(string res)
        {
            return Db.Instance.Load<MusicInfo>(res);
        }
    }
}
