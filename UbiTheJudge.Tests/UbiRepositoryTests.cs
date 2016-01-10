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
        private Mock<DbSet<Song>> mock_song_set;
        private UbiRepository repo;

        private void ConnectMocksToDataStore(IEnumerable<Quartet> data_store)
        {
            var data_source = data_store.AsQueryable<Quartet>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Quartet>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_quartet_set.As<IQueryable<Quartet>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_quartet_set.As<IQueryable<Quartet>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_quartet_set.As<IQueryable<Quartet>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_quartet_set.As<IQueryable<Quartet>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Quartets property getter
            mock_context.Setup(a => a.Quartets).Returns(mock_quartet_set.Object);
        }

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

        private void ConnectMocksToDataStore(IEnumerable<Song> data_store)
        {
            var data_source = data_store.AsQueryable<Song>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Song>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_song_set.As<IQueryable<Song>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_song_set.As<IQueryable<Song>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_song_set.As<IQueryable<Song>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_song_set.As<IQueryable<Song>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Songs property getter
            mock_context.Setup(a => a.Songs).Returns(mock_song_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<UbiContext>();
            mock_user_set = new Mock<DbSet<UbiUser>>();
            mock_score_set = new Mock<DbSet<UserScore>>();
            mock_song_set = new Mock<DbSet<Song>>();
            mock_quartet_set = new Mock<DbSet<Quartet>>();
            repo = new UbiRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_user_set = null;
            mock_score_set = null;
            mock_song_set = null;
            mock_quartet_set = null;
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

        [TestMethod]
        public void UbiRepositoryCanGetOneUserByName()
        {
            var expected = new List<UbiUser>
            {
                new UbiUser {Name = "Bob"},
                new UbiUser {Name = "Sally"}
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            string name1 = "Bob";
            string name2 = "Bobby";
            UbiUser actual_user1 = repo.GetUserByName(name1);
            UbiUser actual_user2 = repo.GetUserByName(name2);
            Assert.AreEqual("Bob", actual_user1.Name);
            Assert.IsNull(actual_user2);
        }

        [TestMethod]
        public void UbiRepositoryIsNameAvailable()
        {
            var expected = new List<UbiUser>
            {
                new UbiUser {Name = "Bob"},
                new UbiUser {Name = "Sally"}
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            string name1 = "Bob";
            string name2 = "Bobby";
            bool available1 = repo.IsNameAvailable(name1);
            bool available2 = repo.IsNameAvailable(name2);
            Assert.IsFalse(available1);
            Assert.IsTrue(available2);
        }

        [TestMethod]
        public void UbiRepositoryCanGetAllScoresForOneUser()
        {
            var expected = new List<UserScore>
            {
                new UserScore {UbiUserId = 1,SongId=1,Score=69.2m},
                new UserScore {UbiUserId = 1,SongId=2,Score=70.2m},
                new UserScore {UbiUserId = 1,SongId=3,Score=71.2m},
                new UserScore {UbiUserId = 2,SongId=1,Score=69.7m}
            };
            mock_score_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            int user_id = 1;
            List<UserScore> actual_scores = repo.GetAllScoresForOneUserId(user_id);

            Assert.AreEqual(actual_scores[0].Score, 69.2m);
            Assert.AreEqual(actual_scores[2].Score, 71.2m);
        }

        [TestMethod]
        public void UbiRepositoryCanGetUserScoreForOneSong()
        {
            var expected = new List<UserScore>
            {
                new UserScore {UbiUserId = 1,SongId=1,Score=50.0m},
                new UserScore {UbiUserId = 1,SongId=2,Score=60.0m},
                new UserScore {UbiUserId = 2,SongId=1,Score=70.0m},
                new UserScore {UbiUserId = 2,SongId=2,Score=80.0m}
            };
            mock_score_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            int user_id = 1;
            int song_id = 2;
            UserScore actual_score = repo.GetUserScoreForOneSong(user_id, song_id);
            Assert.AreEqual(60.0m, actual_score.Score);
        }

        [TestMethod]
        public void UbiRepositoryCanCreateScores()
        {
            List<UserScore> users_scores = new List<UserScore>();
            int user_id = 1;
            int song_id = 2;
            decimal user_song_score = 88.8m;
            mock_score_set.Object.AddRange(users_scores);
            ConnectMocksToDataStore(users_scores);
            bool success = repo.CreateScore(user_id, song_id, user_song_score);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void UbiRepositoryCanCreateQuartets()
        {
            List<Quartet> all_quartets = new List<Quartet>();
            string quartet_name = "MC4";
            int order_of_appearance = 1;
            mock_quartet_set.Object.AddRange(all_quartets);
            ConnectMocksToDataStore(all_quartets);
            bool success = repo.CreateQuartet(quartet_name, order_of_appearance);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void UbiRepositoryCompareUserScoreToJudgesScore()
        {
            Song a_song = new Song
            {
                SongId = 1,
                Name = "My Wild Irish Rose",
                QuartetId = 1,
                DaySung = 1,
                OrderSung = 1,
                JudgesScore = 60.5m
            };
            List<UserScore> users_scores = new List<UserScore> { new UserScore { UbiUserId = 1, SongId = 1, Score = 77.7m } };
            mock_score_set.Object.AddRange(users_scores);
            ConnectMocksToDataStore(users_scores);
            decimal expected = 17.2m;
            decimal actual = repo.CompareScores(a_song, users_scores[0]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UbiRepositoryTallyJudgesScoresForOneQuartet()
        {
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=80.1m},
                new Song {SongId=2,QuartetId=1,JudgesScore=82.1m},
                new Song {SongId=3,QuartetId=2,JudgesScore=50.1m}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            int quartet_id = 1;
            decimal total_score = repo.TallyJudgesScores(quartet_id);
            Assert.AreEqual(162.2m, total_score);
        }

        [TestMethod]
        public void UbiRepositoryRankQuartetsByOrderOfAppearance()
        {
            List<Quartet> all_quartets = new List<Quartet>
            {
                new Quartet {QuartetId=1,D1OOA=3},
                new Quartet {QuartetId=2,D1OOA=2},
                new Quartet {QuartetId=3,D1OOA=1}
            };
            mock_quartet_set.Object.AddRange(all_quartets);
            ConnectMocksToDataStore(all_quartets);
            List<Quartet> order_of_appearance_ranking = repo.RankByOOA();
            Assert.AreEqual(3, order_of_appearance_ranking[0].QuartetId);
            Assert.AreEqual(2, order_of_appearance_ranking[1].QuartetId);
            Assert.AreEqual(1, order_of_appearance_ranking[2].QuartetId);
        }
        
        [TestMethod]
        public void UbiRepositoryRankQuartetsByJudgesScores()
        {
            List<Quartet> all_quartets = new List<Quartet>
            {
                new Quartet {QuartetId=1},
                new Quartet {QuartetId=2},
                new Quartet {QuartetId=3}
            };
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=80.1m},
                new Song {SongId=2,QuartetId=1,JudgesScore=82.1m},
                new Song {SongId=3,QuartetId=2,JudgesScore=50.2m},
                new Song {SongId=4,QuartetId=2,JudgesScore=55.3m},
                new Song {SongId=5,QuartetId=3,JudgesScore=60.4m},
                new Song {SongId=6,QuartetId=3,JudgesScore=60.5m}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            mock_quartet_set.Object.AddRange(all_quartets);
            ConnectMocksToDataStore(all_quartets);
            List<Quartet> judges_ranking = repo.RankByJudgesScores();
            Assert.AreEqual(1, judges_ranking[0].QuartetId);
            Assert.AreEqual(3, judges_ranking[1].QuartetId);
            Assert.AreEqual(2, judges_ranking[2].QuartetId);
        }

        [TestMethod]
        public void UbiRepositoryGetAllSongsByOneQuartet()
        {
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=80.1m},
                new Song {SongId=2,QuartetId=1,JudgesScore=82.1m},
                new Song {SongId=3,QuartetId=2,JudgesScore=50.2m},
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            int quartet_id = 1;
            List<Song> quartet_songs = repo.GetAllSongsForOneQuartet(quartet_id);
            Assert.AreEqual(1, quartet_songs[0].SongId);
        }
        
        [TestMethod]
        public void UbiRepositoryTallyUserScoresForOneQuartet()
        {
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1},
                new Song {SongId=2,QuartetId=1},
                new Song {SongId=3,QuartetId=2}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            List<Quartet> all_quartets = new List<Quartet>
            {
                new Quartet {QuartetId=1,Name="MC4"},
                new Quartet {QuartetId=2,Name="CrossRoads"}
            };
            mock_quartet_set.Object.AddRange(all_quartets);
            ConnectMocksToDataStore(all_quartets);
            List<UserScore> all_scores = new List<UserScore>
            {
                new UserScore {UbiUserId=1,SongId=1,Score=77.7m},
                new UserScore {UbiUserId=1,SongId=2,Score=76.7m},
                new UserScore {UbiUserId=1,SongId=3,Score=75.7m},
                new UserScore {UbiUserId=2,SongId=1,Score=74.7m}
            };
            mock_score_set.Object.AddRange(all_scores);
            ConnectMocksToDataStore(all_scores);
            int quartet_id = 1;
            decimal total_score = repo.TallyUsersScores(quartet_id);
            Assert.AreEqual(229.1m, total_score);
        }
        
        [TestMethod]
        public void UbiRepositoryRankQuartetsByUsersScores()
        {
            List<Quartet> all_quartets = new List<Quartet>
            {
                new Quartet {QuartetId=1,Name="MC4"},
                new Quartet {QuartetId=2,Name="CrossRoads"},
                new Quartet {QuartetId=3,Name="Max Q"}
            };
            mock_quartet_set.Object.AddRange(all_quartets);
            ConnectMocksToDataStore(all_quartets);
            var all_scores = new List<UserScore>
            {
                new UserScore {UbiUserId=1,SongId=1,Score=50.0m},
                new UserScore {UbiUserId=1,SongId=2,Score=50.0m},
                new UserScore {UbiUserId=1,SongId=3,Score=50.0m},
                new UserScore {UbiUserId=1,SongId=4,Score=50.0m},
                new UserScore {UbiUserId=1,SongId=5,Score=50.0m},
                new UserScore {UbiUserId=1,SongId=6,Score=50.0m},
                new UserScore {UbiUserId=2,SongId=1,Score=55.0m},
                new UserScore {UbiUserId=2,SongId=2,Score=55.0m},
                new UserScore {UbiUserId=2,SongId=3,Score=50.0m},
                new UserScore {UbiUserId=2,SongId=4,Score=50.0m},
                new UserScore {UbiUserId=2,SongId=5,Score=60.0m},
                new UserScore {UbiUserId=2,SongId=6,Score=60.0m}
            };
            mock_score_set.Object.AddRange(all_scores);
            ConnectMocksToDataStore(all_scores);
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=80.1m},
                new Song {SongId=2,QuartetId=1,JudgesScore=82.1m},
                new Song {SongId=3,QuartetId=2,JudgesScore=50.2m},
                new Song {SongId=4,QuartetId=2,JudgesScore=55.3m},
                new Song {SongId=5,QuartetId=3,JudgesScore=60.4m},
                new Song {SongId=6,QuartetId=3,JudgesScore=60.5m}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            List<Quartet> users_ranking = repo.RankByUsersScores();
            Assert.AreEqual("Max Q", users_ranking[0].Name);
            Assert.AreEqual("MC4", users_ranking[1].Name);
            Assert.AreEqual("CrossRoads", users_ranking[2].Name);
        }

        [TestMethod]
        public void UbiRepositoryTallyTotalDifferentialForOneUser()
        {
            List<UserScore> all_scores = new List<UserScore>
            {
                new UserScore {UbiUserId=1,SongId=1,Score=77.2m},
                new UserScore {UbiUserId=1,SongId=2,Score=76.7m},
                new UserScore {UbiUserId=1,SongId=3,Score=75.7m},
                new UserScore {UbiUserId=2,SongId=1,Score=74.7m}
            };
            mock_score_set.Object.AddRange(all_scores);
            ConnectMocksToDataStore(all_scores);
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=78.7m},
                new Song {SongId=2,QuartetId=1,JudgesScore=75.7m},
                new Song {SongId=3,QuartetId=2,JudgesScore=74.7m}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            int user_id = 1;
            decimal total_differential = repo.TallyTotalDifferential(user_id);
            Assert.AreEqual(3.5m, total_differential);
        }

        [TestMethod]
        public void UbiRepositoryRankUsersByTotalDifferential()
        {
            List<UbiUser> all_users = new List<UbiUser>
            {
                new UbiUser {Name = "Bob",UbiUserId=1},
                new UbiUser {Name = "Sally",UbiUserId=2},
                new UbiUser {Name = "Austin",UbiUserId=3}
            };
            mock_user_set.Object.AddRange(all_users);
            ConnectMocksToDataStore(all_users);
            List<UserScore> all_scores = new List<UserScore>
            {
                new UserScore {UbiUserId=1,SongId=1,Score=60.1m},
                new UserScore {UbiUserId=1,SongId=2,Score=61.1m},
                new UserScore {UbiUserId=1,SongId=3,Score=62.1m},
                new UserScore {UbiUserId=1,SongId=4,Score=63.1m},
                new UserScore {UbiUserId=2,SongId=1,Score=71.1m},
                new UserScore {UbiUserId=2,SongId=2,Score=72.1m},
                new UserScore {UbiUserId=2,SongId=3,Score=73.1m},
                new UserScore {UbiUserId=2,SongId=4,Score=74.1m},
                new UserScore {UbiUserId=3,SongId=1,Score=73.1m},
                new UserScore {UbiUserId=3,SongId=2,Score=74.1m},
                new UserScore {UbiUserId=3,SongId=3,Score=75.1m},
                new UserScore {UbiUserId=3,SongId=4,Score=76.1m}
            };
            mock_score_set.Object.AddRange(all_scores);
            ConnectMocksToDataStore(all_scores);
            List<Song> all_songs = new List<Song>
            {
                new Song {SongId=1,QuartetId=1,JudgesScore=71.5m},
                new Song {SongId=2,QuartetId=1,JudgesScore=72.5m},
                new Song {SongId=3,QuartetId=2,JudgesScore=73.5m},
                new Song {SongId=4,QuartetId=2,JudgesScore=74.5m}
            };
            mock_song_set.Object.AddRange(all_songs);
            ConnectMocksToDataStore(all_songs);
            List<UbiUser> user_ranking = repo.RankUsersByTotalDifferenial();
            Assert.AreEqual("Sally", user_ranking[0].Name);
            Assert.AreEqual("Austin", user_ranking[1].Name);
            Assert.AreEqual("Bob", user_ranking[2].Name);
        }
    }
}
