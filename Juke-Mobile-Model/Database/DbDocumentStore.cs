using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model.Database
{
    public sealed class DbDocumentStore
    {
        private static volatile IDocumentStore instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Prevents a default instance of the <see cref="DbDocumentStore" /> class from being created.
        /// </summary>
        private DbDocumentStore() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static IDocumentStore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EmbeddableDocumentStore { DataDirectory = new FileInfo("db/").DirectoryName };
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
