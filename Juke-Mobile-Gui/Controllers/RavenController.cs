using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Juke_Mobile_Gui.Controllers
{
    public abstract class RavenController : ApiController
    {
        public IDocumentSession Db { get; set; }
        public bool Autosave { get; set; }

        public RavenController(bool autosave = false)
        {
            this.Autosave = autosave;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Db != null)
                {
                    using (Db)
                    {
                        if (this.Autosave)
                            Db.SaveChanges();
                        Db.Dispose();
                        Db = null;
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}
