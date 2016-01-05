using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class Song
    {
        public int DaySung { get; set; }
        public decimal JudgesScore { get; set; }
        public string Name { get; set; }
        public int OrderSung { get; set; }
        [Key]
        public int SongId { get; set; }
        public int QuartetId { get; set; }
    }
}