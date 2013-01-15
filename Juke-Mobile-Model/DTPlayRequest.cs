using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public class DTPlaylist
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Juker { get; set; }

        public DTPlaylist(PlayRequest pr)
        {
            Artist = pr.MusicInfo.Artist + " (" + pr.MusicInfo.Album + ")".Trim().Replace("()", "");
            Title = pr.MusicInfo.Title;
            Juker = pr.Username;
        }
    }
}
