using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using film_review_web.Models;

namespace film_review_web.Controllers
{
    public class moviesController : Controller
    {
        private MoviesDbEntities db = new MoviesDbEntities();

        // GET: movies
        public ActionResult Index()
        {
            var movies = db.movies.Include(m => m.account).Include(m => m.genres);
            return View(movies.ToList());
        }

        // GET: movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            ViewBag.genresName = db.genres.Find(movies.genresId).genresName;
            return View(movies);
        }

        // GET: movies/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.account, "userId", "username");
            ViewBag.genresId = new SelectList(db.genres, "genresId", "genresName");
            return View();
        }

        // POST: movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(movies movies)
        {
            string fileName = Path.GetFileNameWithoutExtension(movies.ImageFile.FileName);
            string extension = Path.GetExtension(movies.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            movies.images = "/Content/img/uploaded/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Content/img/uploaded/"), fileName);
            movies.ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.account, "userId", "username", movies.userId);
            ViewBag.genresId = new SelectList(db.genres, "genresId", "genresName", movies.genresId);
            return View(movies);
        }

        // GET: movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.account, "userId", "username", movies.userId);
            ViewBag.genresId = new SelectList(db.genres, "genresId", "genresName", movies.genresId);
            return View(movies);
        }

        // POST: movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,releaseDate,runTime,rating,overview,genresId,userId,images")] movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.account, "userId", "username", movies.userId);
            ViewBag.genresId = new SelectList(db.genres, "genresId", "genresName", movies.genresId);
            return View(movies);
        }

        // GET: movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        // POST: movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movies movies = db.movies.Find(id);
            db.movies.Remove(movies);
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
