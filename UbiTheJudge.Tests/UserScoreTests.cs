using System;
using UbiTheJudge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UbiTheJudge.Tests
{
    [TestClass]
    public class UserScoreTests
    {
        [TestMethod]
        public void UserScoreCanHaveInstance()
        {
            UserScore a_score = new UserScore();
            Assert.IsNotNull(a_score);
        }

        [TestMethod]
        public void UserScoreCanHaveAllTheThings()
        {
            UserScore a_score = new UserScore();
            UbiUser a_user = new UbiUser { UbiUserId = 1 };
            Song a_song = new Song { SongId = 1 };
            a_score.UbiUserId = a_user.UbiUserId;
            a_score.SongId = a_song.SongId;
            a_score.Score = 77.2;
            Assert.AreEqual(1, a_score.UbiUserId);
            Assert.AreEqual(1, a_score.SongId);
            Assert.AreEqual(77.2, a_score.Score);
        }

    }
}
