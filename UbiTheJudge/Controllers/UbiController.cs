using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UbiTheJudge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using UbiTheJudge.ViewModels;

namespace UbiTheJudge.Controllers
{
    public class UbiController : Controller
    {
        public UbiRepository Repo { get; set; }

        public UbiController() : base()
        {
            Repo = new UbiRepository();
        }

        private UbiContext db = new UbiContext();

        // GET: Ubi
        public ActionResult Score()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser real_user = Repo.Context.Users.FirstOrDefault(u => u.Id == user_id);
            UbiUser me = null;
            try
            {
                me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).SingleOrDefault();
            }
            catch (Exception)
            {
                bool successful = Repo.AddNewUser(real_user);
            }
            List<Quartet> all_quartets = Repo.RankByOOA();
            return View(all_quartets);
        }

        [Authorize]
        public ActionResult Program()
        {
            /*
            var viewModel = new ScoreViewModel
            {
                Quartets = Repo.RankByOOA(),
                Songs = Repo.GetAllSongs()
            };
            */
            string user_id = User.Identity.GetUserId();
            ApplicationUser real_user = Repo.Context.Users.FirstOrDefault(u => u.Id == user_id);
            UbiUser me = null;
            try
            {
                me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).SingleOrDefault();
            }
            catch (Exception)
            {
                bool successful = Repo.AddNewUser(real_user);
            }
            ScoreViewModel viewModel = new ScoreViewModel();
            viewModel.Songs = Repo.GetAllSongs();
            viewModel.Quartets = Repo.RankByOOA();
            viewModel.Scores = Repo.GetAllScoresForOneUserId(1);
            return View(viewModel);
        }

        public ActionResult ViewScores()
        {
            string user_id = User.Identity.GetUserId();
            UbiUser me = Repo.GetAllUsers().Where(u => u.RealUser.Id == user_id).SingleOrDefault();
            List<UserScore> user_scores = Repo.GetAllScoresForOneUserId(me.UbiUserId);
            return View(user_scores);
        }

        public ActionResult ViewScoresPlease()
        {
            List<UserScore> user_scores = Repo.GetAllScoresForOneUserId(1);
            return View(user_scores);
        }

        public ActionResult ViewAndCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewAndCreate([Bind(Include = "SongId,Score")] UserScore score)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(score);
                db.SaveChanges();
                return RedirectToAction("Test");
            }
            List<Song> all_songs = Repo.GetAllSongs();
            return View(all_songs);
        }

        // GET: Ubi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ubi/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateQuartet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuartet([Bind(Include = "Name,D1OOA")] Quartet quartet)
        {
            if (ModelState.IsValid)
            {
                db.Quartets.Add(quartet);
                db.SaveChanges();
                return RedirectToAction("Program");
            }

            return View(quartet);
        }

        [Authorize]
        public ActionResult CreateScore()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScore([Bind(Include = "SongId,UbiUserId,Score")] UserScore user_score)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(user_score);
                db.SaveChanges();
                return RedirectToAction("Program");
            }

            return View(user_score);
        }

        [Authorize]
        public ActionResult CreateSong()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSong([Bind(Include = "Name,QuartetId")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Program");
            }

            return View(song);
        }

        public ActionResult UsersRanked()
        {
            List<UbiUser> users_ranked = Repo.RankUsersByTotalDifferenial();
            return View(users_ranked);
        }

        public ActionResult QuartetsRankedByJudgesScores()
        {
            List<Quartet> quartets_ranked_by_judges_scores = Repo.RankByJudgesScores();
            return View(quartets_ranked_by_judges_scores);
        }

        public ActionResult QuartetsRankedByUsersScores()
        {
            List<Quartet> quartets_ranked_by_users_scores = Repo.RankByUsersScores();
            return View(quartets_ranked_by_users_scores);
        }

        // POST: Ubi/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ubi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ubi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ubi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ubi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
