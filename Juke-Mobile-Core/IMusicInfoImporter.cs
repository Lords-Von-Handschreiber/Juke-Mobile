using Juke_Mobile_Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Core
{
    public interface IMusicInfoImporter
    {
        void ImportFolder(DirectoryInfo directory);
        MusicInfo ImportMusic(FileInfo file);
    }
}
