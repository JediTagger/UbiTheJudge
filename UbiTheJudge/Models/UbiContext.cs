using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class UbiContext : ApplicationDbContext
    {
        public virtual DbSet<UbiUser> UbiUsers { get; set; }
        public virtual DbSet<UserScore> Scores { get; set; }
        public virtual DbSet<Quartet> Quartets { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
    }
}