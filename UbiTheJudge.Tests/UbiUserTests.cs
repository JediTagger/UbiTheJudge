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
                new UserScore {UbiUserId=1,SongId=1,Score=88.1},
                new UserScore {UbiUserId=1,SongId=2,Score=77.1}
            };
            UbiUser a_user = new UbiUser
            {
                UbiUserId = 1,
                Name = "Bob",
                Scores = list_of_scores
            };
            CollectionAssert.AreEqual(list_of_scores, a_user.Scores);
        }
    }
}
