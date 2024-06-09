using System.ComponentModel.DataAnnotations;

namespace AppIdentity.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}