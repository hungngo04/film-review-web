using film_review_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace film_review_web.Controllers
{
    public class HomeController : Controller
    {
        MoviesDbEntities db = new MoviesDbEntities();
        public ActionResult Index()
        {

            if(Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<movies> movies = db.movies.ToList();
                return View(movies);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CategoryView()
        {
            return PartialView(db.genres.ToList());
        }

        public ActionResult CategoriesShow(int id)
        {
            List<movies> movies = db.movies.Where(x => x.genresId == id).ToList();
            return View(movies);
        }
    }
}