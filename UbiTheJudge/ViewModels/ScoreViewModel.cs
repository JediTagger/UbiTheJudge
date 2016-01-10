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
        public IEnumerable<UbiTheJudge.Models.Quartet> Quartets { get; set; }
        public IEnumerable<UbiTheJudge.Models.Song> Songs { get; set; }
    }
}