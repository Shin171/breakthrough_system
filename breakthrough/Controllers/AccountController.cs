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
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Crypto.Generators;

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
                            ModelState.AddModelError("", "The email address is already in use.");
                            return View(model);
                        }
                    }
                    model.Password = HashPassword(model.Password);

                    string insertQuery = "INSERT INTO accounts (Name, Birthdate, PhoneNumber, Email, Password) VALUES (@Name, @Birthdate, @PhoneNumber, @Email, @Password)";
                    using (var insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        connection.Open();
                        insertCmd.Parameters.AddWithValue("@Name", model.Name);
                        insertCmd.Parameters.AddWithValue("@Birthdate", model.Birthdate);
                        insertCmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                        insertCmd.Parameters.AddWithValue("@Email", model.Email);
                        insertCmd.Parameters.AddWithValue("@Password", model.Password);
                       
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
                // Connect to the database and get the user
                using (var connection = new MySqlConnection(_dbConnection))
                {
                    string query = "SELECT * FROM accounts WHERE Email = @Email";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@Email", model.Email);

                        var reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string storedHashedPassword = reader["Password"].ToString();

                            // Verify the password entered by the user with the stored password
                            if (VerifyPassword(model.Password, storedHashedPassword))
                            {
                                // If the password is correct, authenticate the user
                                FormsAuthentication.SetAuthCookie(model.Email, false);
                                TempData["AccountCreated"] = true;
                                // Optionally, redirect to the dashboard or any other page
                                return RedirectToAction("Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid password.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "User not found.");
                        }
                    }
                }
            }

            // If something goes wrong (validation or authentication fails), return the same view with errors
            return View(model);
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Hash the entered password and compare it to the stored hash
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }

        public ActionResult Dashboard(/*string role*/)
        {
            //// Render different views based on role
            //if (role == "Leader")
            //{
                return RedirectToAction("Dashboard", "Leader");
            //}
            //else
            //{
                //return RedirectToAction("Dashboard", "Disciple");
            //}
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
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }




    }
}