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
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\d]+[-_a-zA-Z\d]{0,2}[a-zA-Z\d]+")]
        public string Name { get; set; }

        public List<UserScore> Scores { get; set; }

        //What does this do??
        public int CompareTo(object obj)
        {
            UbiUser other_user = obj as UbiUser;
            int answer = this.Name.CompareTo(other_user.Name);
            return answer;
        }
    }
}