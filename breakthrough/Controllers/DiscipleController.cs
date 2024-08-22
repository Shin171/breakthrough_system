using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace breakthrough.Controllers
{
    public class DiscipleController : Controller
    {
        // GET: Disciple
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
            FormsAuthentication.SignOut();
            return View();
        }
    }
}