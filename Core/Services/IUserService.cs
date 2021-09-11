using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
