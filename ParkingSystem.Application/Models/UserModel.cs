

using System.Text.Json.Serialization;

namespace ParkingSystem.Application.Models
{
    public class UserModel
    {
        public string? UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public string? RepeatPassword { get; set; }
        [JsonIgnore]
        public List<int>? RoleIds { get; set; }
        public int? RoleId { get; set; }
        [JsonIgnore]
        public string? AccessToken { get; set; }

    }
}
