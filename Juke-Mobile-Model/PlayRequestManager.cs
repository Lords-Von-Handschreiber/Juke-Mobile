using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;
using System.Linq;
using Juke_Mobile_Model.Database;

namespace Juke_Mobile_Model
{
    public class PlayRequestManager
    {
        public void savePlayRequest(PlayRequest playRequest)
        {
            Db.Instance.Store(playRequest);
            Db.Instance.SaveChanges();
        }

        public PlayRequest getNextRequest(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            PlayRequest playRequest = Db.Instance.Query<PlayRequest>()
           .Where(pr => pr.PlayRequestType == PlayRequestType)
           .First<PlayRequest>();

            return playRequest;
        }

        public PlayRequest getNextRequest()
        {
            PlayRequest playRequest = Db.Instance.Query<PlayRequest>()
           .Where(pr => pr.PlayRequestType == PlayRequest.PlayRequestTypeEnum.Queue)
           .First<PlayRequest>();

            return playRequest;
        }

        public List<PlayRequest> getPlayList(PlayRequest.PlayRequestTypeEnum PlayRequestType)
        {
            List<PlayRequest> playRequestList = Db.Instance.Query<PlayRequest>()
          .Where(pr => pr.PlayRequestType == PlayRequest.PlayRequestTypeEnum.Queue).ToList<PlayRequest>();


            return playRequestList;
        }
    }
}
