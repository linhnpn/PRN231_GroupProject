using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.DTOs.Authenticate
{
    public class AuthenRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
