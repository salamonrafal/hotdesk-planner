using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class User: BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UrlAvatar { get; set; }
        public List<UserRole> Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
