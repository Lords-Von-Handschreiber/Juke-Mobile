using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace Juke_Mobile_Core
{
    public class MusicInfoImporterNoDelete: IMusicInfoImporter
    {
        /// <summary>
        /// array that contains all supported file extensions.
        /// </summary>
        private string[] supportedExtensions = new string[] { "mp3", "mp4", "m4a", "mp1", "mp2", "m3a", "m2a", "wma", "wav" };
        /// <summary>
        /// Imports a whole folder. Be aware only first level of folder will be added.
        /// </summary>
        /// <param name="directory"></param>
        public void ImportFolder(DirectoryInfo directory)
        {
            foreach (FileInfo finfo in directory.GetFiles())
            {
                ImportMusic(finfo);
            }
        }

        /// <summary>
        /// Imports a single file, after check wether file extension is supported
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public MusicInfo ImportMusic(FileInfo file)
        {
            MusicInfo info = null;
            if (file.Exists && SupportedMimeType.AllExtensions.Contains(file.Extension.Substring(1)) && supportedExtensions.Contains(file.Extension.Substring(1)))
            {
                info = MP3Analysis.Instance.GetInfo(file);
                using (var dbsession = Db.Instance.OpenSession())
                {
                    var result = dbsession.Query<MusicInfo>().Where(m => m.Title.Equals(info.Title) && m.Artist.Equals(info.Artist) && m.Album.Equals(info.Album)).SingleOrDefault();
                    if (result == null)
                    {
                        dbsession.Store(info);
                        dbsession.SaveChanges();
                    }
                    else
                    {
                        info = result;
                    }
                }
            }
            return info;
        }
    }
}
