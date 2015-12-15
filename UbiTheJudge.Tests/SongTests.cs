using System;
using UbiTheJudge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UbiTheJudge.Tests
{
    [TestClass]
    public class SongTests
    {
        [TestMethod]
        public void SongCanHaveInstance()
        {
            Song a_song = new Song();
            Assert.IsNotNull(a_song);
        }

        [TestMethod]
        public void SongCanHaveAllTheProperties()
        {
            Song a_song = new Song();
            Quartet a_quartet = new Quartet { QuartetId = 1 };
            a_song.SongId = 1;
            a_song.Name = "Bob";
            a_song.DaySung = 1;
            a_song.OrderSung = 1;
            a_song.JudgesScore = 78.2;
            a_song.QuartetId = a_quartet.QuartetId;
            Assert.AreEqual(a_song.QuartetId, a_quartet.QuartetId);
            Assert.AreEqual(1, a_song.SongId);
            Assert.AreEqual("Bob", a_song.Name);
            Assert.AreEqual(1, a_song.DaySung);
            Assert.AreEqual(1, a_song.OrderSung);
            Assert.AreEqual(78.2, a_song.JudgesScore);
        }
    }
}
