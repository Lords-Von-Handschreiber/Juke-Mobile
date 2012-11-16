using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace Juke_Mobile_Model
{
    public class MP3Analysis : MusicAnalysis 
    {


        public MusicInfo GetInfo(System.IO.FileInfo mp3File)
        {
           
            MusicInfo musicInfo = new MusicInfo();
            
            try
            {
                File mp3 = File.Create(mp3File.FullName);
                musicInfo.Album = mp3.Tag.Album;
                musicInfo.Artist = mp3.Tag.AlbumArtists[0];
                musicInfo.Title = mp3.Tag.Title;
            }
            catch (UnsupportedFormatException e)
            {
                throw new System.IO.FileNotFoundException(e.Message);
            }

            




            return musicInfo;
        }
    }
}
