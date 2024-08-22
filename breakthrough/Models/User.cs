using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace breakthrough.Models
{
    public class User
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public bool RememberMe { get; set; }


    }

    //public class ForgotPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; }
    //}

    //public class ResetPasswordViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; }
    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    //    public string ConfirmPassword { get; set; }
    //    public string Token { get; set; }
    //}



}