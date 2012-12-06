using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace Juke_Mobile_Model
{
    /// <summary>
    /// Helper class to get MP3 Information from ID3 Tags
    /// Implemented as singleton.
    /// </summary>
    public sealed class MP3Analysis
    {
        private static volatile MP3Analysis instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Prevents a default instance of the <see cref="MP3Analysis" /> class from being created.
        /// </summary>
        private MP3Analysis() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static MP3Analysis Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MP3Analysis();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <param name="mp3File">The MP3 file.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public MusicInfo GetInfo(System.IO.FileInfo mp3File)
        {
            MusicInfo musicInfo = new MusicInfo();

            try
            {
                File mp3 = File.Create(mp3File.FullName);
                musicInfo.Album = mp3.Tag.Album;
                musicInfo.Artist = mp3.Tag.Performers[0];
                musicInfo.Title = mp3.Tag.Title;
                musicInfo.PhysicalPath = mp3File.FullName;
            }
            catch (UnsupportedFormatException e)
            {
                throw new System.IO.FileNotFoundException(e.Message);
            }

            return musicInfo;
        }
    }
}
