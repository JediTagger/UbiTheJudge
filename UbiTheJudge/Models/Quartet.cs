using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class Quartet
    {
        [Key]
        public int QuartetId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\d]+[-_a-zA-Z\d]{0,2}[a-zA-Z\d]+")]
        public string Name { get; set; }
        //Day 1 Order Of Appearance
        public int D1OOA { get; set; }
        //Day 2 Order Of Appearance
        //Only using one day right now.  Will update later.
        public int D2OOA { get; set; }
        //Judges Total Score
        public decimal JS_Total { get; set; }
        //Users Total Score
        public decimal US_Total { get; set; }
    }
}