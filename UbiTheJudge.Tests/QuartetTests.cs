using System;
using UbiTheJudge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UbiTheJudge.Tests
{
    [TestClass]
    public class QuartetTests
    {
        [TestMethod]
        public void QuartetCanHaveInstance()
        {
            Quartet a_quartet = new Quartet();
            Assert.IsNotNull(a_quartet);
        }

        [TestMethod]
        public void QuartetCanHaveName()
        {
            Quartet a_quartet = new Quartet();
            a_quartet.Name = "Bob";
            Assert.AreEqual("Bob", a_quartet.Name);
        }

        [TestMethod]
        public void QuartetCanHaveID()
        {
            Quartet a_quartet = new Quartet();
            a_quartet.QuartetId = 1;
            Assert.AreEqual(1, a_quartet.QuartetId);
        }

        [TestMethod]
        public void QuartetCanHaveDayOneOrderOfAppearance()
        {
            Quartet a_quartet = new Quartet();
            a_quartet.D1OOA = 1;
            Assert.AreEqual(1, a_quartet.D1OOA);
        }

        [TestMethod]
        public void QuartetCanHaveDayTwoOrderOfAppearance()
        {
            Quartet a_quartet = new Quartet();
            a_quartet.D2OOA = 1;
            Assert.AreEqual(1, a_quartet.D2OOA);
        }

        [TestMethod]
        public void QuartetCanHaveTotalScores()
        {
            Quartet a_quartet = new Quartet();
            a_quartet.US_Total = 77.3m;
            a_quartet.JS_Total = 76.5m;
            Assert.AreEqual(a_quartet.US_Total, 77.3m);
            Assert.AreEqual(a_quartet.JS_Total, 76.5m);
        }
    }
}
