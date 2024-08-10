using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace breakthrough.Controllers
{
    public class LeadersQuizController : Controller
    {
        // GET: LeadersQuiz
        public ActionResult Create()
        {
            return View();
        }
    }
}