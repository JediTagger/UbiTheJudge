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

        public ActionResult Test()
        {
            /*
            var viewModel = new ScoreViewModel
            {
                Quartets = Repo.RankByOOA(),
                Songs = Repo.GetAllSongs()
            };
            */
            ScoreViewModel viewModel = new ScoreViewModel();
            viewModel.Songs = Repo.GetAllSongs();
            viewModel.Quartets = Repo.RankByOOA();
            return View(viewModel);
        }

        public ActionResult Empty()
        {
            return View();
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
                return RedirectToAction("Test");
            }

            return View(quartet);
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
