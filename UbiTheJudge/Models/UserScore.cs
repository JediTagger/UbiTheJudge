using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UbiTheJudge.Models
{
    public class UserScore
    {
        [Key]
        public int key { get; set; }
        public int SongId { get; set; }
        public int UbiUserId { get; set; }
        public decimal Score { get; set; }
    }
}