using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Juke_Mobile_Model;

namespace Juke_Mobile_UnitTest
{
    [TestClass]
    public class MusicAnalysisTest
    {
        public const string scheisse = "Scheisse";
        [TestMethod]
        public void TestT()
        {
            MP3Analysis mp3a = new MP3Analysis();
            int that=mp3a.makethat(1);           
            if (mp3a.makethat(1) != 1)
            {
                throw new ArgumentOutOfRangeException("test that", that, scheisse);
            }
        }
    }
}
