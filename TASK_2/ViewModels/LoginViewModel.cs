using System.ComponentModel.DataAnnotations;

namespace TASK_2.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "you have to enter your email")]
        [EmailAddress(ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
