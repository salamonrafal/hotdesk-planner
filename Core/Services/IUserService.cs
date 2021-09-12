using Core.Models;

namespace Core.Services
{
    public interface IUserService
    {
        public User Get(int userId);
        public int Add(User user);
        public void Remove(User user);
        public void SetRole(User user, UserRole userRole);
    }
}
