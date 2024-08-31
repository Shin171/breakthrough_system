using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace breakthrough.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Quiz title is required.")]
        [StringLength(100, ErrorMessage = "Quiz title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quiz description is required.")]
        [StringLength(500, ErrorMessage = "Quiz description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Option 1 is required.")]
        public string Option1 { get; set; }

        [Required(ErrorMessage = "Option 2 is required.")]
        public string Option2 { get; set; }

        [Required(ErrorMessage = "Option 3 is required.")]
        public string Option3 { get; set; }

        [Required(ErrorMessage = "Option 4 is required.")]
        public string Option4 { get; set; }

        [Required(ErrorMessage = "Correct answer selection is required.")]
        public string CorrectAnswer { get; set; }
    }

    public class QuizEditViewModel
    {
        [Required]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Quiz title is required.")]
        [StringLength(100, ErrorMessage = "Quiz title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quiz description is required.")]
        [StringLength(500, ErrorMessage = "Quiz description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Option 1 is required.")]
        public string Option1 { get; set; }

        [Required(ErrorMessage = "Option 2 is required.")]
        public string Option2 { get; set; }

        [Required(ErrorMessage = "Option 3 is required.")]
        public string Option3 { get; set; }

        [Required(ErrorMessage = "Option 4 is required.")]
        public string Option4 { get; set; }

        [Required(ErrorMessage = "Correct answer selection is required.")]
        public string CorrectAnswer { get; set; }

        [Required(ErrorMessage = "Please assign the quiz to at least one member.")]
        public List<int> AssignedMembers { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }


    }

    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
    }
}