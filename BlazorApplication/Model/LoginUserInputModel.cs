using System.ComponentModel.DataAnnotations;

namespace BlazorApplication.Model
{
    public class LoginUserInputModel
    {
        [Required, MinLength(4), MaxLength(16)]
        public string Username { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
