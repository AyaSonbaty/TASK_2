using System.ComponentModel.DataAnnotations;

namespace TASK_2.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "you have to enter your full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "you have to enter an email")]
        [EmailAddress(ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [MinLength(6, ErrorMessage = "password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "please confirm your password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
