using MySql.Data.MySqlClient;
using System.Configuration;
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

                // Create the accounts table if it doesn't exist
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

                // Create the Quizzes table if it doesn't exist
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Quizzes (
                        QuizId INT AUTO_INCREMENT PRIMARY KEY,
                        Title VARCHAR(100) NOT NULL,
                        Description VARCHAR(500) NOT NULL,
                        Question TEXT NOT NULL,
                        Option1 VARCHAR(255) NOT NULL,
                        Option2 VARCHAR(255) NOT NULL,
                        Option3 VARCHAR(255) NOT NULL,
                        Option4 VARCHAR(255) NOT NULL,
                        CorrectAnswer VARCHAR(255) NOT NULL,
                        DueDate DATE NOT NULL
                    )";
                cmd.ExecuteNonQuery();

                // Create the QuizAssignments table if it doesn't exist
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS QuizAssignments (
                        AssignmentId INT AUTO_INCREMENT PRIMARY KEY,
                        QuizId INT NOT NULL,
                        MemberId INT NOT NULL,
                        FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId) ON DELETE CASCADE,
                        FOREIGN KEY (MemberId) REFERENCES accounts(Id) ON DELETE CASCADE
                    )";
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
