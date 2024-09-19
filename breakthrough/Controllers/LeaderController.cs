using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using breakthrough.Models;
using System;

namespace breakthrough.Controllers
{
    public class LeaderController : Controller
    {
        private string _dbConnection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        // GET: Leader
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Activities()
        {
          
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult AddQuiz()
        {
            return View();
        }

        public ActionResult EditQuiz()
        {
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult DiscipleProgress()
        {
            return View();
        }

    }
}
