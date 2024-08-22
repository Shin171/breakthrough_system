using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace breakthrough.Controllers
{
    public class LeaderController : Controller
    {
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

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}