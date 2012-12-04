using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    /// <summary>
    /// Data Contract for Information read from the ID3 Tags.
    /// </summary>
    public class MusicInfo
    {
        public string Id { get; set; }
        public String Artist{ get; set;}
        public String Album { get; set; }
        public String Title { get; set; }
        public String PhysicalPath { get; set; }

        public override string ToString()
        {
            return Artist + ", " + Title + ", " + Album;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (!(obj is MusicInfo))
                return false;
            MusicInfo mp3obj = (MusicInfo)obj;
            if (!string.IsNullOrEmpty(this.Id) && !string.IsNullOrEmpty(mp3obj.Id) && mp3obj.Id.Equals(this.Id))
            {
                return true;
            }
            if (mp3obj.Artist.Equals(this.Artist) && mp3obj.Album.Equals(this.Album) && mp3obj.Title.Equals(this.Title))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Album.GetHashCode() + this.Artist.GetHashCode() + this.Id.GetHashCode() + this.Title.GetHashCode();
        }
    }
}
