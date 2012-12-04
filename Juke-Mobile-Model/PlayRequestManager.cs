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
    public class PlayRequestManager
    {
        /// <summary>
        /// Saves the play request.
        /// </summary>
        /// <param name="playRequest">The play request.</param>
        public void SavePlayRequest(PlayRequest playRequest)
        {
            Db.Instance.Store(playRequest);
            Db.Instance.SaveChanges();
        }

        /// <summary>
        /// Gets the next request.
        /// </summary>
        /// <param name="PlayRequestType">Type of the play request.</param>
        /// <returns></returns>
        public PlayRequest GetNextRequest(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            PlayRequest playRequest = Db.Instance.Query<PlayRequest>()
           .Where(pr => pr.PlayRequestType == PlayRequestType)
           .First<PlayRequest>();

            return playRequest;
        }

        /// <summary>
        /// Gets the next request.
        /// </summary>
        /// <returns></returns>
        public PlayRequest GetNextRequest()
        {
            return GetNextRequest(PlayRequest.PlayRequestTypeEnum.Queue);
        }

        /// <summary>
        /// Gets the play list.
        /// </summary>
        /// <param name="PlayRequestType">Type of the play request.</param>
        /// <returns></returns>
        public List<PlayRequest> GetPlayList(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            List<PlayRequest> playRequestList = Db.Instance.Query<PlayRequest>()
          .Where(pr => pr.PlayRequestType == PlayRequest.PlayRequestTypeEnum.Queue).ToList<PlayRequest>();

            return playRequestList;
        }
    }
}
