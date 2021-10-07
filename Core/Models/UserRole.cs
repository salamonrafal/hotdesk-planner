using Core.Enums;

namespace Core.Models
{
    public class UserRole
    {
        public RoleType RoleType { get; set; } = RoleType.User;
        public string DisplayName { get; set; } = string.Empty;
    }
}
