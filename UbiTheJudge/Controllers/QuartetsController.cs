using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UbiTheJudge.Models;

namespace UbiTheJudge.Controllers
{
    public class QuartetsController : Controller
    {
        private UbiContext db = new UbiContext();

        // GET: Quartets
        public ActionResult Index()
        {
            return View(db.Quartets.ToList());
        }

        // GET: Quartets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quartet quartet = db.Quartets.Find(id);
            if (quartet == null)
            {
                return HttpNotFound();
            }
            return View(quartet);
        }

        // GET: Quartets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quartets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuartetId,Name,D1OOA,D2OOA,JS_Total,US_Total")] Quartet quartet)
        {
            if (ModelState.IsValid)
            {
                db.Quartets.Add(quartet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quartet);
        }

        // GET: Quartets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quartet quartet = db.Quartets.Find(id);
            if (quartet == null)
            {
                return HttpNotFound();
            }
            return View(quartet);
        }

        // POST: Quartets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuartetId,Name,D1OOA,D2OOA,JS_Total,US_Total")] Quartet quartet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quartet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quartet);
        }

        // GET: Quartets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quartet quartet = db.Quartets.Find(id);
            if (quartet == null)
            {
                return HttpNotFound();
            }
            return View(quartet);
        }

        // POST: Quartets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quartet quartet = db.Quartets.Find(id);
            db.Quartets.Remove(quartet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
