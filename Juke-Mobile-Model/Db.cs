﻿using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public sealed class Db
    {
        private static volatile EmbeddableDocumentStore instance;
        private static object syncRoot = new Object();

        private Db() { }

        public static EmbeddableDocumentStore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null) {
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
