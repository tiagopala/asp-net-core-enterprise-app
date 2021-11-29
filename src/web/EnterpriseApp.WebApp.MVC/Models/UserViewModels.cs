﻿using System.ComponentModel.DataAnnotations;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "Field {0} is not in a valid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(100, ErrorMessage = "Field {0} has to be between {2} and {1} characteres.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords does not match.")]
        [Display(Name = "Password Confirmation")]
        public string PasswordConfirmation { get; set; }
    }

    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "Field {0} is not in a valid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(100, ErrorMessage = "Field {0} has to be between {2} and {1} characteres.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
