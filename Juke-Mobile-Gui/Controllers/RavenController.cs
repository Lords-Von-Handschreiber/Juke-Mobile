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

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenController" /> class.
        /// </summary>
        /// <param name="autosave">if set to <c>true</c> [autosave].</param>
        public RavenController(bool autosave = false)
        {
            this.Autosave = autosave;
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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
