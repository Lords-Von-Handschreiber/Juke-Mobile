using Raven.Client;
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
        private static volatile IDocumentSession instance;
        private static object syncRoot = new Object();
        private static List<IDbReceiver> _receivers = new List<IDbReceiver>();

        /// <summary>
        /// Attaches the specified receiver.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        public static void Attach(IDbReceiver receiver)
        {
            _receivers.Add(receiver);
        }

        /// <summary>
        /// Detaches the specified receiver.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        public static void Detach(IDbReceiver receiver)
        {
            _receivers.Remove(receiver);
        }

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

        /// <summary>
        /// Adding items and updating observer state
        /// </summary>
        /// <param name="item"></param>
        public static void AddItemWithUpdate(dynamic item)
        {
            Db.Instance.Store(item);
            Db.instance.SaveChanges();
            if (item is MusicInfo)
            {
                MusicInfo info = item as MusicInfo;
                foreach (IDbReceiver receiver in _receivers)
                {
                    receiver.Update(info.Id);
                }
            }
        }

        public static void Request(string id)
        {
            foreach (IDbReceiver receiver in _receivers)
            {
                receiver.Update(id);
            }
        }
    }
}
