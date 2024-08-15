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

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string resetToken = Guid.NewGuid().ToString();
                DateTime tokenExpiration = DateTime.UtcNow.AddHours(24);

                using (var connection = new MySqlConnection(_dbConnection))
                {
                    string updateQuery = "UPDATE accounts SET PasswordResetToken = @Token, PasswordResetTokenExpiration = @Expiration WHERE Email = @Email";
                    using (var cmd = new MySqlCommand(updateQuery, connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@Token", resetToken);
                        cmd.Parameters.AddWithValue("@Expiration", tokenExpiration);
                        cmd.Parameters.AddWithValue("@Email", model.Email);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            SendPasswordResetEmail(model.Email, resetToken);
                            return RedirectToAction("ForgotPasswordConfirmation");
                        }
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [NonAction]
        private void SendPasswordResetEmail(string email, string token)
        {
            string resetLink = Url.Action("ResetPassword", "Account", new { email = email, token = token }, protocol: Request.Url.Scheme);

            string subject = "Reset Your Password";
            string body = $"Please reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>";

            using (var smtpClient = new SmtpClient("your-smtp-server.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-password");
                smtpClient.EnableSsl = true;

                using (var mailMessage = new MailMessage("your-email@example.com", email))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);
                }
            }
        }

        [HttpGet]
        public ActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel { Email = email, Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(_dbConnection))
                {
                    string query = "SELECT * FROM accounts WHERE Email = @Email AND PasswordResetToken = @Token AND PasswordResetTokenExpiration > @Now";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Token", model.Token);
                        cmd.Parameters.AddWithValue("@Now", DateTime.UtcNow);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                string updateQuery = "UPDATE accounts SET Password = @Password, PasswordResetToken = NULL, PasswordResetTokenExpiration = NULL WHERE Email = @Email";
                                using (var updateCmd = new MySqlCommand(updateQuery, connection))
                                {
                                    updateCmd.Parameters.AddWithValue("@Password", HashPassword(model.Password));
                                    updateCmd.Parameters.AddWithValue("@Email", model.Email);

                                    updateCmd.ExecuteNonQuery();
                                }

                                return RedirectToAction("ResetPasswordConfirmation");
                            }
                        }
                        connection.Close();
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
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

        public ActionResult Logout()
        {
            return View();
        }

    }
}