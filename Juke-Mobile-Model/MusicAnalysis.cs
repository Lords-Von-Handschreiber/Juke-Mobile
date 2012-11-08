using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public interface MusicAnalysis
    {
        MusicInfo GetInfo(FileInfo mp3File);
    }
}
