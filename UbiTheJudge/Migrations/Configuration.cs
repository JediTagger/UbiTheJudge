namespace UbiTheJudge.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System.Data.Entity.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<UbiTheJudge.Models.UbiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UbiTheJudge.Models.UbiContext context)
        {
            /*

            context.Quartets.AddOrUpdate(q => q.Name,
                new Quartet() { Name = "MC4", D1OOA = 1, },
                new Quartet() { Name = "Max Q", D1OOA = 2, },
                new Quartet() { Name = "Cha-Ching!", D1OOA = 3, },
                new Quartet() { Name = "4 Voices", D1OOA = 4, }
            );
            context.Songs.AddOrUpdate(s => s.Name,
                new Song() { Name = "My Wild Irish Rose", QuartetId = 1 },
                new Song() { Name = "Sweet And Lovely", QuartetId = 1 },
                new Song() { Name = "Heart Of My Heart", QuartetId = 2 },
                new Song() { Name = "Nellie", QuartetId = 2 },
                new Song() { Name = "If I Ruled The World", QuartetId = 3 },
                new Song() { Name = "Some Enchanted Evening", QuartetId = 3 },
                new Song() { Name = "Sweet Roses Of Morn", QuartetId = 4 },
                new Song() { Name = "Down Our Way", QuartetId = 4 }
            );
            context.UbiUsers.AddOrUpdate(u => u.Name,
                new UbiUser() { Name = "Bob" },
                new UbiUser() { Name = "Sally" },
                new UbiUser() { Name = "Fred" },
                new UbiUser() { Name = "Mary" }
            );
            context.SaveChanges();
            */

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
