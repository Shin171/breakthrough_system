using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace breakthrough.Controllers
{
    public class HomeController : Controller
    {
        private string _dbConnection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        public ActionResult Index()
        {
            using (var conn = new MySqlConnection(_dbConnection))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS accounts (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Name VARCHAR(100) NOT NULL,
                        Birthdate DATE NOT NULL,
                        PhoneNumber VARCHAR(15) NOT NULL,
                        Email VARCHAR(100) UNIQUE NOT NULL,
                        Password VARCHAR(256) NOT NULL,
                        Role ENUM('Leader', 'Member') NOT NULL,
                        DataPolicyAccepted BOOLEAN NOT NULL DEFAULT FALSE
                    )", conn);
                cmd.ExecuteNonQuery();
            }

            return View();
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
    }
}