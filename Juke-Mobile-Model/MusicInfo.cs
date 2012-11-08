using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public class MusicInfo
    {
        public String Artist{ get; set;}
        public String Album { get; set; }
        public String Title { get; set; }

        public override string ToString()
        {
            return Artist + ", " + Title + ", " + Album;
        }
    }
}
