using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class UserScore
    {
        public double Score { get; set; }
        public int SongId { get; set; }
        public int UbiUserId { get; set; }
    }
}