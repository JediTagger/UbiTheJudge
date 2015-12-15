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
        public int D1OOA { get; set; }
        public int D2OOA { get; set; }
    }
}