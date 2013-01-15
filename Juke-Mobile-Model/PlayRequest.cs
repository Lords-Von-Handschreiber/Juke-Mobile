using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Juke_Mobile_Model
{
    public class PlayRequest
    {
        public enum PlayRequestTypeEnum { Queue, History };

        public string Id { get; set; }
        public MusicInfo MusicInfo { get; set; }
        public DateTime RequestDateTime { get; set; }
        public String Username { get; set; }
        public PlayRequestTypeEnum PlayRequestType { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return MusicInfo.Title + " gewünscht von " + Username;
        }
    }
}
