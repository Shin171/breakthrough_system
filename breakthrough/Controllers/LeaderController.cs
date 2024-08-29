using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult ActivitiesHistory()
        {
            return View();
        }

        
    }
}