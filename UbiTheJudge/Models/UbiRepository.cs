using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class UbiRepository
    {
        public UbiContext _context;
        public UbiContext Context { get { return _context; } }

        public UbiRepository()
        {
            _context = new UbiContext();
        }

        public UbiRepository(UbiContext a_context)
        {
            _context = a_context;
        }

        public List<UbiUser> GetAllUsers()
        {
            var query = from users in _context.UbiUsers select users;
            return query.ToList();
        }

        public UbiUser GetUserByName(string name)
        {
            var query = from user in _context.UbiUsers where user.Name == name select user;
            return query.SingleOrDefault();
        }

        public bool IsNameAvailable(string name)
        {
            bool available = false;
            try
            {
                UbiUser some_user = GetUserByName(name);
                if (some_user == null)
                {
                    available = true;
                }
            }
            catch (InvalidOperationException) { }

            return available;
        }

        public List<UserScore> GetAllScoresForOneUserId(int user_id)
        {
            var query = from score in _context.Scores where score.UbiUserId == user_id select score;
            return query.ToList();
        }

        public UserScore GetUserScoreForOneSong(int user_id, int song_id)
        {
            var query = from score in _context.Scores where score.UbiUserId == user_id && score.SongId == song_id select score;
            return query.SingleOrDefault();
        }

        public bool CreateQuartet(int quartet_id, string quartet_name, int order_of_appearance)
        {
            Quartet a_quartet = new Quartet { QuartetId = quartet_id, Name = quartet_name, D1OOA = order_of_appearance };
            bool added = true;
            try
            {
                _context.Quartets.Add(a_quartet);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                added = false;
            }
            return added;
        }

        public bool CreateScore(int user_id, int song_id, decimal user_song_score)
        {
            UserScore a_score = new UserScore { UbiUserId = user_id, SongId = song_id, Score = user_song_score };
            bool added = true;
            try
            {
                _context.Scores.Add(a_score);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                added = false;
            }
            return added;
        }

        public decimal CompareScores(Song a_song, UserScore a_score)
        {
            decimal diff = a_score.Score - a_song.JudgesScore;
            return diff;
        }

        public decimal TallyJudgesScores(int quartet_id)
        {
            var query = from song in _context.Songs where song.QuartetId == quartet_id select song.JudgesScore;
            List<decimal> score_list = query.ToList();
            //Will need to refactor next line when Day2 is added.
            decimal total_score = score_list[0] + score_list[1];
            return total_score;
        }

        //Rank all quartets by Order Of Appearance on Day1.  Need to refactor when Day2 is added.
        public List<Quartet> RankByOOA()
        {
            var query = from quartet in _context.Quartets orderby quartet.D1OOA select quartet;
            List<Quartet> all_quartets = query.ToList();
            return all_quartets;
        }

        public List<Quartet> RankByJudgesScores()
        {
            var all_quartets = from quartet in _context.Quartets select quartet;
            foreach (var quartet in all_quartets)
            {
                quartet.JS_Total = TallyJudgesScores(quartet.QuartetId);
            }
            var final_ranking = from quartet in _context.Quartets orderby quartet.JS_Total descending select quartet;
            List<Quartet> judges_ranking = final_ranking.ToList();
            return judges_ranking;
        }

        public List<Song> GetAllSongsForOneQuartet(int quartet_id)
        {
            var query = from song in _context.Songs where song.QuartetId == quartet_id select song;
            return query.ToList();
        }

        public decimal TallyUsersScores(int quartet_id)
        {
            List<Song> quartet_songs = GetAllSongsForOneQuartet(quartet_id);
            //Will need to refactor next two lines when Day2 is added.
            var song_one_scores = from score in _context.Scores where score.SongId == quartet_songs[0].SongId select score.Score;
            var song_two_scores = from score in _context.Scores where score.SongId == quartet_songs[1].SongId select score.Score;
            List<decimal> song_one_score_list = song_one_scores.ToList();
            List<decimal> song_two_score_list = song_two_scores.ToList();
            decimal subtotal1 = song_one_score_list.Sum();
            decimal subtotal2 = song_two_score_list.Sum();
            decimal total_score = subtotal1 + subtotal2;
            return total_score;
        }

        public List<Quartet> RankByUsersScores()
        {
            var all_quartets = from quartet in _context.Quartets select quartet;
            foreach (var quartet in all_quartets)
            {
                quartet.US_Total = TallyUsersScores(quartet.QuartetId);
                _context.SaveChanges();
            }
            var final_ranking = from quartet in _context.Quartets orderby quartet.US_Total descending select quartet;
            List<Quartet> users_ranking = final_ranking.ToList();
            return users_ranking;
        }

        public decimal TallyTotalDifferential(int user_id)
        {
            List<UserScore> user_scores = GetAllScoresForOneUserId(user_id);
            decimal total_differential = 0m;
            foreach (var score in user_scores)
            {
                Song this_song = (from song in _context.Songs where song.SongId == score.SongId select song).SingleOrDefault();
                decimal differential_absolute_value = Math.Abs(CompareScores(this_song, score));
                total_differential += differential_absolute_value;
            }
            return total_differential;
        }

        public List<UbiUser> RankUsersByTotalDifferenial()
        {
            var all_users = from user in _context.UbiUsers select user;
            foreach (var user in all_users)
            {
                user.TD = TallyTotalDifferential(user.UbiUserId);
                _context.SaveChanges();
            }
            var final_ranking = from user in _context.UbiUsers orderby user.TD ascending select user;
            List<UbiUser> TD_ranking = final_ranking.ToList();
            return TD_ranking;
        }

        public bool AddNewUser(ApplicationUser user)
        {
            UbiUser new_user = new UbiUser { RealUser = user, UbiUserId=1};
            bool is_added = true;
            try
            {
                UbiUser added_user = _context.UbiUsers.Add(new_user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }

    }
}