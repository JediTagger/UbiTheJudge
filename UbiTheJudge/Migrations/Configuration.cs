namespace UbiTheJudge.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UbiTheJudge.Models.UbiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UbiTheJudge.Models.UbiContext context)
        {
            context.Quartets.AddOrUpdate(q => q.QuartetId,
                new Quartet() { QuartetId = 1, Name = "MC4", D1OOA = 1, D2OOA = 1, JS_Total = 1.1m, US_Total = 1.1m },
                new Quartet() { QuartetId = 2, Name = "Realtime", D1OOA = 2, D2OOA = 2, JS_Total = 1.1m, US_Total = 1.1m },
                new Quartet() { QuartetId = 3, Name = "Acoustix", D1OOA = 3, D2OOA = 3, JS_Total = 1.1m, US_Total = 1.1m },
                new Quartet() { QuartetId = 4, Name = "Nightlife", D1OOA = 4, D2OOA = 4, JS_Total = 1.1m, US_Total = 1.1m }
                );
            context.Songs.AddOrUpdate(s => s.SongId,
                new Song() { SongId = 1, Name = "Nellie", DaySung = 1, QuartetId = 1, OrderSung = 1, JudgesScore = 61.1m },
                new Song() { SongId = 2, Name = "My Wild Irish Rose", DaySung = 1, QuartetId = 1, OrderSung = 1, JudgesScore = 62.1m },
                new Song() { SongId = 3, Name = "If I Ruled The World", DaySung = 1, QuartetId = 2, OrderSung = 1, JudgesScore = 63.1m },
                new Song() { SongId = 4, Name = "Sweet And Lovely", DaySung = 1, QuartetId = 2, OrderSung = 1, JudgesScore = 64.1m },
                new Song() { SongId = 5, Name = "Heart Of My Heart", DaySung = 1, QuartetId = 3, OrderSung = 1, JudgesScore = 65.1m },
                new Song() { SongId = 6, Name = "Some Enchantd Evening", DaySung = 1, QuartetId = 3, OrderSung = 1, JudgesScore = 66.1m },
                new Song() { SongId = 7, Name = "Sweet Roses Of Morn", DaySung = 1, QuartetId = 4, OrderSung = 1, JudgesScore = 67.1m },
                new Song() { SongId = 8, Name = "Down Our Way", DaySung = 1, QuartetId = 4, OrderSung = 1, JudgesScore = 68.1m }
                );
            context.UbiUsers.AddOrUpdate(u => u.Name,
                new UbiUser() { UbiUserId = 1, Name = "Bob" },
                new UbiUser() { UbiUserId = 2, Name = "Sally" },
                new UbiUser() { UbiUserId = 3, Name = "Fred" },
                new UbiUser() { UbiUserId = 4, Name = "Mary" }
                );
            context.Scores.AddOrUpdate(sc => sc.Score,
                new UserScore() { UbiUserId = 1, SongId = 1, Score = 50.1m },
                new UserScore() { UbiUserId = 1, SongId = 2, Score = 50.2m },
                new UserScore() { UbiUserId = 1, SongId = 3, Score = 50.3m },
                new UserScore() { UbiUserId = 1, SongId = 4, Score = 50.4m },
                new UserScore() { UbiUserId = 2, SongId = 1, Score = 60.1m },
                new UserScore() { UbiUserId = 2, SongId = 2, Score = 60.2m },
                new UserScore() { UbiUserId = 2, SongId = 3, Score = 60.3m },
                new UserScore() { UbiUserId = 2, SongId = 4, Score = 60.4m },
                new UserScore() { UbiUserId = 3, SongId = 1, Score = 70.1m },
                new UserScore() { UbiUserId = 3, SongId = 2, Score = 70.2m },
                new UserScore() { UbiUserId = 3, SongId = 3, Score = 70.3m },
                new UserScore() { UbiUserId = 3, SongId = 4, Score = 70.4m },
                new UserScore() { UbiUserId = 4, SongId = 1, Score = 80.1m },
                new UserScore() { UbiUserId = 4, SongId = 2, Score = 80.2m },
                new UserScore() { UbiUserId = 4, SongId = 3, Score = 80.3m },
                new UserScore() { UbiUserId = 4, SongId = 4, Score = 80.4m }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
