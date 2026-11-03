using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.API.Model
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
