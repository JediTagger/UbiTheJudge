using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class UbiUser : IComparable
    {
        [Key]
        public int UbiUserId { get; set; }
        public virtual ApplicationUser RealUser { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\d]+[-_a-zA-Z\d]{0,2}[a-zA-Z\d]+")]
        public string Name { get; set; }
        //TD means Total Differential.  The total difference between the users scores and the judges scores.
        //This is to rank the users by who got closest to the "right" (the judges) score.
        public decimal TD { get; set; }
        public virtual List<UserScore> Scores { get; set; }

        //What does this do??
        public int CompareTo(object obj)
        {
            UbiUser other_user = obj as UbiUser;
            int answer = this.Name.CompareTo(other_user.Name);
            return answer;
        }
    }
}