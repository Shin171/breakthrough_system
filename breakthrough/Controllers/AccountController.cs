using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using breakthrough.Models;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;

namespace breakthrough.Controllers
{
    public class AccountController : Controller
    {

        private string _dbConnection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != Request.Form["ConfirmPassword"])
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                using (var connection = new MySqlConnection(_dbConnection))
                {
                    string checkQuery = "SELECT COUNT(*) FROM accounts WHERE Email = @Email";
                    using (var checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        connection.Open();
                        checkCmd.Parameters.AddWithValue("@Email", model.Email);
                        int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        connection.Close();

                        if (emailCount > 0)
                        {
                            ModelState.AddModelError("", "The email address is already in use. Please try an alternative!");
                            return View(model);
                        }
                    }

                    model.Password = HashPassword(model.Password);

                    string insertQuery = "INSERT INTO accounts (Name, Birthdate, PhoneNumber, Email, Password, Role) VALUES (@Name, @Birthdate, @PhoneNumber, @Email, @Password, @Role)";
                    using (var insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        connection.Open();
                        insertCmd.Parameters.AddWithValue("@Name", model.Name);
                        insertCmd.Parameters.AddWithValue("@Birthdate", model.Birthdate);
                        insertCmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                        insertCmd.Parameters.AddWithValue("@Email", model.Email);
                        insertCmd.Parameters.AddWithValue("@Password", model.Password);
                        insertCmd.Parameters.AddWithValue("@Role", model.Role);

                        insertCmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(_dbConnection))
                {
                    string query = "SELECT * FROM accounts WHERE Email = @Email AND Password = @Password";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", HashPassword(model.Password));

                        connection.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set user session or cookie here
                                string role = reader["Role"].ToString();
                                connection.Close();
                                return RedirectToAction("Dashboard", new { role });
                            }
                        }
                        connection.Close();
                    }
                }

                ModelState.AddModelError("", "Incorrect email or password. try again!");
            }

            return View(model);
        }

        public ActionResult Dashboard(string role)
        {
            // Render different views based on role
            if (role == "Leader")
            {
                return View("LeaderDashboard");
            }
            else
            {
                return View("DiscipleDashboard");
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

    }
}