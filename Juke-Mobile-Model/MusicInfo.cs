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
    }
}
