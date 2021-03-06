﻿using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Juke_Mobile_Model.Database
{
    /// <summary>
    /// Singleton and Static Subject Class
    /// </summary>
    public sealed class Db
    {
        private static volatile IDocumentStore instance;
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
                            instance = DbDocumentStore.Instance; //.OpenSession();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
