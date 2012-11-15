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

        private Db() { }

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
