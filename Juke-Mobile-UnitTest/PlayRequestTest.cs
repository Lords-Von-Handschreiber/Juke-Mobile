using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Juke_Mobile_Model;
using System.Collections.Generic;

namespace Juke_Mobile_UnitTest
{
    [TestClass]
    public class PlayRequestTest
    {
        /// <summary>
        /// Tests the first in first out.
        /// </summary>
        [TestMethod]
        public void TestFirstInFirstOut()
        {
            PlayRequest pr1 = new PlayRequest()
            {
                MusicInfo = new MusicInfo()
                {
                    Title = "Super Song1"
                },
                PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue,
                RequestDateTime = DateTime.Now,
                Username = "Chris"
            };

            PlayRequest pr2 = new PlayRequest()
            {
                MusicInfo = new MusicInfo()
                {
                    Title = "Super Song2"
                },
                PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue,
                RequestDateTime = DateTime.Now,
                Username = "Chris"
            };

            PlayRequestManager.SavePlayRequest(pr1);
            PlayRequestManager.SavePlayRequest(pr2);

            StringAssert.Equals(PlayRequestManager.GetNextRequest(PlayRequest.PlayRequestTypeEnum.Queue), pr1);
        }

        /// <summary>
        /// Tests the list.
        /// </summary>
        [TestMethod]
        public void TestList()
        {
            PlayRequest pr1 = new PlayRequest()
            {
                MusicInfo = new MusicInfo()
                {
                    Title = "Super Song1"
                },
                PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue,
                RequestDateTime = DateTime.Now,
                Username = "Chris"
            };

            PlayRequest pr2 = new PlayRequest()
            {
                MusicInfo = new MusicInfo()
                {
                    Title = "Super Song2"
                },
                PlayRequestType = PlayRequest.PlayRequestTypeEnum.Queue,
                RequestDateTime = DateTime.Now,
                Username = "Chris"
            };

            PlayRequestManager.SavePlayRequest(pr1);
            PlayRequestManager.SavePlayRequest(pr2);

            List<PlayRequest> playRequests = PlayRequestManager.GetPlayList(PlayRequest.PlayRequestTypeEnum.Queue);

            StringAssert.Equals(playRequests[0], pr1);
            StringAssert.Equals(playRequests[1], pr2);
        }
    }
}
