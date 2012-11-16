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

        [TestMethod]
        public void FileNotFound()
        {
            // act
            try
            {
                new MP3Analysis().GetInfo(new System.IO.FileInfo("Falscher FileName"));
            }
            catch (System.IO.FileNotFoundException e)
            {
                StringAssert.Contains(e.Message, "Falscher FileName");
            }
        }

        [TestMethod]
        public void rightAlbum()
        {
            MusicInfo musicInfo = new MP3Analysis().GetInfo(new System.IO.FileInfo(filepfad));
            if (!musicInfo.Album.Equals(album))
            {
                throw new ArgumentOutOfRangeException(musicInfo.Album,"Das Album wird nicht richtig ausgelsen");
            }
        }

        [TestMethod]
        public void rightTitle()
        {
            MusicInfo musicInfo = new MP3Analysis().GetInfo(new System.IO.FileInfo(filepfad));
            if (!musicInfo.Title.Equals(title))
            {
                throw new ArgumentOutOfRangeException(musicInfo.Title,"Der Titel wird nicht richtig ausgelsen");
            }
        }

        [TestMethod]
        public void rightArtist()
        {
            MusicInfo musicInfo = new MP3Analysis().GetInfo(new System.IO.FileInfo(filepfad));
            if (!musicInfo.Artist.Equals(artist))
            {
                throw new ArgumentOutOfRangeException(musicInfo.Artist,"Der Kuenstler wird nicht richtig ausgelsen");
            }
        }
    }
}
