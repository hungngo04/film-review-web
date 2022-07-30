using film_review_web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace film_review_web.Controllers
{
    public class LoginController : Controller
    {
        string ConnectionString = "Data Source=TIENHUNGBA01; Initial Catalog=MoviesDB; Integrated Security=SSPI;";
        MoviesDbEntities db = new MoviesDbEntities();

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(account acc)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            com.Connection = conn;
            com.CommandText = "select * from account where username='"+acc.username+"' and userpassword='"+acc.userpassword+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                return View("Index");
            }
            else
            {
                conn.Close();
                return View("Error");
            }
        }


    }
}