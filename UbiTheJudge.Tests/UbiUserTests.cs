using System;
using UbiTheJudge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UbiTheJudge.Tests
{
    [TestClass]
    public class UbiUserTests
    {
        [TestMethod]
        public void UbiUserCanCreateInstance()
        {
            UbiUser a_user = new UbiUser();
            Assert.IsNotNull(a_user);
        }

        [TestMethod]
        public void UbiUserCanHaveName()
        {
            UbiUser a_user = new UbiUser();
            a_user.Name = "Bob";
            Assert.AreEqual("Bob", a_user.Name);
        }

        [TestMethod]
        public void UbiUserCanHaveID()
        {
            UbiUser a_user = new UbiUser();
            a_user.UbiUserId = 1;
            Assert.AreEqual(1, a_user.UbiUserId);
        }

        [TestMethod]
        public void UbiUserCanHaveScores()
        {
            List<UserScore> list_of_scores = new List<UserScore>
            {
                new UserScore {UbiUserId=1,SongId=1,Score=88.1m},
                new UserScore {UbiUserId=1,SongId=2,Score=77.1m},
                new UserScore {UbiUserId=2,SongId=1,Score=79.9m}
            };
            UbiUser a_user = new UbiUser
            {
                UbiUserId = 1,
                Name = "Bob",
                Scores = list_of_scores
            };
            CollectionAssert.AreEqual(list_of_scores, a_user.Scores);
        }

        [TestMethod]
        public void UbiUserCanHaveTotalDifferential()
        {
            UbiUser a_user = new UbiUser();
            a_user.TD = 78.1m;
            Assert.AreEqual(78.1m, a_user.TD);
        }
    }
}
