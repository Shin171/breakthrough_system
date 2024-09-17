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

        //public ActionResult EditQuiz(int id)
        //{
        //    var quiz = GetQuizById(id);
        //    if (quiz == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    quiz.AvailableMembers = GetAvailableMembers();
        //    quiz.AssignedMembers = GetAssignedMembers(id);

        //    return View(quiz);
        //}

        //[HttpPost]
        //public ActionResult EditQuiz(QuizEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UpdateQuiz(model);
        //        return RedirectToAction("Activities");
        //    }

        //    model.AvailableMembers = GetAvailableMembers();
        //    return View(model);
        //}

        //private QuizEditViewModel GetQuizById(int quizId)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(_dbConnection))
        //    {
        //        conn.Open();
        //        string query = "SELECT * FROM Quizzes WHERE QuizId = @QuizId";
        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@QuizId", quizId);

        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                return new QuizEditViewModel
        //                {
        //                    QuizId = reader.GetInt32("QuizId"),
        //                    Title = reader.GetString("Title"),
        //                    Description = reader.GetString("Description"),
        //                    Question = reader.GetString("Question"),
        //                    Option1 = reader.GetString("Option1"),
        //                    Option2 = reader.GetString("Option2"),
        //                    Option3 = reader.GetString("Option3"),
        //                    Option4 = reader.GetString("Option4"),
        //                    CorrectAnswer = reader.GetString("CorrectAnswer"),
        //                    DueDate = reader.GetDateTime("DueDate")
        //                };
        //            }
        //        }
        //    }
        //    return null;
        //}

        //private void UpdateQuiz(QuizEditViewModel model)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(_dbConnection))
        //    {
        //        conn.Open();
        //        string query = @"UPDATE Quizzes SET 
        //                         Title = @Title, 
        //                         Description = @Description, 
        //                         Question = @Question, 
        //                         Option1 = @Option1, 
        //                         Option2 = @Option2, 
        //                         Option3 = @Option3, 
        //                         Option4 = @Option4, 
        //                         CorrectAnswer = @CorrectAnswer, 
        //                         DueDate = @DueDate 
        //                         WHERE QuizId = @QuizId";

        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@QuizId", model.QuizId);
        //        cmd.Parameters.AddWithValue("@Title", model.Title);
        //        cmd.Parameters.AddWithValue("@Description", model.Description);
        //        cmd.Parameters.AddWithValue("@Question", model.Question);
        //        cmd.Parameters.AddWithValue("@Option1", model.Option1);
        //        cmd.Parameters.AddWithValue("@Option2", model.Option2);
        //        cmd.Parameters.AddWithValue("@Option3", model.Option3);
        //        cmd.Parameters.AddWithValue("@Option4", model.Option4);
        //        cmd.Parameters.AddWithValue("@CorrectAnswer", model.CorrectAnswer);
        //        cmd.Parameters.AddWithValue("@DueDate", model.DueDate);

        //        cmd.ExecuteNonQuery();

        //        UpdateQuizAssignments(model.QuizId, model.AssignedMembers);
        //    }
        //}

        //private void UpdateQuizAssignments(int quizId, List<int> assignedMembers)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(_dbConnection))
        //    {
        //        conn.Open();

        //        // Delete existing assignments
        //        string deleteQuery = "DELETE FROM QuizAssignments WHERE QuizId = @QuizId";
        //        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
        //        deleteCmd.Parameters.AddWithValue("@QuizId", quizId);
        //        deleteCmd.ExecuteNonQuery();

        //        // Insert new assignments
        //        foreach (var memberId in assignedMembers)
        //        {
        //            string insertQuery = "INSERT INTO QuizAssignments (QuizId, MemberId) VALUES (@QuizId, @MemberId)";
        //            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
        //            insertCmd.Parameters.AddWithValue("@QuizId", quizId);
        //            insertCmd.Parameters.AddWithValue("@MemberId", memberId);
        //            insertCmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private List<Member> GetAvailableMembers()
        //{
        //    var members = new List<Member>();

        //    using (MySqlConnection conn = new MySqlConnection(_dbConnection))
        //    {
        //        conn.Open();
        //        string query = "SELECT * FROM Members";
        //        MySqlCommand cmd = new MySqlCommand(query, conn);

        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                members.Add(new Member
        //                {
        //                    MemberId = reader.GetInt32("MemberId"),
        //                    Name = reader.GetString("Name")
        //                });
        //            }
        //        }
        //    }

        //    return members;
        //}

        //private List<int> GetAssignedMembers(int quizId)
        //{
        //    var assignedMembers = new List<int>();

        //    using (MySqlConnection conn = new MySqlConnection(_dbConnection))
        //    {
        //        conn.Open();
        //        string query = "SELECT MemberId FROM QuizAssignments WHERE QuizId = @QuizId";
        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@QuizId", quizId);

        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                assignedMembers.Add(reader.GetInt32("MemberId"));
        //            }
        //        }
        //    }

        //    return assignedMembers;
        //}
    }
}
