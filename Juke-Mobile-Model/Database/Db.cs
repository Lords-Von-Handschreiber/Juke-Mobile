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
    public sealed class Db
    {
        private static volatile IDocumentSession instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Prevents a default instance of the <see cref="Db" /> class from being created.
        /// </summary>
        private Db() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static IDocumentSession Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = DbDocumentStore.Instance.OpenSession();
                        }
                    }
                }

                return instance;
            }
        }

    }
}
