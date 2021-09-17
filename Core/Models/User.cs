using System.Collections.Generic;

namespace Core.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string UrlAvatar { get; set; } = string.Empty;
        public List<UserRole> Role { get; set; } = new List<UserRole>();
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public readonly static User Empty = new();
    }
}
