using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public class DTPlayRequest
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Juker { get; set; }

        public DTPlayRequest(PlayRequest pr)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(pr.MusicInfo.Artist))
                sb.Append(pr.MusicInfo.Artist);
            if (!string.IsNullOrEmpty(pr.MusicInfo.Artist) && !string.IsNullOrEmpty(pr.MusicInfo.Album))
                sb.Append(" (" + pr.MusicInfo.Album + ")");
            else if (!string.IsNullOrEmpty(pr.MusicInfo.Album))
                sb.Append(pr.MusicInfo.Album);

            Artist = sb.ToString(); // pr.MusicInfo.Artist + " (" + pr.MusicInfo.Album + ")".Replace("()", "");
            Title = pr.MusicInfo.Title;
            Juker = pr.Username;
        }
    }
}
