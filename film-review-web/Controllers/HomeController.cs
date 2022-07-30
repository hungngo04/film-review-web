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
        public ActionResult Index(string username)
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.username = username;
                return View();
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

        public ActionResult CategoriesFilter()
        {
            return PartialView();
        }
    }
}