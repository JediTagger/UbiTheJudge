using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;
using UbiTheJudge.Models;

namespace UbiTheJudge.Tests
{
    [TestClass]
    public class UbiRepositoryTests
    {

        private Mock<UbiContext> mock_context;
        private Mock<DbSet<UbiUser>> mock_user_set;
        private Mock<DbSet<UserScore>> mock_score_set;
        private Mock<DbSet<Quartet>> mock_quartet_set;
        private UbiRepository repo;

        private void ConnectMocksToDataStore(IEnumerable<UbiUser> data_store)
        {
            var data_source = data_store.AsQueryable<UbiUser>();
            // HINT HINT: var data_source = (data_store as IEnumerable<UbiUser>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_user_set.As<IQueryable<UbiUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user_set.As<IQueryable<UbiUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user_set.As<IQueryable<UbiUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user_set.As<IQueryable<UbiUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the UbiUsers property getter
            mock_context.Setup(a => a.UbiUsers).Returns(mock_user_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<UserScore> data_store)
        {
            var data_source = data_store.AsQueryable<UserScore>();
            // HINT HINT: var data_source = (data_store as IEnumerable<UserScore>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_score_set.As<IQueryable<UserScore>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_score_set.As<IQueryable<UserScore>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_score_set.As<IQueryable<UserScore>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_score_set.As<IQueryable<UserScore>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the UserScores property getter
            mock_context.Setup(a => a.Scores).Returns(mock_score_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<UbiContext>();
            mock_user_set = new Mock<DbSet<UbiUser>>();
            mock_score_set = new Mock<DbSet<UserScore>>();
            repo = new UbiRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_user_set = null;
            mock_score_set = null;
            repo = null;
        }

        [TestMethod]
        public void UbiContextCanHaveInstance()
        {
            UbiContext context = new UbiContext();
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void UbiRepositoryCanHaveInstance()
        {
            UbiRepository repo = new UbiRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void UbiRepositoryCanGetAllUsers()
        {
            var expected = new List<UbiUser>
            {
                new UbiUser { Name = "Bob" },
                new UbiUser { Name = "Sally" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            var actual = repo.GetAllUsers();
            Assert.AreEqual("Bob", actual.First().Name);
            CollectionAssert.AreEqual(expected, actual);
        }

        
    }
}
