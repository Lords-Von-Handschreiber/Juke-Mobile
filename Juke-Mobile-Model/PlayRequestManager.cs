using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;
using Juke_Mobile_Model.Database;

namespace Juke_Mobile_Model
{
    public static class PlayRequestManager
    {
        private static List<IPlayRequestReceiver> _receivers = new List<IPlayRequestReceiver>();

        /// <summary>
        /// Attaches the specified receiver.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        public static void Attach(IPlayRequestReceiver receiver)
        {
            _receivers.Add(receiver);
        }

        /// <summary>
        /// Detaches the specified receiver.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        public static void Detach(IPlayRequestReceiver receiver)
        {
            _receivers.Remove(receiver);
        }

        /// <summary>
        /// Saves the play request.
        /// </summary>
        /// <param name="playRequest">The play request.</param>
        public static void SavePlayRequest(PlayRequest playRequest)
        {
            var dbSession = Db.Instance.OpenSession();
            dbSession.Store(playRequest);
            dbSession.SaveChanges();
            dbSession.Dispose();
            foreach (IPlayRequestReceiver receiver in _receivers)
            {
                receiver.Update(playRequest.Id);
            }
        }

        /// <summary>
        /// Gets the next request.
        /// </summary>
        /// <param name="PlayRequestType">Type of the play request.</param>
        /// <returns></returns>
        public static PlayRequest GetNextRequest(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            var dbSession = Db.Instance.OpenSession();
            PlayRequest playRequest = dbSession.Query<PlayRequest>()
           .Where(pr => pr.PlayRequestType == PlayRequestType)
           .OrderBy(pr => pr.RequestDateTime)
           .FirstOrDefault<PlayRequest>();

            dbSession.Dispose();

            return playRequest;
        }

        /// <summary>
        /// Gets the play list.
        /// </summary>
        /// <param name="PlayRequestType">Type of the play request.</param>
        /// <returns></returns>
        public static List<PlayRequest> GetPlayList(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            var dbSession = Db.Instance.OpenSession();
            List<PlayRequest> playRequestList = dbSession.Query<PlayRequest>()
                .Where(pr => pr.PlayRequestType == PlayRequest.PlayRequestTypeEnum.Queue).ToList<PlayRequest>();
            dbSession.Dispose();
            return playRequestList;
        }
    }
}
