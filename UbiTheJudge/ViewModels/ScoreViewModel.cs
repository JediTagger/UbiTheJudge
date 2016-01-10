using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UbiTheJudge.Models;
using System.ComponentModel.DataAnnotations;

namespace UbiTheJudge.ViewModels
{
    public class ScoreViewModel
    {
        [Key]
        public int key { get; set; }
        public IEnumerable<Quartet> Quartets { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<UserScore> Scores { get; set; }
    }
}