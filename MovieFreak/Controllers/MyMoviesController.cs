using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieFreak.DAL;
using MovieFreak.Models;

namespace MovieFreak.Controllers
{
    [Authorize(Users = "Administrator@wp.pl")]
    public class MyMoviesController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: MyMovies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: MyMovies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyMovie myMovie = db.Movies.Find(id);
            if (myMovie == null)
            {
                return HttpNotFound();
            }
            return View(myMovie);
        }

        // GET: MyMovies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyMovies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyMovie myMovie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(myMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myMovie);
        }

        // GET: MyMovies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyMovie myMovie = db.Movies.Find(id);
            if (myMovie == null)
            {
                return HttpNotFound();
            }
            return View(myMovie);
        }

        // POST: MyMovies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyMovie myMovie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myMovie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myMovie);
        }

        // GET: MyMovies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyMovie myMovie = db.Movies.Find(id);
            if (myMovie == null)
            {
                return HttpNotFound();
            }
            return View(myMovie);
        }

        // POST: MyMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyMovie myMovie = db.Movies.Find(id);
            db.Movies.Remove(myMovie);
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
