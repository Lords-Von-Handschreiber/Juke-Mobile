using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Juke_Mobile_Model;

namespace Juke_Mobile_UnitTest
{
    [TestClass]
    public class MusicAnalysisTest
    {
        /*
         *  Für den Test befindet sich das Lied ABBA Thank you 4 the Musik im Testordner
         */
        public const string filepfad = "18 Thank You For The Music.mp3";
        public const string album = "ABBA Gold (Greatest Hits)";
        public const string title = "Thank You For The Music";
        public const string artist = "ABBA";

        /// <summary>
        /// Files the not found.
        /// </summary>
        [TestMethod]
        public void FileNotFound()
        {
            // act
            try
            {
                MP3Analysis.Instance.GetInfo(new System.IO.FileInfo("Falscher FileName"));
            }
            catch (System.IO.FileNotFoundException e)
            {
                StringAssert.Contains(e.Message, "Falscher FileName");
            }
        }

        /// <summary>
        /// Rights the album.
        /// </summary>
        [TestMethod]
        public void RightAlbum()
        {
            MusicInfo musicInfo = MP3Analysis.Instance.GetInfo(new System.IO.FileInfo(filepfad));
            StringAssert.Equals(musicInfo.Album, album);
        }

        /// <summary>
        /// Rights the title.
        /// </summary>
        [TestMethod]
        public void RightTitle()
        {
            MusicInfo musicInfo = MP3Analysis.Instance.GetInfo(new System.IO.FileInfo(filepfad));
            StringAssert.Equals(musicInfo.Title, title);
        }

        /// <summary>
        /// Rights the artist.
        /// </summary>
        [TestMethod]
        public void RightArtist()
        {
            MusicInfo musicInfo = MP3Analysis.Instance.GetInfo(new System.IO.FileInfo(filepfad));
            StringAssert.Equals(musicInfo.Artist, artist);
        }
    }
}
